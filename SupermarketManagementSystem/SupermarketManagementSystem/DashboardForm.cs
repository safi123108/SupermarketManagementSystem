using SupermarketManagementSystem.Forms;
using System;
using System.Windows.Forms;

namespace SupermarketManagementSystem
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {
            InitializeComponent();

            Text = "Local Supermarket Management System";
            Width = 1000;
            Height = 650;
            StartPosition = FormStartPosition.CenterScreen;

            CreateDashboard();
        }

        private void CreateDashboard()
        {
            Label lblTitle = new Label
            {
                Text = "LOCAL SUPERMARKET MANAGEMENT SYSTEM",
                AutoSize = false,
                Width = 900,
                Height = 40,
                Left = 40,
                Top = 30,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Segoe UI", 16,
                    System.Drawing.FontStyle.Bold)
            };

            Controls.Add(lblTitle);

            Button btnProducts = new Button
            {
                Text = "Manage Products",
                Width = 250,
                Height = 70,
                Left = 100,
                Top = 120
            };

            Button btnSuppliers = new Button
            {
                Text = "Manage Suppliers",
                Width = 250,
                Height = 70,
                Left = 600,
                Top = 120
            };

            Button btnStock = new Button
            {
                Text = "Stock Management",
                Width = 250,
                Height = 70,
                Left = 100,
                Top = 260
            };

            Button btnSales = new Button
            {
                Text = "Record Sale",
                Width = 250,
                Height = 70,
                Left = 600,
                Top = 260
            };

            Button btnReports = new Button
            {
                Text = "Reports",
                Width = 250,
                Height = 70,
                Left = 100,
                Top = 400
            };

            Button btnExit = new Button
            {
                Text = "Exit",
                Width = 250,
                Height = 70,
                Left = 600,
                Top = 400
            };

            btnProducts.Click += BtnProducts_Click;
            btnSuppliers.Click += BtnSuppliers_Click;
            btnStock.Click += BtnStock_Click;
            btnSales.Click += BtnSales_Click;
            btnReports.Click += BtnReports_Click;
            btnExit.Click += BtnExit_Click;

            Controls.Add(btnProducts);
            Controls.Add(btnSuppliers);
            Controls.Add(btnStock);
            Controls.Add(btnSales);
            Controls.Add(btnReports);
            Controls.Add(btnExit);
        }

        private void BtnProducts_Click(object sender, EventArgs e)
        {
            ProductForm form = new ProductForm();
            form.ShowDialog();
        }

        private void BtnSuppliers_Click(object sender, EventArgs e)
        {
            SupplierForm form = new SupplierForm();
            form.ShowDialog();
        }

        private void BtnStock_Click(object sender, EventArgs e)
        {
            StockForm form = new StockForm();
            form.ShowDialog();
        }

        private void BtnSales_Click(object sender, EventArgs e)
        {
            SaleForm form = new SaleForm();
            form.ShowDialog();
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            ReportForm form = new ReportForm();
            form.ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}