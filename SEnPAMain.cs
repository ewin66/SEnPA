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
using System.IO;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraGrid.Views.Grid;

namespace SEnPA
{
    public partial class SEnPAMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int uploadButtonsTopPosition = 82, uploadSiteButtonsTopPosition = 82, existingDocumentsPosition = 82;
        public static long currentId = 0, currentBusinessType = 0, currentInvoiceId = 0, currentBusinessId = 0, currentRenewalId=0,currentBusinessRegistrationId=0;
        public static long currentFolderId = 1;
        public static long currentWorkFlow = 0;
        public static string currentUsername = "";
        public long currentCertificateId = 0, certificateRenewalId = 0, currentCertificateRegistrationId = 0;
        long currentTrainingId = 0;
        bool currentTrainingClosed = false;
        bool isRenewal = false;
        int currentBusiness = 0;
        string category = "";
        string currentDocumentDesign = "";
        NavigationPage activePage, previousPage;
        List<NavigationPage> navStack;
        bool backPressed;

        List<string> lstSteps; //for registration progress indicator
        int activeIndex; //for registration progress indicator
       
        #region Navigation

        private void cmdNavBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            //get the last item that was in the list then remove it
            
            try
            {
                NavigationPage chosenBackToPage = navStack[navStack.Count - 1];
                navStack.Remove(chosenBackToPage);

                backPressed = true;
                navigationFrame.SelectedPage = chosenBackToPage;
            }catch(Exception ex)
            {

            }
            
        }

        private void RibbonShow(string page)
        {
            
        }

        private void navigationFrame_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
        {
            activePage = (NavigationPage)navigationFrame.SelectedPage;
            //hide menus
            trainingRibbon.Visible = false;
            registrationRibbon.Visible = false;
            siteRibbon.Visible = false;
            ribbonBtnsUsers.Visible = false;
            ribbonBtnsReferences.Visible = false;
            ribbonBtnsWorkflow.Visible = false;
            ribbonBtnsTemplates.Visible = false;
            recRibbonPage.Visible = false;
            siteRibbonPage.Visible = false;
            renRibbonPage.Visible = false;
            busRibbonPage.Visible = false;
            regRibbonPage.Visible = false;
            ribbonBtnsCertificate.Visible = false;
            regQuickActions.Visible = false;
            payQuickActions.Visible = false;
            docQuickActions.Visible = false;
            trainActions.Visible = false;
            renQuickActions.Visible = false;

            if (navigationFrame.SelectedPage == navPageRenewals) {
                registrationRibbon.Visible = true;                
                renRibbonPage.Visible = true;
                mainRibbon.SelectedPage = renRibbonPage;
            }
            else if(navigationFrame.SelectedPage== navPageViewRenewals)
            {
                renQuickActions.Visible = true;
                mainRibbon.SelectedPage = homeRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageRegistrations) {
                registrationRibbon.Visible = true;
                regRibbonPage.Visible = true;               
                mainRibbon.SelectedPage = regRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageDocuments) {
                docQuickActions.Visible = true;
                mainRibbon.SelectedPage = homeRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageAfterCare)
            {
                trainingRibbon.Visible = true;
                mainRibbon.SelectedPage = trainManageRibbon;
            }           
            else if (navigationFrame.SelectedPage == navPageViewRegistration) {
                regQuickActions.Visible = true;
                mainRibbon.SelectedPage = homeRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPagePayments) {
                payQuickActions.Visible = true;
                mainRibbon.SelectedPage = homeRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageBusiness) {
                registrationRibbon.Visible = true;
                busRibbonPage.Visible = true;
                regRibbonPage.Visible = false;
                mainRibbon.SelectedPage = busRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageRegisteredBusiness) {
                registrationRibbon.Visible = true;
                busRibbonPage.Visible = true;
                regRibbonPage.Visible = false;
                cmdOpenBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdSaveBusiness.Visibility= DevExpress.XtraBars.BarItemVisibility.Never;
                mainRibbon.SelectedPage = busRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageRecomendations) {
                siteRibbon.Visible = true;
                recRibbonPage.Visible = true;
                mainRibbon.SelectedPage = recRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageWorkFlows) {
                ribbonBtnsWorkflow.Visible = true;
            }
            else if (navigationFrame.SelectedPage == navPageBusinessType) { }
            else if (navigationFrame.SelectedPage == navPageUsers) {
                ribbonBtnsUsers.Visible = true;
            }
            else if (navigationFrame.SelectedPage == navPageReferences) {
                ribbonBtnsReferences.Visible = true;
            }
            else if (navigationFrame.SelectedPage == navPageEmail) { }
            else if (navigationFrame.SelectedPage == navPageSMS) { }
            else if (navigationFrame.SelectedPage == navPageDesignDocuments) {
                ribbonBtnsTemplates.Visible = true;
                cmdSaveDocDesign.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else if (navigationFrame.SelectedPage == navPageSiteVisitReport) {
                siteRibbon.Visible = true;
                siteRibbonPage.Visible = true;
                mainRibbon.SelectedPage = siteRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageScheduleSiteVisit) {
                siteRibbon.Visible = true;
                siteRibbonPage.Visible = true;
                mainRibbon.SelectedPage = siteRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageApproveCertificate) {
                registrationRibbon.Visible = true;
                busRibbonPage.Visible = true;
                ribbonBtnsCertificate.Visible = true;
                mainRibbon.SelectedPage = busRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageManageTraining) {
                trainingRibbon.Visible = true;
                trainActions.Visible = true;
                cmdEditTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                cmdTrainingAttendance.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                mainRibbon.SelectedPage = trainManageRibbon;
            }
            else if (navigationFrame.SelectedPage == navPageAttendance) {
                trainingRibbon.Visible = true;
                trainActions.Visible = true;
                cmdEditTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdTrainingAttendance.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                mainRibbon.SelectedPage = trainManageRibbon;
            }           
        }

        private void navigationFrame_SelectedPageChanging(object sender, SelectedPageChangingEventArgs e)
        {
            previousPage = (NavigationPage)navigationFrame.SelectedPage;
            if (backPressed == false)
            {
                navStack.Add(previousPage);
            }
            else
            {
                backPressed = false;
            }
            
        }
        
        #endregion
        
        #region WorkflowProgressIndicators
        
        private void RefreshRegIndicator()
        {
            lstSteps = new List<string>();

            SenpaApi agent = new SenpaApi();
            //get the details of the stages from the server
            using (new OperationContextScope(agent.context))
            {
                int lastPosition = 0;
                
                if (currentId == 0)
                {
                    senpa.WorkFlowStages[] stages = agent.operation.GetWorkFlowStages(1);
                    foreach (senpa.WorkFlowStages stg in stages)
                    {
                        lstSteps.Add(stg.StagePosition + ";" + stg.StageName + ";" + (stg.StagePosition.ToString().Equals("0") ? "true" : "false"));
                        lastPosition = stg.StagePosition;
                    }
                }
                else
                {
                    senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(currentId, "registration");
                    lblStage.Text = currentStage.StageName;

                    senpa.WorkFlowStages[] stages = agent.operation.GetWorkFlowStages(1);
                    foreach(senpa.WorkFlowStages stg in stages)
                    {
                        lstSteps.Add(stg.StagePosition + ";" + stg.StageName + ";" + (stg.StageName.ToString().Equals(currentStage.StageName) ? "true" : "false"));
                        lastPosition = stg.StagePosition;
                    }
                    lstSteps.Add(lastPosition + 1 + ";Complete;" + ("Complete".ToString().Equals(currentStage.StageName) ? "true" : "false"));
                }

               

            }
            
            int stepNo = lstSteps.Count;
            int controlXValue = 0;

            foreach (string item in lstSteps)
            {
                string[] itemBundle = item.Split(';');
                int itemIndex = int.Parse(itemBundle[0]);
                string itemTitle = itemBundle[1];
                bool itemActive = Boolean.Parse(itemBundle[2]);

                if (itemActive == true)
                {
                    activeIndex = itemIndex;
                }
            }

            panRegWorkFlowIndicator.Controls.Clear();

            foreach (string item in lstSteps)
            {
                string[] itemBundle = item.Split(';');
                int itemIndex = int.Parse(itemBundle[0]);
                string itemTitle = itemBundle[1];
                bool itemActive = Boolean.Parse(itemBundle[2]);


                Panel panel = new Panel();
                panel.BackgroundImage = Properties.Resources.current_item_fw;
                panel.BackgroundImageLayout = ImageLayout.Stretch;
                panel.Height = 50;
                panel.Width = panRegWorkFlowIndicator.Width / stepNo;
                panel.Location = new Point(controlXValue, 0);
                panel.AutoSize = true;
                panel.Tag = itemIndex;

                //determine the background image based on the status
                if (itemIndex == 0)
                {
                    if (itemActive == true)
                    {
                        panel.BackgroundImage = Properties.Resources.current_start_item_fw;
                    }
                    else
                    {
                        panel.BackgroundImage = Properties.Resources.completed_start_item_fw;
                    }
                }
                else
                {
                    if (itemActive == true)
                    {
                        panel.BackgroundImage = Properties.Resources.current_item_fw;
                    }
                    else
                    {
                        if (itemIndex == (activeIndex + 1))
                        {
                            panel.BackgroundImage = Properties.Resources.next_undone_item_fw;
                        }
                        else if (itemIndex < activeIndex)
                        {
                            panel.BackgroundImage = Properties.Resources.completed_start_item_fw;
                        }
                        else
                        {
                            panel.BackgroundImage = Properties.Resources.full_undone_item_fw;
                        }

                    }


                }


                Label lbl = new Label();
                lbl.ForeColor = Color.White;
                lbl.Text = itemTitle;
                lbl.Anchor = (AnchorStyles.Right);
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.BackColor = Color.Transparent;
                lbl.Width = panel.Width;
                lbl.Top = 12;
                lbl.AutoSize = false;


                if (itemActive == true)
                {
                    lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
                else
                {
                    lbl.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                }

                panel.Controls.Add(lbl);

                //tbProgress.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                //tbProgress.SetRowSpan(panel, 1);
                panRegWorkFlowIndicator.Controls.Add(panel);
                controlXValue += (panRegWorkFlowIndicator.Width / stepNo);

                
            }

            panRegWorkFlowIndicator.Refresh();
        }

        private void panRegWorkFlowIndicator_Paint(object sender, PaintEventArgs e)
        {
            //panel2 is the parent panel
            foreach (Panel panel in panRegWorkFlowIndicator.Controls)
            {
                int stepNo = lstSteps.Count;
                panel.Width = panRegWorkFlowIndicator.Width / stepNo;
                int controlXValue = 0;

                controlXValue += (panRegWorkFlowIndicator.Width / stepNo);
                panel.Location = new Point(controlXValue * (int)panel.Tag, 0);
                panel.Update();
            }

        }

        private void RefreshRenewalIndicator()
        {
            lstSteps = new List<string>();

            SenpaApi agent = new SenpaApi();
            //get the details of the stages from the server
            using (new OperationContextScope(agent.context))
            {
                int lastPosition = 0;
               
                if (currentRenewalId == 0)
                {
                    senpa.WorkFlowStages[] stages = agent.operation.GetWorkFlowStages(2);
                    foreach (senpa.WorkFlowStages stg in stages)
                    {
                        lstSteps.Add(stg.StagePosition + ";" + stg.StageName + ";" + (stg.StagePosition.ToString().Equals("0") ? "true" : "false"));
                        lastPosition = stg.StagePosition;
                    }
                }
                else
                {
                    senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(currentRenewalId, "renewal");
                    lblStage.Text = currentStage.StageName;

                    senpa.WorkFlowStages[] stages = agent.operation.GetWorkFlowStages(2);
                    foreach (senpa.WorkFlowStages stg in stages)
                    {
                        lstSteps.Add(stg.StagePosition + ";" + stg.StageName + ";" + (stg.StageName.ToString().Equals(currentStage.StageName) ? "true" : "false"));
                        lastPosition = stg.StagePosition;
                    }

                    lstSteps.Add(lastPosition + 1 + ";Complete;" + ("Complete".ToString().Equals(currentStage.StageName) ? "true" : "false"));
                }
                
            }

            int stepNo = lstSteps.Count;
            int controlXValue = 0;

            foreach (string item in lstSteps)
            {
                string[] itemBundle = item.Split(';');
                int itemIndex = int.Parse(itemBundle[0]);
                string itemTitle = itemBundle[1];
                bool itemActive = Boolean.Parse(itemBundle[2]);

                if (itemActive == true)
                {
                    activeIndex = itemIndex;
                }
            }

            panRenewWorkflowIndicator.Controls.Clear();

            foreach (string item in lstSteps)
            {
                string[] itemBundle = item.Split(';');
                int itemIndex = int.Parse(itemBundle[0]);
                string itemTitle = itemBundle[1];
                bool itemActive = Boolean.Parse(itemBundle[2]);


                Panel panel = new Panel();
                panel.BackgroundImage = Properties.Resources.current_item_fw;
                panel.BackgroundImageLayout = ImageLayout.Stretch;
                panel.Height = 50;
                panel.Width = panRenewWorkflowIndicator.Width / stepNo;
                panel.Location = new Point(controlXValue, 0);
                panel.AutoSize = true;
                panel.Tag = itemIndex;

                //determine the background image based on the status
                if (itemIndex == 0)
                {
                    if (itemActive == true)
                    {
                        panel.BackgroundImage = Properties.Resources.current_start_item_fw;
                    }
                    else
                    {
                        panel.BackgroundImage = Properties.Resources.completed_start_item_fw;
                    }
                }
                else
                {
                    if (itemActive == true)
                    {
                        panel.BackgroundImage = Properties.Resources.current_item_fw;
                    }
                    else
                    {
                        if (itemIndex == (activeIndex + 1))
                        {
                            panel.BackgroundImage = Properties.Resources.next_undone_item_fw;
                        }
                        else if (itemIndex < activeIndex)
                        {
                            panel.BackgroundImage = Properties.Resources.completed_start_item_fw;
                        }
                        else
                        {
                            panel.BackgroundImage = Properties.Resources.full_undone_item_fw;
                        }

                    }


                }


                Label lbl = new Label();
                lbl.ForeColor = Color.White;
                lbl.Text = itemTitle;
                lbl.Anchor = (AnchorStyles.Right);
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.BackColor = Color.Transparent;
                lbl.Width = panel.Width;
                lbl.Top = 12;
                lbl.AutoSize = false;


                if (itemActive == true)
                {
                    lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
                else
                {
                    lbl.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                }

                panel.Controls.Add(lbl);

                //tbProgress.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                //tbProgress.SetRowSpan(panel, 1);
                panRenewWorkflowIndicator.Controls.Add(panel);
                controlXValue += (panRenewWorkflowIndicator.Width / stepNo);


            }

            panRenewWorkflowIndicator.Refresh();
        }

        private void panRenewWorkflowIndicator_Paint(object sender, PaintEventArgs e)
        {
            foreach (Panel panel in panRenewWorkflowIndicator.Controls)
            {
                int stepNo = lstSteps.Count;
                panel.Width = panRenewWorkflowIndicator.Width / stepNo;
                int controlXValue = 0;

                controlXValue += (panRenewWorkflowIndicator.Width / stepNo);
                panel.Location = new Point(controlXValue * (int)panel.Tag, 0);
                panel.Update();
            }
        }

        private void RefreshApprovalIndicator()
        {
            lstSteps = new List<string>();

            SenpaApi agent = new SenpaApi();
            //get the details of the stages from the server
            using (new OperationContextScope(agent.context))
            {
                int lastPosition = 0;
               
                if (currentCertificateId == 0)
                {
                    senpa.WorkFlowStages[] stages = agent.operation.GetWorkFlowStages(4);
                    foreach (senpa.WorkFlowStages stg in stages)
                    {
                        lstSteps.Add(stg.StagePosition + ";" + stg.StageName + ";" + (stg.StagePosition.ToString().Equals("0") ? "true" : "false"));
                        lastPosition = stg.StagePosition;
                    }
                }
                else
                {
                    senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(currentCertificateId, "certificate");
                    // lblStage.Text = currentStage.StageName;

                    senpa.WorkFlowStages[] stages = agent.operation.GetWorkFlowStages(4);
                    foreach (senpa.WorkFlowStages stg in stages)
                    {
                        lstSteps.Add(stg.StagePosition + ";" + stg.StageName + ";" + (stg.StageName.ToString().Equals(currentStage.StageName) ? "true" : "false"));
                        lastPosition = stg.StagePosition;
                    }
                    lstSteps.Add(lastPosition + 1 + ";Complete;" + ("Complete".ToString().Equals(currentStage.StageName) ? "true" : "false"));
                }                

            }

            int stepNo = lstSteps.Count;
            int controlXValue = 0;

            foreach (string item in lstSteps)
            {
                string[] itemBundle = item.Split(';');
                int itemIndex = int.Parse(itemBundle[0]);
                string itemTitle = itemBundle[1];
                bool itemActive = Boolean.Parse(itemBundle[2]);

                if (itemActive == true)
                {
                    activeIndex = itemIndex;
                }
            }

            panCertificateWorkFlowIndicator.Controls.Clear();

            foreach (string item in lstSteps)
            {
                string[] itemBundle = item.Split(';');
                int itemIndex = int.Parse(itemBundle[0]);
                string itemTitle = itemBundle[1];
                bool itemActive = Boolean.Parse(itemBundle[2]);


                Panel panel = new Panel();
                panel.BackgroundImage = Properties.Resources.current_item_fw;
                panel.BackgroundImageLayout = ImageLayout.Stretch;
                panel.Height = 50;
                panel.Width = panRenewWorkflowIndicator.Width / stepNo;
                panel.Location = new Point(controlXValue, 0);
                panel.AutoSize = true;
                panel.Tag = itemIndex;

                //determine the background image based on the status
                if (itemIndex == 0)
                {
                    if (itemActive == true)
                    {
                        panel.BackgroundImage = Properties.Resources.current_start_item_fw;
                    }
                    else
                    {
                        panel.BackgroundImage = Properties.Resources.completed_start_item_fw;
                    }
                }
                else
                {
                    if (itemActive == true)
                    {
                        panel.BackgroundImage = Properties.Resources.current_item_fw;
                    }
                    else
                    {
                        if (itemIndex == (activeIndex + 1))
                        {
                            panel.BackgroundImage = Properties.Resources.next_undone_item_fw;
                        }
                        else if (itemIndex < activeIndex)
                        {
                            panel.BackgroundImage = Properties.Resources.completed_start_item_fw;
                        }
                        else
                        {
                            panel.BackgroundImage = Properties.Resources.full_undone_item_fw;
                        }

                    }


                }


                Label lbl = new Label();
                lbl.ForeColor = Color.White;
                lbl.Text = itemTitle;
                lbl.Anchor = (AnchorStyles.Right);
                lbl.TextAlign = ContentAlignment.MiddleRight;
                lbl.BackColor = Color.Transparent;
                lbl.Width = panel.Width;
                lbl.Top = 12;
                lbl.AutoSize = false;


                if (itemActive == true)
                {
                    lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                }
                else
                {
                    lbl.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                }

                panel.Controls.Add(lbl);

                //tbProgress.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                //tbProgress.SetRowSpan(panel, 1);
                panCertificateWorkFlowIndicator.Controls.Add(panel);
                controlXValue += (panCertificateWorkFlowIndicator.Width / stepNo);


            }

            panCertificateWorkFlowIndicator.Refresh();
        }

        private void panCertificateWorkFlowIndicator_Paint(object sender, PaintEventArgs e)
        {
            foreach (Panel panel in panCertificateWorkFlowIndicator.Controls)
            {
                int stepNo = lstSteps.Count;
                panel.Width = panCertificateWorkFlowIndicator.Width / stepNo;
                int controlXValue = 0;

                controlXValue += (panCertificateWorkFlowIndicator.Width / stepNo);
                panel.Location = new Point(controlXValue * (int)panel.Tag, 0);
                panel.Update();
            }
        }
        #endregion

        #region custom

        private void SideBarRights()
        {
            newRegistration.Visible = ((Globals.hasAccess("captureRegistration")) ? true : false);
            navBarAddTraining.Visible = ((Globals.hasAccess("captureTraining")) ? true : false);           

            if (Globals.hasAccess("viewRegistrations") || Globals.hasAccess("processRegistration"))
            {
                registrationsNavBarGroup.Visible = true;
                navBarItemViewRegistrations.Visible = true;
                navBarItemRegisteredBusiness.Visible = true;
                
            }
            else
            {
                registrationsNavBarGroup.Visible = false;
                navBarItemViewRegistrations.Visible = false;
                navBarItemRegisteredBusiness.Visible = false;
            }

            if (Globals.hasAccess("viewRenewals") || Globals.hasAccess("processRenewals"))
            {
                renewalNavBarGroup.Visible = true;
                newRenewal.Visible = true;
            }
            else
            {
                renewalNavBarGroup.Visible = false;
                newRenewal.Visible = false;
            }

            if (Globals.hasAccess("processPayment") || Globals.hasAccess("viewPayments"))
            {
                paymentsNavBarGroup.Visible = true;
            }
            else
            {
                paymentsNavBarGroup.Visible = false;
            }

           

            if (Globals.hasAccess("viewTraining") || Globals.hasAccess("manageTraining"))
            {
                afterCareNavBarGroup.Visible = true;
                navBarFindTrainings.Visible = true;
            }
            else
            {
                afterCareNavBarGroup.Visible = false;
                navBarFindTrainings.Visible = false;
            }
        }

        private void TopBarRights()
        {
            ribbonPageCategory1.Visible = ((Globals.hasAccess("administration")) ? true : false);
            ribbonMessaging.Visible = ((Globals.hasAccess("messaging")) ? true : false);
        }

        public void DocumentButton(senpa.WorkFlowStageDocumentStatus doc, string type = "registration")
        {
            SimpleButton addDocument = new SimpleButton();
            addDocument.Appearance.BackColor = System.Drawing.Color.Lavender;
            addDocument.Appearance.Options.UseBackColor = true;
            addDocument.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            addDocument.ImageOptions.Image = global::SEnPA.Properties.Resources.sign_add;
            addDocument.Location = new System.Drawing.Point(40, this.uploadButtonsTopPosition);
            addDocument.LookAndFeel.SkinName = "Office 2016 Black";
            addDocument.Name = "_" + doc.Id.ToString() + "_" + doc.FK_DocumentTypeId.ToString() + "_" + doc.FK_StageId.ToString();
            addDocument.Size = new System.Drawing.Size(157, 26);
            addDocument.TabIndex = 11;
            addDocument.Text = doc.DocumentType;
            if (type == "registration")
                addDocument.Click += new System.EventHandler(this.UplaodDocument_Click);
            else
                addDocument.Click += new System.EventHandler(this.UplaodRenewalDocument_Click);
            PictureBox pic = new PictureBox();
            if (doc.Uploaded)
                pic.Image = picDone.Image;
            else
                pic.Image = picBlur.Image;

            // pic.Image = ((System.Drawing.Image)(SEnPA.Properties.Resources.sign_tick));
            pic.Location = new System.Drawing.Point(8, this.uploadButtonsTopPosition);
            pic.Name = "pic" + "_" + doc.Id.ToString();
            pic.Size = new System.Drawing.Size(26, 26);
            pic.TabIndex = 12;
            pic.TabStop = false;
            pic.Visible = true;
            if (type == "registration")
            {
                grpDocuments.Controls.Add(addDocument);
                grpDocuments.Controls.Add(pic);
            }
            else
            {
                grpRenewalDocuments.Controls.Add(addDocument);
                grpRenewalDocuments.Controls.Add(pic);
            }
            uploadButtonsTopPosition += 38;
        }

        private void UplaodDocument_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                uploadDocuments.ShowDialog();
                string fileName = uploadDocuments.SafeFileName;
                //MessageBox.Show(fileName);
                byte[] buffer = File.ReadAllBytes(uploadDocuments.FileName);
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    bool done = agent.operation.UploadWorkFlowDocument(long.Parse(lblId.Text), "registration", fileName, buffer, int.Parse(control.Name.Split('_')[2]), 3, false);
                    if (done)
                    {
                        (grpDocuments.Controls["pic" + "_" + control.Name.Split('_')[1]] as PictureBox).Image = picDone.Image;
                    }
                    else
                    {
                        //MessageBox.Show("Not done !!!");
                        ShowMessageBox("Not Done!", "Not Done", "warning");
                    }

                }
            }
            catch(Exception ex) {
                //MessageBox.Show(ex.ToString());
                ShowMessageBox("And Error Occured!", ex.Message, "error");
            }
        }

        private void UplaodRenewalDocument_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                uploadDocuments.ShowDialog();
                string fileName = uploadDocuments.SafeFileName;
                //MessageBox.Show(fileName);
                byte[] buffer = File.ReadAllBytes(uploadDocuments.FileName);
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    bool done = agent.operation.UploadWorkFlowDocument(currentRenewalId, "renewal", fileName, buffer, int.Parse(control.Name.Split('_')[2]), 3, false);
                    if (done)
                    {
                        (grpRenewalDocuments.Controls["pic" + "_" + control.Name.Split('_')[1]] as PictureBox).Image = picDone.Image;
                    }
                    else
                    {
                        //MessageBox.Show("Not done !!!");
                        ShowMessageBox("Not Done!", "Not Done", "warning");
                    }

                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());
                ShowMessageBox("And Error Occured!", ex.Message, "error");
            }
        }

        public void DocumentButton(senpa.SiteVisitReport doc)
        {         
            SimpleButton addDocument = new SimpleButton();
            addDocument.Appearance.BackColor = System.Drawing.Color.Lavender;
            addDocument.Appearance.Options.UseBackColor = true;
            addDocument.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            addDocument.ImageOptions.Image = global::SEnPA.Properties.Resources.sign_add;
            addDocument.Location = new System.Drawing.Point(40, this.uploadSiteButtonsTopPosition);
            addDocument.LookAndFeel.SkinName = "Office 2016 Black";
            addDocument.Name = "_" + doc.Id.ToString() + "_" + doc.FK_SiteVisitId.ToString() + "_" + doc.FK_StakeholderId.ToString();
            addDocument.Size = new System.Drawing.Size(157, 26);
            addDocument.TabIndex = 11;
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                addDocument.Text = agent.operation.GetEntityName(doc.FK_StakeholderId, "stahol");
            }           
            addDocument.Click += new System.EventHandler(this.UplaodSiteReport_Click);
            grpSiteDocuments.Controls.Add(addDocument);

            PictureBox pic = new PictureBox();
            if (doc.UploadStatus)
                pic.Image = picDone.Image;
            else
                pic.Image = picBlur.Image;

            // pic.Image = ((System.Drawing.Image)(SEnPA.Properties.Resources.sign_tick));
            pic.Location = new System.Drawing.Point(8, this.uploadSiteButtonsTopPosition);
            pic.Name = "picSite" + "_" + doc.Id.ToString();
            pic.Size = new System.Drawing.Size(26, 26);
            pic.TabIndex = 12;
            pic.TabStop = false;
            pic.Visible = true;
            grpSiteDocuments.Controls.Add(pic);
            uploadSiteButtonsTopPosition += 38;
        }

        private void UplaodSiteReport_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                uploadDocuments.ShowDialog();
                string fileName = uploadDocuments.SafeFileName;
                //MessageBox.Show(fileName);
                byte[] buffer = File.ReadAllBytes(uploadDocuments.FileName);
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    bool done = agent.operation.UploadSiteVisitWorkFlowDocument(long.Parse(control.Name.Split('_')[2]), int.Parse(control.Name.Split('_')[3]), "", currentId, "registration", fileName, buffer, 0, 3);
                    if (done)
                    {
                        (grpSiteDocuments.Controls["picSite" + "_" + control.Name.Split('_')[1]] as PictureBox).Image = picDone.Image;
                    }
                    else
                    {
                        //MessageBox.Show("Not done !!!");
                        ShowMessageBox("Not Done!", "Not Done!", "warning");

                    }

                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ShowMessageBox("And Error Occured!", ex.Message, "error");
            }
        }

        public void DocumentLink(senpa.WorkFlowStageDocumentStatus doc,string view,string type="registration")
        {
            LinkLabel addDocument = new LinkLabel();
            addDocument.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            addDocument.Location = new System.Drawing.Point(43, this.existingDocumentsPosition);
            addDocument.Name = "_" + doc.Id.ToString() + "_" + doc.FK_DocumentTypeId.ToString() + "_" + doc.FK_StageId.ToString() + "_ex";
            addDocument.Size = new System.Drawing.Size(169, 13);
            addDocument.TabIndex = 27;
            addDocument.TabStop = true;
            addDocument.Text = doc.DocumentType;            
            addDocument.Click += new System.EventHandler(this.OpenDocument_Click);            
            PictureBox pic = new PictureBox();
            if (doc.Uploaded)
                pic.Image = picDoc.Image;
            else
                pic.Image = picBlur.Image;

            // pic.Image = ((System.Drawing.Image)(SEnPA.Properties.Resources.sign_tick));
            pic.Location = new System.Drawing.Point(8, this.existingDocumentsPosition);
            pic.Name = "picView" + "_" + doc.Id.ToString();
            pic.Size = new System.Drawing.Size(26, 26);
            pic.TabIndex = 12;
            pic.TabStop = false;
            pic.Visible = true;
            if(view=="business")
            {
                if (type == "registration")
                {
                    grpBusinessDocuments.Controls.Add(addDocument);
                    grpBusinessDocuments.Controls.Add(pic);
                }
                else if(type=="certificate")
                {
                    grpExistingApprove.Controls.Add(addDocument);
                    grpExistingApprove.Controls.Add(pic);
                }
                else
                {
                    grpRenewalExisting.Controls.Add(addDocument);
                    grpRenewalExisting.Controls.Add(pic);
                }
            }


            existingDocumentsPosition += 38;
        }

        private void OpenDocument_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                long docId = long.Parse(control.Name.Split('_')[1]);
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    senpa.DocumentLibrary doc = agent.operation.GetDocument(docId);
                    string filePath = Application.StartupPath + "\\filer\\" + control.Text;
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc.DocumentData, 0, doc.DocumentData.Length);
                    fs.Flush();
                    fs.Close();
                    System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                    newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                    newProcess.Start();
                    newProcess.WaitForExit();
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.ToString());
                ShowMessageBox("And Error Occured!", ex.Message, "error");
            }
        }

        public void DocumentLink(senpa.AutoDocument doc, string type = "registration")
        {
            LinkLabel addDocument = new LinkLabel();
            addDocument.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            addDocument.Location = new System.Drawing.Point(43, this.existingDocumentsPosition);
            addDocument.Name = "_" + doc.Id.ToString() + "_" + doc.DocumentType.ToString() + "_ex";
            addDocument.Size = new System.Drawing.Size(169, 13);
            addDocument.TabIndex = 27;
            addDocument.TabStop = true;
            addDocument.Text = doc.DocumentTypeName;
            addDocument.Click += new System.EventHandler(this.OpenAutoDocument_Click);
            PictureBox pic = new PictureBox();
            pic.Image = picDoc.Image;
            // pic.Image = ((System.Drawing.Image)(sbfa.Properties.Resources.sign_tick));
            pic.Location = new System.Drawing.Point(8, this.existingDocumentsPosition);
            pic.Name = "picExView" + "_" + doc.Id.ToString();
            pic.Size = new System.Drawing.Size(26, 26);
            pic.TabIndex = 12;
            pic.TabStop = false;
            pic.Visible = true;
            if (type == "training")
            {
                grpCertificates.Controls.Add(addDocument);
                grpCertificates.Controls.Add(pic);
            }
            else if (type == "registration")
            {
                grpBusinessDocuments.Controls.Add(addDocument);
                grpBusinessDocuments.Controls.Add(pic);
            }
            else if (type == "renewal")
            {
                grpRenewalExisting.Controls.Add(addDocument);
                grpRenewalExisting.Controls.Add(pic);
            }
            else if (type == "certificate")
            {
                grpExistingApprove.Controls.Add(addDocument);
                grpExistingApprove.Controls.Add(pic);
            }

            existingDocumentsPosition += 38;
        }

        private void OpenAutoDocument_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                long docId = long.Parse(control.Name.Split('_')[1]);
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {

                    Byte[] doc = agent.operation.GetAutoDocument(control.Name.Split('_')[2], docId);
                    string filePath = Application.StartupPath + "\\filer\\" + control.Text + ".pdf";
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc, 0, doc.Length);
                    fs.Flush();
                    fs.Close();
                    System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                    newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                    newProcess.Start();
                    newProcess.WaitForExit();
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void StakeholderSelect(senpa.Stakeholder stake)
        {
            CheckBox addDocument = new CheckBox();
            addDocument.AutoSize = true;
            addDocument.Location = new System.Drawing.Point(15, this.uploadSiteButtonsTopPosition);
            addDocument.Name = "stake_" + stake.Id.ToString();
            addDocument.Size = new System.Drawing.Size(80, 17);
            addDocument.TabIndex = 0;
            addDocument.Text = stake.Name;
            addDocument.UseVisualStyleBackColor = true;
            
            addDocument.Click += new System.EventHandler(this.AddStakeholder_Click);
            grpSelectStakeholders.Controls.Add(addDocument);
                       
            uploadSiteButtonsTopPosition += 28;
        }

        public void AddStakeholder_Click(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                int currentStake = int.Parse(control.Name.Split('_')[1]);
                if ((control as CheckBox).Checked)
                {
                    long done = agent.operation.SaveSiteVisitReport(long.Parse(lblSiteId.Text), currentStake, "");
                    if (done > 0)
                    {

                    }
                    else
                    {
                        ;
                    }
                }
                else
                {
                    ShowMessageBox("Pending", "Pending", "normal");
                    (control as CheckBox).Checked = true;
                }
            }
        }

        private void InitializeRegForm(long Id)
        {
            isRenewal = false;
            RibbonShow("reg");
            navigationFrame.SelectedPage = navPageRegistrations;
            Globals.SetPickList(cmbBusRegType, "busregtyp");
            Globals.SetPickList(cmbBusType, "bustyp");
            Globals.SetPickList(cmbEdu, "edu");
            Globals.SetPickList(cmbBDO, "BusinessDevelopmentOfficer");
            Globals.SetGenderPickList(cmbGender);
            Globals.SetSalutationPickList(cmbSalutation);
            Globals.SetPickList(cmbResIsland, "isl");
            Globals.SetPickList(cmbBusIsland, "isl");

            cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            uploadButtonsTopPosition = 82;
            grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
            grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {   
                if (Id != 0)
                {
                    //get registration
                    senpa.RegistrationRequest registration = agent.operation.GetRegistrationRequest(Id);
                    lblId.Text = registration.Id.ToString();
                    lblReference.Text = registration.ReferenceNumber;
                    txtBusinessName.Text = registration.BusinessRegistrationNumber;
                    txtBusinessRegNumber.Text = registration.BusinessName;

                    Globals.SetPickListValue(cmbBusType, registration.FK_BusinessTypeId);                   
                    currentBusinessType = registration.FK_BusinessTypeId;
                     Globals.SetPickListValue(cmbBusRegType, registration.FK_BusinessRegistrationTypeId);
                    Globals.SetPickListValue(cmbBusIsland, registration.FK_BusinessIslandLocationId);
                    Globals.SetPickList(cmbBusDistrict, "dis", registration.FK_BusinessIslandLocationId);
                     Globals.SetPickListValue(cmbBusDistrict, registration.FK_BusinessIslandDistrictId);
                    Globals.SetPickListValue(cmbGender, registration.Gender);
                     Globals.SetPickListValue(cmbSalutation, registration.Salutation);
                    txtNIN.Text = registration.NIN;
                    txtFirstName.Text = registration.FirstNames;
                    txtLastName.Text = registration.LastName;
                    txtCitizenship.Text = registration.Citizenship;
                    dtpDOB.EditValue = registration.DOB;
                      Globals.SetPickListValue(cmbResIsland, registration.FK_ResidenceIslandLocationId);
                    Globals.SetPickList(cmbResDistrict, "dis", registration.FK_ResidenceIslandLocationId);
                     Globals.SetPickListValue(cmbResDistrict, registration.FK_ResidenceDistrictLocationId);

                    txtMobile.Text = registration.Mobile;
                    txtHomeTel.Text = registration.HomeTelephone;
                    txtWorkTel.Text = registration.WorkTelephone;
                    txtEmail.Text = registration.Email;

                     Globals.SetPickListValue(cmbEdu, registration.FK_EducationLevelId);
                    chkTerms.Checked = registration.TermsAndConditionsAccepted;

                      Globals.SetPickListValue(cmbBDO, registration.FK_BusinessDevelopmentOfficerId);
                    //set assign list
                   // senpa.ApplicationUserSummary[] userList = agent.operation.GetAssigningUserList(long.Parse(lblId.Text), "registration");
                    //cmbAssign.DataSource = null;
                    //Globals.SetUserPickList(cmbAssign, userList);
                    
                    //get current stage
                    senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "registration");
                   lblStage.Text = currentStage.StageName;

                    RefreshRegIndicator();

                    if (currentStage.RequireDocuments)
                    {
                        senpa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetDocumentsRequiredStatus(long.Parse(lblId.Text), "registration");
                        foreach (senpa.WorkFlowStageDocumentStatus doc in documents)
                        {
                            DocumentButton(doc);
                        }
                    }
                    else
                    {
                      //  grpDocuments.Visible = false;
                    }

                    if (currentStage.RequirePayment)
                    {
                        cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.StageOptional)
                    {
                        cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.RequireSiteVisit)
                    {
                        cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.RequireRecommendations)
                    {
                        cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "registration") || currentStage.StageName=="Complete")
                    {
                        cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                                        
                }
            }

            wrkFlowToolsRibbon.Visible = ((Globals.hasAccess("processRegistration")) ? true : false);
            regSiteRibbon.Visible = ((Globals.hasAccess("siteVisit")) ? true : false);
           
        }
        
        private void InitializeNewRegForm()
        {
            isRenewal = false;
            RibbonShow("reg");
            //Globals.SetPickList(cmbBusDistrict, "dis");
            Globals.SetPickList(cmbBusIsland, "isl");
            Globals.SetPickList(cmbBusRegType, "busregtyp");
            Globals.SetPickList(cmbBusType, "bustyp");
            Globals.SetPickList(cmbEdu, "edu");
            Globals.SetPickList(cmbResIsland, "isl");
            Globals.SetGenderPickList(cmbGender);
            Globals.SetSalutationPickList(cmbSalutation);
            Globals.SetPickList(cmbBDO, "BusinessDevelopmentOfficer");

            currentId = 0;
            uploadButtonsTopPosition = 82;
            grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
            grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
            lblId.Text = "0";
            lblReference.Text = "0";
            txtBusinessName.Text = "";
            txtBusinessRegNumber.Text = "";
            currentBusinessType = 0;
            txtNIN.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtCitizenship.Text = "";
            txtMobile.Text = "";
            txtHomeTel.Text = "";
            txtWorkTel.Text = "";
            txtEmail.Text = "";
            chkTerms.Checked = false;
            lblStage.Text = "New Registration";
            cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
          
            cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmdWorkFlowBack.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            RefreshRegIndicator();
            navigationFrame.SelectedPage = navPageRegistrations;
        }

        private void InitializeSiteVisit()
        {
            RibbonShow("site");
            cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            confirmSiteRibbon.Visible = true;
            notifySiteVisitRibbon.Visible = true;
            Globals.SetPickList(cmbSVBDO, "BusinessDevelopmentOfficer");
            Globals.SetPickList(cmbSVBDM, "BusinessDevelopmentManager");
            Globals.SetPickList(cmbSVHR, "HumanResources");
            this.uploadSiteButtonsTopPosition = 8;
            grpSelectStakeholders.Controls.OfType<CheckBox>().ToList().ForEach(btn => btn.Dispose());
         
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(int.Parse(currentBusinessType.ToString()));
                foreach (senpa.Stakeholder stake in current)
                {
                    StakeholderSelect(stake);
                }
                //check for site visit id
                senpa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "registration");
                if (site == null || site.CreatedBy == null)
                {
                    //add site
                    long siteId = agent.operation.CreateWorkflowSiteVisit(currentId, "registration");
                    lblSiteId.Text = siteId.ToString();
                    site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "registration");
                }
                else
                {
                    ;
                }
                lblSiteId.Text = site.Id.ToString();
                //get applicant details if registration                    
                senpa.RegistrationRequest reg = agent.operation.GetRegistrationRequest(site.FK_DocumentId);
               
                txtSVPhone.Text = reg.Mobile;
                rlblSiteName.Text = reg.FirstNames + " " + reg.LastName + " ("+ reg.NIN + ")";
                rlblSiteBusiness.Text = reg.BusinessName;
                rlblSiteTime.Text = DateTime.Now.ToShortDateString();

                txtSVAddress.Text = site.VisitAddress;
                dtpSVDate.DateTime = site.VisitDate;
                //chkSVConfirmed.Checked = site.Confirmed;
                chkSVPhone.Checked = site.Phone;
                chkSVSMS.Checked = site.SMS;
                chkSVEmail.Checked = site.Email;

                if (site.Confirmed)
                {
                    cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                //get reports
                senpa.SiteVisitReport[] reports = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                foreach (senpa.SiteVisitReport rep in reports)
                {
                    (grpSelectStakeholders.Controls["stake" + "_" + rep.FK_StakeholderId.ToString()] as CheckBox).Checked = true;
                }

            }
        }

        private void InitializeSiteVisitReport()
        {
            RibbonShow("site");
            cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            confirmSiteRibbon.Visible = false;
            notifySiteVisitRibbon.Visible = false;
            Globals.SetPickList(cmbSVRBDO, "BusinessDevelopmentOfficer");
            Globals.SetPickList(cmbSVRBDM, "BusinessDevelopmentManager");
            Globals.SetPickList(cmbSVRHR, "HumanResources");
            Globals.SetPickList(cmbLoanInstitute, "finins");

            uploadSiteButtonsTopPosition = 82;
            grpSiteDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
            grpSiteDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
           
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //check for site visit id
                senpa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "registration");
                if (site == null || site.CreatedBy == null)
                {
                    //add site
                    long siteId = agent.operation.CreateWorkflowSiteVisit(currentId, "registration");
                    lblSiteId.Text = siteId.ToString();
                    site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "registration");
                }
                else
                {
                    ;       
                }
                lblSiteId.Text = site.Id.ToString();
               
                cmbHasLoan.SelectedIndex = ((site.HasLoan) ?1:0);
                Globals.SetPickListValue(cmbLoanInstitute, site.FK_FinanceInstituteId);
                Globals.SetPickListValue(cmbSVRBDO, site.FK_BDO);
                Globals.SetPickListValue(cmbSVRBDM, site.FK_BDM);
                Globals.SetPickListValue(cmbSVRHR, site.FK_HR);
                txtSvBackground.Text = site.Background;
                txtSvBusinessDesc.Text = site.BusinessDescription;
                txtSvConclusion.Text = site.Conclusion;
                txtSvEquipment.Text = site.Equipment;
                txtSvManpower.Text = site.Manpower;
                txtSvMarketing.Text = site.Marketing;
                txtSvPremises.Text = site.Premises;
                txtSvTraining.Text = site.Training;
                txtLoanAmount.Text = site.LoanAmount.ToString();
                txtLoanPurpose.Text = site.LoanPurpose;

                //get applicant details if registration                    
                senpa.RegistrationRequest reg = agent.operation.GetRegistrationRequest(site.FK_DocumentId);
                lblSiteName.Text = reg.FirstNames + " " + reg.LastName;
                lblSiteBusiness.Text = reg.BusinessName;
                lblSiteTime.Text = DateTime.Now.ToShortDateString();               

                senpa.SiteVisitReport[] documents = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                foreach (senpa.SiteVisitReport doc in documents)
                {
                        DocumentButton(doc);                    
                }
            }
        }

        private void InitializeRenewalSiteVisit()
        {
            RibbonShow("site");
            cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            confirmSiteRibbon.Visible = true;
            notifySiteVisitRibbon.Visible = true;
            Globals.SetPickList(cmbSVBDO, "BusinessDevelopmentOfficer");
            Globals.SetPickList(cmbSVBDM, "BusinessDevelopmentManager");
            Globals.SetPickList(cmbSVHR, "HumanResources");
            this.uploadSiteButtonsTopPosition = 8;
            grpSelectStakeholders.Controls.OfType<CheckBox>().ToList().ForEach(btn => btn.Dispose());

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(int.Parse(currentBusinessType.ToString()));
                foreach (senpa.Stakeholder stake in current)
                {
                    StakeholderSelect(stake);
                }
                //check for site visit id
                senpa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentRenewalId, "renewal");
                if (site == null || site.CreatedBy == null)
                {
                    //add site
                    long siteId = agent.operation.CreateWorkflowSiteVisit(currentRenewalId, "renewal");
                    lblSiteId.Text = siteId.ToString();
                    site = agent.operation.GetCurrentWorkflowSiteVisit(currentRenewalId, "renewal");
                }
                else
                {
                    ;
                }
                lblSiteId.Text = site.Id.ToString();
                //get applicant details if registration                    
                senpa.RenewalRequest ren = agent.operation.GetRenewalRequest(site.FK_DocumentId);
                senpa.BusinessRegistration reg = agent.operation.GetBusinessRegistration(ren.FK_BusinessRegistrationId, false);
                txtSVPhone.Text = reg.Mobile;
                rlblSiteName.Text = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                rlblSiteBusiness.Text = reg.BusinessName;
                rlblSiteTime.Text = DateTime.Now.ToShortDateString();

                txtSVAddress.Text = site.VisitAddress;
                dtpSVDate.DateTime = site.VisitDate;
                //chkSVConfirmed.Checked = site.Confirmed;
                chkSVPhone.Checked = site.Phone;
                chkSVSMS.Checked = site.SMS;
                chkSVEmail.Checked = site.Email;

                if (site.Confirmed)
                {
                    cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                //get reports
                senpa.SiteVisitReport[] reports = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                foreach (senpa.SiteVisitReport rep in reports)
                {
                    (grpSelectStakeholders.Controls["stake" + "_" + rep.FK_StakeholderId.ToString()] as CheckBox).Checked = true;
                }

            }
        }

        private void InitializeRenewalSiteVisitReport()
        {
            RibbonShow("site");
            cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            confirmSiteRibbon.Visible = false;
            notifySiteVisitRibbon.Visible = false;
            Globals.SetPickList(cmbSVRBDO, "BusinessDevelopmentOfficer");
            Globals.SetPickList(cmbSVRBDM, "BusinessDevelopmentManager");
            Globals.SetPickList(cmbSVRHR, "HumanResources");
            Globals.SetPickList(cmbLoanInstitute, "finins");

            uploadSiteButtonsTopPosition = 82;
            grpSiteDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
            grpSiteDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //check for site visit id
                senpa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentRenewalId, "renewal");
                if (site == null || site.CreatedBy == null)
                {
                    //add site
                    long siteId = agent.operation.CreateWorkflowSiteVisit(currentRenewalId, "renewal");
                    lblSiteId.Text = siteId.ToString();
                    site = agent.operation.GetCurrentWorkflowSiteVisit(currentRenewalId, "renewal");
                }
                else
                {
                    ;
                }
                lblSiteId.Text = site.Id.ToString();

                cmbHasLoan.SelectedIndex = ((site.HasLoan) ? 1 : 0);
                Globals.SetPickListValue(cmbLoanInstitute, site.FK_FinanceInstituteId);
                Globals.SetPickListValue(cmbSVRBDO, site.FK_BDO);
                Globals.SetPickListValue(cmbSVRBDM, site.FK_BDM);
                Globals.SetPickListValue(cmbSVRHR, site.FK_HR);
                txtSvBackground.Text = site.Background;
                txtSvBusinessDesc.Text = site.BusinessDescription;
                txtSvConclusion.Text = site.Conclusion;
                txtSvEquipment.Text = site.Equipment;
                txtSvManpower.Text = site.Manpower;
                txtSvMarketing.Text = site.Marketing;
                txtSvPremises.Text = site.Premises;
                txtSvTraining.Text = site.Training;
                txtLoanAmount.Text = site.LoanAmount.ToString();
                txtLoanPurpose.Text = site.LoanPurpose;

                //get applicant details if registration                    
                senpa.RenewalRequest ren = agent.operation.GetRenewalRequest(site.FK_DocumentId);
                senpa.BusinessRegistration reg = agent.operation.GetBusinessRegistration(ren.FK_BusinessRegistrationId, false);
                lblSiteName.Text = reg.FirstNames + " " + reg.LastName;
                lblSiteBusiness.Text = reg.BusinessName;
                lblSiteTime.Text = DateTime.Now.ToShortDateString();

                senpa.SiteVisitReport[] documents = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                foreach (senpa.SiteVisitReport doc in documents)
                {
                    DocumentButton(doc);
                }
            }
        }

        private void InitializeBusinessForm(long Id)
        {
            navigationFrame.SelectedPage = navPageBusiness;
            cmdOpenBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            existingDocumentsPosition = 82;
            grpBusinessDocuments.Controls.OfType<LinkLabel>().ToList().ForEach(btn => btn.Dispose());
            grpBusinessDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
            grpCertificates.Controls.OfType<LinkLabel>().ToList().ForEach(btn => btn.Dispose());
            grpCertificates.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                Globals.SetPickList(rcmbBusDistrict, "dis");
                Globals.SetPickList(rcmbBusIsland, "isl");
                Globals.SetPickList(rcmbBusRegType, "busregtyp");
                Globals.SetPickList(rcmbBusType, "bustyp");
                Globals.SetPickList(rcmbEdu, "edu");
                Globals.SetPickList(rcmbResDistrict, "dis");
                Globals.SetPickList(rcmbResIsland, "isl");
                Globals.SetGenderPickList(rcmbGender);
                Globals.SetSalutationPickList(rcmbSalutation);

                if (Id != 0)
                {
                    //get registration
                    senpa.BusinessRegistration registration = agent.operation.GetBusinessRegistration(Id, false);
                    //check certificate status
                    int cert = agent.operation.CheckCertificate(registration.FK_RegistrationRequestId);
                    if (cert == -2)//not registered
                    {
                        ribbonBtnsCertificate.Visible = false;
                        cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else if (cert == -1)//expired
                    {
                        ribbonBtnsCertificate.Visible = false;
                        cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else if (cert == 0)//pending certificate issuing
                    {
                        ribbonBtnsCertificate.Visible = true;
                        cmdIssueCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdViewCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else if (cert == 1)//pending renewal certificate issuing
                    {
                        ribbonBtnsCertificate.Visible = true;
                        cmdIssueCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdViewCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else if (cert == 2)//expiring in comming period
                    {
                        ribbonBtnsCertificate.Visible = false;
                        cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else if (cert == 3)//safe
                    {
                        ribbonBtnsCertificate.Visible = true;
                        cmdIssueCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdViewCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    cmdSaveBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdSaveApproval.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                    rlblId.Text = registration.Id.ToString();
                    lblBusinessLabel.Text = "Registered: " + registration.RegistrationNumber;
                    rtxtBusinessRegNumber.Text = registration.BusinessRegistrationNumber;
                    rtxtBusinessName.Text = registration.BusinessName;

                    Globals.SetPickListValue(rcmbBusType, registration.FK_BusinessTypeId);
                    currentBusinessType = registration.FK_BusinessTypeId;
                    Globals.SetPickListValue(rcmbBusRegType, registration.FK_BusinessRegistrationTypeId);
                    Globals.SetPickListValue(rcmbBusIsland, registration.FK_BusinessIslandLocationId);

                    Globals.SetPickList(rcmbBusDistrict, "dis", registration.FK_BusinessIslandLocationId);
                    Globals.SetPickListValue(rcmbBusDistrict, registration.FK_BusinessIslandDistrictId);

                    Globals.SetPickListValue(rcmbGender, registration.Gender);
                    Globals.SetPickListValue(rcmbSalutation, registration.Salutation);

                    rtxtNIN.Text = registration.NIN;
                    rtxtFirstName.Text = registration.FirstNames;
                    rtxtLastName.Text = registration.LastName;
                    //cmbSalutation.Text = registration.Salutation;
                    rtxtCitizenship.Text = registration.Citizenship;
                    //cmbGender.Text = registration.Gender;
                    rdtpDOB.EditValue = registration.DOB;

                    Globals.SetPickListValue(rcmbResIsland, registration.FK_ResidenceIslandLocationId);
                    Globals.SetPickList(rcmbResDistrict, "dis", registration.FK_ResidenceIslandLocationId);
                    Globals.SetPickListValue(rcmbResDistrict, registration.FK_ResidenceDistrictLocationId);

                    rtxtMobile.Text = registration.Mobile;
                    rtxtHomeTel.Text = registration.HomeTelephone;
                    rtxtWorkTel.Text = registration.WorkTelephone;
                    rtxtEmail.Text = registration.Email;

                    Globals.SetPickListValue(rcmbEdu, registration.FK_EducationLevelId);

                    existingDocumentsPosition = 82;
                    senpa.AutoDocument[] autoDoc = agent.operation.CheckTrainingAutoDocument(registration.FK_RegistrationRequestId, "registration");
                    foreach (senpa.AutoDocument doc in autoDoc)
                    {
                        DocumentLink(doc, "training");
                    }

                    existingDocumentsPosition = 82;

                    autoDoc = agent.operation.CheckAutoDocument(registration.FK_RegistrationRequestId, "registration");
                    foreach (senpa.AutoDocument doc in autoDoc)
                    {
                        DocumentLink(doc);
                    }

                    senpa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetAllRequiredDocuments(registration.FK_RegistrationRequestId, "registration");
                    foreach (senpa.WorkFlowStageDocumentStatus doc in documents)
                    {
                        DocumentLink(doc, "business");
                    }

                }
            }

            if (Globals.hasAccess("processRenewals"))
            {
                cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (Globals.hasAccess("approveCertificate"))
            {
                cmdIssueCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                cmdIssueCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (Globals.hasAccess("printCertificate"))
            {
                cmdViewCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                cmdViewCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void InitializeRecommendations()
        {
            RibbonShow("rec");

            lstRecoStake.Items.Clear();
            treeRecoStake.Nodes["existingStakeholders"].Nodes.Clear();
            Globals.SetPickList(cmbAction, "act");
            //Globals.SetPickList(cmbBDOReco, "BusinessDevelopmentOfficer");
            Globals.SetRecommendationStatusPickList(cmbStatusReco);

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(int.Parse(currentBusinessType.ToString()));
                foreach (senpa.Stakeholder stake in current)
                {
                    string currentStake = "_" + stake.Id.ToString();
                    treeRecoStake.Nodes["existingStakeholders"].Nodes.Add(currentStake, stake.Name);
                }
                //check for last site visit id
                senpa.Recommendations reco = agent.operation.GetCurrentWorkflowRecommendations(currentId, "registration");
                if (reco == null || reco.CreatedBy == null)
                {
                    //add site
                    long recoId = agent.operation.CreateWorkflowRecommendations(currentId, "registration");
                    rlblRecoId.Text = recoId.ToString();
                    reco = agent.operation.GetCurrentWorkflowRecommendations(currentId, "registration");
                }
                else
                {
                    ;
                }

                rlblRecoId.Text = reco.Id.ToString();
                //get applicant details if registration                    
                senpa.RegistrationRequest reg = agent.operation.GetRegistrationRequest(reco.FK_DocumentId);
                rlblName.Text = reg.FirstNames + " " + reg.LastName;
                 rlblContact.Text = reg.Mobile + "," + reg.Email;
                rlblBus.Text = reg.BusinessName;
                //get reports
                senpa.RecommendedAction[] reports = agent.operation.GetRecommendedActions(long.Parse(rlblRecoId.Text));
                foreach (senpa.RecommendedAction rep in reports)
                {
                    try
                    {
                        string[] row = { rep.FK_StakeholderId.ToString(), agent.operation.GetEntityName(rep.FK_StakeholderId, "stahol"), agent.operation.GetEntityName(rep.FK_ActionId, "act"), "", rep.Status };
                        var listViewItem = new ListViewItem(row);
                        lstRecoStake.Items.Add(listViewItem);
                        treeRecoStake.Nodes["existingStakeholders"].Nodes["_" + rep.FK_StakeholderId.ToString()].Remove();
                    }catch(Exception ex)
                    {

                    }
                    
                }
                
            }

            navigationFrame.SelectedPage = navPageRecomendations;
        }

        private void InitializeRenewalRecommendations()
        {
            RibbonShow("rec");

            lstRecoStake.Items.Clear();
            treeRecoStake.Nodes["existingStakeholders"].Nodes.Clear();
            Globals.SetPickList(cmbAction, "act");
            //Globals.SetPickList(cmbBDOReco, "BusinessDevelopmentOfficer");
            Globals.SetRecommendationStatusPickList(cmbStatusReco);

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(int.Parse(currentBusinessType.ToString()));
                foreach (senpa.Stakeholder stake in current)
                {
                    string currentStake = "_" + stake.Id.ToString();
                    treeRecoStake.Nodes["existingStakeholders"].Nodes.Add(currentStake, stake.Name);
                }
                //check for last site visit id
                senpa.Recommendations reco = agent.operation.GetCurrentWorkflowRecommendations(currentRenewalId, "renewal");
                if (reco == null || reco.CreatedBy == null)
                {
                    //add site
                    long recoId = agent.operation.CreateWorkflowRecommendations(currentRenewalId, "renewal");
                    rlblRecoId.Text = recoId.ToString();
                    reco = agent.operation.GetCurrentWorkflowRecommendations(currentRenewalId, "renewal");
                }
                else
                {
                    ;
                }

                rlblRecoId.Text = reco.Id.ToString();
                //get applicant details if registration                    
                senpa.RenewalRequest ren = agent.operation.GetRenewalRequest(reco.FK_DocumentId);
                senpa.BusinessRegistration reg = agent.operation.GetBusinessRegistration(ren.FK_BusinessRegistrationId, false);

                rlblName.Text = reg.FirstNames + " " + reg.LastName;
                rlblContact.Text = reg.Mobile + "," + reg.Email;
                rlblBus.Text = reg.BusinessName;
                //get reports
                senpa.RecommendedAction[] reports = agent.operation.GetRecommendedActions(long.Parse(rlblRecoId.Text));
                foreach (senpa.RecommendedAction rep in reports)
                {
                    try
                    {
                        string[] row = { rep.FK_StakeholderId.ToString(), agent.operation.GetEntityName(rep.FK_StakeholderId, "stahol"), agent.operation.GetEntityName(rep.FK_ActionId, "act"), "", rep.Status };
                        var listViewItem = new ListViewItem(row);
                        lstRecoStake.Items.Add(listViewItem);
                        treeRecoStake.Nodes["existingStakeholders"].Nodes["_" + rep.FK_StakeholderId.ToString()].Remove();
                    }
                    catch (Exception ex)
                    {

                    }

                }

            }

            navigationFrame.SelectedPage = navPageRecomendations;
        }

        private void InitializeRecommendationsSiteVisit()
        {
            long docId = ((isRenewal) ?currentRenewalId:currentId);
            string docType= ((isRenewal) ? "renewal" : "registration");
            RibbonShow("site");
            cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            uploadSiteButtonsTopPosition = 8;
            Globals.SetPickList(cmbSVBDO, "BusinessDevelopmentOfficer");
            Globals.SetPickList(cmbSVBDM, "BusinessDevelopmentManager");
            Globals.SetPickList(cmbSVHR, "HumanResources");
            grpSelectStakeholders.Controls.OfType<CheckBox>().ToList().ForEach(btn => btn.Dispose());
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(int.Parse(currentBusinessType.ToString()));
                foreach (senpa.Stakeholder stake in current)
                {
                    StakeholderSelect(stake);
                }
                //check for site visit id
                senpa.SiteVisit site = agent.operation.GetCurrentWorkflowRecommendationSiteVisit(long.Parse(rlblRecoId.Text), docId, docType);
                if (site == null || site.CreatedBy == null)
                {
                    //add site
                    long siteId = agent.operation.CreateWorkflowRecommendationSiteVisit(docId, docType, long.Parse(rlblRecoId.Text));
                    lblSiteId.Text = siteId.ToString();
                    site = agent.operation.GetCurrentWorkflowRecommendationSiteVisit(long.Parse(rlblRecoId.Text), docId, docType);
                }
                else
                {
                    ;
                }
                lblSiteId.Text = site.Id.ToString();
                //get applicant details if registration      
                if (isRenewal)
                {
                    senpa.RenewalRequest ren = agent.operation.GetRenewalRequest(site.FK_DocumentId);
                    senpa.BusinessRegistration reg = agent.operation.GetBusinessRegistration(ren.FK_BusinessRegistrationId, false);
                    txtSVPhone.Text = reg.Mobile;
                    rlblSiteName.Text = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    rlblSiteBusiness.Text = reg.BusinessName;
                    rlblSiteTime.Text = DateTime.Now.ToShortDateString();
                }
                else
                {
                    senpa.RegistrationRequest reg = agent.operation.GetRegistrationRequest(site.FK_DocumentId);
                    txtSVPhone.Text = reg.Mobile;
                    rlblSiteName.Text = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    rlblSiteBusiness.Text = reg.BusinessName;
                    rlblSiteTime.Text = DateTime.Now.ToShortDateString();
                }

                txtSVAddress.Text = site.VisitAddress;
                dtpSVDate.DateTime = site.VisitDate;
                //chkSVConfirmed.Checked = site.Confirmed;
                chkSVPhone.Checked = site.Phone;
                chkSVSMS.Checked = site.SMS;
                chkSVEmail.Checked = site.Email;

                if (site.Confirmed)
                {
                    cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                //get reports
                senpa.SiteVisitReport[] reports = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                foreach (senpa.SiteVisitReport rep in reports)
                {
                    (grpSelectStakeholders.Controls["stake" + "_" + rep.FK_StakeholderId.ToString()] as CheckBox).Checked = true;
                }
                
            }
        }

        private void InitializeRecommendationsSiteVisitReport()
        {
            long docId = ((isRenewal) ? currentRenewalId : currentId);
            string docType = ((isRenewal) ? "renewal" : "registration");
            RibbonShow("site");
            cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            Globals.SetPickList(cmbSVRBDO, "BusinessDevelopmentOfficer");
            Globals.SetPickList(cmbSVRBDM, "BusinessDevelopmentManager");
            Globals.SetPickList(cmbSVRHR, "HumanResources");
            Globals.SetPickList(cmbLoanInstitute, "finins");

            uploadSiteButtonsTopPosition = 82;
            grpSiteDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
            grpSiteDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //check for site visit id
                //check for site visit id
                senpa.SiteVisit site = agent.operation.GetCurrentWorkflowRecommendationSiteVisit(long.Parse(rlblRecoId.Text), docId, docType);
                if (site == null || site.CreatedBy == null)
                {
                    //add site
                    long siteId = agent.operation.CreateWorkflowRecommendationSiteVisit(docId, docType, long.Parse(rlblRecoId.Text));
                    lblSiteId.Text = siteId.ToString();
                    site = agent.operation.GetCurrentWorkflowRecommendationSiteVisit(long.Parse(rlblRecoId.Text), docId, docType);
                }
                else
                {
                    ;
                }
                lblSiteId.Text = site.Id.ToString();
                cmbHasLoan.SelectedIndex = ((site.HasLoan) ? 1 : 0);
                Globals.SetPickListValue(cmbLoanInstitute, site.FK_FinanceInstituteId);
                Globals.SetPickListValue(cmbSVRBDO, site.FK_BDO);
                Globals.SetPickListValue(cmbSVRBDM, site.FK_BDM);
                Globals.SetPickListValue(cmbSVRHR, site.FK_HR);
                txtSvBackground.Text = site.Background;
                txtSvBusinessDesc.Text = site.BusinessDescription;
                txtSvConclusion.Text = site.Conclusion;
                txtSvEquipment.Text = site.Equipment;
                txtSvManpower.Text = site.Manpower;
                txtSvMarketing.Text = site.Marketing;
                txtSvPremises.Text = site.Premises;
                txtSvTraining.Text = site.Training;
                txtLoanAmount.Text = site.LoanAmount.ToString();
                txtLoanPurpose.Text = site.LoanPurpose;

                //get applicant details if registration  
                if (isRenewal)
                {
                    senpa.RenewalRequest ren = agent.operation.GetRenewalRequest(site.FK_DocumentId);
                    senpa.BusinessRegistration reg = agent.operation.GetBusinessRegistration(ren.FK_BusinessRegistrationId, false);
                    lblSiteName.Text = reg.FirstNames + " " + reg.LastName;
                    lblSiteBusiness.Text = reg.BusinessName;
                    lblSiteTime.Text = DateTime.Now.ToShortDateString();
                }
                else
                {
                    senpa.RegistrationRequest reg = agent.operation.GetRegistrationRequest(site.FK_DocumentId);
                    lblSiteName.Text = reg.FirstNames + " " + reg.LastName;
                    lblSiteBusiness.Text = reg.BusinessName;
                    lblSiteTime.Text = DateTime.Now.ToShortDateString();
                }
                

                senpa.SiteVisitReport[] documents = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                foreach (senpa.SiteVisitReport doc in documents)
                {
                    DocumentButton(doc);
                }
            }
        }

        private void InitializeRenewalForm(long Id)
        {
            isRenewal = true;
            navigationFrame.SelectedPage = navPageRenewals;
            existingDocumentsPosition = 82;
            uploadButtonsTopPosition = 82;
            grpRenewalDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
            grpRenewalDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
            grpRenewalExisting.Controls.OfType<LinkLabel>().ToList().ForEach(btn => btn.Dispose());
            grpRenewalExisting.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
            long renewalId = 0;
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                Globals.SetPickList(cmbRenewBDO, "BusinessDevelopmentOfficer");
                if (Id != 0)
                {
                    //get renewal id
                    renewalId = agent.operation.GetCurrentRenewalID(Id);

                    //get registration
                    senpa.RenewalRequest registration = agent.operation.GetRenewalRequest(renewalId);
                    currentRenewalId = registration.Id;
                    lblRenewReference.Text = registration.ReferenceNumber;
                    txtRenewAdv.Text =  registration.BusinessAdvantages;
                    txtRenewbusDesc.Text = registration.BusinessDescription;
                    txtRenewDisadv.Text = registration.BusinessDisadvantages;

                    senpa.BusinessRegistration busReg = agent.operation.GetBusinessRegistration(registration.FK_BusinessRegistrationId, false);
                    currentBusinessType = busReg.FK_BusinessTypeId;

                    txtRenewEquip.Text = registration.Equipment;
                    txtRenewFunding.Text = registration.Funding;
                    txtRenewGoals.Text = registration.GoalsAndObjectives;
                    txtRenewMktPlan.Text = registration.MarketingPlan;
                    txtRenewOps.Text = registration.OperationsForecast;
                    txtRenewRaw.Text = registration.RawMaterials;
                    txtRenewSales.Text = registration.SalesTarget;
                    txtRenewWrkPrem.Text = registration.WorkingPremises;

                    Globals.SetPickListValue(cmbRenewBDO, registration.FK_BusinessDevelopmentOfficerId);
                    //set assign list
                    senpa.ApplicationUserSummary[] userList = agent.operation.GetAssigningUserList(currentRenewalId, "renewal");
                    cmbRenewAssign.DataSource = null;
                    Globals.SetUserPickList(cmbRenewAssign, userList);

                    //btnWorkFlow.Text = agent.operation.GetCurrentWorkFlowStage(long.Parse(lblId.Text), "registration");
                    //get current stage
                    senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(currentRenewalId, "renewal");
                    lblRenewStage.Text = currentStage.StageName;

                    RefreshRenewalIndicator();

                    if (currentStage.RequireDocuments)
                    {
                        senpa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetDocumentsRequiredStatus(currentRenewalId, "renewal");
                        foreach (senpa.WorkFlowStageDocumentStatus doc in documents)
                        {
                            DocumentButton(doc,"renewal");
                        }
                    }
                    else
                    {
                        //  grpDocuments.Visible = false;
                    }

                    if (currentStage.RequirePayment)
                    {
                       // cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmbRenWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRenWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRenWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdRenWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.StageOptional)
                    {
                        cmbRenWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmbRenWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.RequireSiteVisit)
                    {
                        cmdRenNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdRenNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdRenNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRenNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.RequireRecommendations)
                    {
                        cmdRenNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdRenNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }


                    if (!agent.operation.CheckAccessToStage(currentRenewalId, "renewal") || currentStage.StageName == "Complete")
                    {
                        cmdRenWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else
                    {
                        cmdRenWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }

                    if (currentStage.StageName == "Complete")
                    {
                        cmbSaveRen.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    //get business entity
                    senpa.BusinessRegistration business = agent.operation.GetBusinessRegistration(registration.FK_BusinessRegistrationId, false);
                    currentBusinessRegistrationId = business.FK_RegistrationRequestId;
                    lblRenewName.Text = business.FirstNames + " " + business.LastName + " (" + business.RegistrationNumber + ") " + business.BusinessName;

                    senpa.AutoDocument[] autoDoc = agent.operation.CheckAutoDocument(business.FK_RegistrationRequestId, "renewal");
                    foreach (senpa.AutoDocument doc in autoDoc)
                    {
                        DocumentLink(doc,"renewal");
                    }

                    senpa.WorkFlowStageDocumentStatus[] alldocuments = agent.operation.GetAllRequiredDocuments(business.FK_RegistrationRequestId, "registration");
                    foreach (senpa.WorkFlowStageDocumentStatus doc in alldocuments)
                    {
                        DocumentLink(doc, "business","renewal");
                    }
                }
            }

            wrkFlowToolsRibbonR.Visible = ((Globals.hasAccess("processRegistration")) ? true : false);
            regSiteRibbonR.Visible = ((Globals.hasAccess("siteVisit")) ? true : false);

        }

        private void InitializeLibrary()
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.DocumentFolders[] response = agent.operation.GetFolders(currentFolderId);
                for (int x = 0; x < response.Length; x++)
                {
                    treeFolders.Nodes["documentLibrary_1"].Nodes.Add("folder_" + response[x].Id.ToString(), response[x].FolderName);
                    pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                }

                Globals.SetPickList(cmbDocumentType, "doctyp");

                senpa.ReferenceTable[] doctypes = agent.operation.GetReferenceTableItems("doctyp");
                foreach(senpa.ReferenceTable typ in doctypes)
                {
                    treeFolders.Nodes["documentLibrary_3"].Nodes.Add("docType_" + typ.Id.ToString(), typ.Name);
                }
            }
        }

        private void InitializeTrainingForm()
        {

            navigationFrame.SelectedPage = navPageManageTraining;
            cmdSaveTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            currentTrainingClosed = false;
            // RibbonShow("train");
            Globals.SetPickList(cmbTrainingCategory, "tracat");
            Globals.SetPickList(cmbTrainingCourse, "tracou");
            Globals.SetPickList(cmbTrainingType, "tratyp");
            Globals.SetPickList(cmbTrainingTrainer, "tra");
            Globals.SetTrainingStatusPickList(cmbTrainingStatus);


            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.TrainingSession training = agent.operation.GetTrainingSession(currentTrainingId);
                lblCourse.Text = agent.operation.GetEntityName(training.FK_TrainingCourseId, "tracou");
                lblCategory.Text = agent.operation.GetEntityName(training.FK_TrainingCategoryId, "tracat");
                lblType.Text = agent.operation.GetEntityName(training.FK_TrainingTypeId, "tratyp");
                lblTrainingTime.Text = training.StartDate.ToLongDateString();

                Globals.SetPickListValue(cmbTrainingCategory, training.FK_TrainingCategoryId);
                Globals.SetPickListValue(cmbTrainingCourse, training.FK_TrainingCourseId);
                Globals.SetPickListValue(cmbTrainingType, training.FK_TrainingTypeId);
                cmbTrainingTrainer.SelectedText= training.TrainingBy;

                if (training.Status == "Closed" || training.Status == "Complete")
                {
                    if (training.Status == "Closed")
                        currentTrainingClosed = true;

                    cmdSaveTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdCloseTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (training.Status == "Complete")
                {
                    cmdCloseTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }

                Globals.SetPickListValue(cmbTrainingStatus, training.Status);
                txtTrainingVenue.Text = training.LocationAddress;
                dtpTrainingEnd.Value = training.EndDate;
                dtpTrainingStart.Value = training.StartDate;

                senpa.TrainingRegistrationReport[] current = agent.operation.GetTrainingRegistrationReport(currentTrainingId);
                gridRegisteredTraining.DataSource = current;
                gridRegisteredTraining.RefreshDataSource();                
            }

            if (Globals.hasAccess("captureTraining"))
            {
                cmdAddTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                cmdAddTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

            if (Globals.hasAccess("manageTraining"))
            {
                cmdEditTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdTrainingAttendance.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else
            {
                cmdEditTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                cmdTrainingAttendance.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void InitializeNewTrainingForm()
        {
            navigationFrame.SelectedPage = navPageManageTraining;
            currentTrainingClosed = false;
            cmdSaveTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            cmdCloseTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            // RibbonShow("train");
            Globals.SetPickList(cmbTrainingCategory, "tracat");
            Globals.SetPickList(cmbTrainingCourse, "tracou");
            Globals.SetPickList(cmbTrainingType, "tratyp");
            Globals.SetPickList(cmbTrainingTrainer, "tra");
            Globals.SetTrainingStatusPickList(cmbTrainingStatus);
            cmdCloseTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            lblCourse.Text = "";
            lblCategory.Text = "";
            lblType.Text = "";
            lblTrainingTime.Text = "";
            txtTrainingVenue.Text = "";
            gridRegisteredTraining.DataSource = null;
            gridRegisteredTraining.RefreshDataSource();
            currentTrainingId = 0;
        }

        private void InitializeAttendanceTrainingForm()
        {
            cmdSaveTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            currentTrainingClosed = false;
            navigationFrame.SelectedPage = navPageAttendance;
           // RibbonShow("train");
           
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.TrainingSession training = agent.operation.GetTrainingSession(currentTrainingId);
                alblCourse.Text = agent.operation.GetEntityName(training.FK_TrainingCourseId, "tracou");
                alblCategory.Text = agent.operation.GetEntityName(training.FK_TrainingCategoryId, "tracat");
                alblType.Text = agent.operation.GetEntityName(training.FK_TrainingTypeId, "tratyp");
                alblTrainingTime.Text = training.StartDate.ToLongDateString();

                if(training.Status=="Closed" || training.Status == "Complete")
                {
                    if (training.Status == "Closed")
                        currentTrainingClosed = true;
                    cmdCloseTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (training.Status == "Complete")
                {
                    cmdCloseTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }

                senpa.TrainingRegistrationReport[] response = agent.operation.GetTrainingRegistrationReport(currentTrainingId);
                treeTrainingAttend.Nodes["client"].Nodes.Clear();

                foreach (senpa.TrainingRegistrationReport reg in response)
                {
                    string currentreg = "_" + reg.BusinessRegistrationId.ToString();
                    treeTrainingAttend.Nodes["client"].Nodes.Add(currentreg, reg.FirstNames + " " + reg.LastName + " (" + reg.BusinessName + ")");
                    treeTrainingAttend.Nodes["client"].Nodes[currentreg].Nodes.Add(reg.Island + "(" + reg.NIN + ")");
                }

                senpa.TrainingRegistrationReport[] current = agent.operation.GetAttendedTrainingRegistrationReport(currentTrainingId);
                gridAttendedTraining.DataSource = current;
                gridAttendedTraining.RefreshDataSource();

                foreach (senpa.TrainingRegistrationReport reg in current)
                {
                    treeTrainingAttend.Nodes["client"].Nodes["_" + reg.BusinessRegistrationId.ToString()].Remove();
                }
            }
        }

        UserControl folderControl(string name, long Id)
        {
            Folder newFolder = new Folder();
            newFolder.Controls["lblText"].Text = name;
            newFolder.Controls["lblId"].Text = Id.ToString();
            newFolder.Controls["lblText"].DoubleClick += new System.EventHandler(this.folderPic_DoubleClick);
            newFolder.Controls["folderPic"].DoubleClick += new System.EventHandler(this.folderPic_DoubleClick);
            return newFolder;
        }

        UserControl fileControl(string name, string fileType, long Id)
        {
            Folder newFile = new Folder();
            newFile.Controls["lblText"].Text = name;
            newFile.Controls["lblId"].Text = Id.ToString();
            newFile.Controls["lblText"].DoubleClick += new System.EventHandler(this.filePic_DoubleClick);
            newFile.Controls["folderPic"].DoubleClick += new System.EventHandler(this.filePic_DoubleClick);
            //set filetype image
            if (fileType.IndexOf("pdf") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.PDF;
            else if (fileType.IndexOf("word") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.word;
            else if (fileType.IndexOf("excel") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.excel;
            else if (fileType.IndexOf("jpg") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.JPG;
            else if (fileType.IndexOf("png") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.PNG;
            else if (fileType.IndexOf("gif") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.GIF;
            else
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.uknwn;

            return newFile;
        }

        private void folderPic_DoubleClick(object sender, EventArgs e)
        {
            pnlExplorer.Controls.Clear();
            Control control = (Control)sender;

            currentFolderId = long.Parse(control.Parent.Controls["lblId"].Text);
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                lblFolderMap.Text = agent.operation.GetFolderPath(currentFolderId).Replace(",", " > ");

                senpa.DocumentFolders[] response = agent.operation.GetFolders(currentFolderId);
                for (int x = 0; x < response.Length; x++)
                {
                    pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                }
                //get files
                senpa.DocumentLibrary[] documents = agent.operation.GetFolderDocuments(currentFolderId);
                foreach (senpa.DocumentLibrary doc in documents)
                {
                    pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                }
            }
        }

        private void filePic_DoubleClick(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            long docId = long.Parse(control.Parent.Controls["lblId"].Text);
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.DocumentLibrary doc = agent.operation.GetDocument(docId);
                string filePath = Application.StartupPath + "\\filer\\" + control.Parent.Controls["lblText"].Text;
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(doc.DocumentData, 0, doc.DocumentData.Length);
                fs.Flush();
                fs.Close();
                System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                newProcess.Start();
                newProcess.WaitForExit();
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch
                {

                }
            }
        }

        private TreeNode FindRootNode(TreeNode treeNode)
        {
            while (treeNode.Parent != null)
            {
                treeNode = treeNode.Parent;
            }
            return treeNode;
        }

        private void InitializeWorkflowAdmin()
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.WorkFlows[] response = agent.operation.GetWorkFlows();
                foreach (senpa.WorkFlows wrkFlow in response)
                {
                    string currentFlow = "_" + wrkFlow.Id.ToString();
                    treeWorkFlow.Nodes["workFlows"].Nodes.Add(currentFlow, wrkFlow.WorkFlowName);
                }

                Globals.SetPickList(cmbEnd, "rolgro");
                Globals.SetPickList(cmbStart, "rolgro");
            }
        }

        private void InitializeDocumentDesignAdmin()
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.AutoDocumentsDesign[] response = agent.operation.GetAutoDocumentsDesigns();
                foreach (senpa.AutoDocumentsDesign desg in response)
                {
                    string currentDesign = "_" + desg.DocumentName;
                    treeDesignDocs.Nodes["designs"].Nodes.Add(currentDesign, desg.DocumentName.ToUpper());
                }
                
            }
        }

        private void InitializeBusinessTypes()
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.ReferenceTable[] busType = agent.operation.GetReferenceTableItems("bustyp");
                foreach (senpa.ReferenceTable bus in busType)
                {
                    string currentBus = "_" + bus.Id.ToString();
                    treeBusinessType.Nodes["businessType"].Nodes.Add(currentBus, bus.Name);
                }
            }
        }

        private bool CheckForValue(Control parent)
        {
            bool seT = true;
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(LookUpEdit))
                {
                    if((c as LookUpEdit).EditValue==null)
                    {
                        seT = false;
                        (c as LookUpEdit).Focus();
                        break;
                    }
                }
                else if (c.GetType() == typeof(TextEdit))
                {
                    if ((c as TextEdit).Text == "")
                    {
                        if ((c as TextEdit).Name == "txtNIN")
                        {
                            seT = false;
                            (c as TextEdit).Focus();
                            break;
                        }
                    }
                }
                else
                {
                    CheckForValue(c);
                }
            }

            return seT;
        }
                   
        private void InitializeQuickStats()
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.QuickStats response = agent.operation.GetQuickStats();
                lblregCount.Text = response.RegisteredBusinessCount.ToString();
                lblrenCount.Text = response.PendingRenewalsCount.ToString();
                lblpenregCount.Text = response.PendingBusinessRegistrationsCount.ToString();
                lblsiteCount.Text = response.PendingSiteVisitsCount.ToString();
            }
        }

        public void DocumentLink(senpa.Notifications doc)
        {
            LinkLabel addDocument = new LinkLabel();
            addDocument.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            addDocument.Location = new System.Drawing.Point(43, (this.existingDocumentsPosition));
            addDocument.Name = "_" + doc.Id.ToString() + "_" + doc.DocumentType.ToString() + "_" + doc.FK_DocumentId;
            addDocument.Size = new System.Drawing.Size(169, 13);
            addDocument.TabIndex = 27;
            addDocument.TabStop = true;
            addDocument.Text = doc.Title;
            addDocument.Click += new System.EventHandler(this.OpenNotificationDocument_Click);
            PictureBox pic = new PictureBox();
            pic.Image = picNot.Image;
            // pic.Image = ((System.Drawing.Image)(sbfa.Properties.Resources.sign_tick));
            pic.Location = new System.Drawing.Point(8, this.existingDocumentsPosition);
            pic.Name = "picNotView" + "_" + doc.Id.ToString();
            pic.Size = new System.Drawing.Size(26, 26);
            pic.TabIndex = 12;
            pic.TabStop = false;
            pic.Visible = true;

            pnlNotifications.Controls.Add(addDocument);
            pnlNotifications.Controls.Add(pic);

            existingDocumentsPosition += 38;
        }

        private void OpenNotificationDocument_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                long docId = long.Parse(control.Name.Split('_')[3]);
                string docType = control.Name.Split('_')[2];
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    if(docType=="invoice")
                    {
                        try
                        {
                            currentInvoiceId = docId;

                            new ProcessInvoice().ShowDialog();
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if(docType=="registration" || docType == "registrationSite")
                    {
                        try
                        {
                            currentId = docId;

                            InitializeRegForm(currentId);
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (docType == "renewal" || docType == "renewalSite")
                    {
                        try
                        {
                            currentBusinessId = docId;

                            InitializeRenewalForm(currentBusinessId);
                        }
                        catch (Exception ex)
                        {
                            //MessageBox.Show(ex.ToString());
                            ShowMessageBox("An Error Occured", ex.Message, "error");
                        }
                    }
                    agent.operation.UpdateNotifications(long.Parse(control.Name.Split('_')[1]), true);
                   (pnlNotifications.Controls["picNotView" + "_" + control.Name.Split('_')[1]] as PictureBox).Image = picNotRead.Image;                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void InitializeNotifications()
        {
            pnlNotifications.Controls.OfType<LinkLabel>().ToList().ForEach(btn => btn.Dispose());
            pnlNotifications.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                this.existingDocumentsPosition = 8;
                senpa.Notifications[] response = agent.operation.CheckNotifications("", true);
                foreach(senpa.Notifications not in response)
                {
                    DocumentLink(not);
                }
            }
        }

        #endregion
        
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //Validations for the saving
            if (txtNIN.Text.Trim().Equals(""))
            {
                ShowValidationError("Please enter the NIN before you can save the details");
                return;
            }
            if (txtFirstName.Text.Trim().Equals(""))
            {
                ShowValidationError("Please enter the Name before you can save the details");
                return;
            }
            if (txtLastName.Text.Trim().Equals(""))
            {
                ShowValidationError("Please enter the LastName before you can save the details");
                return;
            }

            try
            {
                Globals.GetComboBoxValue(cmbResIsland);
                Globals.GetComboBoxValue(cmbBusRegType);
                Globals.GetComboBoxValue(cmbBusDistrict);
                Globals.GetComboBoxValue(cmbResIsland);
                Globals.GetComboBoxValue(cmbResDistrict);
                Globals.GetComboBoxValue(cmbBDO);
            }
            catch (Exception ex)
            {
                ShowValidationError("Please enter all the mandatory fields and option boxes before continuing.");
                return;
            }

            if (!CheckForValue(pnlRegistration))
                return;
            if (cmbBDO.ValueMember == null)
            {
                //MessageBox.Show("Please assign Business Development Officer");
                ShowMessageBox("Missing Information", "Please assign Business Development Officer", "warning");
                cmbBDO.Focus();
                return;
            }

            if (cmbSalutation.Text == null || cmbSalutation.Text == "")
            {
                cmbSalutation.Focus();
                return;
            }

            if (cmbGender.Text == null || cmbGender.Text == "")
            {
                cmbGender.Focus();
                return;
            }

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.RegistrationRequest newRegistration = new senpa.RegistrationRequest();
                newRegistration.Id = long.Parse(lblId.Text);
                if (lblReference.Text == "0")
                    newRegistration.ReferenceNumber = Utilities.GenerateReferenceNumber();
                else

                    //try
                    //{
                    newRegistration.ReferenceNumber = lblReference.Text;
                newRegistration.BusinessRegistrationNumber = txtBusinessName.Text;
                newRegistration.BusinessName = txtBusinessRegNumber.Text;
                newRegistration.FK_BusinessTypeId = Globals.GetComboBoxValue(cmbBusType);
                newRegistration.FK_BusinessRegistrationTypeId = Globals.GetComboBoxValue(cmbBusRegType);
                newRegistration.FK_BusinessIslandLocationId = Globals.GetComboBoxValue(cmbBusIsland);
                newRegistration.FK_BusinessIslandDistrictId = Globals.GetComboBoxValue(cmbBusDistrict);
                newRegistration.NIN = txtNIN.Text;
                newRegistration.FirstNames = txtFirstName.Text;
                newRegistration.LastName = txtLastName.Text;
                newRegistration.Salutation = cmbSalutation.SelectedValue.ToString();
                newRegistration.Citizenship = txtCitizenship.Text;
                newRegistration.Gender = cmbGender.SelectedValue.ToString();
                newRegistration.DOB = dtpDOB.DateTime;
                newRegistration.FK_ResidenceIslandLocationId = Globals.GetComboBoxValue(cmbResIsland);
                newRegistration.FK_ResidenceDistrictLocationId = Globals.GetComboBoxValue(cmbResDistrict);
                newRegistration.Mobile = txtMobile.Text;
                newRegistration.HomeTelephone = txtHomeTel.Text;
                newRegistration.WorkTelephone = txtWorkTel.Text;
                newRegistration.Email = txtEmail.Text;
                newRegistration.FK_EducationLevelId = Globals.GetComboBoxValue(cmbEdu);
                newRegistration.TermsAndConditionsAccepted = chkTerms.Checked;
                newRegistration.FK_BusinessDevelopmentOfficerId = Globals.GetComboBoxValue(cmbBDO);
                newRegistration.SubmissionType = "O";
                newRegistration.Status = "";
                newRegistration.StatusReason = "";
                newRegistration.CertificateIssueDate = DateTime.Now;
                newRegistration.DocumentType = "registration";
                newRegistration.RequireWorkFlow = true;
                newRegistration.WorkFlowId = 0;
                newRegistration.WorkFlowStatus = "New";
                newRegistration.Created = DateTime.Now;
                newRegistration.CreatedBy = Globals.userLogged;
                newRegistration.LastModified = DateTime.Now;
                newRegistration.LastModifiedBy = Globals.userLogged;

                //}catch(Exception ex)
                //{
                //    ShowValidationError("Please enter all the mandatory fields and option boxes before continuing.");
                //    return;
                //}


                //validate parameters
                senpa.WorkFlowFieldValidations[] vals = agent.operation.GetValidationsList("registration");
                bool allValidated = true;
                foreach (senpa.WorkFlowFieldValidations aVal in vals)
                {
                    if (!Globals.ValidateLoanField(aVal, newRegistration))
                    {
                        MessageBox.Show(aVal.ParameterFieldName + " field is invalid");
                        allValidated = false;
                        break;
                    }
                }

                if (allValidated)
                {

                    long response = agent.operation.SaveRegistrationRequest(newRegistration);
                    if (response > 0)
                    {
                        currentId = response;
                        lblId.Text = response.ToString();
                        lblReference.Text = newRegistration.ReferenceNumber;
                        uploadButtonsTopPosition = 82;
                        #region access
                        grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                        grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                        senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "registration");
                        lblStage.Text = currentStage.StageName;
                        RefreshRegIndicator();
                        if (currentStage.RequireDocuments)
                        {
                            senpa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetDocumentsRequiredStatus(long.Parse(lblId.Text), "registration");
                            foreach (senpa.WorkFlowStageDocumentStatus doc in documents)
                            {
                                DocumentButton(doc);
                            }
                        }
                        else
                        {
                            //grpDocuments.Visible = false;
                        }

                        if (currentStage.RequirePayment)
                        {
                            cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }
                        else
                        {
                            cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }

                        if (currentStage.RequireSiteVisit)
                        {
                            cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }
                        else
                        {
                            cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }

                        if (currentStage.RequireRecommendations)
                        {
                            cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }
                        else
                        {
                            cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }

                        if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "registration"))
                        {
                            cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }
                        else
                        {
                            cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }

                        if (Globals.organisation != 0 && currentStage.RequireSiteVisit)
                        {
                            //check for site visit id
                            senpa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "registration");
                            if (site == null || site.CreatedBy == null)
                            {
                                ;//ignore
                            }
                            else
                            {
                                // grpSiteReports.Visible = true;
                                senpa.SiteVisitReport[] documents = agent.operation.GetSiteVisitReports(site.Id);
                                foreach (senpa.SiteVisitReport doc in documents)
                                {
                                    if (doc.FK_StakeholderId == Globals.organisation)
                                    {
                                        DocumentButton(doc);
                                    }
                                }
                            }
                        }
                        #endregion

                        //set assign list
                        //senpa.ApplicationUserSummary[] userList = agent.operation.GetAssigningUserList(long.Parse(lblId.Text), "registration");
                        //cmbAssign.DataSource = null;
                        //Globals.SetUserPickList(cmbAssign, userList);
                    }
                    else
                    {
                        //MessageBox.Show("Error Saving");
                        ShowMessageBox("An Error Occured!", "Error Saving: This business is already under the processing phase.", "error");
                    }
                }
            }
        }

        public SEnPAMain()
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }

        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int barItemIndex = barSubItemNavigation.ItemLinks.IndexOf(e.Link);
            navBarControl.ActiveGroup = navBarControl.Groups[barItemIndex];
        }

        private void ribbonControl_Click(object sender, EventArgs e)
        {

        }

        private void navigationPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }


        private void comboBoxEdit6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void labelControl12_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {

        }

        private void labelControl13_Click(object sender, EventArgs e)
        {


        }

        private void SEnPAMain_Load(object sender, EventArgs e)
        {
            siteRibbon.Visible = false;
            trainingRibbon.Visible = false;
            registrationRibbon.Visible = false;
            ribbonMessaging.Visible = true;
            payQuickActions.Visible = false;
            docQuickActions.Visible = false;
            regQuickActions.Visible = false;

            btnSearchRegistrations.EditValue = "";
            txtFindInvoices.EditValue = "";
            txtRegBusFind.EditValue = "";
            txtBarEditUser.EditValue = "";
            txtFindEmail.EditValue = "";
            txtFindSMS.EditValue = "";
            txtFindTraining.EditValue = "";
            txtFindForTraining.EditValue = "";
            btnSearchRenewal.EditValue = "";

            navStack = new List<NavigationPage>();

            //RefreshIndicator();

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //get user profile
                senpa.ApplicationUsers tempUser = agent.operation.GetUser(Globals.userLogged);
                Globals.organisation = tempUser.FK_StakeholderId;

                barUserDetail.Caption = tempUser.FirstName + " " + tempUser.Surname;
                
                lblDashboardUsername.Text = tempUser.FirstName + " " + tempUser.Surname;

                barUserAccess.Caption= "(" + tempUser.FK_RoleGroup + ")";
                barUserOrganisation.Caption = ((tempUser.FK_StakeholderId == 0) ? "SEnPA" : agent.operation.GetEntityName(tempUser.FK_StakeholderId, "stahol"));

                //user group
                senpa.ReferenceTable usrGrp = agent.operation.GetReferenceTableItemByName(tempUser.FK_RoleGroup, "rolgro");
                lblUserGr.Text = usrGrp.Description;
            }
            InitializeLibrary();
            InitializeWorkflowAdmin();
            InitializeBusinessTypes();
            InitializeDocumentDesignAdmin();
            InitializeQuickStats();
            InitializeNotifications();

            Globals.SetPickList(cmbBusIsland, "isl");
            Globals.SetPickList(cmbResIsland, "isl");
            Globals.SetPickList(rcmbBusIsland, "isl");
            Globals.SetPickList(rcmbResIsland, "isl");

            SideBarRights();
            TopBarRights();
        }

        private void cmbBusType_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        private void btnWorkFlow_Click(object sender, EventArgs e)
        {
            if(chkTerms.Checked==false)
            {
                //MessageBox.Show("Cannot proceed without accepting Terms");
                ShowMessageBox("Missing Information", "Please accept the terms and conditions to proceed.", "warning");
                return;
            }
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //check documents
                string response = agent.operation.CheckCurrentStageDocumentRequirements(long.Parse(lblId.Text), "registration");
                if (response.ToLower() == "none")
                {
                    //check recommendations
                    response = agent.operation.CheckCurrentStageRecommendationsRequirements(long.Parse(lblId.Text), "registration");
                    if (response.ToLower() == "none")
                    {
                        //check site
                        response = agent.operation.CheckCurrentStageSiteVisitRequirements(long.Parse(lblId.Text), "registration");
                        if (response.ToLower() == "none")
                        {
                            #region access
                            senpa.DocumentWorkflow wrkFlow = agent.operation.UpdateWorkFlowStage(long.Parse(lblId.Text), "registration");
                            uploadButtonsTopPosition = 82;
                            grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                            grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                            senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "registration");
                            lblStage.Text = currentStage.StageName;
                            if (currentStage.RequireDocuments)
                            {
                                senpa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetDocumentsRequiredStatus(long.Parse(lblId.Text), "registration");
                                foreach (senpa.WorkFlowStageDocumentStatus doc in documents)
                                {
                                    DocumentButton(doc);
                                }
                            }
                            else
                            {
                                //  grpDocuments.Visible = false;
                            }

                            if (currentStage.RequirePayment)
                            {
                                cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }

                            if (currentStage.RequireSiteVisit)
                            {
                                cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }

                            if (currentStage.RequireRecommendations)
                            {
                                cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }


                            if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "registration") || currentStage.StageName == "Complete")
                            {
                                cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                            else
                            {
                                cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }

                            if (currentStage.StageName == "Complete")
                            {
                                //create business register record
                                long saveBusiness = agent.operation.RegisterBusiness(currentId);
                                if (saveBusiness > 0)
                                {
                                    currentBusinessId = saveBusiness;
                                    navigationFrame.SelectedPage = navPageBusiness;
                                    InitializeBusinessForm(currentBusinessId);
                                }

                            }
                            else
                            {

                            }


                            if (Globals.organisation != 0 && currentStage.RequireSiteVisit)
                            {
                                //check for site visit id
                                senpa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "registration");
                                if (site == null || site.CreatedBy == null)
                                {
                                    ;//ignore
                                }
                                else
                                {
                                    // grpSiteReports.Visible = true;
                                    senpa.SiteVisitReport[] documents = agent.operation.GetSiteVisitReports(site.Id);
                                    foreach (senpa.SiteVisitReport doc in documents)
                                    {
                                        if (doc.FK_StakeholderId == Globals.organisation)
                                        {
                                            DocumentButton(doc);
                                        }
                                    }
                                }
                            }
                            #endregion
                            //set assign list
                            //senpa.ApplicationUserSummary[] userList = agent.operation.GetAssigningUserList(long.Parse(lblId.Text), "registration");
                            //cmbAssign.DataSource = null;
                            //Globals.SetUserPickList(cmbAssign, userList);
                        }
                        else
                        {
                            //MessageBox.Show(response);
                            ShowMessageBox(response, response, "normal");
                        }
                    }
                    else
                    {
                        //MessageBox.Show(response);
                        ShowMessageBox(response, response, "normal");
                    }
                }
                else
                {
                    MessageBox.Show(response);
                }

            }
        }

        private void btnManageSiteVisit_Click(object sender, EventArgs e)
        {
            navigationFrame.SelectedPage = navPageScheduleSiteVisit;
            InitializeSiteVisit();
        }

        private void btnSearchRegistrations_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.RegistrationRequest[] response = agent.operation.GetRegistrationRequests(btnSearchRegistrations.EditValue.ToString());              

                gridViewRegistrations.DataSource = response;
                gridViewRegistrations.RefreshDataSource();

                //showing summary
                string searchString = btnSearchRegistrations.Text;
                if (searchString.Equals(""))
                {
                    label13.Text = "Showing " + response.Length.ToString() + " result(s) for registrations.";
                }
                else
                {
                    label13.Text = "Showing " + response.Length.ToString() + " result(s) for the search '" + searchString + "'";
                }
               
                if (response.Length == 0)
                {
                    ShowNoResults();
                }
            }
            
        }

        private void lstRegistrations_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
               // currentId = long.Parse(lstRegistrations.SelectedItems[0].SubItems[0].Text);
            }
            catch (Exception ex)
            {

            }
        }
        
        private void btnSearchRegistrations_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridViewRegistrations_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void gridRegistrations_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                currentId = long.Parse(gridRegistrations.GetRowCellValue(gridRegistrations.FocusedRowHandle, gridRegistrations.Columns[0]).ToString());
               
                InitializeRegForm(currentId);
            }
            catch (Exception ex)
            {

            }
        }

        private void navBarItemViewRegistrations_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageViewRegistration;
        }

        private void newRegistration_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            InitializeNewRegForm();
        }

        private void gridInvoices_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                currentInvoiceId = long.Parse(gridInvoices.GetRowCellValue(gridInvoices.FocusedRowHandle, gridInvoices.Columns[0]).ToString());

                new ProcessInvoice().ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }
                
        private void navBarItemRegisteredBusiness_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageRegisteredBusiness;
        }

        private void txtRegBusFind_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.BusinessRegistration[] response = agent.operation.GetBusinessRegistrations(txtRegBusFind.EditValue.ToString());

                gridViewRegisteredBusness.DataSource = response;
                gridViewRegisteredBusness.RefreshDataSource();

                //showing summary
                string searchString = txtRegBusFind.Text;
                if (searchString.Equals(""))
                {
                    label122.Text = "Showing " + response.Length.ToString() + " result(s) for businesses.";
                }
                else
                {
                    label122.Text = "Showing " + response.Length.ToString() + " result(s) for the search '" + searchString + "'";
                }

                if (response.Length == 0)
                {
                    ShowNoResults();
                }
            }
        }

        private void gridRegisteredBusness_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                currentBusinessId = long.Parse(gridRegisteredBusness.GetRowCellValue(gridRegisteredBusness.FocusedRowHandle, gridRegisteredBusness.Columns[0]).ToString());

                InitializeBusinessForm(currentBusinessId);                
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ShowMessageBox("An Error Occured", ex.Message, "error");
            }
        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void btnRecoAddStake_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                if (treeRecoStake.SelectedNode.Text.ToLower() != "stakeholders")
                {
                    int currentStake = int.Parse(treeRecoStake.SelectedNode.Name.Split('_')[1]);
                    long done = agent.operation.SaveRecommendedAction(long.Parse(rlblRecoId.Text), currentStake, 0,"", false, "", "", true);
                    if (done > 0)
                    {
                        string[] row = { currentStake.ToString(), agent.operation.GetEntityName(currentStake, "stahol"), agent.operation.GetEntityName(1, "act"), "", "" };
                        var listViewItem = new ListViewItem(row);
                        lstRecoStake.Items.Add(listViewItem);
                        treeRecoStake.SelectedNode.Remove();
                        
                    }
                    else
                    {
                        ;
                    }
                }
            }
        }

        private void lstRecoStake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    senpa.RecommendedAction act = agent.operation.GetRecommendedAction(long.Parse(rlblRecoId.Text), int.Parse(lstRecoStake.SelectedItems[0].SubItems[0].Text));
                    Globals.SetPickListValue(cmbStatusReco, act.Status);
                    // Globals.SetPickListValue(cmbBDOReco, act.FK_BusinessDevelopmentOfficer);
                    Globals.SetPickListValue(cmbAction, act.FK_ActionId);

                    txtDetails.Text = act.Details;
                    // txtComments.Text = act.Comments;
                    //dtpDueDateReco.EditValue = act.DeadlineDate;
                    txtReasonReco.Text = act.StatusReason;
                    //chkActionedReco.Checked = act.Actioned;
                    chkActiveReco.Checked = act.Active;
                    chkReminderReco.Checked = act.Reminder;
                    lblsthId.Text = act.FK_StakeholderId.ToString();
                }
            }
            catch { }
        }

        private void btnSaveReco_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRecommendations_Click(object sender, EventArgs e)
        {
            InitializeRecommendations();
        }

        private void btnRecoSiteVisit_Click(object sender, EventArgs e)
        {
           
        }

        private void txtNIN_Leave(object sender, EventArgs e)
        {
            if (txtNIN.Text == "")
                return;

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Resident response = agent.operation.GetResident(txtNIN.Text);

                txtFirstName.Text = response.FirstName;
                txtLastName.Text = response.Surname;

                txtCitizenship.Text = response.Nationality;
                // Type { get => type; set => type = value; }
                // Status { get => status; set => status = value; }
                dtpDOB.DateTime = response.DateOfBirth;
                if (response.FirstName == "")
                    txtNIN.Text = "";
            }
        }

        private void txtBusinessRegNumber_Leave(object sender, EventArgs e)
        {
            if (txtBusinessRegNumber.Text.Equals(""))
            {
                return;
            }

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Business response = agent.operation.GetBusiness(txtBusinessRegNumber.Text);
                
                txtBusinessName.Text = response.Name;
                if (response.Name == "")
                    txtBusinessRegNumber.Text = "";
            }
        }

        private void rtxtNIN_Leave(object sender, EventArgs e)
        {
            if (rtxtNIN.Text == "")
                return;

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Resident response = agent.operation.GetResident(rtxtNIN.Text);

                rtxtFirstName.Text = response.FirstName;
                rtxtLastName.Text = response.Surname;

                rtxtCitizenship.Text = response.Nationality;
                // Type { get => type; set => type = value; }
                // Status { get => status; set => status = value; }
                rdtpDOB.DateTime = response.DateOfBirth;
                if (response.FirstName == "")
                    rtxtNIN.Text = "";
            }
        }

        private void rtxtBusinessRegNumber_Leave(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Business response = agent.operation.GetBusiness(rtxtBusinessRegNumber.Text);

                rtxtBusinessName.Text = response.Name;
                if (response.Name == "")
                    rtxtBusinessRegNumber.Text = "";
            }
        }
              
        private void btnBusinessView_Click(object sender, EventArgs e)
        {
            
        }

        private void txtSearchFiles_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            pnlExplorer.Controls.Clear();
            Control control = (Control)sender;
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                TreeNode temp = treeFolders.SelectedNode;
                if (FindRootNode(temp).Name == "documentLibrary_1")
                {
                    senpa.DocumentLibrary[] documents = agent.operation.SearchDocuments(currentFolderId, txtSearchFiles.Text);
                    foreach (senpa.DocumentLibrary doc in documents)
                    {
                        pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                    }
                }
                else if (FindRootNode(temp).Name == "documentLibrary_3")
                {
                    long currentTypeId = long.Parse(treeFolders.SelectedNode.Name.Split('_')[1]);
                    senpa.DocumentLibrary[] documents = agent.operation.SearchRegistrationDocuments(currentTypeId, txtSearchFiles.Text);
                    foreach (senpa.DocumentLibrary doc in documents)
                    {
                        pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                    }
                }
               
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (currentFolderId < 4)
                return;
            pnlExplorer.Controls.Clear();
            Control control = (Control)sender;
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                currentFolderId = agent.operation.GetParentFolderId(currentFolderId);
                lblFolderMap.Text = agent.operation.GetFolderPath(currentFolderId).Replace(",", " > ");

                senpa.DocumentFolders[] response = agent.operation.GetFolders(currentFolderId);
                for (int x = 0; x < response.Length; x++)
                {
                    pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                }
                //get files
                senpa.DocumentLibrary[] documents = agent.operation.GetFolderDocuments(currentFolderId);
                foreach (senpa.DocumentLibrary doc in documents)
                {
                    pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                }
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                long response = agent.operation.CreateFolder(txtAddFolder.EditValue.ToString(), currentFolderId);
                if (response > 0)
                {
                    if (currentFolderId == 1)
                    {
                        treeFolders.Nodes["documentLibrary_1"].Nodes.Add("folder_" + response, txtAddFolder.EditValue.ToString());
                    }
                    pnlExplorer.Controls.Add(folderControl(txtAddFolder.EditValue.ToString(), response));
                }
                else
                {
                    //MessageBox.Show("Error creating folder");
                    ShowMessageBox("An Error Occured", "Error creating folder", "error");
                }
               
            }
        }

        private void txtFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            uploadDocuments.ShowDialog();
            txtFile.Text = uploadDocuments.FileName;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (txtFile.Text == "")
                return;
            byte[] buffer = File.ReadAllBytes(txtFile.Text);
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                bool done = agent.operation.UploadDocument(0, Path.GetFileName(txtFile.Text), buffer, Globals.GetComboBoxValue(cmbDocumentType), currentFolderId);
                if (done)
                {
                    txtFile.Text = "";
                    //MessageBox.Show("Document saved");
                    ShowMessageBox("Saved!", "The document was saved successfully", "success");
                }
                else
                {
                    MessageBox.Show("Error uploading your document");
                    ShowMessageBox("An Error Occured", "Failed to upload the document. Please try again.", "error");
                }

            }
        }

        private void treeFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            pnlExplorer.Controls.Clear();
           
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                TreeNode temp = treeFolders.SelectedNode;
                if (FindRootNode(temp).Name == "documentLibrary_3")
                {
                    long currentTypeId = long.Parse(treeFolders.SelectedNode.Name.Split('_')[1]);
                    lblFolderMap.Text = "Registrations/" + treeFolders.SelectedNode.Text;
                    //get files
                    senpa.DocumentLibrary[] documents = agent.operation.GetRegistrationsFolderDocuments(currentTypeId);
                    foreach (senpa.DocumentLibrary doc in documents)
                    {
                        pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                    }
                }
                else if (FindRootNode(temp).Name == "documentLibrary_1")
                {
                    currentFolderId = long.Parse(treeFolders.SelectedNode.Name.Split('_')[1]);
                    lblFolderMap.Text = agent.operation.GetFolderPath(currentFolderId).Replace(",", " > ");
                    //get folders
                    senpa.DocumentFolders[] response = agent.operation.GetFolders(currentFolderId);
                    for (int x = 0; x < response.Length; x++)
                    {
                        pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                    }
                    //get files
                    senpa.DocumentLibrary[] documents = agent.operation.GetFolderDocuments(currentFolderId);
                    foreach (senpa.DocumentLibrary doc in documents)
                    {
                        pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                    }
                }
                else
                {
                    lblFolderMap.Text = "Help/" + treeFolders.SelectedNode.Text;
                }
                
            }
        }

        private void barButtonManageWorkflow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {           
            navigationFrame.SelectedPage = navPageWorkFlows;
            RibbonShow("wrk");
        }

        private void btnWrkUp_Click(object sender, EventArgs e)
        {
            Globals.MoveItems(lstStages, Globals.MoveDirection.Up);
           
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                if (long.Parse(lstStages.SelectedItems[0].SubItems[1].Text) > 0)
                {
                    int x = lstStages.SelectedIndices[0];

                    bool done = agent.operation.SwitchStages(long.Parse(lstStages.SelectedItems[0].SubItems[0].Text), long.Parse(lstStages.Items[x + 1].SubItems[0].Text));

                    lstStages.Items.Clear();
                    senpa.WorkFlowStages[] response = agent.operation.GetWorkFlowStages(long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]));
                    foreach (senpa.WorkFlowStages wrkFlow in response)
                    {
                        string[] row = { wrkFlow.Id.ToString(), wrkFlow.StagePosition.ToString(), wrkFlow.StageName, wrkFlow.StageDescription, agent.operation.GetEntityName(wrkFlow.FK_RoleGroupId, "rolgro"), ((wrkFlow.StageAssignMode == 1) ? "Yes" : "No"), ((wrkFlow.StageOptional) ? "Yes" : "No"), ((wrkFlow.RequireDocuments) ? "Yes" : "No"), ((wrkFlow.RequirePayment) ? "Yes" : "No"), ((wrkFlow.RequireSiteVisit) ? "Yes" : "No"), ((wrkFlow.RequireRecommendations) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstStages.Items.Add(listViewItem);
                    }

                    lstStages.Items[x].Selected = true;
                    lstStages.HideSelection = false;
                    lstStages.Focus();
                }  
            }       
            
        }

        private void btnWrkDwn_Click(object sender, EventArgs e)
        {
            Globals.MoveItems(lstStages, Globals.MoveDirection.Down);
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                try
                {
                    int x = lstStages.SelectedIndices[0];

                    bool done = agent.operation.SwitchStages(long.Parse(lstStages.Items[x - 1].SubItems[0].Text), long.Parse(lstStages.SelectedItems[0].SubItems[0].Text));

                    lstStages.Items.Clear();
                    senpa.WorkFlowStages[] response = agent.operation.GetWorkFlowStages(long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]));
                    foreach (senpa.WorkFlowStages wrkFlow in response)
                    {
                        string[] row = { wrkFlow.Id.ToString(), wrkFlow.StagePosition.ToString(), wrkFlow.StageName, wrkFlow.StageDescription, agent.operation.GetEntityName(wrkFlow.FK_RoleGroupId, "rolgro"), ((wrkFlow.StageAssignMode == 1) ? "Yes" : "No"), ((wrkFlow.StageOptional) ? "Yes" : "No"), ((wrkFlow.RequireDocuments) ? "Yes" : "No"), ((wrkFlow.RequirePayment) ? "Yes" : "No"), ((wrkFlow.RequireSiteVisit) ? "Yes" : "No"), ((wrkFlow.RequireRecommendations) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstStages.Items.Add(listViewItem);
                    }

                    lstStages.Items[x].Selected = true;
                    lstStages.HideSelection = false;
                    lstStages.Focus();
                }
                catch {; }
            }
        }

        private void btnSaveWrk_Click(object sender, EventArgs e)
        {
            
        }

        private void treeWorkFlow_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                if (treeWorkFlow.SelectedNode.Text.ToLower() != "work flows")
                {
                    currentWorkFlow = long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]);
                    senpa.WorkFlows wrk = agent.operation.GetWorkFlow(long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]));
                    lblWrkId.Text = wrk.Id.ToString();
                    txtName.Text = wrk.WorkFlowName;
                    txtDescription.Text = wrk.WorkFlowDescription;
                    Globals.SetPickListValue(cmbStart, wrk.FK_StartRoleGroupId);
                    Globals.SetPickListValue(cmbEnd, wrk.FK_EndRoleGroupId);

                    lstStages.Items.Clear();
                    senpa.WorkFlowStages[] response = agent.operation.GetWorkFlowStages(long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]));
                    foreach (senpa.WorkFlowStages wrkFlow in response)
                    {
                        string[] row = { wrkFlow.Id.ToString(), wrkFlow.StagePosition.ToString(), wrkFlow.StageName, wrkFlow.StageDescription, agent.operation.GetEntityName(wrkFlow.FK_RoleGroupId, "rolgro"), ((wrkFlow.StageAssignMode == 1) ? "Yes" : "No"), ((wrkFlow.StageOptional) ? "Yes" : "No"), ((wrkFlow.RequireDocuments) ? "Yes" : "No"), ((wrkFlow.RequirePayment) ? "Yes" : "No"), ((wrkFlow.RequireSiteVisit) ? "Yes" : "No"), ((wrkFlow.RequireRecommendations) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstStages.Items.Add(listViewItem);
                    }

                }
            }
        }

        private void btnStages_Click(object sender, EventArgs e)
        {
           
        }

        private void treeBusinessType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeStakeHolder.Nodes["stakeHolder"].Nodes.Clear();
            treeSavedToBusiness.Nodes["existingStakeholders"].Nodes.Clear();
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                if (treeBusinessType.SelectedNode.Text.ToLower() != "business type")
                {
                    currentBusiness = int.Parse(treeBusinessType.SelectedNode.Name.Split('_')[1]);
                    senpa.Stakeholder[] response = agent.operation.GetStakeholders();
                    foreach (senpa.Stakeholder stake in response)
                    {
                        string currentStake = "_" + stake.Id.ToString();
                        treeStakeHolder.Nodes["stakeHolder"].Nodes.Add(currentStake, stake.Name);
                    }

                    senpa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(currentBusiness);
                    foreach (senpa.Stakeholder stake in current)
                    {
                        string currentStake = "_" + stake.Id.ToString();
                        treeSavedToBusiness.Nodes["existingStakeholders"].Nodes.Add(currentStake, stake.Name);
                        //remove from system roles
                        treeStakeHolder.Nodes["stakeHolder"].Nodes[currentStake].Remove();
                    }
                }
            }
        }

        private void btnAddStakeToBus_Click(object sender, EventArgs e)
        {
            try
            {
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeStakeHolder.SelectedNode.Text.ToLower() != "stakeholders")
                    {
                        TreeNode temp = treeStakeHolder.SelectedNode;
                        int currentStake = int.Parse(treeStakeHolder.SelectedNode.Name.Split('_')[1]);
                        bool done = agent.operation.SaveBusinessTypeStakeholder(currentBusiness, currentStake, true);
                        if (done)
                        {
                            treeStakeHolder.SelectedNode.Remove();
                            treeSavedToBusiness.Nodes["existingStakeholders"].Nodes.Add(temp);
                        }
                        else
                        {
                            ;
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void btnRemoveStake_Click(object sender, EventArgs e)
        {
            try
            {
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeSavedToBusiness.SelectedNode.Text.ToLower() != "stakeholders")
                    {
                        TreeNode temp = treeSavedToBusiness.SelectedNode;
                        int currentStake = int.Parse(treeSavedToBusiness.SelectedNode.Name.Split('_')[1]);
                        bool done = agent.operation.RemoveBusinessTypeStakeholder(currentBusiness, currentStake);
                        if (done)
                        {
                            treeSavedToBusiness.SelectedNode.Remove();
                            treeStakeHolder.Nodes["stakeholder"].Nodes.Add(temp);
                        }
                        else
                        {
                            ;
                        }
                    }
                }
            }
            catch { }
        }

        private void barButtonItemBusinessTypes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageBusinessType;
            RibbonShow("bus");
        }

        private void barButtonItemManageStaeholders_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new ManageStakeholder().ShowDialog();
        }

        private void txtBarEditUser_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.ApplicationUsers[] response = agent.operation.GetUsers(txtBarEditUser.EditValue.ToString());               
                gridUsers.DataSource = response;
                gridUsers.RefreshDataSource();

                //showing summary
                string searchString = txtBarEditUser.Text;
                if (searchString.Equals(""))
                {
                    label123.Text = "Showing " + response.Length.ToString() + " result(s) for users.";
                }
                else
                {
                    label123.Text = "Showing " + response.Length.ToString() + " result(s) for the search '" + searchString + "'";
                }

                if (response.Length == 0)
                {
                    ShowNoResults();
                }
            }
        }

        private void barButtonItemEdit_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            new AddUser().ShowDialog();
        }

        private void barManageUsers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageUsers;
            RibbonShow("user");
        }

        private void barAddUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new AddUser().ShowDialog();
        }

        private void barManageUserGroups_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new ManageUserGroupProperties().ShowDialog();
        }

        private void radioCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            object value = radioCategories.EditValue;
            category = value.ToString();
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                if (category == "dis")
                {
                    cmbParent.DataSource = null;
                    Globals.SetPickList(cmbParent, "isl");
                    lblParent.Text = "Island";
                }
                else
                {
                    cmbParent.DataSource = null;
                    Globals.SetPickList(cmbParent, "refParent");
                    lblParent.Text = "Parent";
                }

                senpa.ReferenceTable[] response = agent.operation.GetReferenceTableItems(category);
                gridReferences.DataSource = response;
                gridReferences.RefreshDataSource();
            }
        }

        private void gridViewReferences_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                try
                {
                    senpa.ReferenceTable response = agent.operation.GetReferenceTableItem(long.Parse(gridViewReferences.GetRowCellValue(gridViewReferences.FocusedRowHandle, gridViewReferences.Columns[0]).ToString()), category);
                    chkActive.Checked = response.Active;
                    txtRefName.Text = response.Name;
                    txtRefDescription.Text = response.Description;
                    Globals.SetPickListValue(cmbParent, response.FK_ParentId);
                }
                catch(Exception ex) {

                }
            }
        }

        private void btnRefSave_Click(object sender, EventArgs e)
        {
            
        }

        private void barManageReferences_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageReferences;
            RibbonShow("ref");
        }

        private void txtFindEmail_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Email[] response = agent.operation.GetEmails(txtFindEmail.EditValue.ToString());
                gridEmails.DataSource = response;
                gridEmails.RefreshDataSource();

                //showing summary
                string searchString = txtFindEmail.Text;
                if (searchString.Equals(""))
                {
                    label62.Text = "Showing " + response.Length.ToString() + " result(s) for emails.";
                }
                else
                {
                    label62.Text = "Showing " + response.Length.ToString() + " result(s) for the search '" + searchString + "'";
                }

                if (response.Length == 0)
                {
                    ShowNoResults();
                }
            }
        }

        private void barViewEmails_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            navigationFrame.SelectedPage = navPageEmail;
        }

        private void barViewSMSs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageSMS;
        }
        
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageViewRegistration;
        }
        
        private void cmdWorkFlow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkTerms.Checked == false)
            {
                //MessageBox.Show("Cannot proceed without accepting Terms");
                ShowMessageBox("Missing Information", "Please accept the terms and conditions to proceed", "error");
                return;
            }
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //check documents
                string response = agent.operation.CheckCurrentStageDocumentRequirements(long.Parse(lblId.Text), "registration");
                if (response.ToLower() == "none")
                {
                    //check recommendations
                    response = agent.operation.CheckCurrentStageRecommendationsRequirements(long.Parse(lblId.Text), "registration");
                    if (response.ToLower() == "none")
                    {
                        //check site
                        response = agent.operation.CheckCurrentStageSiteVisitRequirements(long.Parse(lblId.Text), "registration");
                        if (response.ToLower() == "none")
                        {
                            #region access
                            senpa.DocumentWorkflow wrkFlow = agent.operation.UpdateWorkFlowStage(long.Parse(lblId.Text), "registration");
                            uploadButtonsTopPosition = 82;
                            grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                            grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                            senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "registration");
                            lblStage.Text = currentStage.StageName;
                            RefreshRegIndicator();
                            if (currentStage.RequireDocuments)
                            {
                                cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                senpa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetDocumentsRequiredStatus(long.Parse(lblId.Text), "registration");
                                foreach (senpa.WorkFlowStageDocumentStatus doc in documents)
                                {
                                    DocumentButton(doc);
                                }
                            }
                            else
                            {
                                //  grpDocuments.Visible = false;
                            }

                            if (currentStage.RequirePayment)
                            {
                                cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }

                            if (currentStage.StageOptional)
                            {
                                cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }

                            if (currentStage.RequireSiteVisit)
                            {
                                cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }

                            if (currentStage.RequireRecommendations)
                            {
                                cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }


                            if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "registration") || currentStage.StageName == "Complete")
                            {
                                cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                            else
                            {
                                cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }

                            if (currentStage.StageName == "Complete")
                            {
                                //create business register record
                                long saveBusiness = agent.operation.RegisterBusiness(currentId);
                                if (saveBusiness > 0)
                                {
                                    currentBusinessId = saveBusiness;
                                    InitializeBusinessForm(currentBusinessId);
                                }

                            }
                            else
                            {

                            }


                            if (Globals.organisation != 0 && currentStage.RequireSiteVisit)
                            {
                                //check for site visit id
                                senpa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "registration");
                                if (site == null || site.CreatedBy == null)
                                {
                                    ;//ignore
                                }
                                else
                                {
                                    // grpSiteReports.Visible = true;
                                    senpa.SiteVisitReport[] documents = agent.operation.GetSiteVisitReports(site.Id);
                                    foreach (senpa.SiteVisitReport doc in documents)
                                    {
                                        if (doc.FK_StakeholderId == Globals.organisation)
                                        {
                                            DocumentButton(doc);
                                        }
                                    }
                                }
                            }
                            #endregion
                            //set assign list
                            //senpa.ApplicationUserSummary[] userList = agent.operation.GetAssigningUserList(long.Parse(lblId.Text), "registration");
                            //cmbAssign.DataSource = null;
                            //Globals.SetUserPickList(cmbAssign, userList);
                        }
                        else
                        {
                            //MessageBox.Show(response);
                            ShowMessageBox(response, response, "normal");
                        }
                    }
                    else
                    {
                        //MessageBox.Show(response);
                        ShowMessageBox(response, response, "normal");
                    }
                }
                else
                {
                    //MessageBox.Show(response);
                    ShowMessageBox(response, response, "normal");
                }

            }
        }

        private void txtFindSMS_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.SMS[] response = agent.operation.GetSMSs(txtFindSMS.EditValue.ToString());
                gridSMS.DataSource = response;
                gridSMS.RefreshDataSource();

                //showing summary
                string searchString = txtFindSMS.Text;
                if (searchString.Equals(""))
                {
                    label64.Text = "Showing " + response.Length.ToString() + " result(s) for SMSs.";
                }
                else
                {
                    label64.Text = "Showing " + response.Length.ToString() + " result(s) for the search '" + searchString + "'";
                }

                if (response.Length == 0)
                {
                    ShowNoResults();
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageScheduleSiteVisit;
            InitializeSiteVisit();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitializeRecommendations();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
        }

        private void popUpRegOpen_Click(object sender, EventArgs e)
        {
            try
            {
                currentId = long.Parse(gridRegistrations.GetRowCellValue(gridRegistrations.FocusedRowHandle, gridRegistrations.Columns[0]).ToString());

                InitializeRegForm(currentId);
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbNavBackReg_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //get the last item that was in the list then remove it

            try
            {
                NavigationPage chosenBackToPage = navStack[navStack.Count - 1];
                navStack.Remove(chosenBackToPage);

                backPressed = true;
                navigationFrame.SelectedPage = chosenBackToPage;
            }
            catch (Exception ex)
            {

            }

        }

        private void cmbWorkFlowSkip_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkTerms.Checked == false)
            {
                //MessageBox.Show("Cannot proceed without accepting Terms");
                ShowMessageBox("Missing Information", "Please accept the terms and conditions to proceed", "error");
                return;
            }
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {

                #region access
                bool skip = agent.operation.SkipOptionalStage(long.Parse(lblId.Text), "registration");
                if (skip)
                {
                    uploadButtonsTopPosition = 82;
                    grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                    grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                    senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "registration");
                    lblStage.Text = currentStage.StageName;
                    RefreshRegIndicator();
                    if (currentStage.RequireDocuments)
                    {
                        senpa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetDocumentsRequiredStatus(long.Parse(lblId.Text), "registration");
                        foreach (senpa.WorkFlowStageDocumentStatus doc in documents)
                        {
                            DocumentButton(doc);
                        }
                    }
                    else
                    {
                        //  grpDocuments.Visible = false;
                    }

                    if (currentStage.RequirePayment)
                    {
                        cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.StageOptional)
                    {
                        cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.RequireSiteVisit)
                    {
                        cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.RequireRecommendations)
                    {
                        cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }


                    if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "registration") || currentStage.StageName == "Complete")
                    {
                        cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else
                    {
                        cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }

                    if (currentStage.StageName == "Complete")
                    {
                        //create business register record
                        long saveBusiness = agent.operation.RegisterBusiness(currentId);
                        if (saveBusiness > 0)
                        {
                            currentBusinessId = saveBusiness;
                            navigationFrame.SelectedPage = navPageBusiness;
                            InitializeBusinessForm(currentBusinessId);
                        }

                    }
                    else
                    {

                    }

                    
                    #endregion
                    //set assign list
                    //senpa.ApplicationUserSummary[] userList = agent.operation.GetAssigningUserList(long.Parse(lblId.Text), "registration");
                    //cmbAssign.DataSource = null;
                    //Globals.SetUserPickList(cmbAssign, userList);

                }
                else
                {
                    ShowMessageBox("Skip Optiona Stage", "Failed to skip optional stage", "normal");
                }
            }
        }

        private void cmdWorkFlowBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkTerms.Checked == false)
            {
                //MessageBox.Show("Cannot proceed without accepting Terms");
                ShowMessageBox("Missing Information", "Please accept the terms and conditions to proceed", "error");
                return;
            }
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {

                #region access
                bool skip = agent.operation.ReverseStage(long.Parse(lblId.Text), "registration");
                if (skip)
                {
                    uploadButtonsTopPosition = 82;
                    grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                    grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                    senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "registration");
                    lblStage.Text = currentStage.StageName;
                    RefreshRegIndicator();
                    if (currentStage.RequireDocuments)
                    {
                        senpa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetDocumentsRequiredStatus(long.Parse(lblId.Text), "registration");
                        foreach (senpa.WorkFlowStageDocumentStatus doc in documents)
                        {
                            DocumentButton(doc);
                        }
                    }
                    else
                    {
                        //  grpDocuments.Visible = false;
                    }

                    if (currentStage.RequirePayment)
                    {
                        cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.StageOptional)
                    {
                        cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmbWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.RequireSiteVisit)
                    {
                        cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.RequireRecommendations)
                    {
                        cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }


                    if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "registration") || currentStage.StageName == "Complete")
                    {
                        cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else
                    {
                        cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }

                    if (currentStage.StageName == "Complete")
                    {
                        //create business register record
                        long saveBusiness = agent.operation.RegisterBusiness(currentId);
                        if (saveBusiness > 0)
                        {
                            currentBusinessId = saveBusiness;
                            navigationFrame.SelectedPage = navPageBusiness;
                            InitializeBusinessForm(currentBusinessId);
                        }

                    }
                    else
                    {

                    }


                    if (Globals.organisation != 0 && currentStage.RequireSiteVisit)
                    {
                        //check for site visit id
                        senpa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "registration");
                        if (site == null || site.CreatedBy == null)
                        {
                            ;//ignore
                        }
                        else
                        {
                            // grpSiteReports.Visible = true;
                            senpa.SiteVisitReport[] documents = agent.operation.GetSiteVisitReports(site.Id);
                            foreach (senpa.SiteVisitReport doc in documents)
                            {
                                if (doc.FK_StakeholderId == Globals.organisation)
                                {
                                    DocumentButton(doc);
                                }
                            }
                        }
                    }
                    #endregion
                    //set assign list
                    //senpa.ApplicationUserSummary[] userList = agent.operation.GetAssigningUserList(long.Parse(lblId.Text), "registration");
                    //cmbAssign.DataSource = null;
                    //Globals.SetUserPickList(cmbAssign, userList);

                }
                else
                {
                    ShowMessageBox("Skip Optional Stage", "Failed to skip optional stage", "normal");
                }
            }
        }

        private void popUpInvoiceOpen_Click(object sender, EventArgs e)
        {
            try
            {
                currentInvoiceId = long.Parse(gridInvoices.GetRowCellValue(gridInvoices.FocusedRowHandle, gridInvoices.Columns[0]).ToString());

                new ProcessInvoice().ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbBusIsland_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.SetPickList(cmbBusDistrict, "dis", Globals.GetComboBoxValue(cmbBusIsland));
            }catch(Exception ex)
            {

            }
            
        }        

        private void rcmbBusIsland_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.SetPickList(rcmbBusDistrict, "dis", Globals.GetComboBoxValue(rcmbBusIsland));
            }
            catch (Exception ex)
            {

            }
            
        }

        private void rcmbResIsland_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.SetPickList(rcmbResDistrict, "dis", Globals.GetComboBoxValue(rcmbResIsland));
            }
            catch (Exception ex)
            {

            }

            
        }

        private void cmbRecoSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //if status changed to complete check for stakeholder report first
                string status = cmbStatusReco.SelectedValue.ToString();
                if (status == "Complete")
                {
                    string response = agent.operation.CheckCurrentStageRecommandationSiteVisitRequirements(long.Parse(lblId.Text), "registration", long.Parse(rlblRecoId.Text), int.Parse(lblsthId.Text));
                    if (response.ToLower() == "none")
                    {
                        ;//continue
                    }
                    else
                    {
                        //MessageBox.Show(response);
                        ShowMessageBox(response, response, "normal");
                        return;
                    }
                }

                long done = agent.operation.SaveRecommendedAction(long.Parse(rlblRecoId.Text), int.Parse(lblsthId.Text), Globals.GetComboBoxValue(cmbAction), txtDetails.Text, chkReminderReco.Checked, cmbStatusReco.SelectedValue.ToString(), txtReasonReco.Text, chkActiveReco.Checked);
                if (done > 0)
                {
                    lstRecoStake.Items.Clear();
                    senpa.RecommendedAction[] reports = agent.operation.GetRecommendedActions(long.Parse(rlblRecoId.Text));
                    foreach (senpa.RecommendedAction rep in reports)
                    {
                        string[] row = { rep.FK_StakeholderId.ToString(), agent.operation.GetEntityName(rep.FK_StakeholderId, "stahol"), agent.operation.GetEntityName(rep.FK_ActionId, "act"), "", rep.Status };
                        var listViewItem = new ListViewItem(row);
                        lstRecoStake.Items.Add(listViewItem);
                    }
                }
            }
        }

        private void cmdRecoSite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageScheduleSiteVisit;
            InitializeRecommendationsSiteVisit();
        }

        private void btnRenewRecommendations_Click(object sender, EventArgs e)
        {

        }

        private void btnRenewManageSiteVisit_Click(object sender, EventArgs e)
        {

        }

        private void cmdRenWorkFlow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //check documents
                string response = agent.operation.CheckCurrentStageDocumentRequirements(currentRenewalId, "renewal");
                if (response.ToLower() == "none")
                {
                    //check recommendations
                    response = agent.operation.CheckCurrentStageRecommendationsRequirements(currentRenewalId, "renewal");
                    if (response.ToLower() == "none")
                    {
                        //check site
                        response = agent.operation.CheckCurrentStageSiteVisitRequirements(currentRenewalId, "renewal");
                        if (response.ToLower() == "none")
                        {
                            #region access
                            senpa.DocumentWorkflow wrkFlow = agent.operation.UpdateWorkFlowStage(currentRenewalId, "renewal");
                            uploadButtonsTopPosition = 82;
                            grpRenewalDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                            grpRenewalDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                            grpRenewalExisting.Controls.OfType<LinkLabel>().ToList().ForEach(btn => btn.Dispose());
                            grpRenewalExisting.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                            senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(currentRenewalId, "renewal");
                            lblRenewStage.Text = currentStage.StageName;
                            RefreshRenewalIndicator();
                            if (currentStage.RequireDocuments)
                            {
                                cmdRenWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                senpa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetDocumentsRequiredStatus(currentRenewalId, "renewal");
                                foreach (senpa.WorkFlowStageDocumentStatus doc in documents)
                                {
                                    DocumentButton(doc, "renewal");
                                }
                            }
                            else
                            {
                                //  grpDocuments.Visible = false;
                            }

                            if (currentStage.RequirePayment)
                            {
                                cmbRenWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                cmdRenWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                cmdRenWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmdRenWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }

                            if (currentStage.StageOptional)
                            {
                                cmbRenWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmbRenWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }

                            if (currentStage.RequireSiteVisit)
                            {
                                cmdRenNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                cmdRenNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmdRenNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                cmdRenNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }

                            if (currentStage.RequireRecommendations)
                            {
                                cmdRenNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            else
                            {
                                cmdRenNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }


                            if (!agent.operation.CheckAccessToStage(currentRenewalId, "renewal") || currentStage.StageName == "Complete")
                            {
                                cmdRenWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                            else
                            {
                                cmdRenWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }

                            if (currentStage.StageName == "Complete")
                            {
                                senpa.RenewalRequest ren = agent.operation.GetRenewalRequest(currentRenewalId);
                                senpa.BusinessRegistration reg = agent.operation.GetBusinessRegistration(ren.FK_BusinessRegistrationId, false);

                                //set renew status
                                bool renew = agent.operation.SetRenewal(reg.FK_RegistrationRequestId);
                                if (renew)
                                {
                                    ShowMessageBox("Renew Complete", "Renew process Completed!!", "normal");
                                     currentBusinessId = reg.Id;
                                    InitializeBusinessForm(currentBusinessId);
                                }
                                else
                                {
                                    ShowMessageBox("Renew Complete", "Renew process did not complete!!", "error");
                                }
                            }
                            else
                            {

                            }

                            //get business entity
                            senpa.RenewalRequest ren1 = agent.operation.GetRenewalRequest(currentRenewalId);
                            senpa.BusinessRegistration business = agent.operation.GetBusinessRegistration(ren1.FK_BusinessRegistrationId, false);
                            currentBusinessRegistrationId = business.FK_RegistrationRequestId;
                            lblRenewName.Text = business.FirstNames + " " + business.LastName + " (" + business.RegistrationNumber + ") " + business.BusinessName;

                            senpa.AutoDocument[] autoDoc = agent.operation.CheckAutoDocument(business.FK_RegistrationRequestId, "renewal");
                            foreach (senpa.AutoDocument doc in autoDoc)
                            {
                                DocumentLink(doc, "renewal");
                            }

                            senpa.WorkFlowStageDocumentStatus[] alldocuments = agent.operation.GetAllRequiredDocuments(business.FK_RegistrationRequestId, "registration");
                            foreach (senpa.WorkFlowStageDocumentStatus doc in alldocuments)
                            {
                                DocumentLink(doc, "business", "renewal");
                            }
                           
                            #endregion
                            //set assign list
                            senpa.ApplicationUserSummary[] userList = agent.operation.GetAssigningUserList(currentRenewalId, "renewal");
                            cmbRenewAssign.DataSource = null;
                            Globals.SetUserPickList(cmbRenewAssign, userList);
                        }
                        else
                        {
                            //MessageBox.Show(response);
                            ShowMessageBox(response, response, "normal");
                        }
                    }
                    else
                    {
                        //MessageBox.Show(response);
                        ShowMessageBox(response, response, "normal");
                    }
                }
                else
                {
                    //MessageBox.Show(response);
                    ShowMessageBox(response, response, "normal");
                }

            }
        }

        private void cmbSaveRen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.RenewalRequest newRegistration = new senpa.RenewalRequest();
                newRegistration.Id = currentRenewalId;
                newRegistration.ReferenceNumber = lblRenewReference.Text;
                newRegistration.BusinessAdvantages = txtRenewAdv.Text;
                newRegistration.BusinessDescription = txtRenewbusDesc.Text;

                newRegistration.BusinessDisadvantages = txtRenewDisadv.Text;
                newRegistration.CertificateRenewalDate = DateTime.Now;
                newRegistration.DocumentType = "renewal";
                newRegistration.Equipment = txtRenewEquip.Text;
                newRegistration.FK_BusinessDevelopmentOfficerId = Globals.GetComboBoxValue(cmbRenewBDO);
                newRegistration.FK_BusinessRegistrationId = currentBusinessId;
                newRegistration.Funding = txtRenewFunding.Text;
                newRegistration.GoalsAndObjectives = txtRenewGoals.Text;
                newRegistration.MarketingPlan = txtRenewMktPlan.Text;
                newRegistration.OperationsForecast = txtRenewOps.Text;
                newRegistration.RawMaterials = txtRenewRaw.Text;
                newRegistration.RequireWorkFlow = true;
                newRegistration.SalesTarget = txtRenewSales.Text;
                newRegistration.WorkFlowId = 0;
                newRegistration.WorkFlowStatus = "New";
                newRegistration.Status = "";
                newRegistration.StatusReason = "";
                newRegistration.WorkingPremises = txtRenewWrkPrem.Text;
                newRegistration.CreatedBy = Globals.userLogged;
                newRegistration.LastModified = DateTime.Now;
                newRegistration.LastModifiedBy = Globals.userLogged;
                newRegistration.SubmissionType = "O";

                long response = agent.operation.SaveRenewalRequest(newRegistration);
                if (response > 0)
                {
                    currentRenewalId = response;
                    uploadButtonsTopPosition = 82;
                    #region access
                    grpRenewalDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                    grpRenewalDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                    senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(currentRenewalId, "renewal");
                    lblRenewStage.Text = currentStage.StageName;
                    if (currentStage.RequireDocuments)
                    {
                        senpa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetDocumentsRequiredStatus(currentRenewalId, "renewal");
                        foreach (senpa.WorkFlowStageDocumentStatus doc in documents)
                        {
                            DocumentButton(doc, "renewal");
                        }
                    }
                    else
                    {
                        //  grpDocuments.Visible = false;
                    }

                    if (currentStage.RequirePayment)
                    {
                        cmbRenWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRenWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRenWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdRenWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.StageOptional)
                    {
                        cmbRenWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmbRenWorkFlowSkip.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.RequireSiteVisit)
                    {
                        cmdRenNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdRenNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdRenNavNewVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRenNavVisitReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    if (currentStage.RequireRecommendations)
                    {
                        cmdRenNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdRenNavRecommendations.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }


                    if (!agent.operation.CheckAccessToStage(currentRenewalId, "renewal") || currentStage.StageName == "Complete")
                    {
                        cmdRenWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else
                    {
                        cmdRenWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    #endregion

                    //set assign list
                    senpa.ApplicationUserSummary[] userList = agent.operation.GetAssigningUserList(currentRenewalId, "renewal");
                    cmbRenewAssign.DataSource = null;
                    Globals.SetUserPickList(cmbRenewAssign, userList);
                }
                else
                {
                    //MessageBox.Show("Error Saving");
                    ShowMessageBox("An Error Occured", "Error Saving", "error");
                }
            }
        }

        private void cmbNavBackRen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //get the last item that was in the list then remove it

            try
            {
                NavigationPage chosenBackToPage = navStack[navStack.Count - 1];
                navStack.Remove(chosenBackToPage);

                backPressed = true;
                navigationFrame.SelectedPage = chosenBackToPage;
            }
            catch (Exception ex)
            {

            }

        }

        private void mainRibbon_Click(object sender, EventArgs e)
        {

        }

        private void cmdSaveWorkflow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                long wrk = agent.operation.CreateWorkFlow(txtName.Text, txtDescription.Text, Globals.GetComboBoxValue(cmbStart), Globals.GetComboBoxValue(cmbEnd));
                if (wrk > 0)
                {
                    string currentFlow = "_" + wrk.ToString();
                    treeWorkFlow.Nodes["workFlows"].Nodes.Add(currentFlow, txtName.Text);
                    lstStages.Items.Clear();
                    currentWorkFlow = wrk;
                }
            }
        }

        private void cmdManageStages_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new ManageStages().ShowDialog();
        }

        private void cmdSaveReferences_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                bool wrk = agent.operation.SaveReferenceTable(txtRefName.Text, txtRefDescription.Text, chkActive.Checked, category, Globals.GetComboBoxValue(cmbParent));
                if (wrk)
                {
                    senpa.ReferenceTable[] response = agent.operation.GetReferenceTableItems(category);
                    gridReferences.DataSource = response;
                    gridReferences.RefreshDataSource();
                }
            }
        }

        private void cmdEditUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentUsername = (gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, gridViewUsers.Columns[0]).ToString());
            }
            catch (Exception ex)
            {

            }
            new ManagerUserProperties().ShowDialog();
        }

        private void popUpEditUser_Click(object sender, EventArgs e)
        {
            try
            {
                currentUsername = (gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, gridViewUsers.Columns[0]).ToString());
            }
            catch (Exception ex)
            {

            }
            new ManagerUserProperties().ShowDialog();
        }

        private void cmdOpenBusiness_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentBusinessId = long.Parse(gridRegisteredBusness.GetRowCellValue(gridRegisteredBusness.FocusedRowHandle, gridRegisteredBusness.Columns[0]).ToString());

                InitializeBusinessForm(currentBusinessId);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ShowMessageBox("An Error Occured", ex.Message, "error");
            }
        }

        private void cmdRenewBusiness_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentBusinessId = long.Parse(gridRegisteredBusness.GetRowCellValue(gridRegisteredBusness.FocusedRowHandle, gridRegisteredBusness.Columns[0]).ToString());

                InitializeRenewalForm(currentBusinessId);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ShowMessageBox("An Error Occured", ex.Message, "error");
            }
        }

        private void navPageRegisteredBusiness_Enter(object sender, EventArgs e)
        {
            RibbonShow("regbus");
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {

        }

        private void cmdViewCertificate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.BusinessRegistration reg = agent.operation.GetBusinessRegistration(currentBusinessId, false);
                Byte[] doc = agent.operation.GetAutoDocument("certificate", reg.FK_RegistrationRequestId);
                string filePath = Application.StartupPath + "\\filer\\" + "Certificate"+currentBusinessId.ToString() + ".pdf";
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(doc, 0, doc.Length);
                fs.Flush();
                fs.Close();
                System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                newProcess.Start();
                newProcess.WaitForExit();
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch
                {

                }
            }
        }

        private void cmdIssueCertificate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageApproveCertificate;
            cmdRefreshCert.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            cmdIssueCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            existingDocumentsPosition = 82;
            grpExistingApprove.Controls.OfType<LinkLabel>().ToList().ForEach(btn => btn.Dispose());
            grpExistingApprove.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.BusinessRegistration reg = agent.operation.GetBusinessRegistration(currentBusinessId, false);
                currentCertificateRegistrationId = reg.FK_RegistrationRequestId;
               

                int cert = agent.operation.CheckCertificate(currentCertificateRegistrationId);
                if (cert == 0 || cert == 1)
                {
                    //get renew Id and cert details                   
                    senpa.CottageCertificate certificate = agent.operation.GetCertificateDetails(currentCertificateRegistrationId);
                    currentCertificateId = certificate.Id;
                    lblCertificate.Text = certificate.CertificateNumber;
                    lblExpiry.Text = certificate.NextRenewalDate.ToShortDateString();
                    lblIssued.Text = certificate.CertificateIssueDate.ToShortDateString();
                    dtpNextRenewalDate.DateTime = certificate.NextRenewalDate;
                    dtpRenewalDate.DateTime = certificate.LastRenewalDate;
                    if (cert == 0)
                        lblBanner.Text = "Issue Certificate";
                    else
                    {
                        lblBanner.Text = "Renew Certificate";
                        long x = agent.operation.LastRenewalId(currentBusinessId);
                        if (x == 0)
                        {
                            ShowMessageBox("Renew Certificate", "Cannot Renew Certificate", "error");
                        }
                        else
                            certificateRenewalId = x;
                    }

                    senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(currentCertificateId, "certificate");
                    //check access
                    if (!agent.operation.CheckAccessToStage(currentCertificateId, "certificate"))
                    {
                        cmdSaveBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdSaveApproval.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdNotApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else
                    {                       
                        cmdSaveBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdSaveApproval.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdNotApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                   }
                    
                    RefreshApprovalIndicator();
                   
                    // cmdRefreshCert.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    senpa.WorkFlowStageDocumentStatus[] alldocuments = agent.operation.GetAllRequiredDocuments(currentCertificateRegistrationId, "registration");
                    foreach (senpa.WorkFlowStageDocumentStatus doc in alldocuments)
                    {
                        DocumentLink(doc, "business", "certificate");
                    }
                }
                else
                {
                    ShowMessageBox("Renew Certificate", "Certificate renewal forbidden", "error");
                }
            }
        }

        private void cmdRefreshCert_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.BusinessRegistration registration = agent.operation.GetBusinessRegistration(currentBusinessId, false);
                //check certificate status
                int cert = agent.operation.CheckCertificate(registration.FK_RegistrationRequestId);
                if (cert == -2)//not registered
                {
                    ribbonBtnsCertificate.Visible = false;
                    cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else if (cert == -1)//expired
                {
                    ribbonBtnsCertificate.Visible = false;
                    cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else if (cert == 0)//pending certificate issuing
                {
                    ribbonBtnsCertificate.Visible = true;
                    cmdIssueCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdViewCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else if (cert == 1)//expiring in comming period
                {
                    ribbonBtnsCertificate.Visible = false;
                    cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else if (cert == 2)//safe
                {
                    ribbonBtnsCertificate.Visible = true;
                    cmdIssueCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdViewCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdRenewBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdNotApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                cmdRefreshCert.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        private void cmdWorkFlowRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitializeRegForm(currentId);
        }

        private void cmbResIsland_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.SetPickList(cmbResDistrict, "dis", Globals.GetComboBoxValue(cmbResIsland));
            }catch(Exception ex)
            {

            }
            
        }

        private void cmdDocumentDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageDesignDocuments;
            RibbonShow("docdes");
        }

        private void treeDesignDocs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                if (treeDesignDocs.SelectedNode.Text.ToLower() != "designs")
                {
                    currentDocumentDesign = (treeDesignDocs.SelectedNode.Name.Split('_')[1]);
                    senpa.AutoDocumentsDesign desg = agent.operation.GetAutoDocumentsDesign(currentDocumentDesign);

                    txtDocDesignBody.Text = desg.DocumentDesign;
                    txtDocDesignSMS.Text = desg.DocumentDesignSMS;
                    txtDocDesignSubject.Text = desg.EmailSubject;
                    chkDocEmail.Checked = desg.Email;
                    chkDocSMS.Checked = desg.SMS;
                }
            }
        }

        private void cmdSaveDocDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                bool save = agent.operation.SaveAutoDocumentsDesign(currentDocumentDesign, txtDocDesignSMS.Text, txtDocDesignBody.Text, txtDocDesignSubject.Text, chkDocSMS.Checked, chkDocEmail.Checked);
                if (save)
                {
                    System.IO.File.WriteAllText(Application.StartupPath + "\\html\\design.html", txtDocDesignBody.Text);
                    webViewer.Url = new Uri(Application.StartupPath + "\\html\\design.html");
                    webViewer.Refresh();
                }                        
            }
        }

        private void cmdRegistrationRules_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new ValidationRules().Show(this);
        }

        private void cmdChargeRules_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new ChargeRules().Show(this);
        }

        private void cmdNavVisitReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //check for site visit id
                senpa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "registration");
                if (site == null || site.CreatedBy == null)
                {
                    ;
                }
                else
                {
                    if (site.Confirmed)
                    {
                        navigationFrame.SelectedPage = navPageSiteVisitReport;
                        InitializeSiteVisitReport();
                    }
                }
            }
        }

        private void cmdSiteSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.SiteVisit site = new senpa.SiteVisit();
                site.Id = long.Parse(lblSiteId.Text);
                site.HasLoan = ((cmbHasLoan.SelectedIndex == 0) ? false : true);
                site.FK_FinanceInstituteId = Globals.GetComboBoxValue(cmbLoanInstitute);
                site.LoanAmount = float.Parse(txtLoanAmount.Text);
                site.LoanPurpose = txtLoanPurpose.Text;
                site.Background = txtSvBackground.Text;
                site.BusinessDescription = txtSvBusinessDesc.Text;
                site.Premises = txtSvPremises.Text;
                site.Equipment = txtSvEquipment.Text;
                site.Marketing = txtSvMarketing.Text;
                site.Manpower = txtSvManpower.Text;
                site.Training = txtSvTraining.Text;
                site.Conclusion = txtSvConclusion.Text;
                site.FK_BDO = Globals.GetComboBoxValue(cmbSVRBDO);
                site.FK_BDM = Globals.GetComboBoxValue(cmbSVRBDM);
                site.FK_HR = Globals.GetComboBoxValue(cmbSVRHR);                

                bool response = agent.operation.SaveSiteVisit(site);
                if (response)
                {
                    ShowMessageBox("Saved", "Record updated", "normal");
                }
                else
                {
                    //MessageBox.Show("Error Saving");
                    ShowMessageBox("An Error Occured", "An unknown error occured while saving", "error");

                }
            }
        }

        private void cmdSiteScheduleSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.SiteVisit site = new senpa.SiteVisit();
                site.Id = long.Parse(lblSiteId.Text);
                site.FK_BDO = Globals.GetComboBoxValue(cmbSVBDO);
                site.FK_BDM = Globals.GetComboBoxValue(cmbSVBDM);
                site.FK_HR = Globals.GetComboBoxValue(cmbSVHR);
                site.VisitDate = dtpSVDate.DateTime;
                site.VisitAddress = txtSVAddress.Text;
               // site.Confirmed = chkSVConfirmed.Checked;
                site.Phone = chkSVPhone.Checked;
                site.SMS = chkSVSMS.Checked;
                site.Email = chkSVEmail.Checked;
                

                bool response = agent.operation.ScheduleSiteVisit(site);
                if (response)
                {
                    ShowMessageBox("Saved", "Record updated", "normal");
                    if (site.Confirmed)
                    {
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                }
                else
                {
                    //MessageBox.Show("Error Saving");
                    ShowMessageBox("An Error Occured", "An unknown error occured while saving", "error");

                }
            }
        }

        private void cmbNavBackSite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //get the last item that was in the list then remove it

            try
            {
                NavigationPage chosenBackToPage = navStack[navStack.Count - 1];
                navStack.Remove(chosenBackToPage);

                backPressed = true;
                navigationFrame.SelectedPage = chosenBackToPage;
            }
            catch (Exception ex)
            {

            }
        }

        private void cmdSaveApproval_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                #region access
                bool done = agent.operation.UpdatePeriod(currentCertificateRegistrationId, dtpRenewalDate.DateTime, dtpNextRenewalDate.DateTime);
                if (done)
                    ShowMessageBox("Saved Period","Validity period set","normal");
                #endregion
            }
        }

        private void txtFindTraining_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.TrainingSessionReport[] response = agent.operation.GetTrainingSessions(txtFindTraining.EditValue.ToString());
                gridTraining.DataSource = response;
                gridTraining.RefreshDataSource();

                //showing summary
                string searchString = txtFindTraining.Text;
                if (searchString.Equals(""))
                {
                    label121.Text = "Showing " + response.Length.ToString() + " result(s) for sessions.";
                }
                else
                {
                    label121.Text = "Showing " + response.Length.ToString() + " result(s) for the search '" + searchString + "'";
                }

                if (response.Length == 0)
                {
                    ShowNoResults();
                }
            }
        }

        private void cmdEditTraining_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentTrainingId = long.Parse(gridViewTraining.GetRowCellValue(gridViewTraining.FocusedRowHandle, gridViewTraining.Columns[0]).ToString());
                InitializeTrainingForm();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ShowMessageBox("An Error Occured", ex.Message, "error");
            }
        }

        private void addToTrainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeForTraining.SelectedNode.Text.ToLower() != "business")
                    {
                        long currentForTrainingBusiness = long.Parse(treeForTraining.SelectedNode.Name.Split('_')[1]);

                        long done = agent.operation.SaveTrainingRegistration(currentTrainingId, currentForTrainingBusiness);
                        if (done > 0)
                            treeForTraining.SelectedNode.Remove();
                        else
                            ShowMessageBox("Adding Client to Training", "Failed to add client", "error");
                    }
                }
            }
            catch { }
        }

        private void buttonEdit1_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.BusinessRegistration[] response = agent.operation.GetBusinessRegistrations(txtFindForTraining.EditValue.ToString());
                treeForTraining.Nodes["client"].Nodes.Clear();
               
                foreach (senpa.BusinessRegistration reg in response)
                {
                    string currentreg = "_" + reg.FK_RegistrationRequestId.ToString();
                    treeForTraining.Nodes["client"].Nodes.Add(currentreg, reg.FirstNames + " "+reg.LastName + " ("+ reg.BusinessName + ")");
                    treeForTraining.Nodes["client"].Nodes[currentreg].Nodes.Add(reg.RegistrationNumber+"("+reg.NIN+")");
                }
            }  
        }

        private void refreshTraining_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.TrainingRegistrationReport[] current = agent.operation.GetTrainingRegistrationReport(currentTrainingId);
                gridRegisteredTraining.DataSource = current;
                gridRegisteredTraining.RefreshDataSource();
            }
        }

        private void navBarAddTraining_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            InitializeNewTrainingForm();
        }

        private void navBarFindTrainings_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageAfterCare;
        }

        private void cmdSaveTraining_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if((dtpTrainingStart.Value > dtpTrainingEnd.Value) || dtpTrainingStart.Value<DateTime.Now)
            {
                ShowMessageBox("Date Error", "Invalid start to end dates", "error");
                return;
            }

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.TrainingSession training = new senpa.TrainingSession();
                training.Id = currentTrainingId;
                training.FK_TrainingCourseId = Globals.GetComboBoxValue(cmbTrainingCourse);
                training.FK_TrainingCategoryId = Globals.GetComboBoxValue(cmbTrainingCategory);
                training.FK_TrainingTypeId = Globals.GetComboBoxValue(cmbTrainingType);
                training.StartDate = dtpTrainingStart.Value;
                training.EndDate = dtpTrainingEnd.Value;
                training.LocationAddress = txtTrainingVenue.Text;
                training.TrainingBy = cmbTrainingTrainer.SelectedText;
                training.Status = cmbTrainingStatus.SelectedValue.ToString();
                training.StatusReason = "";

                long Id = agent.operation.SaveTrainingSession(training);
                if(Id>0 && currentTrainingId==0)
                {
                    currentTrainingId = Id;
                    InitializeTrainingForm();
                }
                else if (Id > 0 && currentTrainingId != 0)
                {
                    ;
                }
                else
                {
                    ShowMessageBox("Saving Training", "Failed to save record", "error");
                }

                if (training.Status == "Closed" || training.Status == "Complete")
                {
                    cmdSaveTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdCloseTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (training.Status == "Complete")
                {
                    cmdCloseTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }
        }

        private void cmdTrainingAttendance_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentTrainingId = long.Parse(gridViewTraining.GetRowCellValue(gridViewTraining.FocusedRowHandle, gridViewTraining.Columns[0]).ToString());
                InitializeAttendanceTrainingForm();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ShowMessageBox("An Error Occured", ex.Message, "error");
            }
        }

        private void addToAttendedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!currentTrainingClosed)
            {
                SenpaApi agent = new SenpaApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeTrainingAttend.SelectedNode.Text.ToLower() != "business")
                    {
                        long currentForTrainingBusiness = long.Parse(treeTrainingAttend.SelectedNode.Name.Split('_')[1]);

                        bool done = agent.operation.UpdateAttendance(currentTrainingId, currentForTrainingBusiness, true);
                        if (done)
                            treeTrainingAttend.SelectedNode.Remove();
                        else
                            ShowMessageBox("Marking Clientas attended", "Failed to add client", "error");
                    }
                }
            }
            else
            {
                ShowMessageBox("Adding to Attendance","Cannot add to closed Session","error");
            }
        }

        private void refreshAttended_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {             

                senpa.TrainingRegistrationReport[] response = agent.operation.GetTrainingRegistrationReport(currentTrainingId);
                treeTrainingAttend.Nodes["client"].Nodes.Clear();

                foreach (senpa.TrainingRegistrationReport reg in response)
                {
                    string currentreg = "_" + reg.BusinessRegistrationId.ToString();
                    treeTrainingAttend.Nodes["client"].Nodes.Add(currentreg, reg.FirstNames + " " + reg.LastName + " (" + reg.BusinessName + ")");
                    treeTrainingAttend.Nodes["client"].Nodes[currentreg].Nodes.Add(reg.Island + "(" + reg.NIN + ")");
                }

                senpa.TrainingRegistrationReport[] current = agent.operation.GetAttendedTrainingRegistrationReport(currentTrainingId);
                gridAttendedTraining.DataSource = current;
                gridAttendedTraining.RefreshDataSource();

                foreach (senpa.TrainingRegistrationReport reg in current)
                {
                    treeTrainingAttend.Nodes["client"].Nodes["_" + reg.BusinessRegistrationId.ToString()].Remove();
                }
            }
        }

        private void cmdCloseTraining_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {

                bool closed = agent.operation.CloseTraining(currentTrainingId);
                if(closed)
                {
                    cmdCloseTraining.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else
                {
                    ShowMessageBox("Sending Certificates","Error sending","error");
                }
            }
        }

        private void navPageAfterCare_Enter(object sender, EventArgs e)
        {
          
        }

        private void cmdOpenRegistration_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentId = long.Parse(gridRegistrations.GetRowCellValue(gridRegistrations.FocusedRowHandle, gridRegistrations.Columns[0]).ToString());

                InitializeRegForm(currentId);
            }
            catch (Exception ex)
            {

            }
        }

        private void cmdOpenInvoice_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentInvoiceId = long.Parse(gridInvoices.GetRowCellValue(gridInvoices.FocusedRowHandle, gridInvoices.Columns[0]).ToString());

                new ProcessInvoice().ShowDialog();
            }
            catch (Exception ex)
            {

            }
        }

        private void cmdFolderBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (currentFolderId < 4)//reserved folder ids
                return;
            pnlExplorer.Controls.Clear();
           
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                currentFolderId = agent.operation.GetParentFolderId(currentFolderId);
                lblFolderMap.Text = agent.operation.GetFolderPath(currentFolderId).Replace(",", " > ");

                senpa.DocumentFolders[] response = agent.operation.GetFolders(currentFolderId);
                for (int x = 0; x < response.Length; x++)
                {
                    pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                }
                //get files
                senpa.DocumentLibrary[] documents = agent.operation.GetFolderDocuments(currentFolderId);
                foreach (senpa.DocumentLibrary doc in documents)
                {
                    pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                }
            }
        }

        private void cmdNotifySiteVisit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                
                bool response = agent.operation.NotifySiteVisit(long.Parse(lblSiteId.Text));
                if (response)
                {
                    ShowMessageBox("Notification", "Notification sent successfully to all Stakeholders", "normal");
                }
                else
                {
                    //MessageBox.Show("Error Saving");
                    ShowMessageBox("An Error Occured", "An unknown error occured while saving", "error");

                }
            }
        }

        private void btnWrkDel_Click(object sender, EventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                bool delete = agent.operation.DeleteStage(currentWorkFlow, long.Parse(lstStages.SelectedItems[0].SubItems[0].Text));
                if(delete)
                {
                    lstStages.Items.Clear();
                    senpa.WorkFlowStages[] response = agent.operation.GetWorkFlowStages(long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]));
                    foreach (senpa.WorkFlowStages wrkFlow in response)
                    {
                        string[] row = { wrkFlow.Id.ToString(), wrkFlow.StagePosition.ToString(), wrkFlow.StageName, wrkFlow.StageDescription, agent.operation.GetEntityName(wrkFlow.FK_RoleGroupId, "rolgro"), ((wrkFlow.StageAssignMode == 1) ? "Yes" : "No"), ((wrkFlow.StageOptional) ? "Yes" : "No"), ((wrkFlow.RequireDocuments) ? "Yes" : "No"), ((wrkFlow.RequirePayment) ? "Yes" : "No"), ((wrkFlow.RequireSiteVisit) ? "Yes" : "No"), ((wrkFlow.RequireRecommendations) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstStages.Items.Add(listViewItem);
                    }
                }
            }
        }

        private void cmbNavBackReco_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //get the last item that was in the list then remove it
            try
            {
                NavigationPage chosenBackToPage = navStack[navStack.Count - 1];
                navStack.Remove(chosenBackToPage);

                backPressed = true;
                navigationFrame.SelectedPage = chosenBackToPage;
            }
            catch (Exception ex)
            {

            }
        }

        private void cmbNavBackBus_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //get the last item that was in the list then remove it

            try
            {
                NavigationPage chosenBackToPage = navStack[navStack.Count - 1];
                navStack.Remove(chosenBackToPage);

                backPressed = true;
                navigationFrame.SelectedPage = chosenBackToPage;
            }
            catch (Exception ex)
            {

            }
        }

        private void gridRegistrations_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                 string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"]);
                if (category == "Complete")
                {
                e.Appearance.BackColor = Color.White;
                e.Appearance.BackColor2 = Color.SpringGreen;
                }
                else if (category == "Saved" || category == "Submitted")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.SlateGray;
                }
                else if (category == "Payment")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.LightSteelBlue;
                }
                else if (category == "Site Visit")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.Lavender;
                }
            }
        }

        private void cmdOpenRenewal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentBusinessId = long.Parse(gridViewRenewals.GetRowCellValue(gridViewRenewals.FocusedRowHandle, gridViewRenewals.Columns[0]).ToString());

                InitializeRenewalForm(currentBusinessId);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ShowMessageBox("An Error Occured", ex.Message, "error");
            }
        }

        private void btnSearchRenewal_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.ActiveRenewalRequest[] response = agent.operation.GetActiveBusinessRenewals(btnSearchRenewal.EditValue.ToString());

                gridRenewals.DataSource = response;
                gridRenewals.RefreshDataSource();

                //showing summary
                string searchString = btnSearchRenewal.Text;
                if (searchString.Equals(""))
                {
                    label119.Text = "Showing " + response.Length.ToString() + " result(s) for renewals.";
                }
                else
                {
                    label119.Text = "Showing " + response.Length.ToString() + " result(s) for the search '" + searchString + "'";
                }

                if (response.Length == 0)
                {
                    ShowNoResults();
                }
            }
        }

        private void gridViewRenewals_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"]);
                if (category == "Complete")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.SpringGreen;
                }
                else if (category == "Saved" || category == "Submitted")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.SlateGray;
                }
                else if (category == "Payment")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.LightSteelBlue;
                }
                else if (category == "Site Visit")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.Lavender;
                }
            }
        }

        private void navPageViewRegistration_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdRecoVisitReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageSiteVisitReport;
            InitializeRecommendationsSiteVisitReport();
        }

        private void cmdApproveCertificate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                #region access
                senpa.DocumentWorkflow wrkFlow = agent.operation.UpdateWorkFlowStage(currentCertificateId, "certificate");
                existingDocumentsPosition = 82;
                grpExistingApprove.Controls.OfType<LinkLabel>().ToList().ForEach(btn => btn.Dispose());
                grpExistingApprove.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                senpa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(currentCertificateId, "certificate");

                RefreshApprovalIndicator();
                //check access
                if (!agent.operation.CheckAccessToStage(currentCertificateId, "certificate"))
                {
                    cmdSaveBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdSaveApproval.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdNotApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else
                {
                    cmdSaveBusiness.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdSaveApproval.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdNotApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }

                if (currentStage.StageName == "Complete")
                {
                    //issue cert
                    bool issued = agent.operation.IssueCertificate(currentCertificateRegistrationId, certificateRenewalId);
                    if (issued)
                    {
                        ShowMessageBox("Issuing Certificate", "Certificate has been Issued", "normal");
                        cmdRefreshCert.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdIssueCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdNotApproveCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdViewCertificate.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        ShowMessageBox("Issuing Certificate", "Certificate has failed", "error");
                    }
                }
                else
                {

                }

                senpa.WorkFlowStageDocumentStatus[] alldocuments = agent.operation.GetAllRequiredDocuments(currentCertificateRegistrationId, "registration");
                foreach (senpa.WorkFlowStageDocumentStatus doc in alldocuments)
                {
                    DocumentLink(doc, "business", "certificate");
                }
                #endregion
               

            }
        }

        private void btnSaveRenewal_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRenewWorkFlow_Click(object sender, EventArgs e)
        {
           
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            navigationFrame.SelectedPage = navPageViewRegistration;
        }

        private void gridRenewals_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InitializeNewRegForm();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            navigationFrame.SelectedPage = navPageViewRegistration;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            navigationFrame.SelectedPage = navPageViewRenewals;
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            navigationFrame.SelectedPage = navPageRegisteredBusiness;
        }

        private void txtFindInvoices_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Invoice[] response = agent.operation.GetInvoices(txtFindInvoices.EditValue.ToString());
                gridViewInvoices.DataSource = response;
                gridViewInvoices.RefreshDataSource();

                //showing summary
                string searchString = txtFindInvoices.Text;
                if (searchString.Equals(""))
                {
                    label120.Text = "Showing " + response.Length.ToString() + " result(s) for invoices.";
                }
                else
                {
                    label120.Text = "Showing " + response.Length.ToString() + " result(s) for the search '" + searchString + "'";
                }

                if (response.Length == 0)
                {
                    ShowNoResults();
                }
            }
        }

        private void cmdHomeDashboard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageHome;
            InitializeQuickStats();
            InitializeNotifications();
        }

        private void cmdConfirmSiteVisit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                bool response = agent.operation.ConfirmSiteVisit(long.Parse(lblSiteId.Text));
                if (response)
                {
                    cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    notifySiteVisitRibbon.Visible = true;
                    cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
            }
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserAccountSettings userAccountSettings = new UserAccountSettings();
            userAccountSettings.Show();
        }

        private void txtBusinessRegNumber_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (txtBusinessRegNumber.Text.Equals(""))
            {
                return;
            }

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Business response = agent.operation.GetBusiness(txtBusinessRegNumber.Text);

                txtBusinessName.Text = response.Name;
                if (response.Name == "")
                    txtBusinessRegNumber.Text = "";
            }
        }

        private void txtNIN_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (txtNIN.Text == "")
                return;

            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                senpa.Resident response = agent.operation.GetResident(txtNIN.Text);

                txtFirstName.Text = response.FirstName;
                txtLastName.Text = response.Surname;

                txtCitizenship.Text = response.Nationality;
                // Type { get => type; set => type = value; }
                // Status { get => status; set => status = value; }
                dtpDOB.DateTime = response.DateOfBirth;
                if (response.FirstName == "")
                    txtNIN.Text = "";
            }
        }

        private void panel122_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SEnPAMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void cmbRenWorkFlowSkip_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void cmdRenWorkFlowRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
               
                InitializeRenewalForm(currentBusinessId);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ShowMessageBox("An Error Occured", ex.Message, "error");
            }
        }

        private void cmdRenNavNewVisit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageScheduleSiteVisit;
            InitializeRenewalSiteVisit();
        }

        private void cmdRenNavVisitReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                //check for site visit id
                senpa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentRenewalId, "renewal");
                if (site == null || site.CreatedBy == null)
                {
                    ;
                }
                else
                {
                    if (site.Confirmed)
                    {
                        navigationFrame.SelectedPage = navPageSiteVisitReport;
                        InitializeRenewalSiteVisitReport();
                    }
                }
            }
        }

        private void cmdRenNavRecommendations_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            InitializeRenewalRecommendations();
        }

        private void gridViewRenewals_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                currentBusinessId = long.Parse(gridViewRenewals.GetRowCellValue(gridViewRenewals.FocusedRowHandle, gridViewRenewals.Columns[0]).ToString());

                InitializeRenewalForm(currentBusinessId);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
                ShowMessageBox("An Error Occured", ex.Message, "error");
            }
        }

        private void newRenewal_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageViewRenewals;
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                bool done = agent.operation.ClearEmails();

                senpa.Email[] response = agent.operation.GetEmails(txtFindEmail.EditValue.ToString());
                gridEmails.DataSource = response;
                gridEmails.RefreshDataSource();
            }
        }

        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SenpaApi agent = new SenpaApi();
            using (new OperationContextScope(agent.context))
            {
                bool done = agent.operation.ClearSMSs();

                senpa.SMS[] response = agent.operation.GetSMSs(txtFindSMS.EditValue.ToString());
                gridSMS.DataSource = response;
                gridSMS.RefreshDataSource();
            }
        }

        private void officeNavigationBar_Click(object sender, EventArgs e)
        {

        }

        public void ShowMessageBox(string caption, string message, string boxType)
        {
            if (boxType.Equals("error"))
            {
                //XtraMessageBox.Show(message, message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowErrorBox(message);
            }else if (boxType.Equals("success"))
            {
                XtraMessageBox.Show(message, message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (boxType.Equals("warning"))
            {
                XtraMessageBox.Show(message, message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                XtraMessageBox.Show(message,message, MessageBoxButtons.OK);
                
            }

        }

        #region MessageBoxes

        public void ShowValidationError(string message)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Validation Error!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Orange;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }

        public void ShowNotFound(string item)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Not Found!", Description = "The " + item + " with this number could not be found. Please ensure you entered it correctly and try again." };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Orange;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }

        public void ShowNoResults()
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Not Results!", Description = "There are no results for the current search." };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Gray;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }


        public void ShowQualification()
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Qualifies!", Description = "This business qualifies for the loan application. Would you like to proceed to the load application now?" };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "Yes, proceed", Result = System.Windows.Forms.DialogResult.Yes };
            FlyoutCommand command2 = new FlyoutCommand() { Text = "Not at this time", Result = System.Windows.Forms.DialogResult.No };
            action.Commands.Add(command1);
            action.Commands.Add(command2);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.ForestGreen;
            properties.Appearance.ForeColor = Color.White;
            if (DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties) == DialogResult.Yes)
            {
                
            }
        }

        public void ShowDisqualification(string reason)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Disqualified!", Description = "This business does not qualify for the loan application" + Environment.NewLine + Environment.NewLine + "Reasons for the disqualification are:" + Environment.NewLine + Environment.NewLine + reason };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);

        }

        public void ShowErrorBox(string reason)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Disqualified!", Description = "This business does not qualify for the loan application" + Environment.NewLine + Environment.NewLine + "Reasons for the disqualification are:" + Environment.NewLine + Environment.NewLine + reason };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);

        }

        public bool isNumeric(string text)
        {
            double myNum = 0;

            if (Double.TryParse(text, out myNum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        private void InitializeHomePage()
        {
            //add procedures to load the quick stats

            navigationFrame.SelectedPage = navPageHome;
        }

    }
}