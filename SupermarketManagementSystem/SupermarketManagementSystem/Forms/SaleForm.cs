using System;
using System.Windows.Forms;
using SupermarketManagementSystem.Services;

namespace SupermarketManagementSystem.Forms
{
    public class SaleForm : Form
    {
        private readonly SaleService
            _saleService = new();

        private DataGridView dgv;

        private TextBox txtProductId;
        private TextBox txtQuantity;

        private Button btnSale;

        public SaleForm()
        {
            Text = "Record Sale";
            Width = 1000;
            Height = 650;
            StartPosition =
                FormStartPosition.CenterParent;

            CreateControls();
            LoadSales();
        }

        private void CreateControls()
        {
            Label lblTitle = new()
            {
                Text = "RECORD SALE",
                Left = 380,
                Top = 30,
                AutoSize = true,
                Font = new System.Drawing.Font(
                    "Segoe UI",
                    16,
                    System.Drawing.FontStyle.Bold)
            };

            Label lblProduct = new()
            {
                Text = "Product Id",
                Left = 100,
                Top = 120
            };

            txtProductId = new TextBox()
            {
                Left = 250,
                Top = 115,
                Width = 250
            };

            Label lblQty = new()
            {
                Text = "Quantity Sold",
                Left = 100,
                Top = 170
            };

            txtQuantity = new TextBox()
            {
                Left = 250,
                Top = 165,
                Width = 250
            };

            btnSale = new Button()
            {
                Text = "Record Sale",
                Left = 600,
                Top = 135,
                Width = 180
            };

            dgv = new DataGridView()
            {
                Left = 50,
                Top = 280,
                Width = 880,
                Height = 280,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode =
                    DataGridViewSelectionMode
                    .FullRowSelect,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode
                    .Fill
            };

            btnSale.Click +=
                BtnSale_Click;

            dgv.CellDoubleClick +=
                Dgv_CellDoubleClick;

            Controls.Add(lblTitle);

            Controls.Add(lblProduct);
            Controls.Add(txtProductId);

            Controls.Add(lblQty);
            Controls.Add(txtQuantity);

            Controls.Add(btnSale);

            Controls.Add(dgv);
        }

        private void LoadSales()
        {
            dgv.DataSource = null;
            dgv.DataSource =
                _saleService.GetAll();
        }

        private void BtnSale_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                _saleService.RecordSale(
                    int.Parse(
                        txtProductId.Text),
                    int.Parse(
                        txtQuantity.Text));

                LoadSales();

                MessageBox.Show(
                    "Sale Recorded");
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message);
            }
        }

        private void Dgv_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row =
                dgv.Rows[e.RowIndex];

            txtProductId.Text =
                row.Cells["ProductId"]
                    .Value
                    .ToString();

            txtQuantity.Text =
                row.Cells["QuantitySold"]
                    .Value
                    .ToString();
        }
    }
}