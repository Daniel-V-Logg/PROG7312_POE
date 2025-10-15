namespace MunicipalServiceApp
{
    partial class DiagnosticsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabIndexes = new System.Windows.Forms.TabPage();
            this.txtIndexStats = new System.Windows.Forms.TextBox();
            this.tabPopular = new System.Windows.Forms.TabPage();
            this.txtTopEvents = new System.Windows.Forms.TextBox();
            this.tabCategories = new System.Windows.Forms.TabPage();
            this.txtCategoryDist = new System.Windows.Forms.TextBox();
            this.tabSearches = new System.Windows.Forms.TabPage();
            this.txtRecentSearches = new System.Windows.Forms.TextBox();
            this.tabDates = new System.Windows.Forms.TabPage();
            this.txtDateRange = new System.Windows.Forms.TextBox();
            this.tabPersistence = new System.Windows.Forms.TabPage();
            this.txtPersistence = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabIndexes.SuspendLayout();
            this.tabPopular.SuspendLayout();
            this.tabCategories.SuspendLayout();
            this.tabSearches.SuspendLayout();
            this.tabDates.SuspendLayout();
            this.tabPersistence.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(400, 24);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Data Structures Diagnostics";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabIndexes);
            this.tabControl.Controls.Add(this.tabPopular);
            this.tabControl.Controls.Add(this.tabCategories);
            this.tabControl.Controls.Add(this.tabSearches);
            this.tabControl.Controls.Add(this.tabDates);
            this.tabControl.Controls.Add(this.tabPersistence);
            this.tabControl.Location = new System.Drawing.Point(20, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(760, 400);
            this.tabControl.TabIndex = 1;
            // 
            // tabIndexes
            // 
            this.tabIndexes.Controls.Add(this.txtIndexStats);
            this.tabIndexes.Location = new System.Drawing.Point(4, 22);
            this.tabIndexes.Name = "tabIndexes";
            this.tabIndexes.Padding = new System.Windows.Forms.Padding(3);
            this.tabIndexes.Size = new System.Drawing.Size(752, 374);
            this.tabIndexes.TabIndex = 0;
            this.tabIndexes.Text = "Index Summary";
            this.tabIndexes.UseVisualStyleBackColor = true;
            // 
            // txtIndexStats
            // 
            this.txtIndexStats.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtIndexStats.Location = new System.Drawing.Point(10, 10);
            this.txtIndexStats.Multiline = true;
            this.txtIndexStats.Name = "txtIndexStats";
            this.txtIndexStats.ReadOnly = true;
            this.txtIndexStats.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtIndexStats.Size = new System.Drawing.Size(730, 350);
            this.txtIndexStats.TabIndex = 0;
            // 
            // tabPopular
            // 
            this.tabPopular.Controls.Add(this.txtTopEvents);
            this.tabPopular.Location = new System.Drawing.Point(4, 22);
            this.tabPopular.Name = "tabPopular";
            this.tabPopular.Padding = new System.Windows.Forms.Padding(3);
            this.tabPopular.Size = new System.Drawing.Size(752, 374);
            this.tabPopular.TabIndex = 1;
            this.tabPopular.Text = "Popular Events";
            this.tabPopular.UseVisualStyleBackColor = true;
            // 
            // txtTopEvents
            // 
            this.txtTopEvents.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtTopEvents.Location = new System.Drawing.Point(10, 10);
            this.txtTopEvents.Multiline = true;
            this.txtTopEvents.Name = "txtTopEvents";
            this.txtTopEvents.ReadOnly = true;
            this.txtTopEvents.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTopEvents.Size = new System.Drawing.Size(730, 350);
            this.txtTopEvents.TabIndex = 0;
            // 
            // tabCategories
            // 
            this.tabCategories.Controls.Add(this.txtCategoryDist);
            this.tabCategories.Location = new System.Drawing.Point(4, 22);
            this.tabCategories.Name = "tabCategories";
            this.tabCategories.Size = new System.Drawing.Size(752, 374);
            this.tabCategories.TabIndex = 2;
            this.tabCategories.Text = "Category Distribution";
            this.tabCategories.UseVisualStyleBackColor = true;
            // 
            // txtCategoryDist
            // 
            this.txtCategoryDist.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtCategoryDist.Location = new System.Drawing.Point(10, 10);
            this.txtCategoryDist.Multiline = true;
            this.txtCategoryDist.Name = "txtCategoryDist";
            this.txtCategoryDist.ReadOnly = true;
            this.txtCategoryDist.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCategoryDist.Size = new System.Drawing.Size(730, 350);
            this.txtCategoryDist.TabIndex = 0;
            // 
            // tabSearches
            // 
            this.tabSearches.Controls.Add(this.txtRecentSearches);
            this.tabSearches.Location = new System.Drawing.Point(4, 22);
            this.tabSearches.Name = "tabSearches";
            this.tabSearches.Size = new System.Drawing.Size(752, 374);
            this.tabSearches.TabIndex = 3;
            this.tabSearches.Text = "Search History";
            this.tabSearches.UseVisualStyleBackColor = true;
            // 
            // txtRecentSearches
            // 
            this.txtRecentSearches.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtRecentSearches.Location = new System.Drawing.Point(10, 10);
            this.txtRecentSearches.Multiline = true;
            this.txtRecentSearches.Name = "txtRecentSearches";
            this.txtRecentSearches.ReadOnly = true;
            this.txtRecentSearches.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRecentSearches.Size = new System.Drawing.Size(730, 350);
            this.txtRecentSearches.TabIndex = 0;
            // 
            // tabDates
            // 
            this.tabDates.Controls.Add(this.txtDateRange);
            this.tabDates.Location = new System.Drawing.Point(4, 22);
            this.tabDates.Name = "tabDates";
            this.tabDates.Size = new System.Drawing.Size(752, 374);
            this.tabDates.TabIndex = 4;
            this.tabDates.Text = "Date Coverage";
            this.tabDates.UseVisualStyleBackColor = true;
            // 
            // txtDateRange
            // 
            this.txtDateRange.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtDateRange.Location = new System.Drawing.Point(10, 10);
            this.txtDateRange.Multiline = true;
            this.txtDateRange.Name = "txtDateRange";
            this.txtDateRange.ReadOnly = true;
            this.txtDateRange.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDateRange.Size = new System.Drawing.Size(730, 350);
            this.txtDateRange.TabIndex = 0;
            // 
            // tabPersistence
            // 
            this.tabPersistence.Controls.Add(this.txtPersistence);
            this.tabPersistence.Location = new System.Drawing.Point(4, 22);
            this.tabPersistence.Name = "tabPersistence";
            this.tabPersistence.Size = new System.Drawing.Size(752, 374);
            this.tabPersistence.TabIndex = 5;
            this.tabPersistence.Text = "Persistence Status";
            this.tabPersistence.UseVisualStyleBackColor = true;
            // 
            // txtPersistence
            // 
            this.txtPersistence.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtPersistence.Location = new System.Drawing.Point(10, 10);
            this.txtPersistence.Multiline = true;
            this.txtPersistence.Name = "txtPersistence";
            this.txtPersistence.ReadOnly = true;
            this.txtPersistence.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPersistence.Size = new System.Drawing.Size(730, 350);
            this.txtPersistence.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(0, 102, 204);
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(580, 475);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 35);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Gray;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(690, 475);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(90, 35);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // DiagnosticsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 530);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DiagnosticsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Structures Diagnostics";
            this.tabControl.ResumeLayout(false);
            this.tabIndexes.ResumeLayout(false);
            this.tabIndexes.PerformLayout();
            this.tabPopular.ResumeLayout(false);
            this.tabPopular.PerformLayout();
            this.tabCategories.ResumeLayout(false);
            this.tabCategories.PerformLayout();
            this.tabSearches.ResumeLayout(false);
            this.tabSearches.PerformLayout();
            this.tabDates.ResumeLayout(false);
            this.tabDates.PerformLayout();
            this.tabPersistence.ResumeLayout(false);
            this.tabPersistence.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabIndexes;
        private System.Windows.Forms.TextBox txtIndexStats;
        private System.Windows.Forms.TabPage tabPopular;
        private System.Windows.Forms.TextBox txtTopEvents;
        private System.Windows.Forms.TabPage tabCategories;
        private System.Windows.Forms.TextBox txtCategoryDist;
        private System.Windows.Forms.TabPage tabSearches;
        private System.Windows.Forms.TextBox txtRecentSearches;
        private System.Windows.Forms.TabPage tabDates;
        private System.Windows.Forms.TextBox txtDateRange;
        private System.Windows.Forms.TabPage tabPersistence;
        private System.Windows.Forms.TextBox txtPersistence;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnClose;
    }
}

