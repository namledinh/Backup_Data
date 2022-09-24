using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nLogin
{
    public partial class QuenMatkhau : Form
    {
        public QuenMatkhau()
        {
            InitializeComponent();
            label1.Text = "";
        }
        Modify modify = new Modify();           
        private void btnLLMK_QMK_Click(object sender, EventArgs e)
        {
            string email = txtEmail_QMK.Text;
            if (email.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Email bạn đã đăng ký!");
            }
            else
            {
                string query = "Select * from login where Email = '" + email + "'";
                if (modify.Taikhoans(query).Count != 0)
                {
                    label1.ForeColor = Color.Blue;
                    label1.Text = "Mật khẩu: " + modify.Taikhoans(query)[0].Matkhau;
                }
                else
                {
                    label1.ForeColor = Color.Red;
                    label1.Text = "Email này không phải email bạn đã đăng ký!";
                }
            }
        }
    }
}
