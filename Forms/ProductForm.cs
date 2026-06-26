using System;
using System.Windows.Forms;

namespace SupermarketManagementSystem.Forms
{
    public class ProductForm : Form
    {
        public TextBox txtBarcode;
        public TextBox txtProductName;
        public TextBox txtPrice;
        public TextBox txtQuantity;

        public ComboBox cmbCategory;
        public ComboBox cmbSupplier;

        public DateTimePicker dtpExpiry;
        public DateTimePicker dtpRestock;

        public Button btnAdd;
        public Button btnUpdate;
        public Button btnDelete;
        public Button btnClear;

        public DataGridView dgvProducts;

        public ProductForm()
        {
            Text = "Product Management";
            Width = 1200;
            Height = 700;
            StartPosition = FormStartPosition.CenterParent;

            InitializeControls();
        }

        private void InitializeControls()
        {
            int leftLabel = 40;
            int leftControl = 180;
            int top = 30;
            int gap = 45;

            AddLabel("Barcode", leftLabel, top);
            txtBarcode = AddTextBox(leftControl, top);

            top += gap;
            AddLabel("Product Name", leftLabel, top);
            txtProductName = AddTextBox(leftControl, top);

            top += gap;
            AddLabel("Category", leftLabel, top);
            cmbCategory = AddComboBox(leftControl, top);

            top += gap;
            AddLabel("Supplier", leftLabel, top);
            cmbSupplier = AddComboBox(leftControl, top);

            top += gap;
            AddLabel("Price", leftLabel, top);
            txtPrice = AddTextBox(leftControl, top);

            top += gap;
            AddLabel("Quantity", leftLabel, top);
            txtQuantity = AddTextBox(leftControl, top);

            top += gap;
            AddLabel("Expiry Date", leftLabel, top);
            dtpExpiry = AddDate(leftControl, top);

            top += gap;
            AddLabel("Restock Date", leftLabel, top);
            dtpRestock = AddDate(leftControl, top);

            btnAdd = AddButton("Add", 500, 60);
            btnUpdate = AddButton("Update", 500, 120);
            btnDelete = AddButton("Delete", 500, 180);
            btnClear = AddButton("Clear", 500, 240);

            dgvProducts = new DataGridView
            {
                Left = 40,
                Top = 420,
                Width = 1100,
                Height = 220,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            Controls.Add(dgvProducts);
        }

        private void AddLabel(string text, int x, int y)
        {
            Controls.Add(new Label
            {
                Text = text,
                Left = x,
                Top = y,
                Width = 120
            });
        }

        private TextBox AddTextBox(int x, int y)
        {
            var t = new TextBox
            {
                Left = x,
                Top = y,
                Width = 220
            };

            Controls.Add(t);
            return t;
        }

        private ComboBox AddComboBox(int x, int y)
        {
            var c = new ComboBox
            {
                Left = x,
                Top = y,
                Width = 220,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            Controls.Add(c);
            return c;
        }

        private DateTimePicker AddDate(int x, int y)
        {
            var d = new DateTimePicker
            {
                Left = x,
                Top = y,
                Width = 220
            };

            Controls.Add(d);
            return d;
        }

        private Button AddButton(string text, int x, int y)
        {
            var b = new Button
            {
                Text = text,
                Left = x,
                Top = y,
                Width = 150,
                Height = 40
            };

            Controls.Add(b);
            return b;
        }
    }
}