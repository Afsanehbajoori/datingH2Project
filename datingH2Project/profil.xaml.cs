using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
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
using System.Data;
using System.Threading;

namespace datingH2Project
{
    /// <summary>
    /// Interaction logic for profil.xaml
    /// </summary>
    public partial class profil : Window , Iusers 
    {
        
        public List<userInfo> users = new List<userInfo>();
        public delegate void delupdateUser(string fn, string ln, DateTime dob, int ht, int wt, string g);
        public delegate List<userInfo> delGetUserInfos(string ln, string fn, string g);
        public delegate void delDelete(string username);
       

        public profil()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        //public List<userInfo> GetUserInfos(string ln, string fn, string g)
        //{

        //    using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
        //    {

        //        sqlCon.Open();
        //        var output = sqlCon.Query<userInfo>($"select * from TblUserInfo where firstname= '{ln}' or lastname = '{fn}' or gender ='{g}' ").ToList();
        //        return output;

        //    }

        //}

        public async Task<List<userInfo>> GetUserInfos(string ln, string fn, string g)
        {
            await Task.Run(() =>
            {
                using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
                {
                  
                    sqlCon.Open();
                    users=sqlCon.Query<userInfo>($"select * from TblUserInfo where firstname= '{ln}' or lastname = '{fn}' or gender ='{g}' ").ToList();
                  
                }
            });
            return users;
        }



        public void updateUser(string fn, string ln, DateTime dob, int ht, int wt, string g)
        {

            if (usernameText.Text == "")
            {
                MessageBox.Show("Enter your username, please");
                usernameText.Focus();
            }
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
                {

                    sqlCon.Open();
                    users.Add(new userInfo { firstname = fn, lastname = ln, dateofbirth = dob, height = ht, weight = wt, gender = g });
                    sqlCon.Execute("UserInfoUpdate @firstname , @lastname , @dateofbirth , @height , @weight , @gender", users);

                    MessageBox.Show("Successful Updating.");
                    clear();

                }
            }


        }


        

        public void delete(string username)
        {
            using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
            {

               
                    sqlCon.Open();
                    SqlCommand sqlCmd = new SqlCommand("UserInfoDelete", sqlCon);
                    sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCmd.Parameters.AddWithValue("@username", usernameText.Text);
                    sqlCmd.ExecuteNonQuery();
                    MessageBox.Show("Your Profile deleted successfully.");
                    clear();
                    acount ac = new acount();
                    ac.Show();
                    this.Close();

                
            }
        }

        public void view(string username)
        {
            if (usernameText.Text == "")
            {
                MessageBox.Show("Enter your username , please");
                usernameText.Focus();
            }
            else
            {
                using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
                {

                    sqlCon.Open();
                    SqlCommand cmd = new SqlCommand("select firstname,lastname,dateOfbirth,height,weight,gender from TblUserInfo where username=@username", sqlCon);
                    cmd.Parameters.AddWithValue("@username", usernameText.Text);
                    SqlDataReader da = cmd.ExecuteReader();
                    while (da.Read())
                    {
                        firstnameText.Text = da.GetValue(0).ToString();
                        lastnameText.Text = da.GetValue(1).ToString();
                        dateofbirthText.Text = da.GetValue(2).ToString();
                        heightText.Text = da.GetValue(3).ToString();
                        weightText.Text = da.GetValue(4).ToString();
                        genderText.Text = da.GetValue(5).ToString();
                    }
                    sqlCon.Close();

                }
            }

        }

        public void view()
        {
            using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
            {

                sqlCon.Open();
                
                SqlCommand cmd = new SqlCommand("select firstname,lastname,dateOfbirth,height,weight,gender from TblUserInfo where firstname = @fn ", sqlCon);
                //where firstname = '" + listofusers.SelectedItem.ToString() + "'
                cmd.Parameters.AddWithValue("@fn", SqlDbType.NVarChar).Value = listofusers.Text;
                SqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    //string tFirstname = da["firstname"].ToString();
                    string tFirstname = da.GetValue(0).ToString();
                    string tLastname = da.GetValue(1).ToString();
                    string tDateofbirth = da.GetValue(2).ToString();
                    string tHeight = da.GetValue(3).ToString();
                    string tWeight = da.GetValue(4).ToString();
                    string tGender = da.GetValue(5).ToString();

                    firstnameText.Text = tFirstname;
                    lastnameText.Text = tLastname;
                    dateofbirthText.Text = tDateofbirth;
                    heightText.Text = tHeight;
                    weightText.Text = tWeight;
                    genderText.Text = tGender;
                }
                sqlCon.Close();

            }

        }


        public void clear()
        {
            usernameText.Text = firstnameText.Text = lastnameText.Text = dateofbirthText.Text = heightText.Text = weightText.Text = genderText.Text = "";
        }

        public void exit()
        {
            MainWindow mv = new MainWindow();
            mv.Show();
            this.Close();
        }



        private  void Edit_Click(object sender, RoutedEventArgs e)
        {
            //DataAccess db = new DataAccess();
            //db.updateUser(firstnameText.Text, lastnameText.Text,Convert.ToDateTime( dateofbirthText.Text),Convert.ToInt32(heightText.Text),Convert.ToInt32( weightText.Text), genderText.Text);

            try {

                delupdateUser delupdate = updateUser;
                delupdate(firstnameText.Text, lastnameText.Text, Convert.ToDateTime(dateofbirthText.Text), Convert.ToInt32(heightText.Text), Convert.ToInt32(weightText.Text), genderText.Text);
            }
            catch
            {
           
               MessageBox.Show("You have to enter all of your information.");
            }
        }



    private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (usernameText.Text == "")
            {
                MessageBox.Show("Enter your username , please");
                usernameText.Focus();
            }
            else
            {
                //clear();
                delDelete deldelt = delete;
                deldelt(usernameText.Text);
            }
          }


        private void Chat_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void Search_Click(object sender, RoutedEventArgs e)
        {
            //DataAccess db = new DataAccess();
            //List<userInfo> users = new List<userInfo>();
            //users = db.GetUserInfos(firstnameText.Text, lastnameText.Text, genderText.Text);

            await GetUserInfos(firstnameText.Text, lastnameText.Text, genderText.Text);

            //delGetUserInfos delGetUser = GetUserInfos;
            //users = delGetUser(firstnameText.Text, lastnameText.Text, genderText.Text);

            listofusers.ItemsSource = users;
            clear();

        }



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            exit();

        }
                  

        private void AddProfile_Click(object sender, RoutedEventArgs e)
        {
            acount ac = new acount();
            ac.Show();
            this.Close();
            clear();

        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            view(usernameText.Text);
            
        }

        private void listofusers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            view();

        }
    }
}
