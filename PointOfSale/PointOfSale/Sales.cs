using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;

namespace PointOfSale
{
    public partial class Sales : Form
    {
        string filename = "H://totalSales.txt";
        StreamReader objReader;
        StreamReader objWriter;
        private Font verdana10Font;
        private StreamReader reader;
        private String roomType;
        private String totalCost;
        private String durationStay;
        

        //Reading File names

        public Sales()
        {
            InitializeComponent();
        }

        private void PrintTextFileHandler(object sender, PrintPageEventArgs ppeArgs)
        {
            //Get the Graphics object
            Graphics g = ppeArgs.Graphics;
            float linesPerPage = 0;
            float yPos = 0;
            int count = 0;
            //Read margins from PrintPageEventArgs
            float leftMargin = ppeArgs.MarginBounds.Left;
            float topMargin = ppeArgs.MarginBounds.Top;
            string line = null;
            //Calculate the lines per page on the basis of the height of the page and the height of the font
            linesPerPage = ppeArgs.MarginBounds.Height /
            verdana10Font.GetHeight(g);
            //Read lines one by one, using StreamReader
            while (count < linesPerPage &&
            ((line = reader.ReadLine()) != null))
            {
                //Calculate the starting position
                yPos = topMargin + (count *
                verdana10Font.GetHeight(g));
                //Draw text
                g.DrawString(line, verdana10Font, Brushes.Black,
                leftMargin, yPos, new StringFormat());
                //Move to next line
                count++;
            }
            //If PrintPageEventArgs has more pages to print
            if (line != null)
            {
                ppeArgs.HasMorePages = true;
            }
            else
            {
                ppeArgs.HasMorePages = false;
            }
        }

        //Load reciept information as sales report
        private void Sales_Load(object sender, EventArgs e)

        {
            if (File.Exists(filename) == true)
            {
                //objReader = new StreamReader(filename);
                //listBox1.Text = objReader.ReadLine();
                //objReader.Close();
                string[] lines = File.ReadAllLines("H:/totalSales.txt");
                listBox1.Items.AddRange(lines);
              
            }
//Error message if fies do not exist
            else
            {
                MessageBox.Show("File does not exist");
            }

        }

        private void rtbSales_TextChanged(object sender, EventArgs e)
        {
           

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void btnPrint2_Click(object sender, EventArgs e)
        {
            string filename = listBox1.Text.ToString();
            //Create a StreamReader object
            reader = new StreamReader("H:/totalSales.txt");
            //Create a Verdana font with size 10
            verdana10Font = new Font("Verdana", 10);
            //Create a PrintDocument object
            PrintDocument pd = new PrintDocument();
            //Add PrintPage event handler
            pd.PrintPage += new PrintPageEventHandler(this.PrintTextFileHandler);
            //Call Print Method
            pd.Print();
            //Close the reader
            if (reader != null)
                reader.Close();
        }

        //Browse through files for reciepts to then print 
    }
}

