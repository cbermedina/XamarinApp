using SQLite.Net.Attributes;
using SuBienApp.Models;

namespace SuBienApp.Classes
{
    public class CustomObject:BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int CustomId { get; set; }
        public string Name { get; set; }
    }
}
