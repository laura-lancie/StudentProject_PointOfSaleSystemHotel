using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PointOfSale
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        int count; 

        private void LoginForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("H://Images/Logo.jpg");
        }

        //Above the start of 3 attempts error count 
        //Above is the logo image to load on opening

            //The username and passsword string

            //Error message to load if incorrect, if incorrect three times, system will close

        private void btnEnter_Click(object sender, EventArgs e)
        {


            string username, password;

            username = txtUser.Text;
            password = txtPass.Text;


            count = count + 1;

            if (count > 3)
            {
                MessageBox.Show("Too many attempts to login", "Blocked", MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }

            if (username == "Name" && password == "123")
            {
                Main ma = new Main();
                this.Hide();
                ma.Show();
                

            }

            else
            {
                lblError.Text = "Try again";

            }
        }


        //Above once clicked enter and username and password information correct, open Main form

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
         
        }

        //Logo

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = Image.FromFile("H://Images/Logo.jpg");
        }
    }
}
