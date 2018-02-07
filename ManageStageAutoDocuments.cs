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
    public partial class ManageStageAutoDocuments : DevExpress.XtraEditors.XtraForm
    {
        long currentId = 0;
        public ManageStageAutoDocuments()
        {
            InitializeComponent();
        }

        private void ManageStageAutoDocuments_Load(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                lstDocuments.Items.Clear();
                senpa.WorkFlowStagesAutoDocuments[] response = agent.operation.GetWorkFlowStagesAutoDocuments(ManageStages.currentWorkFlowStage);
                foreach (senpa.WorkFlowStagesAutoDocuments wrkFlow in response)
                {
                    string[] row = { wrkFlow.Id.ToString(), wrkFlow.FK_AutoDocumentName, ((wrkFlow.SendEmail)==-1 ? "No" : ((wrkFlow.SendEmail) == 0 ? "On Enter" : "On Leave")), ((wrkFlow.SendSMS) == -1 ? "No" : ((wrkFlow.SendSMS) == 0 ? "On Enter" : "On Leave")) };
                    var listViewItem = new ListViewItem(row);
                    lstDocuments.Items.Add(listViewItem);
                }
            }

            Globals.SetStageMessagingPickList(cmbEmail);
            Globals.SetStageMessagingPickList(cmbSMS);
            Globals.SetAutoDocumentPickList(cmbTemplate);
        }

        private void lstDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                try
                {
                    senpa.WorkFlowStagesAutoDocuments response = agent.operation.GetWorkFlowStagesAutoDocument(long.Parse(lstDocuments.SelectedItems[0].SubItems[0].Text));
                    currentId = response.Id;
                    Globals.SetPickListValue(cmbEmail, response.SendEmail);
                    Globals.SetPickListValue(cmbSMS, response.SendSMS);
                    Globals.SetPickListValue(cmbTemplate, response.FK_AutoDocumentName);
                    chkActive.Checked = response.Active;
                }
                catch { }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                long wrk = agent.operation.CreateWorkFlowStagesAutoDocument(ManageStages.currentWorkFlowStage, cmbTemplate.SelectedValue.ToString(), Globals.GetComboBoxValue(cmbEmail), Globals.GetComboBoxValue(cmbSMS), chkActive.Checked);
                if (wrk > -1)
                {
                    lstDocuments.Items.Clear();
                    senpa.WorkFlowStagesAutoDocuments[] response = agent.operation.GetWorkFlowStagesAutoDocuments(ManageStages.currentWorkFlowStage);
                    foreach (senpa.WorkFlowStagesAutoDocuments wrkFlow in response)
                    {
                        string[] row = { wrkFlow.Id.ToString(), wrkFlow.FK_AutoDocumentName, ((wrkFlow.SendEmail) == -1 ? "No" : ((wrkFlow.SendEmail) == 0 ? "On Enter" : "On Leave")), ((wrkFlow.SendSMS) == -1 ? "No" : ((wrkFlow.SendSMS) == 0 ? "On Enter" : "On Leave")) };
                        var listViewItem = new ListViewItem(row);
                        lstDocuments.Items.Add(listViewItem);
                    }
                }
            }
        }
    }
}