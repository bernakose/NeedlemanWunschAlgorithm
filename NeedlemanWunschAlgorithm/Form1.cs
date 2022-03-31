using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeedlemanWunschAlgorithm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try

            {

                if (File.Exists(@"C:\Users\Berna\Desktop\Seq1.txt"))   //Öncelikle bu isimde bir dosyanın var olup olmadığını kontrol edelim.

                {

                    string[] line = File.ReadAllLines(@"C:\Users\Berna\Desktop\Seq1.txt");
                    textBox1.Text = line[1];

                }

                if (File.Exists(@"C:\Users\Berna\Desktop\Seq2.txt"))   //Öncelikle bu isimde bir dosyanın var olup olmadığını kontrol edelim.

                {

                    string[] line = File.ReadAllLines(@"C:\Users\Berna\Desktop\Seq2.txt");
                    textBox2.Text = line[1];

                }

                else

                    MessageBox.Show("Dosya Bulunamadı...", "Error");

            }

            catch (Exception ex)

            {

                MessageBox.Show("Hata :" + ex.ToString(), "Error");

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int match, mismatch, gap;
            int[,] Matrix;

            if (textBox3.Text == "")
            {
                match = 1;
            }
            else
            {
                match = Convert.ToInt32(textBox3.Text);
            }

            if (textBox4.Text == "")
            {
                mismatch = -1;
            }
            else
            {
                mismatch = Convert.ToInt32(textBox4.Text);
            }

            if (textBox5.Text == "")
            {
                gap = -2;
            }
            else
            {
                gap = Convert.ToInt32(textBox5.Text);
            }

            Matrix = new int[textBox1.Text.Length + 1, textBox2.Text.Length + 1];

        }
    }
}
