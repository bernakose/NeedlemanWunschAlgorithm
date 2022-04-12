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
        int counter=0;

        public void degerleriAta()
        {
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
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

            textBox3.Text = match.ToString();
            textBox4.Text = mismatch.ToString();
            textBox5.Text = gap.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
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
            tabloyuDoldur(dizin1, dizin2);

            timer1.Stop();
        }

        public int sequenceKarsilastir(int i, int j)
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
            int f1 = 0, f2 = 0, f3 = 0;
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
                        int karsilastirma = sequenceKarsilastir(i - 1, j - 1);

                        int parca1 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j - 1].Value);
                        f1 = karsilastirma + parca1;
                        int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                        f2 = gap + parca2;
                        int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                        f3 = gap + parca3;
                    }
                    else if (i - 1 >= 1 && j >= 1)
                    {
                        int parca2 = Convert.ToInt32(dataGridView1.Rows[i - 1].Cells[j].Value);
                        f2 = gap + parca2;
                        f1 = rs.Next(-50, f2);
                        f3 = rs.Next(-50, f2);
                    }
                    else if (i >= 1 && j - 1 >= 1)
                    {
                        int parca3 = Convert.ToInt32(dataGridView1.Rows[i].Cells[j - 1].Value);
                        f3 = gap + parca3;
                        f1 = rs.Next(-50, f3);
                        f2 = rs.Next(-50, f3);
                    }                   

                    int sonuc1 = islemSonuc(f1, f2, f3);
                    dataGridView1.Rows[i].Cells[j].Value = sonuc1;
                }
            }
        }

        public int islemSonuc(int formul1, int formul2, int formul3)
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            label10.Text = "Run Time: " + counter.ToString();
        }

        void tabloyuDoldur(string[] dizin1, string[] dizin2)
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

            int komsu1, komsu2, komsu3;
            int karsilastir = 0;

            while (i > 1 && j > 1)
            {
                komsu1 = Convert.ToInt32(dataGridView1.Rows[j].Cells[i - 1].Value);
                komsu2 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i - 1].Value);
                komsu3 = Convert.ToInt32(dataGridView1.Rows[j - 1].Cells[i].Value);

                karsilastir = sequenceKarsilastir(j - 1, i - 1);

                if (karsilastir == 1)
                {
                    j = j - 1;
                    i = i - 1;
                    iDegerleri.Add(i);
                    jDegerleri.Add(j);
                    komsular.Add(komsu2);
                    dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.MediumPurple;
                }
                else if (karsilastir == -1)
                {
                    int enBuyukKomsu = enBuyukKomsuBul(komsu1, komsu2, komsu3);

                    if (enBuyukKomsu == komsu2)
                    {
                        j = j - 1;
                        i = i - 1;
                        iDegerleri.Add(i);
                        jDegerleri.Add(j);
                        komsular.Add(komsu2);
                        dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.MediumPurple;
                    }

                    else if (enBuyukKomsu == komsu1)
                    {
                        i = i - 1;
                        iDegerleri.Add(i);
                        jDegerleri.Add(j);
                        komsular.Add(komsu1);
                        dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.MediumPurple;
                    }
                    else if (enBuyukKomsu == komsu3)
                    {
                        j = j - 1;
                        iDegerleri.Add(i);
                        jDegerleri.Add(j);
                        komsular.Add(komsu3);
                        dataGridView1.Rows[j].Cells[i].Style.BackColor = Color.MediumPurple;
                    }
                }
            }
            seqDiziliminiYap(iDegerleri, jDegerleri);
        }

        public int enBuyukKomsuBul(int komsu1, int komsu2, int komsu3)
        {
            int gEnBuyuk = komsu1;
            int enBuyuk = komsu1;//geçici değişken atıyoruz

            if (komsu1 > komsu2 && komsu1 > komsu3)
            {
                gEnBuyuk = komsu1;
            }
            else if (komsu2 > komsu1 && komsu2 > komsu3)
            {
                gEnBuyuk = komsu2;
            }
            else if (komsu3 > komsu2 && komsu3 > komsu1)
            {
                gEnBuyuk = komsu3;
            }
            enBuyuk = gEnBuyuk;
            return enBuyuk;
        }

        void seqDiziliminiYap(ArrayList liste1, ArrayList liste2)
        {
            ArrayList dizilim1 = new ArrayList();
            ArrayList dizilim2 = new ArrayList();

            for (int a = liste1.Count - 1; a >= 0; a--)//i=listelerde de gezienen indis değeri
            {
                int i = Convert.ToInt32(liste1[a]);
                int j = Convert.ToInt32(liste2[a]);

                if (dataGridView1.Rows[0].Cells[i].Value.ToString() == "")
                {
                    dizilim1.Add("--");
                }
                else if (dataGridView1.Rows[j].Cells[0].Value.ToString() == "")
                {
                    dizilim2.Add("--");
                }
                else if (Convert.ToInt32(liste1[a]) == Convert.ToInt32(liste1[a + 1]))
                {
                    dizilim1.Add("--");
                }
                else if (Convert.ToInt32(liste2[a]) == Convert.ToInt32(liste2[a + 1]))
                {
                    dizilim2.Add("--");
                }
                else
                {
                    dizilim1.Add(dataGridView1.Rows[0].Cells[i].Value);
                    dizilim2.Add(dataGridView1.Rows[j].Cells[0].Value);
                }
            }

            foreach (var item in dizilim1)
            {
                textBox6.Text += item.ToString();
            }

            foreach (var item in dizilim2)
            {
                textBox7.Text += item.ToString();
            }
            int match = Convert.ToInt32(textBox3.Text);
            int mismatch = Convert.ToInt32(textBox4.Text);
            int gap = Convert.ToInt32(textBox5.Text);
            int skor = 0;

            for (int i = 0; i < dizilim1.Count; i++)
            {
                if (dizilim1[i].ToString() == dizilim2[i].ToString())
                {
                    skor += match;
                }
                else if (dizilim1[i].ToString() != dizilim2[i].ToString())
                {
                    skor += mismatch;
                }
                else if (dizilim1[i].ToString() == "--" || dizilim2[i].ToString() == "--")
                {
                    skor += gap;
                }
            }
            textBox8.Text = skor.ToString();
        }
    }
}
