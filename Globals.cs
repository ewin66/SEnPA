using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraEditors;
using System.Reflection;

namespace SEnPA
{
    public class Globals
    {
        public static string userLogged = "";
        public static List<string> userRoles = new List<string>();
        public static List<string> userGroupRoles = new List<string>();
        public static int organisation = 0;

        public static bool hasAccess(string role)
        {
            bool yes = false;
            foreach (string t in Globals.userGroupRoles)
            {
                if (t.ToLower() == "administrators" || t.ToLower() == "ceo")
                {
                    yes = true;
                    break;
                }
            }

            if (!yes)
            {
                foreach (string s in Globals.userRoles)
                {
                    if (s.ToLower() == role.ToLower())
                    {
                        yes = true;
                        break;
                    }
                }
            }

            return yes;
        }

        //set pick list
        public static void SetPickList(System.Windows.Forms.ComboBox lst, string type, int parentId = 0)
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();

            if (type == "BusinessDevelopmentOfficer" || type == "BusinessDevelopmentManager" || type == "HumanResources")
            {
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    senpa.PickList[] lstItems = agent.operation.GetUserPickList(type);
                    foreach (senpa.PickList typ in lstItems)
                    {
                        r = dt.NewRow();
                        r["Value"] = typ.Id;
                        r["Text"] = typ.Text;
                        dt.Rows.Add(r);
                    }
                }
            }
            else if (type == "dis")
            {
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    senpa.PickList[] lstItems = agent.operation.GetChildPickList(type, parentId);
                    foreach (senpa.PickList typ in lstItems)
                    {
                        r = dt.NewRow();
                        r["Value"] = typ.Id;
                        r["Text"] = typ.Text;
                        dt.Rows.Add(r);
                    }
                }
            }
            else if (type == "refParent")
            {
                r = dt.NewRow();
                r["Value"] = 0;
                r["Text"] = "Default";
                dt.Rows.Add(r);
            }
            else
            {
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    senpa.PickList[] lstItems = agent.operation.GetPickList(type);
                    foreach (senpa.PickList typ in lstItems)
                    {
                        r = dt.NewRow();
                        r["Value"] = typ.Id;
                        r["Text"] = typ.Text;
                        dt.Rows.Add(r);
                    }
                }
            }

            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";

        }

        //set pick list
        public static void SetUserPickList(System.Windows.Forms.ComboBox lst, senpa.ApplicationUserSummary[] userList)
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();
            foreach (senpa.ApplicationUserSummary typ in userList)
            {
                r = dt.NewRow();
                r["Value"] = typ.Id;
                r["Text"] = typ.FirstName + " " + typ.Surname;
                dt.Rows.Add(r);
            }

            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";

        }

        public static int GetComboBoxValue(System.Windows.Forms.ComboBox lst)
        {
            return int.Parse(lst.SelectedValue.ToString());
        }

        //set pick list value
        public static void SetPickListValue(System.Windows.Forms.ComboBox lst, object value)
        {
            lst.SelectedValue = value;
        }

        //set pick list
        public static void SetGenderPickList(System.Windows.Forms.ComboBox lst)
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();

            r = dt.NewRow();
            r["Value"] = "M";
            r["Text"] = "Male";
            dt.Rows.Add(r);
            r = dt.NewRow();
            r["Value"] = "F";
            r["Text"] = "Female";
            dt.Rows.Add(r);

            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";

        }

        //set pick list
        public static void SetSalutationPickList(System.Windows.Forms.ComboBox lst)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();

            r = dt.NewRow();
            r["Value"] = "Mr";
            r["Text"] = "Mr";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "Mrs";
            r["Text"] = "Mrs";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "Dr";
            r["Text"] = "Dr";
            dt.Rows.Add(r);

            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";
        }

        public static void SetRecommendationStatusPickList(System.Windows.Forms.ComboBox lst)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();

            r = dt.NewRow();
            r["Value"] = "Pending";
            r["Text"] = "Pending";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "Failed";
            r["Text"] = "Failed";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "Complete";
            r["Text"] = "Complete";
            dt.Rows.Add(r);

            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";
        }

        public static void SetAutoDocumentPickList(System.Windows.Forms.ComboBox lst)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();

            r = dt.NewRow();
            r["Value"] = "";
            r["Text"] = "None";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "registrationSubmit";
            r["Text"] = "Registration Submitted";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "invoice";
            r["Text"] = "Invoice";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "ackPayment";
            r["Text"] = "Payment Acknowledgement";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "receiptRegistration";
            r["Text"] = "Registration Receipt";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "receiptRenewal";
            r["Text"] = "Renewal Receipt";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "receipt";
            r["Text"] = "Receipt";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "certificateIssued";
            r["Text"] = "Certificate Issued";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "certificateRenewed";
            r["Text"] = "Certificate Renewed";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "training";
            r["Text"] = "Training Certificate";
            dt.Rows.Add(r);


            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";
        }

        public static void SetStageMessagingPickList(System.Windows.Forms.ComboBox lst)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();

            r = dt.NewRow();
            r["Value"] = -1;
            r["Text"] = "Do Not Send";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = 0;
            r["Text"] = "On Enter";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = 1;
            r["Text"] = "On Leave";
            dt.Rows.Add(r);


            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";
        }

        public static void SetDataTypePickList(System.Windows.Forms.ComboBox lst, string type = "doc")
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();

            r = dt.NewRow();
            r["Value"] = "quantity";
            r["Text"] = "Quantity";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "text";
            r["Text"] = "Text";
            dt.Rows.Add(r);
            if (type == "fee")
            {
                r = dt.NewRow();
                r["Value"] = "percentage";
                r["Text"] = "Percentage";
                dt.Rows.Add(r);
            }

            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";
        }

        public static void SetEvaluationPickList(System.Windows.Forms.ComboBox lst, string type)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();
            switch (type)
            {
                case "quantity":
                    r = dt.NewRow();
                    r["Value"] = "less";
                    r["Text"] = "Less Than";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "greater";
                    r["Text"] = "Greater Than";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "equal";
                    r["Text"] = "Equal to";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "not";
                    r["Text"] = "Not Equal To";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "in";
                    r["Text"] = "Range From-To";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "between";
                    r["Text"] = "Between Range";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "out";
                    r["Text"] = "Outside Range";
                    dt.Rows.Add(r);
                    break;
                case "text":
                    r = dt.NewRow();
                    r["Value"] = "equal";
                    r["Text"] = "Equal To";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "not";
                    r["Text"] = "Not Equal To";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "like";
                    r["Text"] = "Like";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "not like";
                    r["Text"] = "Not Like";
                    dt.Rows.Add(r);
                    break;
            }
            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";
        }

        public static void SetValueExecutionPickList(System.Windows.Forms.ComboBox lst)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();

            r = dt.NewRow();
            r["Value"] = "add";
            r["Text"] = "Add";
            dt.Rows.Add(r);

            r = dt.NewRow();
            r["Value"] = "subtract";
            r["Text"] = "Subtract";
            dt.Rows.Add(r);

            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";
        }

        public static void SetFieldsPickList(System.Windows.Forms.ComboBox lst, string type)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();
            switch (type)
            {
                case "registration":
                    r = dt.NewRow();
                    r["Value"] = "BusinessRegistrationNumber";
                    r["Text"] = "Business Registration Number";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "BusinessName";
                    r["Text"] = "Business Name";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "NIN";
                    r["Text"] = "NIN";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "FirstNames";
                    r["Text"] = "First Names";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "LastName";
                    r["Text"] = "Last Name";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "Citizenship";
                    r["Text"] = "Citizenship";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "Mobile";
                    r["Text"] = "Mobile";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "HomeTelephone";
                    r["Text"] = "Home Telephone";
                    dt.Rows.Add(r);
                    r = dt.NewRow();
                    r["Value"] = "WorkTelephone";
                    r["Text"] = "Work Telephone";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "Email";
                    r["Text"] = "Email";
                    dt.Rows.Add(r);
                    break;
                case "renewal":
                    r = dt.NewRow();
                    r["Value"] = "BusinessDescription";
                    r["Text"] = "Business Description";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "WorkingPremises";
                    r["Text"] = "Working Premises";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "SalesTarget";
                    r["Text"] = "Sales Target";
                    dt.Rows.Add(r);

                    r = dt.NewRow();
                    r["Value"] = "Equipment";
                    r["Text"] = "Equipment";
                    dt.Rows.Add(r);
                    break;
            }
            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";
        }

        //set pick list
        public static void SetTrainingStatusPickList(System.Windows.Forms.ComboBox lst)
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("Value");
            dt.Columns.Add("Text");
            DataRow r = dt.NewRow();

            r = dt.NewRow();
            r["Value"] = "Pending";
            r["Text"] = "Pending";
            dt.Rows.Add(r);
            r = dt.NewRow();
            r["Value"] = "Complete";
            r["Text"] = "Complete";
            dt.Rows.Add(r);

            lst.DataSource = dt;
            lst.ValueMember = "Value";
            lst.DisplayMember = "Text";

        }

        public enum MoveDirection { Up = -1, Down = 1 };

        public static void MoveItems(ListView sender, MoveDirection direction)
        {
            bool valid = sender.SelectedItems.Count > 0 &&
                        ((direction == MoveDirection.Down && (sender.SelectedItems[sender.SelectedItems.Count - 1].Index < sender.Items.Count - 1))
                        || (direction == MoveDirection.Up && (sender.SelectedItems[0].Index > 0)));

            if (valid)
            {
                bool start = true;
                int first_idx = 0;
                List<ListViewItem> items = new List<ListViewItem>();

                // ambil data
                foreach (ListViewItem i in sender.SelectedItems)
                {
                    if (start)
                    {
                        first_idx = i.Index;
                        start = false;
                    }
                    items.Add(i);
                }

                sender.BeginUpdate();

                // hapus
                foreach (ListViewItem i in sender.SelectedItems) i.Remove();

                // insert
                if (direction == MoveDirection.Up)
                {
                    int insert_to = first_idx - 1;
                    foreach (ListViewItem i in items)
                    {
                        sender.Items.Insert(insert_to, i);
                        insert_to++;
                    }
                }
                else
                {
                    int insert_to = first_idx + 1;
                    foreach (ListViewItem i in items)
                    {
                        sender.Items.Insert(insert_to, i);
                        insert_to++;
                    }
                }
                sender.EndUpdate();
            }
        }

        public static bool ValidateLoanField(senpa.WorkFlowFieldValidations aVal, senpa.RegistrationRequest classObject)
        {
            object val = null;
            bool found = false;
            FieldInfo[] fields = classObject.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo f in fields)
            {
                if (f.Name == aVal.ParameterField + "Field")
                {
                    val = f.GetValue(classObject);
                    found = true;
                    break;
                }
                else
                {

                }

            }

            if (found)
                return ValidateField(aVal, val);
            else
                return false;
        }

        static bool ValidateField(senpa.WorkFlowFieldValidations aVal, object val)
        {
            bool allValidated = true;

            if (aVal.ParameterDataType == "quantity")
            {
                if (aVal.ParameterEvaluationType == "less")
                {
                    if (float.Parse(val.ToString()) < float.Parse(aVal.ParameterValue))
                        allValidated = true;
                    else
                        allValidated = false;
                }
                else if (aVal.ParameterEvaluationType == "greater")
                {
                    if (float.Parse(val.ToString()) > float.Parse(aVal.ParameterValue))
                        allValidated = true;
                    else
                        allValidated = false;
                }
                else if (aVal.ParameterEvaluationType == "equal")
                {
                    if (float.Parse(val.ToString()) == float.Parse(aVal.ParameterValue))
                        allValidated = true;
                    else
                        allValidated = false;
                }
                else if (aVal.ParameterEvaluationType == "not")
                {
                    if (float.Parse(val.ToString()) != float.Parse(aVal.ParameterValue))
                        allValidated = true;
                    else
                        allValidated = false;
                }
                else if (aVal.ParameterEvaluationType == "in")
                {
                    if ((float.Parse(val.ToString()) >= float.Parse(aVal.ParameterValue)) && (float.Parse(val.ToString()) <= float.Parse(aVal.ParameterMaxValue)))
                        allValidated = true;
                    else
                        allValidated = false;
                }
                else if (aVal.ParameterEvaluationType == "between")
                {
                    if ((float.Parse(val.ToString()) > float.Parse(aVal.ParameterValue)) && (float.Parse(val.ToString()) < float.Parse(aVal.ParameterMaxValue)))
                        allValidated = true;
                    else
                        allValidated = false;
                }
                else if (aVal.ParameterEvaluationType == "out")
                {
                    if ((float.Parse(val.ToString()) < float.Parse(aVal.ParameterValue)) || (float.Parse(val.ToString()) > float.Parse(aVal.ParameterMaxValue)))
                        allValidated = true;
                    else
                        allValidated = false;
                }
            }
            else
            {
                if (aVal.ParameterEvaluationType == "equal")
                {
                    //loop if value is , separated
                    string[] temp = aVal.ParameterValue.Split(',');
                    foreach (string x in temp)
                    {
                        if (val.ToString() == x)
                        {
                            allValidated = true;
                            break;
                        }
                        else
                            allValidated = false;
                    }

                }
                else if (aVal.ParameterEvaluationType == "not")
                {
                    if (val.ToString() != aVal.ParameterValue)
                        allValidated = true;
                    else
                        allValidated = false;
                }
                else if (aVal.ParameterEvaluationType == "like")
                {
                    if (val.ToString().IndexOf(aVal.ParameterValue) > -1)
                        allValidated = true;
                    else
                        allValidated = false;
                }
                else if (aVal.ParameterEvaluationType == "not like")
                {
                    if (val.ToString().IndexOf(aVal.ParameterValue) < 0)
                        allValidated = true;
                    else
                        allValidated = false;
                }

            }

            return allValidated;
        }

    }

    public class ComboboxItem
    {
        public string Text { get; set; }
        public object Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public class Utilities
    {
        public static string GenerateReferenceNumber()
        {
            return DateTime.Now.ToString("yMdHms");
        }

    }

    public class SenpaApi
    {
        public HttpRequestMessageProperty httpRequestProperty;
        public static string authorizationKey = "";
        public static string accessToken = "";
        public OperationContext context;
        public senpa.SEnPAClient operation;
        public OperationContextScope opScope;

        public SenpaApi()
        {
            operation = new senpa.SEnPAClient();

            httpRequestProperty = new HttpRequestMessageProperty();
            context = new OperationContext(operation.InnerChannel);
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = accessToken;
            context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
        }

        public static string SignIn(string user, string password)
        {
            string loginResponse = "failed";
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes("senpa:senpa");
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
                    senpaSecurity.AuthenticationResponse response = security.Authenticate(user, password);
                    if (response.authenticationStatus)
                    {
                        accessToken = response.accessToken;
                        Globals.userLogged = response.username;


                        loginResponse = "successful";
                    }
                    else
                    {
                        //XtraMessageBox.Show(response.responseMessage, "Login failed!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        loginResponse = "failed";
                    }
                }
                catch (Exception ex)
                {
                    loginResponse = "error";
                }

            }

            return loginResponse;
        }


    }


}
