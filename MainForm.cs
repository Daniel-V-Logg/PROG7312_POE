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
            btnServiceStatus.Enabled = true;
            btnServiceStatus.Text = "Service Request Status";
        }


        private void btnReportIssues_Click(object sender, EventArgs e)
        {
            var reportForm = new ReportIssuesForm();
            reportForm.ShowDialog();
        }

        private void btnLocalEvents_Click(object sender, EventArgs e)
        {
            var eventsForm = new LocalEventsForm();
            eventsForm.ShowDialog();
        }


        private void MainForm_Load(object sender, EventArgs e)
        {
            btnLocalEvents.Enabled = true;
            btnServiceStatus.Enabled = true;
            btnServiceStatus.Text = "Service Request Status";
        }

        private void btnServiceStatus_Click(object sender, EventArgs e)
        {
            var serviceStatusForm = new ServiceRequestStatusForm();
            serviceStatusForm.ShowDialog();
        }
    }
}
