using System.Windows.Forms;

namespace SupermarketManagementSystem.Forms
{
    public class StockForm : Form
    {
        public ListBox lstStock;

        public StockForm()
        {
            Text = "Stock Management";
            Width = 900;
            Height = 600;
            StartPosition = FormStartPosition.CenterParent;

            lstStock = new ListBox
            {
                Dock = DockStyle.Fill
            };

            Controls.Add(lstStock);
        }
    }
}