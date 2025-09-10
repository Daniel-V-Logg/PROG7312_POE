using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MunicipalServiceApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            var reportForm = new ReportIssuesForm();
            reportForm.ShowDialog();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            // Disable future features as per requirements
            btnLocalEvents.Enabled = false;
            btnServiceStatus.Enabled = false;
            
            // Set tooltips for disabled buttons
            btnLocalEvents.Text = "Local Events and Announcements\n(Coming Soon)";
            btnServiceStatus.Text = "Service Request Status\n(Coming Soon)";
        }
    }
}
