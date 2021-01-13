using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Drawing;
using System.Data;
using System.Windows.Controls;
using System.Windows;

namespace datingH2Project
{
    //public class DataAccess

    //{

    //    List<userInfo> users = new List<userInfo>();
    //    public List<userInfo> GetUserInfos(string ln , string fn, string g)
    //    {
        
    //            using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
    //            {
    //                sqlCon.Open();
    //                var output = sqlCon.Query<userInfo>($"select * from tblUserInfo where firstname= '{ln}'and lastname = '{fn}' and gender ='{g}' ").ToList();
    //                return output;

    //            }
           
    //    }
    //    public void InsertUser(string fn, string ln, DateTime dob, int ht,int wt, string g)
    //    {
    //        using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
    //        {
    //            sqlCon.Open();
    //            users.Add(new userInfo { firstname = fn, lastname = ln, dateofbirth = dob, height = ht, weight = wt, gender = g });
    //            sqlCon.Execute("UserInfoAdd @firstname , @lastname , @dateofbirth , @height , @weight , @gender", users);
                
                
    //        }

    //    }

    //    public void updateUser(string fn, string ln, DateTime dob, int ht, int wt, string g)
    //    {
    //        using (SqlConnection sqlCon = new SqlConnection(sql.con("dating")))
    //        {
    //            sqlCon.Open();
    //            users.Add(new userInfo { firstname = fn, lastname = ln, dateofbirth = dob, height = ht, weight = wt, gender = g });
    //            sqlCon.Execute("UserInfoUpdate @firstname , @lastname , @dateofbirth , @height , @weight , @gender", users);


    //        }

    //    }

    //}
}
