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
    public partial class ChargeRules : DevExpress.XtraEditors.XtraForm
    {
        int currentTypeId = 0;
        long currentId = 0;
        public ChargeRules()
        {
            InitializeComponent();
        }

        private void ChargeRules_Load(object sender, EventArgs e)
        {
            Globals.SetDataTypePickList(cmbDataType,"fee");
            Globals.SetDataTypePickList(cmbEvalDataType);
            Globals.SetValueExecutionPickList(cmbExecutiontype);
        }

        private void treeFees_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                if (treeFees.SelectedNode.Text.ToLower() != "fees")
                {
                    currentTypeId = int.Parse(treeFees.SelectedNode.Name.Split('_')[0]);
                    Globals.SetFieldsPickList(cmbField, treeFees.SelectedNode.Name.Split('_')[1]);
                    Globals.SetFieldsPickList(cmbEvalField, treeFees.SelectedNode.Name.Split('_')[1]);

                    lstRules.Items.Clear();
                    senpa.FeeRules[] response = agent.operation.GetFeeRulesList(treeFees.SelectedNode.Name.Split('_')[1]);
                    foreach (senpa.FeeRules rule in response)
                    {
                        string[] row = { rule.Id.ToString(), rule.RuleName, rule.RuleType, rule.RuleField, rule.RuleExecutionType, rule.RuleExecutionValue, rule.RuleEvaluationField,rule.RuleEvaluationDataType,rule.RuleEvaluationType,rule.RuleEvaluationValue,rule.RuleEvaluationMaxValue, ((rule.Active) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstRules.Items.Add(listViewItem);
                    }

                }
            }
        }

        private void cmbEvalDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.SetEvaluationPickList(cmbEvaluationType, cmbEvalDataType.SelectedValue.ToString());
        }

        private void lstRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                try
                {
                    currentId = long.Parse(lstRules.SelectedItems[0].SubItems[0].Text);
                    senpa.FeeRules response = agent.operation.GetFeeRule(currentId);
                    Globals.SetPickListValue(cmbField, response.RuleField);
                    chkActive.Checked = response.Active;
                    txtName.Text = response.RuleName;
                    Globals.SetPickListValue(cmbDataType, response.RuleType);
                    Globals.SetPickListValue(cmbExecutiontype, response.RuleExecutionType);
                    txtValue.Text = response.RuleExecutionValue;

                    Globals.SetPickListValue(cmbEvalField, response.RuleEvaluationField);
                    Globals.SetPickListValue(cmbEvalDataType, response.RuleEvaluationDataType);
                    Globals.SetPickListValue(cmbEvaluationType, response.RuleEvaluationType);                    
                    txtEvalValue.Text = response.RuleEvaluationValue;
                    txtEvalValueMax.Text = response.RuleEvaluationMaxValue;
                    
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
                    long save = agent.operation.SaveFeeRule(currentTypeId, txtName.Text, cmbDataType.SelectedValue.ToString(), cmbField.SelectedValue.ToString(), cmbExecutiontype.SelectedValue.ToString(), txtValue.Text, cmbEvalField.SelectedValue.ToString(), cmbEvalDataType.SelectedValue.ToString(), cmbEvaluationType.SelectedValue.ToString(), txtEvalValue.Text, txtEvalValueMax.Text, chkActive.Checked);
                    if (save > 0)
                    {
                        currentId = save;
                        string[] row = {save.ToString(),txtName.Text, cmbDataType.SelectedText, cmbField.SelectedText, cmbExecutiontype.SelectedText, txtValue.Text, cmbEvalField.SelectedText, cmbEvalDataType.SelectedText, cmbEvaluationType.SelectedText, txtEvalValue.Text, txtEvalValueMax.Text, ((chkActive.Checked) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstRules.Items.Add(listViewItem);
                    }
                }
                catch { }
            }
        }
    }
}