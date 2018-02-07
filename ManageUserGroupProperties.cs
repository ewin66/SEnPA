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
using System.ServiceModel;
using System.Net;

namespace SEnPA
{
    public partial class ManageUserGroupProperties : DevExpress.XtraEditors.XtraForm
    {
       
        public ManageUserGroupProperties()
        {
            InitializeComponent();
        }

        private void ManageUserGroupProperties_Load(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //get application user roles
                senpa.ApplicationRoles[] roles = agent.operation.GetApplicationRoles("default");
                foreach (senpa.ApplicationRoles role in roles)
                {
                    string currentRole = role.Name;
                    treeSystemRoles.Nodes["systemRoles"].Nodes.Add(currentRole, currentRole);
                    treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                }
               
                //get user group roles
                //get application user group roles
                senpa.ApplicationRoleGroups[] rolesGroup = agent.operation.GetApplicationGroupRoles("default");
                foreach (senpa.ApplicationRoleGroups role in rolesGroup)
                {
                    string currentRole = role.Name;
                    treeUserGroups.Nodes["userGroups"].Nodes.Add(currentRole, currentRole);
                    treeUserGroups.Nodes["userGroups"].Nodes[currentRole].Nodes.Add(role.Description);
                }
               
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //get application user roles
                senpa.UserRoleActionResponse response = agent.operation.AddUserGroup(txtGroup.Text, txtDescription.Text);
                if(response.actionStatus)
                {
                    string currentRole = txtGroup.Text;
                    treeUserGroups.Nodes["userGroups"].Nodes.Add(currentRole, currentRole);
                    treeUserGroups.Nodes["userGroups"].Nodes[currentRole].Nodes.Add(txtDescription.Text);
                }

            }
        }

        private void treeUserGroups_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeSystemRoles.Nodes["systemRoles"].Nodes.Clear();
            treeUserRoles.Nodes["userSystemRoles"].Nodes.Clear();
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //get application user roles
                senpa.ApplicationRoles[] roles = agent.operation.GetApplicationRoles("default");
                foreach (senpa.ApplicationRoles role in roles)
                {
                    string currentRole = role.Name;
                    treeSystemRoles.Nodes["systemRoles"].Nodes.Add(currentRole, currentRole);
                    treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                }
                //get selected user roles
                senpa.ApplicationRoles[] userRoles = agent.operation.GetApplicationUserGroupRoles(treeUserGroups.SelectedNode.Text);
                foreach (senpa.ApplicationRoles role in userRoles)
                {
                    string currentRole = role.Name;
                    treeUserRoles.Nodes["userSystemRoles"].Nodes.Add(currentRole, currentRole);
                    treeUserRoles.Nodes["userSystemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    //remove from system roles
                    treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Remove();
                }

            }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.UserRoleActionResponse response = agent.operation.AddUserGroupRole(treeUserGroups.SelectedNode.Text, treeSystemRoles.SelectedNode.Text);
                if (response.actionStatus)
                {
                    TreeNode temp = treeSystemRoles.SelectedNode;
                    treeSystemRoles.SelectedNode.Remove();
                    treeUserRoles.Nodes["userSystemRoles"].Nodes.Add(temp);
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
                senpa.UserRoleActionResponse response = agent.operation.RemoveUserGroupRole(treeUserGroups.SelectedNode.Text, treeUserRoles.SelectedNode.Text);
                if (response.actionStatus)
                {
                    TreeNode temp = treeUserRoles.SelectedNode;
                    treeUserRoles.SelectedNode.Remove();
                    treeSystemRoles.Nodes["systemRoles"].Nodes.Add(temp);
                }
                else
                {
                    ;
                }
            }
        }
    }
}