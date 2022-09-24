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
    public partial class Dangnhap : Form
    {
        public Dangnhap()
        {
            InitializeComponent();
        }

        private void linkLabel1_QuanMK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            QuenMatkhau qmk = new QuenMatkhau();
            qmk.ShowDialog();
            this.Show();
        }

        private void linkLabel2_DKTK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Dangky DK = new Dangky();
            DK.ShowDialog();
            this.Show();
        }

        Modify modify = new Modify();
        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            string tenTK = txtUsername.Text;
            string MK = txtPassword.Text;
            if(tenTK.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản","Thông báo", MessageBoxButtons.OK);               
            }
            else 
            if(MK.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Thông báo", MessageBoxButtons.OK);               
            }
            else
            {
                string query = "SELECT *  FROM login WHERE UserName = '"+tenTK+"' and PassWord = '"+MK+"'";
                if(modify.Taikhoans(query).Count!=0)
                {
                    this.Hide();
                    Home home = new Home();
                    home.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Tên tài khoản hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }      
    }
}
