using System.Windows.Forms;
using SupermarketManagementSystem.Services;

namespace SupermarketManagementSystem.Forms
{
    public class ReportForm : Form
    {
        private readonly ReportService
            _reportService = new();

        private DataGridView dgv;

        public ReportForm()
        {
            Text = "Reports";
            Width = 900;
            Height = 700;
            StartPosition =
                FormStartPosition.CenterParent;

            CreateControls();
            LoadInventoryTable();
        }

        private void CreateControls()
        {
            Label lblTitle = new()
            {
                Text = "SYSTEM REPORTS",
                Left = 320,
                Top = 30,
                AutoSize = true,
                Font = new System.Drawing.Font(
                    "Segoe UI",
                    16,
                    System.Drawing.FontStyle.Bold)
            };

            Label lblProducts = new()
            {
                Text =
                    $"Total Products : {_reportService.GetTotalProducts()}",
                Left = 120,
                Top = 100,
                Width = 400
            };

            Label lblSuppliers = new()
            {
                Text =
                    $"Total Suppliers : {_reportService.GetTotalSuppliers()}",
                Left = 120,
                Top = 140,
                Width = 400
            };

            Label lblSales = new()
            {
                Text =
                    $"Total Sales : {_reportService.GetTotalSales()}",
                Left = 120,
                Top = 180,
                Width = 400
            };

            Label lblValue = new()
            {
                Text =
                    $"Inventory Value : PKR {_reportService.GetInventoryValue():N2}",
                Left = 120,
                Top = 220,
                Width = 500
            };

            dgv = new DataGridView()
            {
                Left = 50,
                Top = 300,
                Width = 780,
                Height = 250,
                ReadOnly = true,
                MultiSelect = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect
            };

            Label lblGrandTotal = new()
            {
                Text =
                    $"Grand Inventory Value : PKR {_reportService.GetInventoryValue():N2}",
                Left = 50,
                Top = 580,
                Width = 500,
                Font = new System.Drawing.Font(
                    "Segoe UI",
                    10,
                    System.Drawing.FontStyle.Bold)
            };

            Controls.Add(lblTitle);
            Controls.Add(lblProducts);
            Controls.Add(lblSuppliers);
            Controls.Add(lblSales);
            Controls.Add(lblValue);
            Controls.Add(dgv);
            Controls.Add(lblGrandTotal);
        }

        private void LoadInventoryTable()
        {
            dgv.DataSource = null;
            dgv.DataSource =
                _reportService.GetInventoryReport();
        }
    }
}