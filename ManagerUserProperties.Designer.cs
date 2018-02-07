namespace SEnPA
{
    partial class ManagerUserProperties
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("System Roles");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("System Group Roles");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerUserProperties));
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("User Roles");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("User Group Roles");
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnAddRole = new DevExpress.XtraEditors.SimpleButton();
            this.treeSystemRoles = new System.Windows.Forms.TreeView();
            this.systemRolesMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userIcons = new System.Windows.Forms.ImageList(this.components);
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            this.btnRemoveRole = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl4 = new DevExpress.XtraEditors.PanelControl();
            this.lblDescription = new System.Windows.Forms.Label();
            this.treeUserRoles = new System.Windows.Forms.TreeView();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.label2 = new System.Windows.Forms.Label();
            this.chkExpires = new System.Windows.Forms.CheckBox();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.cmbStakeholder = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnChange = new DevExpress.XtraEditors.SimpleButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.progressBar = new DevExpress.XtraEditors.MarqueeProgressBarControl();
            this.chkLocked = new System.Windows.Forms.CheckBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.lblChanged = new System.Windows.Forms.Label();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.label3 = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.systemRolesMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).BeginInit();
            this.splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).BeginInit();
            this.panelControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.btnAddRole);
            this.splitContainerControl1.Panel1.Controls.Add(this.treeSystemRoles);
            this.splitContainerControl1.Panel1.Controls.Add(this.panelControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.splitContainerControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(790, 516);
            this.splitContainerControl1.SplitterPosition = 255;
            this.splitContainerControl1.TabIndex = 0;
            // 
            // btnAddRole
            // 
            this.btnAddRole.ImageOptions.Image = global::SEnPA.Properties.Resources.ic_action_goright;
            this.btnAddRole.Location = new System.Drawing.Point(210, 216);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(37, 37);
            this.btnAddRole.TabIndex = 3;
            this.btnAddRole.Click += new System.EventHandler(this.btnAddRole_Click);
            // 
            // treeSystemRoles
            // 
            this.treeSystemRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeSystemRoles.ContextMenuStrip = this.systemRolesMenu;
            this.treeSystemRoles.Location = new System.Drawing.Point(0, 37);
            this.treeSystemRoles.Name = "treeSystemRoles";
            treeNode1.Name = "systemRoles";
            treeNode1.StateImageIndex = 0;
            treeNode1.Text = "System Roles";
            treeNode2.Name = "systemGroupRoles";
            treeNode2.StateImageKey = "Users.png";
            treeNode2.Text = "System Group Roles";
            this.treeSystemRoles.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            this.treeSystemRoles.Size = new System.Drawing.Size(204, 476);
            this.treeSystemRoles.StateImageList = this.userIcons;
            this.treeSystemRoles.TabIndex = 2;
            this.treeSystemRoles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeSystemRoles_AfterSelect);
            // 
            // systemRolesMenu
            // 
            this.systemRolesMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.systemRolesMenu.Name = "systemRolesMenu";
            this.systemRolesMenu.Size = new System.Drawing.Size(114, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "&Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // userIcons
            // 
            this.userIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("userIcons.ImageStream")));
            this.userIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.userIcons.Images.SetKeyName(0, "User (2).png");
            this.userIcons.Images.SetKeyName(1, "user1_16.png");
            this.userIcons.Images.SetKeyName(2, "Users.png");
            this.userIcons.Images.SetKeyName(3, "User Group.png");
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.label1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(255, 31);
            this.panelControl1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "System Roles";
            // 
            // splitContainerControl2
            // 
            this.splitContainerControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl2.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl2.Name = "splitContainerControl2";
            this.splitContainerControl2.Panel1.Controls.Add(this.btnRemoveRole);
            this.splitContainerControl2.Panel1.Controls.Add(this.panelControl4);
            this.splitContainerControl2.Panel1.Controls.Add(this.treeUserRoles);
            this.splitContainerControl2.Panel1.Controls.Add(this.panelControl2);
            this.splitContainerControl2.Panel1.Text = "Panel1";
            this.splitContainerControl2.Panel2.Controls.Add(this.chkExpires);
            this.splitContainerControl2.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl2.Panel2.Controls.Add(this.progressBar);
            this.splitContainerControl2.Panel2.Controls.Add(this.chkLocked);
            this.splitContainerControl2.Panel2.Controls.Add(this.chkActive);
            this.splitContainerControl2.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl2.Panel2.Controls.Add(this.panelControl3);
            this.splitContainerControl2.Panel2.Text = "Panel2";
            this.splitContainerControl2.Size = new System.Drawing.Size(523, 516);
            this.splitContainerControl2.SplitterPosition = 261;
            this.splitContainerControl2.TabIndex = 0;
            // 
            // btnRemoveRole
            // 
            this.btnRemoveRole.ImageOptions.Image = global::SEnPA.Properties.Resources.ic_action_goleft;
            this.btnRemoveRole.Location = new System.Drawing.Point(7, 216);
            this.btnRemoveRole.Name = "btnRemoveRole";
            this.btnRemoveRole.Size = new System.Drawing.Size(37, 37);
            this.btnRemoveRole.TabIndex = 5;
            this.btnRemoveRole.Click += new System.EventHandler(this.btnRemoveRole_Click);
            // 
            // panelControl4
            // 
            this.panelControl4.Controls.Add(this.lblDescription);
            this.panelControl4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl4.Location = new System.Drawing.Point(0, 454);
            this.panelControl4.Name = "panelControl4";
            this.panelControl4.Size = new System.Drawing.Size(261, 62);
            this.panelControl4.TabIndex = 4;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(6, 6);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(23, 13);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "....";
            // 
            // treeUserRoles
            // 
            this.treeUserRoles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeUserRoles.Location = new System.Drawing.Point(49, 37);
            this.treeUserRoles.Name = "treeUserRoles";
            treeNode3.Name = "userSystemRoles";
            treeNode3.StateImageKey = "user1_16.png";
            treeNode3.Text = "User Roles";
            treeNode4.Name = "userSystemGroupRoles";
            treeNode4.StateImageKey = "User Group.png";
            treeNode4.Text = "User Group Roles";
            this.treeUserRoles.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this.treeUserRoles.Size = new System.Drawing.Size(212, 408);
            this.treeUserRoles.StateImageList = this.userIcons;
            this.treeUserRoles.TabIndex = 3;
            this.treeUserRoles.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeUserRoles_AfterSelect);
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.label2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(261, 31);
            this.panelControl2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "User Assigned Roles";
            // 
            // chkExpires
            // 
            this.chkExpires.AutoSize = true;
            this.chkExpires.Location = new System.Drawing.Point(155, 36);
            this.chkExpires.Name = "chkExpires";
            this.chkExpires.Size = new System.Drawing.Size(111, 17);
            this.chkExpires.TabIndex = 10;
            this.chkExpires.Text = "Paasword Expires";
            this.chkExpires.UseVisualStyleBackColor = true;
            this.chkExpires.CheckedChanged += new System.EventHandler(this.chkExpires_CheckedChanged);
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.cmbStakeholder);
            this.groupControl2.Controls.Add(this.label10);
            this.groupControl2.Controls.Add(this.txtMobile);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.txtEmail);
            this.groupControl2.Controls.Add(this.label7);
            this.groupControl2.Controls.Add(this.txtSurname);
            this.groupControl2.Controls.Add(this.txtName);
            this.groupControl2.Controls.Add(this.btnChange);
            this.groupControl2.Controls.Add(this.label6);
            this.groupControl2.Controls.Add(this.label5);
            this.groupControl2.Location = new System.Drawing.Point(15, 160);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(246, 332);
            this.groupControl2.TabIndex = 14;
            this.groupControl2.Text = "Name && Contacts";
            // 
            // cmbStakeholder
            // 
            this.cmbStakeholder.FormattingEnabled = true;
            this.cmbStakeholder.Location = new System.Drawing.Point(20, 271);
            this.cmbStakeholder.Name = "cmbStakeholder";
            this.cmbStakeholder.Size = new System.Drawing.Size(209, 21);
            this.cmbStakeholder.TabIndex = 21;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 251);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = "Organisation";
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(20, 218);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(212, 21);
            this.txtMobile.TabIndex = 17;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "Mobile";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(16, 159);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(216, 21);
            this.txtEmail.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Email";
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(16, 107);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(175, 21);
            this.txtSurname.TabIndex = 13;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(16, 52);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(175, 21);
            this.txtName.TabIndex = 12;
            // 
            // btnChange
            // 
            this.btnChange.ImageOptions.Image = global::SEnPA.Properties.Resources.sign_tick;
            this.btnChange.Location = new System.Drawing.Point(165, 304);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(65, 23);
            this.btnChange.TabIndex = 11;
            this.btnChange.Text = "Change";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Surname";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "First Name";
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.EditValue = 0;
            this.progressBar.Location = new System.Drawing.Point(0, 498);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(250, 18);
            this.progressBar.TabIndex = 13;
            // 
            // chkLocked
            // 
            this.chkLocked.AutoSize = true;
            this.chkLocked.Location = new System.Drawing.Point(86, 37);
            this.chkLocked.Name = "chkLocked";
            this.chkLocked.Size = new System.Drawing.Size(59, 17);
            this.chkLocked.TabIndex = 12;
            this.chkLocked.Text = "Locked";
            this.chkLocked.UseVisualStyleBackColor = true;
            this.chkLocked.CheckedChanged += new System.EventHandler(this.chkLocked_CheckedChanged);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(18, 37);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 11;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.label8);
            this.groupControl1.Controls.Add(this.lblExpiry);
            this.groupControl1.Controls.Add(this.lblChanged);
            this.groupControl1.Location = new System.Drawing.Point(15, 68);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(246, 86);
            this.groupControl1.TabIndex = 9;
            this.groupControl1.Text = "Password";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 58);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 13);
            this.label9.TabIndex = 9;
            this.label9.Text = "Last Changed";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 27);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Expiry Date";
            // 
            // lblExpiry
            // 
            this.lblExpiry.AutoSize = true;
            this.lblExpiry.Location = new System.Drawing.Point(97, 27);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Size = new System.Drawing.Size(19, 13);
            this.lblExpiry.TabIndex = 6;
            this.lblExpiry.Text = "...";
            // 
            // lblChanged
            // 
            this.lblChanged.AutoSize = true;
            this.lblChanged.Location = new System.Drawing.Point(100, 58);
            this.lblChanged.Name = "lblChanged";
            this.lblChanged.Size = new System.Drawing.Size(23, 13);
            this.lblChanged.TabIndex = 5;
            this.lblChanged.Text = "....";
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.label3);
            this.panelControl3.Controls.Add(this.lblUsername);
            this.panelControl3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl3.Location = new System.Drawing.Point(0, 0);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(250, 31);
            this.panelControl3.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "User Status";
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(81, 9);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(23, 13);
            this.lblUsername.TabIndex = 10;
            this.lblUsername.Text = "....";
            // 
            // ManagerUserProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 516);
            this.Controls.Add(this.splitContainerControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManagerUserProperties";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manager User Properties";
            this.Load += new System.EventHandler(this.ManagerUserProperties_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.systemRolesMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl2)).EndInit();
            this.splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl4)).EndInit();
            this.panelControl4.ResumeLayout(false);
            this.panelControl4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeSystemRoles;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TreeView treeUserRoles;
        private DevExpress.XtraEditors.PanelControl panelControl4;
        private System.Windows.Forms.Label lblDescription;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.Label lblChanged;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkLocked;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.CheckBox chkExpires;
        private System.Windows.Forms.ImageList userIcons;
        private System.Windows.Forms.ContextMenuStrip systemRolesMenu;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private DevExpress.XtraEditors.SimpleButton btnAddRole;
        private DevExpress.XtraEditors.SimpleButton btnRemoveRole;
        private DevExpress.XtraEditors.MarqueeProgressBarControl progressBar;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnChange;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbStakeholder;
    }
}