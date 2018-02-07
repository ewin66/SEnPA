using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEnPA
{
    public partial class SignIn : Form
    {
        string username, password;
        string signInSuccessful = "failed";

        public SignIn()
        {
            InitializeComponent();
        }
        
        private void SignIn_Load(object sender, EventArgs e)
        {
           
        }

        private void btnSignIn_Click_1(object sender, EventArgs e)
        {
          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdSignIn_Click(object sender, EventArgs e)
        {
            pbarSigningIn.Visible = true;
            cmdSignIn.Visible = false;

            username = txtUsername.Text;
            password = txtPassword.Text;

            backgroundWorker1.RunWorkerAsync();
        }

        public void ShowError()
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Login Failed!", Description = "Username or password are incorrect. Please check the details and try again." };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);

        }

        public void ShowConnectionError()
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Connection Error!", Description = "The connection to the server failed. Please ensure you are connected to the network and try again." };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);

        }
        private void picClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (signInSuccessful.Equals("failed"))
            {
                pbarSigningIn.Visible = false;
                cmdSignIn.Visible = true;
                ShowError();
            }
            else if (signInSuccessful.Equals("successful"))
            {
                SEnPAMain openMain = new SEnPAMain();
                openMain.Show();

                pbarSigningIn.Visible = false;
                cmdSignIn.Visible = true;
                this.Hide();
            }
            else
            {               
                pbarSigningIn.Visible = false;
                cmdSignIn.Visible = true;
                ShowConnectionError();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string loginResponse = SenpaApi.SignIn(username, password);
            signInSuccessful = loginResponse;

            if (loginResponse.Equals("successful"))
            {
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {

                    //get user group roles
                    senpa.ApplicationRoleGroups[] accessGroupRoles = agent.operation.GetApplicationGroupRoles(Globals.userLogged);
                    foreach (senpa.ApplicationRoleGroups roleName in accessGroupRoles)
                    {
                        Globals.userGroupRoles.Add(roleName.Name);
                    }

                    //get user roles
                    senpa.ApplicationRoles[] accessRoles = agent.operation.GetApplicationRoles(Globals.userLogged);
                    foreach (senpa.ApplicationRoles roleName in accessRoles)
                    {
                        Globals.userRoles.Add(roleName.Name);
                    }
                }

                signInSuccessful = loginResponse;
            }
            

        }
    }
}
