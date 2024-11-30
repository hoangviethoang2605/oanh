using System;
using System.Windows.Forms;

namespace oanh
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string CustomerName;
        public string CustomerNumber;
        public string CustomerAddress;
        public string StaffName;
        public string Date;
        public string Total;

        private void Form2_Load(object sender, EventArgs e)
        {
            CustomerName = Form1.CustomerName;
            CustomerNumber = Form1.CustomerNumber;
            CustomerAddress = Form1.CustomerAddress;
            StaffName = Form1.StaffName;
            Date = Form1.Date;
            Total = Form1.Total;
            lb_CUSTOMER_NAME.Text += CustomerName;
            lb_CUSTOMER_ADDRESS.Text += CustomerAddress;
            lb_CUSTOMER_NUMBER.Text += CustomerNumber;
            lb_DATE.Text += Date;
            lb_TOTAL.Text += Total;
            lb_STAFF.Text += StaffName;
        }

    }
}
