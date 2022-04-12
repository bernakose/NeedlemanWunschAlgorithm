using System;
using System.Collections;
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

        int match, mismatch, gap;
        string[] FirstSequence;
        string[] SecondSequence;
        int diziboyutu, diziboyutu1;

        public void degerleriAta()
        {
            if (textBox3.Text == "")
            {
                match = 1;
                textBox3.Text = match.ToString();
            }
            else
            {
                match = Convert.ToInt32(textBox3.Text);
            }

            if (textBox4.Text == "")
            {
                mismatch = -1;
                textBox4.Text = mismatch.ToString();

            }
            else
            {
                mismatch = Convert.ToInt32(textBox4.Text);
            }

            if (textBox5.Text == "")
            {
                gap = -2;
                textBox5.Text = gap.ToString();

            }
            else
            {
                gap = Convert.ToInt32(textBox5.Text);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            try

            {

                if (File.Exists(@"C:\Users\Berna\Desktop\Seq1.txt") && File.Exists(@"C:\Users\Berna\Desktop\Seq2.txt"))

                {

                    FirstSequence = File.ReadAllLines(@"C:\Users\Berna\Desktop\Seq1.txt");
                    textBox1.Text = FirstSequence[1];
                    diziboyutu = Convert.ToInt32(FirstSequence[0]);

                    SecondSequence = File.ReadAllLines(@"C:\Users\Berna\Desktop\Seq2.txt");
                    textBox2.Text = SecondSequence[1];
                    diziboyutu1 = Convert.ToInt32(SecondSequence[0]);

                    //Matrix = new int[FirstSequence.Length + 1, SecondSequence.Length + 1];

                }

                else

                    MessageBox.Show("Dosya Bulunamadı...", "Error");

            }

            catch (Exception ex)

            {

                MessageBox.Show("Hata :" + ex.ToString(), "Error");

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string metin = textBox1.Text;
            string[] dizin1 = new string[diziboyutu];

            string metin2 = textBox2.Text;
            string[] dizin2 = new string[diziboyutu1];

            for (int i = 0; i < metin.Length; i++)
            {
                dizin1[i] = metin[i].ToString();
            }

            for (int i = 0; i < metin2.Length; i++)
            {
                dizin2[i] = metin2[i].ToString();
            }

            DataTable tablo = new DataTable();

            string dizilimRow = " ";
            string header = "";
            tablo.Columns.Add(dizilimRow);
            tablo.Columns.Add(dizilimRow + dizilimRow);

            DataRow row1 = tablo.NewRow();
            tablo.Rows.Add(row1);
            tablo.Rows.Add(dizilimRow);

            dataGridView1.DataSource = tablo;

            for (int i = 0; i < dizin1.Length; i++)
            {
                tablo.Columns.Add(header);
                header += header;
            }

            for (int i = 0; i < dizin2.Length; i++)//aşağı doğru olanlar
            {
                DataRow row = tablo.NewRow();
                row[dizilimRow] = dizin2[i];
                tablo.Rows.Add(row);
                dataGridView1.DataSource = tablo;
            }

            for (int i = 1; i < dizin1.Length + 1; i++)
            {
                dataGridView1.Rows[0].Cells[i + 1].Value = dizin1[i - 1];
            }

            dataGridView1.Rows[1].Cells[1].Value = 0;

            degerleriAta();
            hizala(dizin1, dizin2);
            score(dizin1, dizin2);
        }

        public int dizilimKarsilastirma(int i, int j)
        {
            int match = Convert.ToInt32(textBox3.Text);
            int mismatch = Convert.ToInt32(textBox4.Text);

            int sonuc = 0;

            if (String.Compare(dataGridView1.Rows[0].Cells[j + 1].Value.ToString(), dataGridView1.Rows[i + 1].Cells[0].Value.ToString()) == 0)
            {
                sonuc = match;
            }
            else
            {
                sonuc = mismatch;
            }

            return sonuc;
        }

        void hizala(string[] dizin1, string[] dizin2)
        {

            int gap = Convert.ToInt32(textBox5.Text);
            int t1 = 0, t2 = 0, t3 = 0;
            Random rs = new Random(1);

            for (int j = 1; j < dizin2.Length + 2; j++)//cell
            {
                for (int i = 1; i < dizin1.Length + 2; i++)//row
                {

                    if (i == 1 && j == 1)
                    {

                    }
                    else if (i - 1 >= 1 && j - 1 >= 1)
                    {
                        int karsilastirma = dizilimKarsilastirma(i - 1, j - 1);

                        int parca1 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value);
                        t1 = karsilastirma + parca1;
                        int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                        t2 = gap + parca2;
                        int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                        t3 = gap + parca3;
                    }
                    else if (i - 1 >= 1 && j >= 1)
                    {
                        int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                        t2 = gap + parca2;
                        t1 = rs.Next(-50, t2);
                        t3 = rs.Next(-50, t2);
                    }
                    else if (i >= 1 && j - 1 >= 1)
                    {
                        int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                        t3 = gap + parca3;
                        t1 = rs.Next(-50, t3);
                        t2 = rs.Next(-50, t3);
                    }

                    int sonucc = islemlerSonuc(t1, t2, t3);
                    dataGridView1.Rows[i].Cells[j].Value = sonucc;
                }

            }
        }

        public int islemlerSonuc(int formul1, int formul2, int formul3)
        {
            int enbuyuk = formul1;
            int sonuc = formul1;//geçici değişken atıyoruz

            if (formul1 > formul2 && formul1 > formul3)
            {
                enbuyuk = formul1;
            }
            else if (formul2 > formul3)
            {
                enbuyuk = formul2;
            }
            else if (formul3 > formul2)
            {
                enbuyuk = formul3;
            }
            sonuc = enbuyuk;
            return sonuc;
        }

        void score(string[] dizin1, string[] dizin2)
        {
            int i = (dizin1.Length) + 1;
            int j = (dizin2.Length) + 1;

            ArrayList iDegerleri = new ArrayList();
            ArrayList jDegerleri = new ArrayList();
            ArrayList komsular = new ArrayList();

            int ilkDeger = Convert.ToInt32(dataGridView1.Rows[j].Cells[i].Value);
            dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.MediumPurple;
            int sonDeger = Convert.ToInt32(dataGridView1.Rows[1].Cells[1].Value);
            dataGridView1.Rows[1].Cells[1].Style.BackColor = Color.MediumPurple;

            iDegerleri.Add(i);
            jDegerleri.Add(j);
            komsular.Add(ilkDeger);
            komsular.Add(sonDeger);

            int komsuDeger1, komsuDeger2, komsuDeger3;
            int karsilastirma = 0;

            while (i > 1 && j > 1)
            {
                komsuDeger1 = Convert.ToInt32(dataGridView1.Rows[j].Cells[i - 1].Value);
                komsuDeger2 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i - 1].Value);
                komsuDeger3 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i].Value);

                karsilastirma = dizilimKarsilastirma(j - 1, i - 1);

                if (karsilastirma == 1)
                {
                    j = j - 1;
                    i = i - 1;
                    iDegerleri.Add(i);
                    jDegerleri.Add(j);
                    komsular.Add(komsuDeger2);
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.MediumPurple;
                }
                else if (karsilastirma == -1)
                {
                    int enBuyukKomsu = enBuyukKomsuyuBul(komsuDeger1, komsuDeger2, komsuDeger3);

                    if (enBuyukKomsu == komsuDeger2)
                    {
                        j = j - 1;
                        i = i - 1;
                        iDegerleri.Add(i);
                        jDegerleri.Add(j);
                        komsular.Add(komsuDeger2);
                        dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.MediumPurple;
                    }

                    else if (enBuyukKomsu == komsuDeger1)
                    {
                        i = i - 1;
                        iDegerleri.Add(i);
                        jDegerleri.Add(j);
                        komsular.Add(komsuDeger1);
                        dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.MediumPurple;
                    }
                    else if (enBuyukKomsu == komsuDeger3)
                    {
                        j = j - 1;
                        iDegerleri.Add(i);
                        jDegerleri.Add(j);
                        komsular.Add(komsuDeger3);
                        dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.MediumPurple;
                    }
                }
            }
            dizilimiYap(iDegerleri, jDegerleri);
        }


        public int enBuyukKomsuyuBul(int komsu1, int komsu2, int komsu3)
        {
            int geciciEnBuyuk = komsu1;
            int enBuyuk = komsu1;//geçici değişken atıyoruz

            if (komsu1 > komsu2 && komsu1 > komsu3)
            {
                geciciEnBuyuk = komsu1;
            }
            else if (komsu2 > komsu1 && komsu2 > komsu3)
            {
                geciciEnBuyuk = komsu2;
            }
            else if (komsu3 > komsu2 && komsu3 > komsu1)
            {
                geciciEnBuyuk = komsu3;
            }
            enBuyuk = geciciEnBuyuk;
            return enBuyuk;
        }

        void dizilimiYap(ArrayList liste1, ArrayList liste2)
        {
            ArrayList dizilim1 = new ArrayList();
            ArrayList dizilim2 = new ArrayList();

            for (int a = liste1.Count - 1; a >= 0; a--)//i=listelerde de gezienen indis değeri
            {
                int i = Convert.ToInt32(liste1[a]);
                int j = Convert.ToInt32(liste2[a]);
                dizilim1.Add(dataGridView1.Rows[0].Cells[i].Value.ToString());
                dizilim2.Add(dataGridView1.Rows[j].Cells[0].Value.ToString());

            }

            foreach (var item in dizilim1)
            {
                textBox6.Text += item.ToString();
            }

            foreach (var item in dizilim2)
            {
                textBox7.Text += item.ToString();
            }
        }
    }
}
