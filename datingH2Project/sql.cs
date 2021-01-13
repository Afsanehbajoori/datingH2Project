using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datingH2Project
{
    public static class sql
    {
        public static string con(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}

//    use master
//go
//drop database dating
//go
//create database dating
//go
//use dating
//go
//drop table Tbluser
//go

//create table TblUser(
//id int identity(1,1)  ,
//username nvarchar(150) primary key,
//password nvarchar(150)
//)
//go

//drop table TblUserInfo

//create table TblUserInfo(
//id int identity(1,1)  ,
//username nvarchar(150) primary key,(unik)
//foreign key(username) references Tbluser(username),

//firstname nvarchar(150),
//lastname nvarchar(150),
//dateofbirth nvarchar(150),
//height tinyint,
//weight decimal ,
//gender nvarchar(50)

//)

//go



//create index user_first_name_last_name_idx
//on TblUserInfo(firstname , lastname);




//alter proc UserAdd
//@id int,
//@username nvarchar(150),
//@password nvarchar(150)

//as 

//insert into TblUser(username, password) values(@username , @password)




//alter proc UserInfoAdd
//@username nvarchar(150),
//@firstname nvarchar(50),
//@lastname nvarchar(50),
//@dateofbirth nvarchar(50),
//@height tinyint,
//@weight decimal,
//@gender nvarchar(50)

//as 
//insert into TblUserInfo(username, firstname , lastname, dateofbirth , height, weight , gender) values(@username, @firstname , @lastname, @dateofbirth , @height, @weight, @gender)




//alter proc UserInfoDelete
//@username nvarchar(150)
//as

//  DELETE FROM TblUserInfo WHERE  username=@username



//alter proc UserInfoUpdate
//@firstname nvarchar(50),
//@lastname nvarchar(50),
//@dateofbirth nvarchar(50),
//@height tinyint,
//@weight decimal,
//@gender nvarchar(50)

//as 
//update TblUserInfo set firstname = @firstname, lastname = @lastname, dateofbirth = @dateofbirth, height = @height, weight = @weight, gender = @gender where(firstname = @firstname)



