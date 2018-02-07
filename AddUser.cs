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
    public partial class AddUser : DevExpress.XtraEditors.XtraForm
    {
       
        public AddUser()
        {
            InitializeComponent();
        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            txtPassword.ReadOnly = true;
            txtConfirm.ReadOnly = true;
            txtUsername.ReadOnly = true;
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //get application user group roles
                senpa.ApplicationRoleGroups[] rolesGroup = agent.operation.GetApplicationGroupRoles("default");
                foreach (senpa.ApplicationRoleGroups role in rolesGroup)
                {
                    cmbRoleGroups.Items.Add(role.Name);
                }
                Globals.SetPickList(cmbStakeholder, "stahol");
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (chkAutoUsername.Checked && txtName.Text.Length>0)
                txtUsername.Text = (txtName.Text.Substring(0, 1) + txtSurname.Text).ToLower();
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            if (chkAutoUsername.Checked && txtName.Text.Length > 0)
                txtUsername.Text = (txtName.Text.Substring(0, 1) + txtSurname.Text).ToLower();
        }

        private void chkAutoUsername_CheckedChanged(object sender, EventArgs e)
        {
            txtUsername.ReadOnly = chkAutoUsername.Checked;
        }

        private void chkDefaultPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.ReadOnly = chkDefaultPassword.Checked;
            txtConfirm.ReadOnly = chkDefaultPassword.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                string password = "";
                if (chkDefaultPassword.Checked)
                    password = agent.operation.DefaultPassword();
                else
                    password = txtPassword.Text;
               senpa.UserActionResponse response = agent.operation.AddUser(txtUsername.Text, password,Globals.GetComboBoxValue(cmbStakeholder), cmbRoleGroups.Text, txtName.Text, txtSurname.Text, txtEmail.Text, txtMobile.Text, chkPasswordExpires.Checked, chkActive.Checked, chkLocked.Checked);
                if (response.actionStatus)
                {
                    txtName.Text = "";
                    txtSurname.Text = "";
                    txtUsername.Text = "";
                    txtEmail.Text = "";
                    txtMobile.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    MessageBox.Show(response.responseMessage);
                }
            }
        }

        private void labelControl12_Click(object sender, EventArgs e)
        {

        }
    }
}