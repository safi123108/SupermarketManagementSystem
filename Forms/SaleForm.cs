using System.Windows.Forms;

namespace SupermarketManagementSystem.Forms
{
    public class SaleForm : Form
    {
        public DataGridView dgvSales;

        public SaleForm()
        {
            Text = "Sales";
            Width = 1000;
            Height = 600;
            StartPosition = FormStartPosition.CenterParent;

            dgvSales = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill
            };

            Controls.Add(dgvSales);
        }
    }
}