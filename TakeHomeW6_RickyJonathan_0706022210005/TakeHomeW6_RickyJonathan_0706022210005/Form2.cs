using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TakeHomeW6_RickyJonathan_0706022210005
{
    public partial class Form2 : Form
    {
        string keyboard = "Q,W,E,R,T,Y,U,I,O,P,A,S,D,F,G,H,J,K,L,ENTER,Z,X,C,V,B,N,M,DELETE";
        string tebak = "";
        int baris = 0;
        int kolom = -1;
        int coba = 0;
        public bool win = false;
        string dirandom = "";
        Random random = new Random();
        List<string> listo = new List<string>();
        List<Button> buttonlist = new List<Button>();
        List<Button> keyboardd = new List<Button>();
        public Form2()
        {
            InitializeComponent();
        }

        int input;
        private void Form2_Load(object sender, EventArgs e)
        {
            string[] qwerty = keyboard.Split(new char[] { ',' });
            string[] words = System.IO.File.ReadAllLines(@"C:\Users\KIMBERLY\Downloads\wordlist.txt");
            string[] kata = words.SelectMany(line => line.Split(',')).ToArray();
            listo = kata.ToList();
            dirandom = listo[random.Next(listo.Count)];
            input = Form1.n;
            MessageBox.Show(dirandom);
            for (int i = 0; i < input; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(50, 50);
                    button.Location = new Point(50 * j + 60, 50 * i + 30);
                    button.Tag = i.ToString() + "," + j.ToString();
                    button.Click += new EventHandler(keyboard_Click);
                    buttonlist.Add(button);
                    Controls.Add(buttonlist[buttonlist.Count - 1]);
                }
            }
            for (int j = 0; j < qwerty.Length; j++)
            {
                Button button = new Button();
                button.Text = qwerty[j];
                
                button.BackColor = Color.LightGray;
                button.Size = (j == 19 || j == 27) ? new Size(60, 40) : new Size(40, 40);
                int x = 350 + ((j % 10) + 1) * 40;
                int y = 100 + (((j / 10) + 1) * 40);
                if (j == 19)
                {
                    x -= 10;
                }
                button.Location = new Point(x, y);
                button.Visible = true;
                button.BackColor = Color.White;
                button.Click += keyboard_Click;
                keyboardd.Add(button);
                this.Controls.Add(button);
            }
        }
        private void keyboard_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Text == "DELETE")
            {
                for (int i = 4; i >= 0; i--)
                {
                    int a = baris * 5 + i;
                    if (buttonlist[a].Text != "")
                    {
                        buttonlist[a].Text = "";
                        if (i < kolom)
                        {
                            kolom = i;
                        }
                        break;
                    }
                }
                tebak = "";
                for (int j = 0; j < 5; j++)
                {
                    int a = baris * 5 + j;
                    if (buttonlist[a].Text != "")
                    {
                        tebak += buttonlist[a].Text;
                    }
                }
               
            }
            else if (btn != null && btn.Text == "ENTER")
            {
                if (tebak.Length < 5)
                {
                    MessageBox.Show("ISI SAMPAI FULL DULU BANG", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    if (listo.Contains(tebak.ToLower()))
                    {
                        int hurufbenar = 0;
                        for (int i = 0; i < 5; i++)
                        {
                            if (dirandom[i] == tebak.ToLower()[i])
                            {
                                buttonlist[baris * 5 + i].BackColor = Color.DarkSeaGreen;
                                hurufbenar++;
                            }
                            else if (dirandom.Contains(tebak.ToLower()[i]))
                            {
                                buttonlist[baris * 5 + i].BackColor = Color.LightYellow;
                            }
                            else
                            {
                                buttonlist[baris * 5 + i].BackColor = Color.LightGray;
                            }
                        }
                        if (hurufbenar == 5)
                        {
                            MessageBox.Show("You Win!!! The Word is : "+ dirandom.ToUpper());
                            win = true;
                        }
                        else
                        {
                            coba++;
                            tebak = "";
                            win = false;
                        }

                        if (coba == input)
                        {
                            MessageBox.Show("You lose! The word is: " + dirandom.ToUpper());
                        }
                        baris++;
                        kolom++;
                    }
                    else
                    {
                        MessageBox.Show("The word "+tebak+ " not in the word list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        kolom = -1;
                    }
                }

            }
            else
            {
                if (tebak.Length < 5)
                {
                    foreach (Button button in buttonlist)
                    {
                        if (button.Text == "")
                        {
                            button.Text = btn.Text;
                            tebak += btn.Text;
                            int index = buttonlist.IndexOf(button);
                            int row = index / 5;
                            int col = index % 5;
                            break;
                        }
                    }
                }
            }
        }
    }
}

