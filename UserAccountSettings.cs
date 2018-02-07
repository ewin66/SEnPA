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
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Net;

namespace SEnPA
{
    public partial class UserAccountSettings : DevExpress.XtraEditors.XtraForm
    {
        public HttpRequestMessageProperty httpRequestProperty;
        public static string authorizationKey = "";
        public static string accessToken = "";
        public OperationContext context;
        public senpa.SEnPAClient operation;
        public OperationContextScope opScope;

        public UserAccountSettings()
        {
            InitializeComponent();
        }

        private void UserAccountSettings_Load(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //get user profile
                senpa.ApplicationUsers tempUser = agent.operation.GetUser(Globals.userLogged);

                txtName.Text = tempUser.FirstName;
                txtSurname.Text = tempUser.Surname;
                txtEmail.Text = tempUser.EmailAddress;
                txtPhone.Text = tempUser.MobileNumber;

            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text.Equals(""))
            {
                MessageBox.Show("The name cannot be blank");
                return;
            }
            if (txtSurname.Text.Equals(""))
            {
                MessageBox.Show("The surname cannot be blank");
                return;
            }
            if (txtEmail.Text.Equals(""))
            {
                MessageBox.Show("The email address cannot be blank");
                return;
            }
            if (txtPhone.Text.Equals(""))
            {
                MessageBox.Show("The phone number cannot be blank");
                return;
            }

            string successful = "failed";
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes("sbfa:sbfa");
            string base64 = Convert.ToBase64String(bytes, Base64FormattingOptions.None);
            authorizationKey = "Basic " + base64;

            senpaSecurity.SEnPASecurityClient security = new senpaSecurity.SEnPASecurityClient();
            var httpRequestPropertySecurity = new HttpRequestMessageProperty();
            httpRequestPropertySecurity.Headers[HttpRequestHeader.Authorization] = authorizationKey;

            var contextSecurity = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(contextSecurity))
            {
                try
                {
                    contextSecurity.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestPropertySecurity;
                    bool response = security.UpdateUserDetails(Globals.userLogged, txtName.Text, txtSurname.Text, txtEmail.Text, txtPhone.Text);
                    if (response)
                    {
                        MessageBox.Show("User information updated successfully.");
                        successful = "successful";
                    }
                    else
                    {
                        MessageBox.Show("User information update failed");
                        successful = "failed";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("User information update failed" + " : " + ex.Message);
                    successful = "error";
                }

            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            if (!txtNewPassword.Text.Equals(txtReEnterNewPassword.Text))
            {
                MessageBox.Show("Password change failed" + " : The two new passwords entered are not the same.");
                return;
            }

            string successful = "failed";
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes("sbfa:sbfa");
            string base64 = Convert.ToBase64String(bytes, Base64FormattingOptions.None);
            authorizationKey = "Basic " + base64;

            senpaSecurity.SEnPASecurityClient security = new senpaSecurity.SEnPASecurityClient();
            var httpRequestPropertySecurity = new HttpRequestMessageProperty();
            httpRequestPropertySecurity.Headers[HttpRequestHeader.Authorization] = authorizationKey;

            var contextSecurity = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(contextSecurity))
            {
                try
                {
                    contextSecurity.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestPropertySecurity;
                    senpaSecurity.PasswordChangeResponse response = security.ChangePassword(Globals.userLogged, txtOldPassword.Text, txtNewPassword.Text);
                    if (response.changeStatus)
                    {
                        MessageBox.Show("Password changed successfully");
                        successful = "successful";
                    }
                    else
                    {
                        MessageBox.Show("Password change failed" + " : " + response.responseMessage);
                        successful = "failed";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Password change failed" + " : " + ex.Message);
                    successful = "error";
                }

            }
        }
    }
}