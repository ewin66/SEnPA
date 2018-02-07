using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ServiceModel.Channels;
using System.Net;
using System.ServiceModel;

namespace SEnPA
{
    public partial class ManagerUserProperties : DevExpress.XtraEditors.XtraForm
    {
       
        public ManagerUserProperties()
        {
            InitializeComponent();
        }

        private TreeNode FindRootNode(TreeNode treeNode)
        {
            while (treeNode.Parent != null)
            {
                treeNode = treeNode.Parent;
            }
            return treeNode;
        }

        private void ManagerUserProperties_Load(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context)) { 
                senpa.ApplicationUsers response = agent.operation.GetUser(SEnPAMain.currentUsername);
                lblUsername.Text = SEnPAMain.currentUsername;
                chkActive.Checked = response.Active;
                chkExpires.Checked = response.PasswordExpires;
                chkLocked.Checked = response.Locked;
                lblChanged.Text = response.PasswordLastChanged.ToShortDateString();
                lblExpiry.Text = response.PasswordExpiryDate.ToShortDateString();
                txtEmail.Text = response.EmailAddress;
                txtMobile.Text = response.MobileNumber;
                txtName.Text = response.FirstName;
                txtSurname.Text = response.Surname;
                Globals.SetPickList(cmbStakeholder, "stahol");
                Globals.SetPickListValue(cmbStakeholder, response.FK_StakeholderId);

                //get application user roles
                senpa.ApplicationRoles[] roles = agent.operation.GetApplicationRoles("default");
                foreach (senpa.ApplicationRoles role in roles)
                {
                    string currentRole = role.Name;
                    treeSystemRoles.Nodes["systemRoles"].Nodes.Add(currentRole, currentRole);
                    treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                }
                //get selected user roles
                senpa.ApplicationRoles[] userRoles = agent.operation.GetApplicationRoles(SEnPAMain.currentUsername);
                foreach (senpa.ApplicationRoles role in userRoles)
                {
                    string currentRole = role.Name;
                    treeUserRoles.Nodes["userSystemRoles"].Nodes.Add(currentRole, currentRole);
                    treeUserRoles.Nodes["userSystemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    //remove from system roles
                    treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Remove();
                }
                //get user group roles
                //get application user group roles
                senpa.ApplicationRoleGroups[] rolesGroup = agent.operation.GetApplicationGroupRoles("default");
                foreach (senpa.ApplicationRoleGroups role in rolesGroup)
                {
                    string currentRole = role.Name;
                    treeSystemRoles.Nodes["systemGroupRoles"].Nodes.Add(currentRole, currentRole);
                    treeSystemRoles.Nodes["systemGroupRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                }
                //get selected user roles
                senpa.ApplicationRoleGroups[] userRolesGroup = agent.operation.GetApplicationGroupRoles(SEnPAMain.currentUsername);
                foreach (senpa.ApplicationRoleGroups role in userRolesGroup)
                {
                    string currentRole = role.Name;
                    treeUserRoles.Nodes["userSystemGroupRoles"].Nodes.Add(currentRole, currentRole);
                    treeUserRoles.Nodes["userSystemGroupRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    //remove from system roles
                    treeSystemRoles.Nodes["systemGroupRoles"].Nodes[currentRole].Remove();
                }
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void treeSystemRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {            
            try
            {
                lblDescription.Text = treeSystemRoles.SelectedNode.Nodes[treeSystemRoles.SelectedNode.Text].Text;
            }
            catch { }
        }

        private void treeUserRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                lblDescription.Text = treeUserRoles.SelectedNode.Nodes[treeUserRoles.SelectedNode.Text].Text;
            }
            catch { }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                TreeNode temp = treeSystemRoles.SelectedNode;
                senpa.UserRoleActionResponse response;
                if (FindRootNode(temp).Name == "systemRoles")
                {
                    response = agent.operation.AddRole(SEnPAMain.currentUsername, treeSystemRoles.SelectedNode.Text);
                }
                else
                {
                    response = agent.operation.AddGroupRole(SEnPAMain.currentUsername, treeSystemRoles.SelectedNode.Text);
                }
                if (response.actionStatus)
                {                   
                    if (FindRootNode(temp).Name== "systemRoles")
                    {                        
                        treeSystemRoles.SelectedNode.Remove();
                        treeUserRoles.Nodes["userSystemRoles"].Nodes.Add(temp);
                    }
                    else
                    {
                        treeSystemRoles.SelectedNode.Remove();
                        treeUserRoles.Nodes["userSystemGroupRoles"].Nodes.Add(temp);
                    }
                }
                else
                {
                    ;
                }
            }            
        }

        private void btnRemoveRole_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                TreeNode temp = treeUserRoles.SelectedNode;
                senpa.UserRoleActionResponse response;
                if (FindRootNode(temp).Name == "userSystemRoles")
                {
                    response = agent.operation.RemoveRole(SEnPAMain.currentUsername, treeUserRoles.SelectedNode.Text);
                }
                else
                {
                    response = agent.operation.RemoveGroupRole(SEnPAMain.currentUsername, treeUserRoles.SelectedNode.Text);
                }
                if (response.actionStatus)
                {
                    
                    if (FindRootNode(temp).Name == "userSystemRoles")
                    {
                        treeUserRoles.SelectedNode.Remove();
                        treeSystemRoles.Nodes["systemRoles"].Nodes.Add(temp);
                    }
                    else
                    {
                        treeUserRoles.SelectedNode.Remove();
                        treeSystemRoles.Nodes["systemGroupRoles"].Nodes.Add(temp);
                    }
                   
                }
                else
                {
                    ;
                }
            }
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.UserActionResponse response = agent.operation.UpdateUser(SEnPAMain.currentUsername, ((chkActive.Checked)? "enable" : "disable"));
                chkActive.Checked = response.actionStatus;
            }
        }

        private void chkLocked_CheckedChanged(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.UserActionResponse response = agent.operation.UpdateUser(SEnPAMain.currentUsername, ((chkLocked.Checked) ? "lock" : "unlock"));
                chkLocked.Checked = response.actionStatus;
            }
        }

        private void chkExpires_CheckedChanged(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.UserActionResponse response = agent.operation.UpdateUser(SEnPAMain.currentUsername, ((chkExpires.Checked) ? "expire" : "notexpire"));
                chkExpires.Checked = response.actionStatus;
            }
        }
    }
}