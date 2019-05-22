using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace qrcode_gen
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            /*
            textBox4.Text = "00";
            textBox5.Text = "00";
            textBox6.Text = "00";
            textBox10.Text = "00";
            textBox11.Text = "00";
            textBox12.Text = "0a";
            //webKitBrowser1.Navigate("http://localhost/qrcode_gen/");
            //webKitBrowser1.Url = new Uri(String.Format("file:///{0}/script/index.html", Application.StartupPath));
            */
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            webKitBrowser1.ShowPrintDialog();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            webKitBrowser1.ShowPrintPreviewDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox4.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string local_Path = Application.StartupPath;
            string save_folder = "\\script";

            //richTextBox1.AppendText(local_Path + save_folder);
            //richTextBox1.AppendText("\n");
            //richTextBox1.AppendText(user_define_mac);
            //richTextBox1.AppendText("\n");

            if (textBox4.TextLength == textBox5.TextLength && textBox5.TextLength == textBox6.TextLength && textBox6.TextLength == textBox10.TextLength
                 && textBox10.TextLength == textBox11.TextLength && textBox11.TextLength == textBox12.TextLength && textBox12.TextLength == 2)
            {
                //MessageBox.Show("ok!");
            }
            else {

                MessageBox.Show("ERROR Input !");
                return;
            }
            
            textBox4.Text = textBox4.Text.ToUpper();
            textBox5.Text = textBox5.Text.ToUpper();
            textBox6.Text = textBox6.Text.ToUpper();
            textBox10.Text = textBox10.Text.ToUpper();
            textBox11.Text = textBox11.Text.ToUpper();
            textBox12.Text = textBox12.Text.ToUpper();

            string MAC_start = textBox1.Text + ":" + textBox2.Text + ":" + textBox3.Text + ":" + textBox4.Text + ":" + textBox5.Text + ":" + textBox6.Text;
            string MAC_end = textBox7.Text + ":" + textBox8.Text + ":" + textBox9.Text + ":" + textBox10.Text + ":" + textBox11.Text + ":" + textBox12.Text;
            string pattrn = @"(^[0-9a-fA-F][0-9a-fA-F]:[0-9a-fA-F][0-9a-fA-F]:[0-9a-fA-F][0-9a-fA-F]:[0-9a-fA-F][0-9a-fA-F]:[0-9a-fA-F][0-9a-fA-F]:[0-9a-fA-F][0-9a-fA-F]$)";
            
            int intMAC_start = Convert.ToInt32(textBox4.Text+textBox5.Text+textBox6.Text, 16);
            int intMAC_end = Convert.ToInt32(textBox10.Text+textBox11.Text+textBox12.Text, 16);
            
            if (!System.Text.RegularExpressions.Regex.IsMatch(MAC_start, pattrn) || !System.Text.RegularExpressions.Regex.IsMatch(MAC_end, pattrn))
            {
                MessageBox.Show("ERROR : MAC  Addr Format Error!");
            }
            else if (intMAC_end < intMAC_start)
            {
                MessageBox.Show("ERROR : End of MAC Addr is smaller than Start MAC Addr !!!");
            }
            else
            {
#if DEBUG
                int print_mac_num = intMAC_end - intMAC_start + 1;
                string mac_array_first_bracket = "var define_mac_arr = [";
                string mac_array_end_bracket = "];";
                string mac_array_data;

                if (print_mac_num > 2000) {
                    MessageBox.Show("ERROR: 太多啦~~ 共 " + print_mac_num.ToString() + " 個\n最大 2000 個 ~~~");
                    return;
                }

                //MessageBox.Show(print_mac_num.ToString());
                mac_array_data = "'";
                for (int i = 0; i < print_mac_num; i++) {
                    mac_array_data += "000C15" + (intMAC_start + i).ToString("X" + 6.ToString());
                    if(i != (print_mac_num-1)){
                        mac_array_data += "', '";
                    }
                }
                mac_array_data += "'";
                string user_define_mac = mac_array_first_bracket + mac_array_data + mac_array_end_bracket;
                //MessageBox.Show(user_define_mac);


                richTextBox1.Clear();
                richTextBox1.AppendText(MAC_start + " ~ " + MAC_end);
                // 使用 true 會導致寫入檔案位置是從檔案結尾開始寫，累加的概念
                //using (StreamWriter sw = new StreamWriter(Path.Combine(local_Path + save_folder, "user_define.js"), true))
                using (StreamWriter sw = new StreamWriter(Path.Combine(local_Path + save_folder, "user_define.js"), false))
                {
                    sw.WriteLine(user_define_mac);
                }
                webKitBrowser1.Url = new Uri(String.Format("file:///{0}/script/index.html", Application.StartupPath));
#endif
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string pattrn = @"(^[0-9a-fA-F][0-9a-fA-F]$)";
            if (textBox4.TextLength == 2)
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox4.Text, pattrn))
                {
                    MessageBox.Show("ERROR : xx:xx:xx:OO:xx:xx Format Error!");
                }
                else {
                    textBox5.Focus();   
                }
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            string pattrn = @"(^[0-9a-fA-F][0-9a-fA-F]$)";
            if ((textBox5.TextLength == 2))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox5.Text, pattrn))
                {
                    MessageBox.Show("ERROR : xx:xx:xx:xx:OO:xx Format Error!");
                }
                else {
                    textBox6.Focus();  
                }
            }
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            string pattrn = @"(^[0-9a-fA-F][0-9a-fA-F]$)";
            if ((textBox6.TextLength == 2))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox6.Text, pattrn))
                {
                    MessageBox.Show("ERROR : xx:xx:xx:xx:xx:OO Format Error!");
                }
                else {
                    textBox10.Focus(); 
                }
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            string pattrn = @"(^[0-9a-fA-F][0-9a-fA-F]$)";
            if ((textBox10.TextLength == 2))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox10.Text, pattrn)) {
                    MessageBox.Show("ERROR : xx:xx:xx:OO:xx:xx Format Error!");
                }
                else{
                    textBox11.Focus(); 
                }
            }
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            string pattrn = @"(^[0-9a-fA-F][0-9a-fA-F]$)";
            if ((textBox11.TextLength == 2))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox11.Text, pattrn))
                {
                    MessageBox.Show("ERROR : xx:xx:xx:xx:OO:xx Format Error!");
                }
                else {
                    textBox12.Focus(); 
                }
            }
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            string pattrn = @"(^[0-9a-fA-F][0-9a-fA-F]$)";
            if ((textBox12.TextLength == 2))
            {
                if (!System.Text.RegularExpressions.Regex.IsMatch(textBox12.Text, pattrn))
                {
                    MessageBox.Show("ERROR : xx:xx:xx:xx:xx:OO Format Error!");
                }
                else {
                    button1.Focus();
                }
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }



    }
}
