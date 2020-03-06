using SQLite.Net.Attributes;
using SuBienApp.Models;

namespace SuBienApp.Classes
{
    public class TodoItemImage : BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int TodoItemImageId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public string Type { get; set; }
    }
}
