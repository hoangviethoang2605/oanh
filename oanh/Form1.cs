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
            try
            {
                comboBox1.Items.Clear();
                DataTable productTable = Database.Query("Select IDMATHANG, TENMATHANG, GIA from MATHANG order by TENMATHANG");
                foreach (DataRow row in productTable.Rows)
                {
                    ProductItem product = new ProductItem
                    {
                        Id = Convert.ToInt32(row["IDMATHANG"]),
                        Name = row["TENMATHANG"].ToString(),
                        Price = Convert.ToDouble(row["GIA"])
                    };

                    comboBox1.Items.Add(product);
                }
                comboBox1.DisplayMember = "Name";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        public void LoadStaffIntoComboBox()
        {
            try
            {
                comboBox2.Items.Clear();
                DataTable staffTable = Database.Query("Select TENNHANVIEN from NHANVIEN order by TENNHANVIEN");
                foreach (DataRow row in staffTable.Rows)
                {
                    comboBox2.Items.Add(row["TENNHANVIEN"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        public class ProductItem
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public double Price { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string searchName = comboBox1.Text.Trim();
            tbx_PRICE.Clear();

            if (string.IsNullOrEmpty(searchName) && comboBox1.SelectedIndex == -1)
            {
                LoadProductsIntoComboBox();
                return;
            }

            if (searchName.Length < 2)
                return;

            try
            {
                comboBox1.Items.Clear();
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Name", searchName);
                DataTable productTable = Database.Query("SELECT IDMATHANG, TENMATHANG, GIA FROM MATHANG WHERE TENMATHANG LIKE '%' + @Name + '%'", parameters);

                foreach (DataRow row in productTable.Rows)
                {
                    ProductItem product = new ProductItem
                    {
                        Id = Convert.ToInt32(row["IDMATHANG"]),
                        Name = row["TENMATHANG"].ToString(),
                        Price = Convert.ToDouble(row["GIA"])
                    };
                    comboBox1.Items.Add(product);
                }
                comboBox1.DisplayMember = "Name";
                comboBox1.DroppedDown = true;
                comboBox1.IntegralHeight = true;
                comboBox1.SelectionStart = comboBox1.Text.Length;
                comboBox1.SelectionLength = 0;
                Cursor.Current = Cursors.Default;
                if (comboBox1.Items.Count == 0)
                {
                    errorProvider1.SetError(comboBox1, "Không có sản phẩm với tên được nhập");
                    return;
                }
                else
                {
                    errorProvider1.SetError(comboBox1, "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is ProductItem selectedProduct)
            {
                double selectedProductPrice = selectedProduct.Price;
                tbx_PRICE.Text = selectedProductPrice.ToString();
            }
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            string searchName = comboBox2.Text.Trim();

            if (string.IsNullOrEmpty(searchName) && comboBox2.SelectedIndex == -1)
            {
                LoadStaffIntoComboBox();
                return;
            }

            if (searchName.Length < 2)
                return;

            try
            {
                comboBox2.Items.Clear();
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("@Name", searchName);
                DataTable nhanVienTable = Database.Query(
                    "SELECT TENNHANVIEN FROM NHANVIEN WHERE TENNHANVIEN LIKE '%' + @Name + '%'", parameters);
                foreach (DataRow row in nhanVienTable.Rows)
                {
                    comboBox2.Items.Add(row["TENNHANVIEN"].ToString());
                }

                comboBox2.DisplayMember = "TENNHANVIEN";
                comboBox2.DroppedDown = true;
                comboBox2.IntegralHeight = true;
                comboBox2.SelectionStart = comboBox2.Text.Length;
                comboBox2.SelectionLength = 0;
                Cursor.Current = Cursors.Default;
                if (comboBox2.Items.Count == 0)
                {
                    errorProvider1.SetError(comboBox2, "Không có nhân viên với tên được nhập");
                    return;
                }
                else
                {
                    errorProvider1.SetError(comboBox2, "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        public int Pay = 0;

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
                ProductItem selectedProduct = (ProductItem)comboBox1.SelectedItem;
                string productID = selectedProduct.Id.ToString();
                string productName = selectedProduct.Name;
                string productQuantity = nud_QUANTITY.Value.ToString();
                string productPrice = selectedProduct.Price.ToString();
                string Total = (Decimal.ToDouble(nud_QUANTITY.Value) * selectedProduct.Price).ToString();
                dataGridView1.Rows.Add(productID, productName, productQuantity, productPrice, Total);
                ConfigureDataGridView();
                Pay += Int32.Parse(Total);
                tbx_TOTAL.Text = Pay.ToString();
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
            if (tbx_CUSTOMER_NUMBER.TextLength != 10 || !tbx_CUSTOMER_NUMBER.Text.All(Char.IsDigit) || !tbx_CUSTOMER_NUMBER.Text.StartsWith("0"))
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
