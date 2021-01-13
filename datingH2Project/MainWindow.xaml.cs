using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace datingH2Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        
        
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public void clear()
        {
            usernameText.Text = passwordText.Password = confirmpasswordText.Password = "";
        }

        private void signupButton_Click(object sender, RoutedEventArgs e)
        {
            
            
                if (usernameText.Text == "" || passwordText.Password == "")
                    MessageBox.Show("please fill username and password");
                else if (passwordText.Password != confirmpasswordText.Password)
                    MessageBox.Show("password is not matched!");

                else
                {
                    using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
                    {
                        
                        sqlCon.Open();
                        bool exists = false;
                        using (SqlCommand cmd = new SqlCommand("select count(*) from TblUser where username = @username", sqlCon))
                        {
                            cmd.Parameters.AddWithValue("username", usernameText.Text);
                            exists = (int)cmd.ExecuteScalar() > 0;
                        }
                        if (exists)
                            MessageBox.Show("This username has been using by another user.");
                        else
                        {
                            using (SqlConnection sqlConn = new SqlConnection(sql.con("dating")))
                            {
                               
                                sqlConn.Open();
                                SqlCommand sqlCmd = new SqlCommand("UserAdd", sqlConn);
                                sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                                sqlCmd.Parameters.AddWithValue("@id", 0);
                                sqlCmd.Parameters.AddWithValue("@username", usernameText.Text.Trim());
                                sqlCmd.Parameters.AddWithValue("@password", passwordText.Password.Trim());
                                sqlCmd.ExecuteNonQuery();
                                acount aco = new acount();
                                aco.Show();
                                this.Close();
                                clear();
                            }
                        }
                    }
                }   
           
        }
            

            private void signinButton_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                if (usernameText.Text == "" || passwordText.Password == "")
                    MessageBox.Show("please fill username and password");
                else if (passwordText.Password != confirmpasswordText.Password)
                    MessageBox.Show("password is not matched!");

                else
                {
                    using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
                    {
                        //if (sqlCon.State == ConnectionState.Closed) 
                        sqlCon.Open();
                        string query = "select * from TblUser where username ='" + usernameText.Text.Trim() + "' and password = '" + passwordText.Password.Trim() + "' ";
                        SqlDataAdapter sda = new SqlDataAdapter(query, sqlCon);
                        DataTable dtbl = new DataTable();
                        sda.Fill(dtbl);
                        if (dtbl.Rows.Count > 0)
                        {

                            profil pro = new profil();
                            pro.Show();
                            this.Close();
                            clear();
                        }
                        else
                        {
                            MessageBox.Show("check your username and password!");
                        }

                    }

                }

            }
            catch(Exception exp)
            {
                MessageBox.Show("There is an error:" + exp.Message);
            }
           
            
                
         }
    }
}

