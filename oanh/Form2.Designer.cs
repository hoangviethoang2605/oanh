namespace oanh
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_STAFF = new System.Windows.Forms.Label();
            this.lb_CUSTOMER_NAME = new System.Windows.Forms.Label();
            this.lb_CUSTOMER_ADDRESS = new System.Windows.Forms.Label();
            this.lb_CUSTOMER_NUMBER = new System.Windows.Forms.Label();
            this.lb_TOTAL = new System.Windows.Forms.Label();
            this.lb_DATE = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_STAFF
            // 
            this.lb_STAFF.AutoSize = true;
            this.lb_STAFF.Location = new System.Drawing.Point(24, 32);
            this.lb_STAFF.Name = "lb_STAFF";
            this.lb_STAFF.Size = new System.Drawing.Size(85, 13);
            this.lb_STAFF.TabIndex = 0;
            this.lb_STAFF.Text = "Tên Nhân Viên: ";
            // 
            // lb_CUSTOMER_NAME
            // 
            this.lb_CUSTOMER_NAME.AutoSize = true;
            this.lb_CUSTOMER_NAME.Location = new System.Drawing.Point(24, 93);
            this.lb_CUSTOMER_NAME.Name = "lb_CUSTOMER_NAME";
            this.lb_CUSTOMER_NAME.Size = new System.Drawing.Size(95, 13);
            this.lb_CUSTOMER_NAME.TabIndex = 1;
            this.lb_CUSTOMER_NAME.Text = "Tên Khách Hàng: ";
            // 
            // lb_CUSTOMER_ADDRESS
            // 
            this.lb_CUSTOMER_ADDRESS.AutoSize = true;
            this.lb_CUSTOMER_ADDRESS.Location = new System.Drawing.Point(24, 154);
            this.lb_CUSTOMER_ADDRESS.Name = "lb_CUSTOMER_ADDRESS";
            this.lb_CUSTOMER_ADDRESS.Size = new System.Drawing.Size(46, 13);
            this.lb_CUSTOMER_ADDRESS.TabIndex = 2;
            this.lb_CUSTOMER_ADDRESS.Text = "Địa chỉ: ";
            // 
            // lb_CUSTOMER_NUMBER
            // 
            this.lb_CUSTOMER_NUMBER.AutoSize = true;
            this.lb_CUSTOMER_NUMBER.Location = new System.Drawing.Point(24, 215);
            this.lb_CUSTOMER_NUMBER.Name = "lb_CUSTOMER_NUMBER";
            this.lb_CUSTOMER_NUMBER.Size = new System.Drawing.Size(81, 13);
            this.lb_CUSTOMER_NUMBER.TabIndex = 3;
            this.lb_CUSTOMER_NUMBER.Text = "Số Điện Thoại: ";
            // 
            // lb_TOTAL
            // 
            this.lb_TOTAL.AutoSize = true;
            this.lb_TOTAL.Location = new System.Drawing.Point(24, 276);
            this.lb_TOTAL.Name = "lb_TOTAL";
            this.lb_TOTAL.Size = new System.Drawing.Size(58, 13);
            this.lb_TOTAL.TabIndex = 4;
            this.lb_TOTAL.Text = "Tổng tiền: ";
            // 
            // lb_DATE
            // 
            this.lb_DATE.AutoSize = true;
            this.lb_DATE.Location = new System.Drawing.Point(316, 32);
            this.lb_DATE.Name = "lb_DATE";
            this.lb_DATE.Size = new System.Drawing.Size(59, 13);
            this.lb_DATE.TabIndex = 5;
            this.lb_DATE.Text = "Ngày bán: ";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 346);
            this.Controls.Add(this.lb_DATE);
            this.Controls.Add(this.lb_TOTAL);
            this.Controls.Add(this.lb_CUSTOMER_NUMBER);
            this.Controls.Add(this.lb_CUSTOMER_ADDRESS);
            this.Controls.Add(this.lb_CUSTOMER_NAME);
            this.Controls.Add(this.lb_STAFF);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_STAFF;
        private System.Windows.Forms.Label lb_CUSTOMER_NAME;
        private System.Windows.Forms.Label lb_CUSTOMER_ADDRESS;
        private System.Windows.Forms.Label lb_CUSTOMER_NUMBER;
        private System.Windows.Forms.Label lb_TOTAL;
        private System.Windows.Forms.Label lb_DATE;
    }
}