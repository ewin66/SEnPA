namespace SEnPA
{
    partial class ValidationRules
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Registration");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Renewal");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("Documents", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValidationRules));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeDocuments = new System.Windows.Forms.TreeView();
            this.chkNew = new System.Windows.Forms.CheckBox();
            this.cmbEvaluationType = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbDataType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbField = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtValueMax = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.txtText = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblField = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstRules = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.panel25 = new System.Windows.Forms.Panel();
            this.labelControl18 = new DevExpress.XtraEditors.LabelControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panel75 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.separatorControl11 = new DevExpress.XtraEditors.SeparatorControl();
            this.label46 = new System.Windows.Forms.Label();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.panel25.SuspendLayout();
            this.panel75.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl11)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Style3D;
            this.panelControl1.Controls.Add(this.splitContainerControl1);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.barDockControlLeft);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(898, 608);
            this.panelControl1.TabIndex = 0;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 128);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeDocuments);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.chkNew);
            this.splitContainerControl1.Panel2.Controls.Add(this.cmbEvaluationType);
            this.splitContainerControl1.Panel2.Controls.Add(this.label6);
            this.splitContainerControl1.Panel2.Controls.Add(this.cmbDataType);
            this.splitContainerControl1.Panel2.Controls.Add(this.label5);
            this.splitContainerControl1.Panel2.Controls.Add(this.cmbField);
            this.splitContainerControl1.Panel2.Controls.Add(this.panel1);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtValueMax);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtValue);
            this.splitContainerControl1.Panel2.Controls.Add(this.txtText);
            this.splitContainerControl1.Panel2.Controls.Add(this.chkActive);
            this.splitContainerControl1.Panel2.Controls.Add(this.label4);
            this.splitContainerControl1.Panel2.Controls.Add(this.label3);
            this.splitContainerControl1.Panel2.Controls.Add(this.lblField);
            this.splitContainerControl1.Panel2.Controls.Add(this.label1);
            this.splitContainerControl1.Panel2.Controls.Add(this.lstRules);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(894, 478);
            this.splitContainerControl1.SplitterPosition = 213;
            this.splitContainerControl1.TabIndex = 4;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeDocuments
            // 
            this.treeDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeDocuments.Location = new System.Drawing.Point(0, 0);
            this.treeDocuments.Name = "treeDocuments";
            treeNode1.Name = "registration";
            treeNode1.Text = "Registration";
            treeNode2.Name = "renewal";
            treeNode2.Text = "Renewal";
            treeNode3.Name = "document";
            treeNode3.Text = "Documents";
            this.treeDocuments.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode3});
            this.treeDocuments.Size = new System.Drawing.Size(213, 478);
            this.treeDocuments.TabIndex = 0;
            this.treeDocuments.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeDocuments_AfterSelect);
            // 
            // chkNew
            // 
            this.chkNew.AutoSize = true;
            this.chkNew.Location = new System.Drawing.Point(202, 232);
            this.chkNew.Name = "chkNew";
            this.chkNew.Size = new System.Drawing.Size(112, 17);
            this.chkNew.TabIndex = 32;
            this.chkNew.Text = "Save as New Rule";
            this.chkNew.UseVisualStyleBackColor = true;
            // 
            // cmbEvaluationType
            // 
            this.cmbEvaluationType.FormattingEnabled = true;
            this.cmbEvaluationType.Location = new System.Drawing.Point(93, 187);
            this.cmbEvaluationType.Name = "cmbEvaluationType";
            this.cmbEvaluationType.Size = new System.Drawing.Size(286, 21);
            this.cmbEvaluationType.TabIndex = 31;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(27, 190);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 13);
            this.label6.TabIndex = 30;
            this.label6.Text = "Evaluator";
            // 
            // cmbDataType
            // 
            this.cmbDataType.FormattingEnabled = true;
            this.cmbDataType.Location = new System.Drawing.Point(93, 98);
            this.cmbDataType.Name = "cmbDataType";
            this.cmbDataType.Size = new System.Drawing.Size(286, 21);
            this.cmbDataType.TabIndex = 29;
            this.cmbDataType.SelectedIndexChanged += new System.EventHandler(this.cmbDataType_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Data Type";
            // 
            // cmbField
            // 
            this.cmbField.FormattingEnabled = true;
            this.cmbField.Location = new System.Drawing.Point(93, 17);
            this.cmbField.Name = "cmbField";
            this.cmbField.Size = new System.Drawing.Size(286, 21);
            this.cmbField.TabIndex = 27;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            this.panel1.Controls.Add(this.labelControl1);
            this.panel1.Location = new System.Drawing.Point(1, 259);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(672, 36);
            this.panel1.TabIndex = 26;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseForeColor = true;
            this.labelControl1.Location = new System.Drawing.Point(5, 8);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(31, 17);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Rules";
            // 
            // txtValueMax
            // 
            this.txtValueMax.Location = new System.Drawing.Point(442, 147);
            this.txtValueMax.Name = "txtValueMax";
            this.txtValueMax.Size = new System.Drawing.Size(215, 21);
            this.txtValueMax.TabIndex = 9;
            // 
            // txtValue
            // 
            this.txtValue.Location = new System.Drawing.Point(93, 145);
            this.txtValue.Name = "txtValue";
            this.txtValue.Size = new System.Drawing.Size(224, 21);
            this.txtValue.TabIndex = 8;
            // 
            // txtText
            // 
            this.txtText.ForeColor = System.Drawing.Color.Black;
            this.txtText.Location = new System.Drawing.Point(93, 57);
            this.txtText.Name = "txtText";
            this.txtText.Size = new System.Drawing.Size(564, 21);
            this.txtText.TabIndex = 6;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(93, 232);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 5;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Value (Maximum)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Value";
            // 
            // lblField
            // 
            this.lblField.AutoSize = true;
            this.lblField.Location = new System.Drawing.Point(27, 20);
            this.lblField.Name = "lblField";
            this.lblField.Size = new System.Drawing.Size(29, 13);
            this.lblField.TabIndex = 1;
            this.lblField.Text = "Field";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Text";
            // 
            // lstRules
            // 
            this.lstRules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstRules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.lstRules.FullRowSelect = true;
            this.lstRules.GridLines = true;
            this.lstRules.Location = new System.Drawing.Point(1, 295);
            this.lstRules.Name = "lstRules";
            this.lstRules.Size = new System.Drawing.Size(672, 183);
            this.lstRules.TabIndex = 0;
            this.lstRules.UseCompatibleStateImageBehavior = false;
            this.lstRules.View = System.Windows.Forms.View.Details;
            this.lstRules.SelectedIndexChanged += new System.EventHandler(this.lstRules_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Field";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Text";
            this.columnHeader4.Width = 94;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Data Type";
            this.columnHeader3.Width = 83;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Value";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Value(max)";
            this.columnHeader6.Width = 80;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Evaluation Type";
            this.columnHeader7.Width = 126;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Active";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.panel25);
            this.panelControl2.Controls.Add(this.btnSave);
            this.panelControl2.Controls.Add(this.panel75);
            this.panelControl2.Controls.Add(this.separatorControl11);
            this.panelControl2.Controls.Add(this.label46);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(2, 2);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(894, 126);
            this.panelControl2.TabIndex = 3;
            // 
            // panel25
            // 
            this.panel25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            this.panel25.Controls.Add(this.labelControl18);
            this.panel25.Location = new System.Drawing.Point(1, 85);
            this.panel25.Name = "panel25";
            this.panel25.Size = new System.Drawing.Size(897, 36);
            this.panel25.TabIndex = 56;
            // 
            // labelControl18
            // 
            this.labelControl18.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl18.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl18.Appearance.Options.UseFont = true;
            this.labelControl18.Appearance.Options.UseForeColor = true;
            this.labelControl18.Location = new System.Drawing.Point(5, 8);
            this.labelControl18.Name = "labelControl18";
            this.labelControl18.Size = new System.Drawing.Size(101, 17);
            this.labelControl18.TabIndex = 1;
            this.labelControl18.Text = "Validation Details";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(807, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 41);
            this.btnSave.TabIndex = 20;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel75
            // 
            this.panel75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(115)))), ((int)(((byte)(199)))));
            this.panel75.Controls.Add(this.label20);
            this.panel75.Location = new System.Drawing.Point(3, 2);
            this.panel75.Name = "panel75";
            this.panel75.Size = new System.Drawing.Size(301, 33);
            this.panel75.TabIndex = 55;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(126, 5);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(172, 21);
            this.label20.TabIndex = 0;
            this.label20.Text = "Manage Validation";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // separatorControl11
            // 
            this.separatorControl11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl11.Location = new System.Drawing.Point(0, 55);
            this.separatorControl11.Name = "separatorControl11";
            this.separatorControl11.Size = new System.Drawing.Size(889, 23);
            this.separatorControl11.TabIndex = 54;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.Green;
            this.label46.Location = new System.Drawing.Point(58, 38);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(250, 16);
            this.label46.TabIndex = 53;
            this.label46.Text = "Create, edit and configure validation Rules\r\n";
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(2, 2);
            this.barDockControlLeft.Manager = null;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 604);
            // 
            // ValidationRules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 608);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ValidationRules";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Validation Rules";
            this.Load += new System.EventHandler(this.ValidationRules_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.panel25.ResumeLayout(false);
            this.panel25.PerformLayout();
            this.panel75.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl11)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.TreeView treeDocuments;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.TextBox txtValueMax;
        private System.Windows.Forms.TextBox txtValue;
        private System.Windows.Forms.TextBox txtText;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lstRules;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.Panel panel25;
        private DevExpress.XtraEditors.LabelControl labelControl18;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Panel panel75;
        private System.Windows.Forms.Label label20;
        private DevExpress.XtraEditors.SeparatorControl separatorControl11;
        private System.Windows.Forms.Label label46;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ComboBox cmbField;
        private System.Windows.Forms.Label lblField;
        private System.Windows.Forms.ComboBox cmbDataType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbEvaluationType;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox chkNew;
    }
}