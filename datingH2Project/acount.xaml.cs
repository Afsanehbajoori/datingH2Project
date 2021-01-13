using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using Dapper;
using System.Data.SqlTypes;
using System.Drawing;
using System.Data;

namespace datingH2Project
{
    /// <summary>
    /// Interaction logic for acount.xaml
    /// </summary>
    public partial class acount : Window ,Iusers
    {
        public string gender { get; set; }
        

        public List<userInfo> users = new List<userInfo>();
        public delegate void delAddUser(string un,string fn, string ln, DateTime dob, int ht, int wt, string g);
        public delegate void delDelete(string username);

        public acount()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }



        public void addUser(string un, string fn, string ln, DateTime dob, int ht, int wt, string g)
        {

            using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
            {

                if (sqlCon.State == ConnectionState.Closed)
                sqlCon.Open();
                users.Add(new userInfo { username = un, firstname = fn, lastname = ln, dateofbirth = dob, height = ht, weight = wt, gender = g });
             
                sqlCon.Execute("UserInfoAdd @username ,@firstname , @lastname , @dateofbirth , @height , @weight , @gender", users);


            }

        }

        public void delete(string username)
        {
            using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
            {

                
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("UserDelete", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@username", usernameText.Text);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Your Account deleted successfully.");
                    clear();
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                

            }

        }

        public void exit()
        {
            MainWindow mv = new MainWindow();
            mv.Show();
            this.Close();
        }

        public void clear()
        {
            usernameText.Text = firstnameText.Text = lastnameText.Text = dateofbirth.Text = heightText.Text = weightText.Text = gender = "";
        }


        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                //DataAccess db = new DataAccess();
                //db.InsertUser(firstnameText.Text, lastnameText.Text,Convert.ToDateTime(dateofbirth.Text), Convert.ToInt32(heightText.Text), Convert.ToInt32(weightText.Text), gender);
                if (usernameText.Text == "")
            {
                MessageBox.Show("Enter Your Username, Please");
                usernameText.Focus();
            }
            else if (firstnameText.Text == "")
            {
                MessageBox.Show("Enter Your First Name, Please");
                firstnameText.Focus();
            }
            else if (lastnameText.Text == "")
            {
                MessageBox.Show("Enter Your Last Name, Please");
                lastnameText.Focus();
            }
            else if (dateofbirth.Text == "")
            {
                MessageBox.Show("Enter Your Date Of Birthday, Please");
                dateofbirth.Focus();
            }
            else if (heightText.Text == "")
            {
                MessageBox.Show("Enter Your Height, Please");
                heightText.Focus();
            }
            else if (weightText.Text == "")
            {
                MessageBox.Show("Enter Your Weigth, Please");
                weightText.Focus();
            }
            //else if (gender.Equals == "")
            //{
            //    MessageBox.Show("choose Your Gender, Please");
            //    this.Focus();
            //}
            else
            {
                delAddUser delInsert = addUser;
                delInsert(usernameText.Text, firstnameText.Text, lastnameText.Text, Convert.ToDateTime(dateofbirth.Text), Convert.ToInt32(heightText.Text), Convert.ToInt32(weightText.Text), gender);


                profil pro = new profil();
                pro.Show();
                this.Close();
                clear();
            }

            }
            catch (Exception exp)
            {
                MessageBox.Show("There is an error:" + exp.Message);
            }

        }

        

        private void male_Checked(object sender, RoutedEventArgs e)
        {
            gender = "male";
        }

        private void female_Checked(object sender, RoutedEventArgs e)
        {
            gender = "female";
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

            exit();

        }

        

        private void DeleteAccount_Click(object sender, RoutedEventArgs e)
        {
            if (usernameText.Text == "")
            {
                MessageBox.Show("Enter your username, please");
                usernameText.Focus();
            }
            else
            {

                //clear();
                delDelete deldelt = delete;
                deldelt(usernameText.Text);
            }

        }
    }
}
