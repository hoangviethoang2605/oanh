using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace oanh
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadProductsIntoComboBox();
            LoadStaffIntoComboBox();
            DateTime today = DateTime.Today;
            tbx_DATE.Text = today.ToString("dd/MM/yyyy");
        }

        public void ConfigureDataGridView()
        {
            DataGridViewButtonColumn deleteColumn = new DataGridViewButtonColumn();
            deleteColumn.HeaderText = "Xóa";
            deleteColumn.Name = "DeleteColumn";
            deleteColumn.Text = "Xóa";
            deleteColumn.UseColumnTextForButtonValue = true;
            if (!dataGridView1.Columns.Contains("DeleteColumn"))
            {
                dataGridView1.Columns.Add(deleteColumn);
            }
        }

        public void LoadProductsIntoComboBox()
        {
            comboBox1.DataSource = null;
            DataTable productTable =
                Database.Query("Select IDMATHANG, TENMATHANG, GIA from MATHANG order by TENMATHANG");
            comboBox1.DataSource = productTable;
            comboBox1.ValueMember = "IDMATHANG";
            comboBox1.DisplayMember = "TENMATHANG";
        }

        public void LoadStaffIntoComboBox()
        {
            comboBox2.DataSource = null;
            DataTable staffTable = Database.Query("Select IDNHANVIEN, TENNHANVIEN from NHANVIEN order by TENNHANVIEN");
            comboBox2.DataSource = staffTable;
            comboBox2.ValueMember = "IDNHANVIEN";
            comboBox2.DisplayMember = "TENNHANVIEN";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataRowView item = comboBox1.SelectedItem as DataRowView;
            if (item == null) return;
            double selectedProductPrice = (double)item["GIA"];
            tbx_PRICE.Text = selectedProductPrice.ToString();
        }

        private void UpdateTotalPrice()
        {
            double total = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) break;
                total += Double.Parse(row.Cells[4].Value.ToString());
            }
            tbx_TOTAL.Text = total.ToString();
        }

        private void btn_ADD_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (nud_QUANTITY.Value == 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataRowView selectedProduct = comboBox1.SelectedItem as DataRowView;
                string productID = selectedProduct["IDMATHANG"].ToString();
                string productName = selectedProduct["TENMATHANG"].ToString();
                string productQuantity = nud_QUANTITY.Value.ToString();
                string productPrice = selectedProduct["GIA"].ToString();
                double Total = (double)nud_QUANTITY.Value * (double)selectedProduct["GIA"];
                dataGridView1.Rows.Add(productID, productName, productQuantity, productPrice, Total);
                ConfigureDataGridView();
                UpdateTotalPrice();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["DeleteColumn"].Index && e.RowIndex >= 0)
            {
                try
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    UpdateTotalPrice();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool Check()
        {
            errorProvider1.Clear();
            tbx_CUSTOMER_NAME.Text = tbx_CUSTOMER_NAME.Text.Trim();
            tbx_CUSTOMER_ADDRESS.Text = tbx_CUSTOMER_ADDRESS.Text.Trim();
            tbx_CUSTOMER_NUMBER.Text = tbx_CUSTOMER_NUMBER.Text.Trim();
            if (tbx_CUSTOMER_NAME.TextLength == 0)
            {
                errorProvider1.SetError(tbx_CUSTOMER_NAME, "Tên không được để trống");
                return false;
            }

            if (tbx_CUSTOMER_ADDRESS.TextLength == 0)
            {
                errorProvider1.SetError(tbx_CUSTOMER_ADDRESS, "Địa chỉ không được để trống");
                return false;
            }

            if (tbx_CUSTOMER_NUMBER.TextLength != 10 || !tbx_CUSTOMER_NUMBER.Text.All(Char.IsDigit) ||
                !tbx_CUSTOMER_NUMBER.Text.StartsWith("0"))
            {
                errorProvider1.SetError(tbx_CUSTOMER_NUMBER, "Số điện thoại không hợp lệ");
                return false;
            }

            if (comboBox2.Text == "")
            {
                errorProvider1.SetError(comboBox2, "Chưa có nhân viên phụ trách");
                return false;
            }

            if (tbx_TOTAL.TextLength == 0)
            {
                errorProvider1.SetError(tbx_TOTAL, "Chưa có sản phẩm nào được chọn");
                return false;
            }

            return true;
        }

        private void btn_CONFIRM_Click(object sender, EventArgs e)
        {
            if (!Check())
            {
                return;
            }

            CustomerName = tbx_CUSTOMER_NAME.Text;
            CustomerNumber = tbx_CUSTOMER_NUMBER.Text;
            CustomerAddress = tbx_CUSTOMER_ADDRESS.Text;
            StaffName = comboBox2.Text;
            Date = tbx_DATE.Text;
            Total = tbx_TOTAL.Text;
            form2.ShowDialog();
            Clear();
        }

        private void btn_CANCEL_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public void Clear()
        {
            tbx_CUSTOMER_ADDRESS.Text = string.Empty;
            tbx_CUSTOMER_NAME.Text = string.Empty;
            tbx_CUSTOMER_NUMBER.Text = string.Empty;
            tbx_PRICE.Text = string.Empty;
            tbx_TOTAL.Text = string.Empty;
            nud_QUANTITY.Value = 0;
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            dataGridView1.Rows.Clear();
            if (dataGridView1.Columns.Contains("DeleteColumn"))
            {
                dataGridView1.Columns.Remove("DeleteColumn");
            }
        }

        public static string CustomerName;
        public static string CustomerNumber;
        public static string CustomerAddress;
        public static string StaffName;
        public static string Date;
        public static string Total;
        public Form2 form2 = new Form2();
    }
}