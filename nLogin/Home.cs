using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace nLogin
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
        }
      
        private void btnExit_Home_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn đăng xuất không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information)==DialogResult.OK)
            {
                this.Hide();
                Dangnhap dangnhap = new Dangnhap();
                dangnhap.ShowDialog();
                this.Close();
            }    
        }
        // Tạo đường dẫn chứa thư mục lưu
        FolderBrowserDialog fd;
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            fd = new FolderBrowserDialog();
            fd.Description = "Chọn thư mục lưu";
            fd.ShowNewFolderButton = true;
            fd.RootFolder = Environment.SpecialFolder.MyComputer;
            if(fd.ShowDialog() == DialogResult.OK)
            {
                txtDuong_dan.Text = fd.SelectedPath.ToString();
                Console.WriteLine(txtDuong_dan.Text);
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn đường dẫn sao lưu", "Thông báo!", MessageBoxButtons.OK);
            }
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {           
            // thực hiện lấy danh sách các packages lưu vào file text.txt
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Arguments = @"/c adb shell pm list packages -f -3 >" + txtDuong_dan.Text + @"\Text.txt";
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.Start();
            // thực hiện backup các app vào trong đường dẫn chứa thư mục lưu các app đó
            Thread.Sleep(TimeSpan.FromSeconds(1));
            string filePath = txtDuong_dan.Text + @"\Text.txt";
            if(File.Exists(filePath))
            {
                string[] readText = File.ReadAllLines(filePath);
                foreach(string line in readText)
                {
                    int str1 = line.IndexOf("/");
                    int str2 = line.LastIndexOf("/");
                    string str3 = line.Substring(str1, str2 -8);
                    string str = line.Substring(str2 + 10);
                    Process process = new Process();
                    process.StartInfo.FileName = "cmd.exe";
                    process.StartInfo.Arguments = @"/c adb pull " + str3 + @" " + txtDuong_dan.Text + @"\" + str;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.CreateNoWindow = true;
                    process.Start();
                }
                // thực hiện backup các thư mục chứa ảnh
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Process process1 = new Process();
                process1.StartInfo.FileName = "cmd.exe";
                process1.StartInfo.Arguments = @"/c cd "+ txtDuong_dan.Text + @" && cd DCIM" + @" && adb pull sdcard/DCIM";
                /*Thread.Sleep(TimeSpan.FromSeconds(1));
                process1.StartInfo.Arguments = @"/c cd " + txtDuong_dan.Text + @" && cd Pictures" + @" && adb pull sdcard/Pictires";*/
                //MessageBox.Show(@"/c cd " + txtDuong_dan.Text + @" && adb pull /sdcard");
                process1.StartInfo.UseShellExecute= false;
                process1.StartInfo.RedirectStandardInput = true;
                process1.StartInfo.RedirectStandardOutput= true;
                process1.StartInfo.CreateNoWindow = true;
                process1.Start();
                // thực hiện backup full data
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Process process2 = new Process();
                process2.StartInfo.FileName = "cmd.exe";
                process2.StartInfo.Arguments = @"/c cd "+ txtDuong_dan.Text + @" && adb backup -all";
                process2.StartInfo.UseShellExecute = false;
                process2.StartInfo.RedirectStandardInput = true;
                process2.StartInfo.RedirectStandardOutput = true;
                process2.StartInfo.CreateNoWindow = true;
                process2.Start();
            }
        }
        private void btnRestore_Click(object sender, EventArgs e)
        {
            // thực hiện restore dữ liệu
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = @"/c cd " + txtDuong_dan.Text + @" && adb restore backup.ab";
            process.StartInfo.UseShellExecute=false;
            process.StartInfo.RedirectStandardInput=true;
            process.StartInfo.RedirectStandardOutput=true;
            process.StartInfo.CreateNoWindow=true;
            process.Start();
            // thực hiện restore các thư mục ảnh
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Process process1 = new Process();
            process1.StartInfo.FileName = "cmd.exe";
            process1.StartInfo.Arguments = @"/c cd "+ txtDuong_dan.Text + @" && adb push DCIM /sdcard";
            process1.StartInfo.Arguments = @"/c cd " + txtDuong_dan.Text + @" && adb push Pictures /sdcard";
            process1.StartInfo.UseShellExecute=false;
            process1.StartInfo.RedirectStandardInput = true;
            process1.StartInfo.RedirectStandardOutput = true;
            process1.StartInfo.CreateNoWindow = true;
            process1.Start();
            // thực hiện cài đặt các app bằng cách cài đặt các gói .apk
            Thread.Sleep(TimeSpan.FromSeconds(1));
            string filePath = txtDuong_dan.Text + @"\Text.txt";
            if (File.Exists(filePath))
            {
                string[] readText = File.ReadAllLines(filePath);
                foreach (string line in readText)
                {
                    int str1 = line.IndexOf("/");
                    int str2 = line.LastIndexOf("/");
                    string str3 = line.Substring(str1, str2 - 8);
                    string str = line.Substring(str2 + 10);
                    string ab = txtDuong_dan.Text + @"\" + str;
                    string[] ac = Directory.GetFiles(ab, "*.apk", SearchOption.AllDirectories);
                    foreach(string ad in ac)
                    {
                        Process process2 = new Process();
                        process2.StartInfo.FileName = "cmd.exe";
                        process2.StartInfo.Arguments = @"/c adb install -multiple "+ ad;
                        process2.StartInfo.UseShellExecute=false;
                        process2.StartInfo.RedirectStandardInput = true;
                        process2.StartInfo.RedirectStandardOutput = true;
                        process2.StartInfo.CreateNoWindow = true;
                        process2.Start();
                    }
                }
            }
        }
    }
}
