using System;
using Org.W3c.Dom;
using SQLite.Net.Attributes;

namespace SuBienApp.Models
{
    public class FileItemImage :BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int  FileItemImageId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string Origin { get; set; }

        public override int GetHashCode()
        {
            return this.FileItemImageId;
        } 
    }
}
