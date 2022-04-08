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

        int match, mismatch, gap;
        string[] FirstSequence;
        string[] SecondSequence;
        int[,] Matrix;
        int diziboyutu, diziboyutu1;
        List<List<Trace>> BackTraces;

        private void Form1_Load(object sender, EventArgs e)
        {            

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

                    Matrix = new int[FirstSequence.Length + 1, SecondSequence.Length + 1];

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
                listBox1.Items.Add(dizin1[i]);
            }

            for (int i = 0; i < metin2.Length; i++)
            {
                dizin2[i] = metin2[i].ToString();
                listBox1.Items.Add("--" + dizin2[i]);

            }


            DataTable tablo = new DataTable();
            tablo.Columns.Add(" ");
            tablo.Rows.Add(" ");

            for (int i = 0; i < dizin1.Length; i++)
            {

                DataRow row1 = tablo.NewRow();
                tablo.Columns.Add(dizin1[i]);
                
                row1[" "] = dizin2[i];
                tablo.Rows.Add(row1);
            }

            dataGridView1.DataSource = tablo;

            
        }




        public void FillMatrix()
        {
            for (int i = 0; i < Matrix.GetLength(0); i++)
            {
                Matrix[i, 0] = i * gap;
            }

            for (int j = 0; j < Matrix.GetLength(1); j++)
            {
                Matrix[0, j] = j * gap;
            }


            for (int i = 1; i < Matrix.GetLength(0); i++)
            {
                for (int j = 1; j < Matrix.GetLength(1); j++)
                {
                    int topValue = Matrix[i - 1, j] + gap;
                    int leftValue = Matrix[i, j - 1] + gap;
                    int diagonalValue = Matrix[i - 1, j - 1] + (FirstSequence[i - 1] == SecondSequence[j - 1] ? match : mismatch);
                    Matrix[i, j] = Math.Max(Math.Max(topValue, leftValue), diagonalValue);
                }
            }
        }

        //public void TraceBack(List<Trace> traces)
        //{
        //    // Continue tracing until Matrix[1,1], Matrix[1,0] or Matrix[0,1]
        //    while (!((traces.Last().RowIndex == 1 && traces.Last().ColIndex == 1) ||
        //             (traces.Last().RowIndex == 1 && traces.Last().ColIndex == 0) ||
        //             (traces.Last().RowIndex == 0 && traces.Last().ColIndex == 1)))
        //    {
        //        bool isSourceTop = false;
        //        bool isSourceLeft = false;
        //        bool isSourceDiagonal = false;

        //        int topValue = Matrix[traces.Last().RowIndex - 1, traces.Last().ColIndex] + gap;
        //        int leftValue = Matrix[traces.Last().RowIndex, traces.Last().ColIndex - 1] + gap;
        //        int diagonalValue = Matrix[traces.Last().RowIndex - 1, traces.Last().ColIndex - 1] +
        //                            (FirstSequence[traces.Last().RowIndex - 1] ==
        //                             SecondSequence[traces.Last().ColIndex - 1]
        //                                ? match
        //                                : mismatch);

        //        // Set flags
        //        if (topValue == Matrix[traces.Last().RowIndex, traces.Last().ColIndex])
        //        {
        //            isSourceTop = true;
        //        }

        //        if (leftValue == Matrix[traces.Last().RowIndex, traces.Last().ColIndex])
        //        {
        //            isSourceLeft = true;
        //        }

        //        if (diagonalValue == Matrix[traces.Last().RowIndex, traces.Last().ColIndex])
        //        {
        //            isSourceDiagonal = true;
        //        }

        //        // Handle all possibilities, there might be alternative traces
        //        // If there such trace exists, handle it as different traceback recursively.
        //        if (isSourceTop && isSourceLeft && isSourceDiagonal)
        //        {
        //            var tempTrace = new List<Trace>(traces);

        //            //top condition
        //            tempTrace.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex - 1,
        //                ColIndex = traces.Last().ColIndex
        //            });
        //            TraceBack(tempTrace); //recursive call

        //            //left condition
        //            tempTrace = new List<Trace>(traces);
        //            tempTrace.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex,
        //                ColIndex = traces.Last().ColIndex - 1
        //            });
        //            TraceBack(tempTrace);
        //            traces.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex - 1,
        //                ColIndex = traces.Last().ColIndex - 1
        //            });
        //        }
        //        else if (isSourceTop && isSourceLeft)
        //        {
        //            var tempTrace = new List<Trace>(traces);

        //            //top condition
        //            tempTrace.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex - 1,
        //                ColIndex = traces.Last().ColIndex
        //            });
        //            TraceBack(tempTrace);

        //            //left condition
        //            traces.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex,
        //                ColIndex = traces.Last().ColIndex - 1
        //            });
        //        }
        //        else if (isSourceTop && isSourceDiagonal)
        //        {
        //            var tempTrace = new List<Trace>(traces);

        //            //top condition
        //            tempTrace.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex - 1,
        //                ColIndex = traces.Last().ColIndex
        //            });
        //            TraceBack(tempTrace);

        //            //diagonal condition
        //            traces.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex - 1,
        //                ColIndex = traces.Last().ColIndex - 1
        //            });
        //        }
        //        else if (isSourceLeft && isSourceDiagonal)
        //        {
        //            var tempTrace = new List<Trace>(traces);

        //            //left condition
        //            tempTrace.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex,
        //                ColIndex = traces.Last().ColIndex - 1
        //            });
        //            TraceBack(tempTrace);

        //            //diagonal condition
        //            traces.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex - 1,
        //                ColIndex = traces.Last().ColIndex - 1
        //            });
        //        }
        //        else if (isSourceTop)
        //        {
        //            traces.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex - 1,
        //                ColIndex = traces.Last().ColIndex
        //            });
        //        }
        //        else if (isSourceLeft)
        //        {
        //            traces.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex,
        //                ColIndex = traces.Last().ColIndex - 1
        //            });
        //        }
        //        else
        //        {
        //            traces.Add(new Trace
        //            {
        //                RowIndex = traces.Last().RowIndex - 1,
        //                ColIndex = traces.Last().ColIndex - 1
        //            });
        //        }
        //    }

        //    traces.Add(new Trace { RowIndex = 0, ColIndex = 0 });
        //    BackTraces.Add(traces);
        //}
        public class Trace
        {
            public int RowIndex { get; set; }
            public int ColIndex { get; set; }
        }


    }
}
