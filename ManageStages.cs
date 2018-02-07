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
    public partial class ManageStages : DevExpress.XtraEditors.XtraForm
    {
       
        public static long currentWorkFlowStage = 0;
        public ManageStages()
        {
            InitializeComponent();
        }      

        private void ManageStages_Load(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {  
                    senpa.WorkFlowStages[] response = agent.operation.GetWorkFlowStages(SEnPAMain.currentWorkFlow);
                    foreach (senpa.WorkFlowStages wrkFlow in response)
                    {
                    string currentFlow = "_" + wrkFlow.Id.ToString();
                    treeStages.Nodes["workStages"].Nodes.Add(currentFlow, wrkFlow.StageName);
                }

                Globals.SetPickList(cmbGroup, "rolgro");
                Globals.SetPickList(cmbDocType, "doctyp");
            }
            Globals.SetStageMessagingPickList(cmbEmail);
            Globals.SetStageMessagingPickList(cmbSMS);
            Globals.SetAutoDocumentPickList(cmbTemplate);
            cmbAssign.SelectedIndex = 0;
        }

        private void treeStages_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                if (treeStages.SelectedNode.Text.ToLower() != "stages")
                {
                    currentWorkFlowStage = long.Parse(treeStages.SelectedNode.Name.Split('_')[1]);
                    senpa.WorkFlowStages wrk = agent.operation.GetWorkFlowStage(currentWorkFlowStage);
                    
                    txtName.Text = wrk.StageName;
                    txtDescription.Text = wrk.StageDescription;
                   Globals.SetPickListValue( cmbGroup,wrk.FK_RoleGroupId);
                    cmbAssign.SelectedIndex = wrk.StageAssignMode;
                    chkDoc.Checked = wrk.RequireDocuments;
                    chkOptional.Checked = wrk.StageOptional;
                    chkPay.Checked = wrk.RequirePayment;
                    chkSite.Checked = wrk.RequireSiteVisit;
                    chkReco.Checked = wrk.RequireRecommendations;
                    Globals.SetPickListValue(cmbEmail, wrk.SendEmail);
                    Globals.SetPickListValue(cmbSMS, wrk.SendSMS);
                    Globals.SetPickListValue(cmbTemplate, wrk.FK_AutoDocumentName);

                    lstDocuments.Items.Clear();
                    senpa.WorkFlowStageDocuments[] response = agent.operation.GetWorkFlowStageDocuments(currentWorkFlowStage);
                    foreach (senpa.WorkFlowStageDocuments wrkFlow in response)
                    {
                        string[] row = { wrkFlow.Id.ToString(), agent.operation.GetDocumentTypeName(wrkFlow.FK_DocumentTypeId), ((wrkFlow.DocumentRequired) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstDocuments.Items.Add(listViewItem);
                    }

                }
            }
        }

        private void lstDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                try
                {
                    senpa.WorkFlowStageDocuments response = agent.operation.GetWorkFlowStageDocument(long.Parse(lstDocuments.SelectedItems[0].SubItems[0].Text));
                    Globals.SetPickListValue(cmbDocType, response.FK_DocumentTypeId);
                    chkRequired.Checked = response.DocumentRequired;
                }
                catch { }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                long wrk = agent.operation.CreateWorkFlowStage(SEnPAMain.currentWorkFlow,0,txtName.Text,txtDescription.Text,Globals.GetComboBoxValue(cmbGroup),cmbAssign.SelectedIndex,chkOptional.Checked,chkDoc.Checked,chkPay.Checked,chkSite.Checked,chkReco.Checked,cmbTemplate.SelectedValue.ToString(), Globals.GetComboBoxValue(cmbEmail), Globals.GetComboBoxValue(cmbSMS));
                if (wrk>0)
                {
                    lstDocuments.Items.Clear();
                    string currentFlow = "_" + wrk.ToString();
                    treeStages.Nodes["workStages"].Nodes.Add(currentFlow, txtName.Text);
                }
                else if(wrk==-1)
                {
                    ;//error msg
                }
            }
        }

        private void btnDocs_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                bool wrk = agent.operation.CreateWorkFlowStageDocument(currentWorkFlowStage, Globals.GetComboBoxValue(cmbDocType), chkRequired.Checked);
                if (wrk)
                {
                    lstDocuments.Items.Clear();
                    senpa.WorkFlowStageDocuments[] response = agent.operation.GetWorkFlowStageDocuments(currentWorkFlowStage);
                    foreach (senpa.WorkFlowStageDocuments wrkFlow in response)
                    {
                        string[] row = { wrkFlow.Id.ToString(), agent.operation.GetDocumentTypeName(wrkFlow.FK_DocumentTypeId), ((wrkFlow.DocumentRequired) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstDocuments.Items.Add(listViewItem);
                    }
                }
            }
        }

        private void btnAutoDocuments_Click(object sender, EventArgs e)
        {
            new ManageStageAutoDocuments().ShowDialog();
        }
    }
}