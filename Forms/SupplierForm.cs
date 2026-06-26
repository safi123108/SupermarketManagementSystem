using System.Windows.Forms;

namespace SupermarketManagementSystem.Forms
{
    public class SupplierForm : Form
    {
        public DataGridView dgvSuppliers;

        public SupplierForm()
        {
            Text = "Supplier Management";
            Width = 1000;
            Height = 600;
            StartPosition = FormStartPosition.CenterParent;

            dgvSuppliers = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill
            };

            Controls.Add(dgvSuppliers);
        }
    }
}