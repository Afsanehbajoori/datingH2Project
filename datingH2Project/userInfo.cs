using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;
using Dapper;

namespace datingH2Project
{
    public class userInfo :user
    {


        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime dateofbirth { get; set; }
        public int height { get; set; }
        public int weight { get; set; }
        public string gender { get; set; }



        public string fullInfo
        {
            get
            {
                return $" { firstname } {lastname}({dateofbirth} -{height} , {weight} ,{gender})  ";


            }
        }




    }
}
