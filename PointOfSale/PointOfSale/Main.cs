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
    public partial class Main : Form
    {
        string filename;
        StreamReader objReader;
        StreamWriter objWriter;
        private Font verdana10Font;
        private StreamReader reader;
        private String roomType;
        private String totalCost;
        private String durationStay;
        public double totalPrice;
        

        //Reading the files from system

        public Main()
        {
            InitializeComponent();
        }

        //Calculating price times how many nights staying

        void payCalulator(int hours, double rate, double discount)
        {
            double pay = 0.0;
            pay = hours * rate - discount;
            totalPrice = pay;
            MessageBox.Show("Your Total is: " + pay.ToString("C"));
            tbTotal.Text = pay.ToString("C") + roomType + durationStay;

        }

        //Print dialog information 

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

        //Clear the text area fields button 

        private void button1_Click(object sender, EventArgs e)
        {
            tbPrice.Text = String.Empty;
            tbAmount.Text = String.Empty;

        }

        //Images for POS system to load on opening

        private void Main_Load(object sender, EventArgs e)

        {
            pictureBox1.Image = Image.FromFile("H://Images/Room1.jpg");
            pictureBox2.Image = Image.FromFile("H://Images/Room2.jpg");
            pictureBox3.Image = Image.FromFile("H://Images/Room3.jpg");
            pictureBox4.Image = Image.FromFile("H://Images/Room4.jpg");
            pictureBox5.Image = Image.FromFile("H://Images/Logo.jpg");
        }

        private void tbPrice_TextChanged(object sender, EventArgs e)
        {

        }

        //Calculate total button
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            int hours = 0;
            double rate = 0.0;
            double discount = 0.0;
            hours = int.Parse(tbPrice.Text);
            rate = double.Parse(tbAmount.Text);
            discount = double.Parse(tbDiscount.Text);
          
            payCalulator(hours, rate, discount);
        }

        //Save the information to text file button - receipt
        private void btnSave_Click(object sender, EventArgs e)
        {
            String name = DateTime.Now.ToString("HH_mm_ss_dd_MM_yyyy");
            filename = "Booking_DateTime" + name; 
            Console.WriteLine("H:/" + filename + ".txt");
            objWriter = new StreamWriter("H:/" + filename + ".txt");


            objWriter.WriteLine("Room type booked: " + roomType);
            objWriter.WriteLine("Total Cost: " + tbTotal.Text);
            objWriter.WriteLine("Duration of Stay: " + tbAmount.Text + "Days");
            objWriter.Close();
            MessageBox.Show("Recorded");
            objWriter.Close();
            StreamReader sr =new StreamReader("H:/totalSales.txt");
           
            String total = sr.ReadLine();
            Console.WriteLine(total);
            sr.Close();
            objWriter = new StreamWriter("H:/totalSales.txt");
            total = (totalPrice + Double.Parse(total)).ToString();
            objWriter.WriteLine(total);
            objWriter.Close();
        }

        //print the total for customer
        private void btnPrint_Click(object sender, EventArgs e)
        {
            string filename = tbTotal.Text.ToString();
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

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            fdlg.InitialDirectory = @"H:// ";
            fdlg.Filter =
            "Text files (*.txt | .txt | All files (*.*) | *.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                tbTotal.Text = fdlg.FileName;
            }
        }

        //Prices of rooms, will move to calculate area after click

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            roomType = "Family Room";
            tbPrice.Text = "45";
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            roomType = "Single Room";
            tbPrice.Text = "25";
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            roomType = "Double Room";
            tbPrice.Text = "30";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            roomType = "Deluxe Room";
            tbPrice.Text = "60";
        }

        //Logo
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox5.Image = Image.FromFile("H://Images/Logo.jpg");
        }

        //Open sales report form

        private void btnReport_Click(object sender, EventArgs e)
        {
            Sales sa = new Sales();
            this.Hide();
            sa.Show();
        }

        private void tbTotal_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbDiscount_TextChanged(object sender, EventArgs e)
        {

        }

     
    }
}



