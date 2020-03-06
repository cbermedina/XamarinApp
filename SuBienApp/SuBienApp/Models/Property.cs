using SQLite.Net.Attributes;

namespace SuBienApp.Models
{
  public  class Property:BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int PropertyId { get; set; }

        public string Contact { get; set; }

        public string Name { get; set; }


        public string Phone { get; set; }

        public string Telephone { get; set; }

        public string Photo { get; set; }

        public string Mail { get; set; }

        public string Address { get; set; }

        public string Province { get; set; }

        public decimal Area { get; set; }

        public int Bedrooms { get; set; }

        public int Bathrooms { get; set; }

        public int Level { get; set; }

        public int Levels { get; set; }

        public int PropertyTypeId { get; set; }

        public bool Elevator { get; set; }

        public bool Parking { get; set; }

        public bool Fourthuseful { get; set; }

        public bool Gatedcommunity { get; set; }

        public string InfoKeys { get; set; }

        public string InfoCommission { get; set; }

        public string Observation { get; set; }

        public decimal Administration { get; set; }

        public decimal PropertyTax { get; set; }

        public decimal Price { get; set; }

        public decimal Antiquity { get; set; }

        public string PhotoFullPath => Photo == null ? string.Empty : string.Format("{0}", Photo);

        public override int GetHashCode()
        {
            return this.PropertyId;
        }
      
    }
}
