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
    public partial class ManageStakeholder : DevExpress.XtraEditors.XtraForm
    {
        int currentStakeholder = 0;
        public ManageStakeholder()
        {
            InitializeComponent();
        }

        private void treeStakeholder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                if (treeStakeholder.SelectedNode.Text.ToLower() != "stakeholders")
                {
                    currentStakeholder = int.Parse(treeStakeholder.SelectedNode.Name.Split('_')[1]);
                    senpa.Stakeholder stake = agent.operation.GetStakeholder(currentStakeholder);

                    txtName.Text = stake.Name;
                    txtDescription.Text = stake.Description;
                    txtMobile.Text = stake.Mobile;
                    txtEmail.Text = stake.Email;                   
                    chkActive.Checked = stake.Active;

                    lstBusiness.Items.Clear();
                    senpa.ReferenceTable[] response = agent.operation.GetStakeholderBusinessTypes(currentStakeholder);
                    foreach (senpa.ReferenceTable busType in response)
                    {
                        string[] row = { busType.Id.ToString(), busType.Name, busType.Description };
                        var listViewItem = new ListViewItem(row);
                        lstBusiness.Items.Add(listViewItem);
                    }

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                long stake = agent.operation.SaveStakeholder(txtName.Text, txtDescription.Text, txtMobile.Text, txtEmail.Text, chkActive.Checked);
                if (stake > 0)
                {
                    lstBusiness.Items.Clear();
                    string currentStake = "_" + stake.ToString();
                    treeStakeholder.Nodes["stakeHolder"].Nodes.Add(currentStake, txtName.Text);
                }
            }
        }

        private void ManageStakeholder_Load(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Stakeholder[] response = agent.operation.GetStakeholders();
                foreach (senpa.Stakeholder stake in response)
                {
                    string currentStake = "_" + stake.Id.ToString();
                    treeStakeholder.Nodes["stakeHolder"].Nodes.Add(currentStake, stake.Name);
                }
                
            }
        }
    }
}