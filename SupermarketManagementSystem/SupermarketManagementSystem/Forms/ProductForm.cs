using System;
using System.Linq;
using System.Windows.Forms;
using SupermarketManagementSystem.Models;
using SupermarketManagementSystem.Services;

namespace SupermarketManagementSystem.Forms
{
    public class ProductForm : Form
    {
        private readonly ProductService _productService = new();

        private DataGridView dgv;
        private TextBox txtBarcode;
        private TextBox txtName;
        private TextBox txtCategoryId;
        private TextBox txtSupplierId;
        private TextBox txtPrice;
        private TextBox txtQuantity;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        private int selectedProductId = 0;

        public ProductForm()
        {
            Text = "Manage Products";
            Width = 1200;
            Height = 700;
            StartPosition = FormStartPosition.CenterParent;

            CreateControls();
            LoadProducts();
        }

        private void CreateControls()
        {
            Label lblTitle = new()
            {
                Text = "PRODUCT MANAGEMENT",
                AutoSize = true,
                Font = new System.Drawing.Font(
                    "Segoe UI",
                    16,
                    System.Drawing.FontStyle.Bold),
                Left = 420,
                Top = 20
            };

            Label lblBarcode = new()
            {
                Text = "Barcode",
                Left = 40,
                Top = 90,
                Width = 100
            };

            txtBarcode = new TextBox()
            {
                Left = 150,
                Top = 85,
                Width = 200
            };

            Label lblName = new()
            {
                Text = "Product Name",
                Left = 400,
                Top = 90
            };

            txtName = new TextBox()
            {
                Left = 520,
                Top = 85,
                Width = 250
            };

            Label lblCategory = new()
            {
                Text = "Category Id",
                Left = 40,
                Top = 130
            };

            txtCategoryId = new TextBox()
            {
                Left = 150,
                Top = 125,
                Width = 200
            };

            Label lblSupplier = new()
            {
                Text = "Supplier Id",
                Left = 400,
                Top = 130
            };

            txtSupplierId = new TextBox()
            {
                Left = 520,
                Top = 125,
                Width = 250
            };

            Label lblPrice = new()
            {
                Text = "Price",
                Left = 40,
                Top = 170
            };

            txtPrice = new TextBox()
            {
                Left = 150,
                Top = 165,
                Width = 200
            };

            Label lblQty = new()
            {
                Text = "Quantity",
                Left = 400,
                Top = 170
            };

            txtQuantity = new TextBox()
            {
                Left = 520,
                Top = 165,
                Width = 250
            };

            btnAdd = new Button()
            {
                Text = "Add Product",
                Left = 150,
                Top = 220,
                Width = 150
            };

            btnUpdate = new Button()
            {
                Text = "Update Product",
                Left = 350,
                Top = 220,
                Width = 150
            };

            btnDelete = new Button()
            {
                Text = "Delete Product",
                Left = 550,
                Top = 220,
                Width = 150
            };

            dgv = new DataGridView()
            {
                Left = 40,
                Top = 300,
                Width = 1100,
                Height = 300,
                ReadOnly = true,
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill
            };

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            dgv.CellDoubleClick += Dgv_CellDoubleClick;

            Controls.Add(lblTitle);

            Controls.Add(lblBarcode);
            Controls.Add(txtBarcode);

            Controls.Add(lblName);
            Controls.Add(txtName);

            Controls.Add(lblCategory);
            Controls.Add(txtCategoryId);

            Controls.Add(lblSupplier);
            Controls.Add(txtSupplierId);

            Controls.Add(lblPrice);
            Controls.Add(txtPrice);

            Controls.Add(lblQty);
            Controls.Add(txtQuantity);

            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);

            Controls.Add(dgv);
        }

        private void LoadProducts()
        {
            dgv.DataSource = null;
            dgv.DataSource = _productService.GetAll();
        }

        private void BtnAdd_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                Product p = new()
                {
                    Barcode = txtBarcode.Text,
                    ProductName = txtName.Text,
                    CategoryId =
                        int.Parse(txtCategoryId.Text),
                    SupplierId =
                        int.Parse(txtSupplierId.Text),
                    Price =
                        decimal.Parse(txtPrice.Text),
                    QuantityInStock =
                        int.Parse(txtQuantity.Text),
                    ExpiryDate =
                        DateTime.Today.AddMonths(6),
                    RestockDate =
                        DateTime.Today,
                    StockStatus = "Available"
                };

                _productService.Add(p);

                ClearFields();
                LoadProducts();

                MessageBox.Show(
                    "Product Added Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnUpdate_Click(
            object sender,
            EventArgs e)
        {
            if (selectedProductId == 0)
                return;

            try
            {
                Product p = new()
                {
                    ProductId = selectedProductId,
                    Barcode = txtBarcode.Text,
                    ProductName = txtName.Text,
                    CategoryId =
                        int.Parse(txtCategoryId.Text),
                    SupplierId =
                        int.Parse(txtSupplierId.Text),
                    Price =
                        decimal.Parse(txtPrice.Text),
                    QuantityInStock =
                        int.Parse(txtQuantity.Text),
                    ExpiryDate =
                        DateTime.Today.AddMonths(6),
                    RestockDate =
                        DateTime.Today,
                    StockStatus = "Available"
                };

                _productService.Update(p);

                ClearFields();
                LoadProducts();

                MessageBox.Show(
                    "Product Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDelete_Click(
            object sender,
            EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0)
                return;

            int id =
                Convert.ToInt32(
                    dgv.SelectedRows[0]
                    .Cells["ProductId"].Value);

            _productService.Delete(id);

            LoadProducts();

            MessageBox.Show(
                "Product Deleted");
        }

        private void Dgv_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgv.Rows[e.RowIndex];

            selectedProductId =
                Convert.ToInt32(
                    row.Cells["ProductId"].Value);

            txtBarcode.Text =
                row.Cells["Barcode"].Value.ToString();

            txtName.Text =
                row.Cells["ProductName"].Value.ToString();

            txtCategoryId.Text =
                row.Cells["CategoryId"].Value.ToString();

            txtSupplierId.Text =
                row.Cells["SupplierId"].Value.ToString();

            txtPrice.Text =
                row.Cells["Price"].Value.ToString();

            txtQuantity.Text =
                row.Cells["QuantityInStock"]
                .Value.ToString();
        }

        private void ClearFields()
        {
            selectedProductId = 0;

            txtBarcode.Clear();
            txtName.Clear();
            txtCategoryId.Clear();
            txtSupplierId.Clear();
            txtPrice.Clear();
            txtQuantity.Clear();
        }
    }
}