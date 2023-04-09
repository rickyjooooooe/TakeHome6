using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TakeHomeW6_RickyJonathan_0706022210005
{
    public partial class Form1 : Form
    {
        public static int n;
        public Form1()
        {
            InitializeComponent();
        }
        private void textboxint_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            textboxint.KeyPress += new KeyPressEventHandler(textboxint_KeyPress);
        }

        private void textboxint_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonplay_Click(object sender, EventArgs e)
        {
            n = Convert.ToInt32(textboxint.Text);
            if (n < 3)
            {
                MessageBox.Show("HARUS LEBIH DARI 3", "ERROR");
                textboxint.Text = "";
            }
            else
            {
                Form2 form2 = new Form2();
                form2.Show();
            }
        }
    }
}
