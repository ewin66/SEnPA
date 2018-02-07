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

namespace SEnPA
{
    public partial class ValidationRules : DevExpress.XtraEditors.XtraForm
    {
        string currentDoc = "";
        long currentId = 0;
        public ValidationRules()
        {
            InitializeComponent();
        }

        private void ValidationRules_Load(object sender, EventArgs e)
        {
            Globals.SetDataTypePickList(cmbDataType);
        }

        private void treeDocuments_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                if (treeDocuments.SelectedNode.Text.ToLower() != "documents")
                {                    
                    currentDoc =treeDocuments.SelectedNode.Name;
                    Globals.SetFieldsPickList(cmbField, currentDoc);
                    
                    lstRules.Items.Clear();
                    senpa.WorkFlowFieldValidations[] response = agent.operation.GetValidationsList(currentDoc);
                    foreach (senpa.WorkFlowFieldValidations rule in response)
                    {
                        string[] row = { rule.Id.ToString(), rule.ParameterField, rule.ParameterFieldName,rule.ParameterDataType,rule.ParameterValue,rule.ParameterMaxValue,rule.ParameterEvaluationType,((rule.Active)?"Yes":"No") };
                        var listViewItem = new ListViewItem(row);
                        lstRules.Items.Add(listViewItem);
                    }

                }
            }
        }

        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.SetEvaluationPickList(cmbEvaluationType, cmbDataType.SelectedValue.ToString());
        }

        private void lstRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                try
                {
                    chkNew.Checked = false;
                    currentId = long.Parse(lstRules.SelectedItems[0].SubItems[0].Text);
                    senpa.WorkFlowFieldValidations response = agent.operation.GetValidation(currentId);
                    Globals.SetPickListValue(cmbField, response.ParameterField);
                    chkActive.Checked = response.Active;
                    Globals.SetPickListValue(cmbDataType, response.ParameterDataType);
                    txtText.Text = response.ParameterFieldName;
                    txtValue.Text = response.ParameterValue;
                    txtValueMax.Text = response.ParameterMaxValue;
                    Globals.SetPickListValue(cmbEvaluationType, response.ParameterEvaluationType);
                }
                catch { }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                try
                {
                    long save = agent.operation.SaveValidation(((chkNew.Checked) ? 0 : currentId), currentDoc, cmbField.SelectedValue.ToString(), cmbDataType.SelectedValue.ToString(), txtText.Text, txtValue.Text, txtValueMax.Text, cmbEvaluationType.SelectedValue.ToString(), chkActive.Checked);
                    if (save > 0)
                    {
                        currentId = save;
                        chkNew.Checked = false;
                        string[] row = { save.ToString(), cmbField.SelectedText, txtText.Text, cmbDataType.SelectedText, txtValue.Text, txtValueMax.Text, cmbEvaluationType.SelectedText, ((chkActive.Checked) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstRules.Items.Add(listViewItem);
                    }                    
                }
                catch { }
            }
        }
    }
}