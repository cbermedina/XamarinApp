using SQLite.Net.Attributes;
using System;

namespace SuBienApp.Models
{
    public  class PropertyType : BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int PropertyTypeId { get; set; }

        public string Name { get; set; }

        public override int GetHashCode()
        {
            return this.PropertyTypeId;
        }
    }
}
