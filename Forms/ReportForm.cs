using System.Windows.Forms;

namespace SupermarketManagementSystem.Forms
{
    public class ReportForm : Form
    {
        public RichTextBox txtReport;

        public ReportForm()
        {
            Text = "Reports";
            Width = 1000;
            Height = 600;
            StartPosition = FormStartPosition.CenterParent;

            txtReport = new RichTextBox
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(txtReport);
        }
    }
}