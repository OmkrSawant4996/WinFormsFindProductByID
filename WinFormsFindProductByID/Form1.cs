using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using ProductLib;

namespace WinFormsFindProductByID
{
    public partial class Form1 : Form
    {
        ProductDataStore productDataStore;
        public Form1()
        {
            InitializeComponent();
            productDataStore = new ProductDataStore(ConfigurationManager.ConnectionStrings["connstr"].ConnectionString);
        }

        private void cBoxProductID_SelectedIndexChanged(object sender, EventArgs e)
        {
            int productID = int.Parse(cBoxProductID.Text);
            Products product = productDataStore.GetProduct(productID);

            txtProductName.Text = product.ProductName.ToString();
            txtProductPrice.Text = product.UnitPrice.ToString();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            List<int> productList = productDataStore.GetAllProductId();
            cBoxProductID.DataSource = productList;
        }
    }
}
