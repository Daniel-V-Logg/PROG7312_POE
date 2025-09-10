namespace MunicipalServiceApp
{
    partial class ReportIssuesForm
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
            this.lblLocation = new System.Windows.Forms.Label();
            this.txtLocation = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.rtbDescription = new System.Windows.Forms.RichTextBox();
            this.btnAttachFile = new System.Windows.Forms.Button();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.engagementProgressBar = new System.Windows.Forms.ProgressBar();
            this.lblEncouragement = new System.Windows.Forms.Label();
            this.grpIssueDetails = new System.Windows.Forms.GroupBox();
            this.grpEngagement = new System.Windows.Forms.GroupBox();
            this.grpIssueDetails.SuspendLayout();
            this.grpEngagement.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpIssueDetails
            // 
            this.grpIssueDetails.Controls.Add(this.lblLocation);
            this.grpIssueDetails.Controls.Add(this.txtLocation);
            this.grpIssueDetails.Controls.Add(this.lblCategory);
            this.grpIssueDetails.Controls.Add(this.cmbCategory);
            this.grpIssueDetails.Controls.Add(this.lblDescription);
            this.grpIssueDetails.Controls.Add(this.rtbDescription);
            this.grpIssueDetails.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpIssueDetails.Location = new System.Drawing.Point(20, 20);
            this.grpIssueDetails.Name = "grpIssueDetails";
            this.grpIssueDetails.Size = new System.Drawing.Size(520, 200);
            this.grpIssueDetails.TabIndex = 0;
            this.grpIssueDetails.TabStop = false;
            this.grpIssueDetails.Text = "Issue Details";
            // 
            // lblLocation
            // 
            this.lblLocation.AutoSize = true;
            this.lblLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocation.Location = new System.Drawing.Point(20, 30);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(56, 15);
            this.lblLocation.TabIndex = 0;
            this.lblLocation.Text = "Location:";
            // 
            // txtLocation
            // 
            this.txtLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocation.Location = new System.Drawing.Point(120, 27);
            this.txtLocation.Name = "txtLocation";
            this.txtLocation.Size = new System.Drawing.Size(380, 21);
            this.txtLocation.TabIndex = 1;
            this.txtLocation.Text = "";
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.Location = new System.Drawing.Point(20, 65);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(57, 15);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "Sanitation",
            "Roads",
            "Utilities"});
            this.cmbCategory.Location = new System.Drawing.Point(120, 62);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(200, 23);
            this.cmbCategory.TabIndex = 3;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(20, 100);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(70, 15);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Description:";
            // 
            // rtbDescription
            // 
            this.rtbDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbDescription.Location = new System.Drawing.Point(120, 97);
            this.rtbDescription.Name = "rtbDescription";
            this.rtbDescription.Size = new System.Drawing.Size(380, 80);
            this.rtbDescription.TabIndex = 5;
            this.rtbDescription.Text = "";
            // 
            // grpEngagement
            // 
            this.grpEngagement.Controls.Add(this.lblEncouragement);
            this.grpEngagement.Controls.Add(this.engagementProgressBar);
            this.grpEngagement.Controls.Add(this.lblAttachment);
            this.grpEngagement.Controls.Add(this.btnAttachFile);
            this.grpEngagement.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpEngagement.Location = new System.Drawing.Point(20, 240);
            this.grpEngagement.Name = "grpEngagement";
            this.grpEngagement.Size = new System.Drawing.Size(520, 80);
            this.grpEngagement.TabIndex = 1;
            this.grpEngagement.TabStop = false;
            this.grpEngagement.Text = "Engagement & Attachments";
            // 
            // lblEncouragement
            // 
            this.lblEncouragement.AutoSize = true;
            this.lblEncouragement.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEncouragement.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.lblEncouragement.Location = new System.Drawing.Point(20, 25);
            this.lblEncouragement.Name = "lblEncouragement";
            this.lblEncouragement.Size = new System.Drawing.Size(0, 15);
            this.lblEncouragement.TabIndex = 0;
            // 
            // engagementProgressBar
            // 
            this.engagementProgressBar.Location = new System.Drawing.Point(400, 25);
            this.engagementProgressBar.Name = "engagementProgressBar";
            this.engagementProgressBar.Size = new System.Drawing.Size(100, 20);
            this.engagementProgressBar.TabIndex = 1;
            this.engagementProgressBar.Value = 30;
            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAttachment.Location = new System.Drawing.Point(20, 50);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(90, 15);
            this.lblAttachment.TabIndex = 2;
            this.lblAttachment.Text = "No file attached";
            // 
            // btnAttachFile
            // 
            this.btnAttachFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.btnAttachFile.FlatAppearance.BorderSize = 0;
            this.btnAttachFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttachFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttachFile.ForeColor = System.Drawing.Color.White;
            this.btnAttachFile.Location = new System.Drawing.Point(400, 50);
            this.btnAttachFile.Name = "btnAttachFile";
            this.btnAttachFile.Size = new System.Drawing.Size(100, 25);
            this.btnAttachFile.TabIndex = 3;
            this.btnAttachFile.Text = "Attach File";
            this.btnAttachFile.UseVisualStyleBackColor = false;
            this.btnAttachFile.Click += new System.EventHandler(this.btnAttach_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(0)))));
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(350, 340);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 35);
            this.btnSubmit.TabIndex = 2;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = false;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Gray;
            this.btnBack.FlatAppearance.BorderSize = 0;
            this.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(240, 340);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 35);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back to Menu";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // ReportIssuesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(560, 400);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.grpEngagement);
            this.Controls.Add(this.grpIssueDetails);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ReportIssuesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Report an Issue - Municipal Services";
            this.grpIssueDetails.ResumeLayout(false);
            this.grpIssueDetails.PerformLayout();
            this.grpEngagement.ResumeLayout(false);
            this.grpEngagement.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLocation;
        private System.Windows.Forms.TextBox txtLocation;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.RichTextBox rtbDescription;
        private System.Windows.Forms.Button btnAttachFile;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.ProgressBar engagementProgressBar;
        private System.Windows.Forms.Label lblEncouragement;
        private System.Windows.Forms.GroupBox grpIssueDetails;
        private System.Windows.Forms.GroupBox grpEngagement;
    }
}