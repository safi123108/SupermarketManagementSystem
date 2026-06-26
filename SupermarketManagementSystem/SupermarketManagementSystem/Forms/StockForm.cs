using System;
using System.Windows.Forms;
using SupermarketManagementSystem.Services;

namespace SupermarketManagementSystem.Forms
{
    public class StockForm : Form
    {
        private readonly StockService _stockService = new();

        private DataGridView dgv;
        private TextBox txtProductId;
        private TextBox txtQuantity;

        private Button btnUpdate;

        public StockForm()
        {
            Text = "Stock Management";
            Width = 1000;
            Height = 650;
            StartPosition =
                FormStartPosition.CenterParent;

            CreateControls();
            LoadStock();
        }

        private void CreateControls()
        {
            Label lblTitle = new()
            {
                Text = "STOCK MANAGEMENT",
                Left = 350,
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
                Left = 80,
                Top = 110
            };

            txtProductId = new TextBox()
            {
                Left = 220,
                Top = 105,
                Width = 200
            };

            Label lblQty = new()
            {
                Text = "Quantity",
                Left = 80,
                Top = 160
            };

            txtQuantity = new TextBox()
            {
                Left = 220,
                Top = 155,
                Width = 200
            };

            btnUpdate = new Button()
            {
                Text = "Update Stock",
                Left = 500,
                Top = 130,
                Width = 180
            };

            dgv = new DataGridView()
            {
                Left = 50,
                Top = 260,
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

            btnUpdate.Click +=
                BtnUpdate_Click;

            dgv.CellDoubleClick +=
                Dgv_CellDoubleClick;

            Controls.Add(lblTitle);

            Controls.Add(lblProduct);
            Controls.Add(txtProductId);

            Controls.Add(lblQty);
            Controls.Add(txtQuantity);

            Controls.Add(btnUpdate);

            Controls.Add(dgv);
        }

        private void LoadStock()
        {
            dgv.DataSource = null;
            dgv.DataSource =
                _stockService.GetAllStock();
        }

        private void BtnUpdate_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                _stockService.UpdateStock(
                    int.Parse(
                        txtProductId.Text),
                    int.Parse(
                        txtQuantity.Text));

                LoadStock();

                MessageBox.Show(
                    "Stock Updated");
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
                row.Cells["QuantityInStock"]
                    .Value
                    .ToString();
        }
    }
}