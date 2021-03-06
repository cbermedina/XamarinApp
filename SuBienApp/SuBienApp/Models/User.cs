﻿using SQLite.Net.Attributes;

namespace SuBienApp.Models
{
    public class User:BaseEntity
    {
        [PrimaryKey,AutoIncrement]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Photo { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public bool IsRemembered { get; set; }

        public string Password { get; set; }

        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName); } }

       // public string PhotoFullPath { get { return string.Format("http://zulu-software.com/ECommerce{0}", Photo.Substring(1)); } }

        public override int GetHashCode()
        {
            return UserId;
        }
    }

}
