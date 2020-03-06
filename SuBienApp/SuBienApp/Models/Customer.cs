using SQLite.Net.Attributes;
using System;

namespace SuBienApp.Models
{
   public class Customer : BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Mail { get; set; }

        public string Telephone { get; set; }

        public string Phone { get; set; }

        public int PropertyTypeId { get; set; }

        public string Province { get; set; }

        public decimal Price { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public int Mts { get; set; }

        public int Floor { get; set; }

        public Boolean Elevator { get; set; }

        public Boolean Parking { get; set; }

        public Boolean Fourthuseful { get; set; }

        public Boolean Gatedcommunity { get; set; }

        public string Observation { get; set; }

        public override int GetHashCode()
        {
            return this.Id;
        }
    }
}
