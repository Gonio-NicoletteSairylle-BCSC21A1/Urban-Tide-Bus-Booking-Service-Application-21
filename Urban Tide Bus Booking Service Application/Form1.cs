using System;
using System.Linq;
using System.Windows.Forms;
using Urban_Tide_Bus_Booking_Service_Application.Manager;

namespace Urban_Tide_Bus_Booking_Service_Application
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();

        }

        // Login button click event handler
        private void button1Login_Click(object sender, EventArgs e)
        {

            var userLoggedIn = UserManager.Instance.AuthenticateUser(textBox1Username.Text, textBox2Password.Text);

            if (userLoggedIn == null)
            {
                MessageBox.Show("Invalid");
            }
            else
            {
                if (userLoggedIn.Roles.Contains("admin"))
                {
                    // Pass userLoggedIn to the constructor
                    Form3AdministratorPage adminPage = new Form3AdministratorPage(userLoggedIn);
                    adminPage.Show();
                    this.Hide();
                }
                else if (userLoggedIn.Roles.Contains("user"))
                {
                    // User login
                    var userMainMenu = new Form2UserMainMenu
                    {
                        Username = userLoggedIn.Username // Pass the username to the form
                    };
                    userMainMenu.Show();
                    this.Hide();
                }
            }
        }

        // Create account button click event
        private void button2guest_Click(object sender, EventArgs e)
        {
            Form4CreateAccount form4 = new Form4CreateAccount();
            form4.Show();
            this.Hide();
        }

        // Forgot password link click event
        private void linkLabel1Forgotuserorpass_Click(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormforgetPass form4 = new FormforgetPass();
            form4.Show();
            this.Hide();
        }

        // Form closing event handler
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        // Form load event handler (optional)
        private void Form1_Load(object sender, EventArgs e)
        {
            // Optional: Any initialization logic
        }
    }
}
