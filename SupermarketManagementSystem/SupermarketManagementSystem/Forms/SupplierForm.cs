using System;
using System.Windows.Forms;
using SupermarketManagementSystem.Models;
using SupermarketManagementSystem.Services;

namespace SupermarketManagementSystem.Forms
{
    public class SupplierForm : Form
    {
        private readonly SupplierService
            _supplierService = new();

        private DataGridView dgv;

        private TextBox txtName;
        private TextBox txtPhone;
        private TextBox txtAddress;

        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;

        private int selectedSupplierId = 0;

        public SupplierForm()
        {
            Text = "Manage Suppliers";
            Width = 1000;
            Height = 650;
            StartPosition =
                FormStartPosition.CenterParent;

            CreateControls();
            LoadSuppliers();
        }

        private void CreateControls()
        {
            Label lblTitle = new()
            {
                Text = "SUPPLIER MANAGEMENT",
                Left = 330,
                Top = 30,
                AutoSize = true,
                Font = new System.Drawing.Font(
                    "Segoe UI",
                    16,
                    System.Drawing.FontStyle.Bold)
            };

            Label lblName = new()
            {
                Text = "Supplier Name",
                Left = 80,
                Top = 100
            };

            txtName = new TextBox()
            {
                Left = 220,
                Top = 95,
                Width = 250
            };

            Label lblPhone = new()
            {
                Text = "Phone",
                Left = 80,
                Top = 150
            };

            txtPhone = new TextBox()
            {
                Left = 220,
                Top = 145,
                Width = 250
            };

            Label lblAddress = new()
            {
                Text = "Address",
                Left = 80,
                Top = 200
            };

            txtAddress = new TextBox()
            {
                Left = 220,
                Top = 195,
                Width = 400
            };

            btnAdd = new Button()
            {
                Text = "Add Supplier",
                Left = 180,
                Top = 260,
                Width = 150
            };

            btnUpdate = new Button()
            {
                Text = "Update Supplier",
                Left = 380,
                Top = 260,
                Width = 150
            };

            btnDelete = new Button()
            {
                Text = "Delete Supplier",
                Left = 580,
                Top = 260,
                Width = 150
            };

            dgv = new DataGridView()
            {
                Left = 50,
                Top = 340,
                Width = 880,
                Height = 220,
                ReadOnly = true,
                MultiSelect = false,
                SelectionMode =
                    DataGridViewSelectionMode
                    .FullRowSelect,
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode
                    .Fill
            };

            btnAdd.Click += BtnAdd_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnDelete.Click += BtnDelete_Click;
            dgv.CellDoubleClick +=
                Dgv_CellDoubleClick;

            Controls.Add(lblTitle);

            Controls.Add(lblName);
            Controls.Add(txtName);

            Controls.Add(lblPhone);
            Controls.Add(txtPhone);

            Controls.Add(lblAddress);
            Controls.Add(txtAddress);

            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);

            Controls.Add(dgv);
        }

        private void LoadSuppliers()
        {
            dgv.DataSource = null;
            dgv.DataSource =
                _supplierService.GetAll();
        }

        private void BtnAdd_Click(
            object sender,
            EventArgs e)
        {
            try
            {
                Supplier s = new()
                {
                    SupplierName = txtName.Text,
                    Phone = txtPhone.Text,
                    Address = txtAddress.Text
                };

                _supplierService.Add(s);

                LoadSuppliers();
                ClearFields();

                MessageBox.Show(
                    "Supplier Added");
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
            if (selectedSupplierId == 0)
                return;

            try
            {
                Supplier s = new()
                {
                    SupplierId =
                        selectedSupplierId,
                    SupplierName =
                        txtName.Text,
                    Phone =
                        txtPhone.Text,
                    Address =
                        txtAddress.Text
                };

                _supplierService.Update(s);

                LoadSuppliers();
                ClearFields();

                MessageBox.Show(
                    "Supplier Updated");
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
            if (selectedSupplierId == 0)
                return;

            _supplierService.Delete(
                selectedSupplierId);

            LoadSuppliers();
            ClearFields();

            MessageBox.Show(
                "Supplier Deleted");
        }

        private void Dgv_CellDoubleClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row =
                dgv.Rows[e.RowIndex];

            selectedSupplierId =
                Convert.ToInt32(
                    row.Cells["SupplierId"]
                    .Value);

            txtName.Text =
                row.Cells["SupplierName"]
                .Value.ToString();

            txtPhone.Text =
                row.Cells["Phone"]
                .Value.ToString();

            txtAddress.Text =
                row.Cells["Address"]
                .Value.ToString();
        }

        private void ClearFields()
        {
            selectedSupplierId = 0;

            txtName.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
        }
    }
}