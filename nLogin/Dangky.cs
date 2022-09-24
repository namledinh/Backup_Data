using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace nLogin
{
    public partial class Dangky : Form
    {
        public Dangky()
        {
            InitializeComponent();
        }

        public bool CheckAccount(string ac)
        {
            return Regex.IsMatch(ac, @"^[a-zA-Z0-9]{6,24}$");
        }
        public bool CheckEmail(string em)
        {
            return Regex.IsMatch(em,@"^[a-zA-Z0-9_.]{3,24}@gmail.com(.vn|)$");
        }
        Modify modify = new Modify();
        private void btnDK_DK_Click(object sender, EventArgs e)
        {
            string tentk = txtUN_DK.Text;
            string matkhau = txtPW_DK.Text;
            string xnmatkhau = txtRP_DK.Text;
            string email = txtEmail_DK.Text;
            if (!CheckAccount(tentk)) { MessageBox.Show("Vui lòng nhập tên tài khoản từ 6 tới 24 ký tự"); return; }
            if (!CheckAccount(matkhau)) { MessageBox.Show("Vui lòng nhập mật khẩu từ 6 tới 24 ký tự"); return; }            
            if(xnmatkhau!=matkhau) { MessageBox.Show("Mật khẩu không khớp với mật khẩu đã đặt!"); return; }
            if (!CheckEmail(email)) { MessageBox.Show("Vui lòng nhập đúng định dạng của Email "); return; }           
            if(modify.Taikhoans("Select * from login where Email = '" + email +"'").Count != 0) { MessageBox.Show("Email này đã được đăng ký, vui lòng đăng ký Email khác!"); return; }
            try
            {
                string query = "Insert into login values ('" +tentk+"','"+matkhau+"','"+email+"')";
                modify.Command(query);
                if (MessageBox.Show("Bạn đã đăng ký thành công!", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch 
            {
                MessageBox.Show("Tên tài khoản này đã được đăng ký, vui lòng nhập tên tài khoản khác!");
            }
        }
    }
}
