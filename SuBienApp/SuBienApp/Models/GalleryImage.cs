using Newtonsoft.Json;
using SQLite.Net.Attributes;
using SuBienApp.Models;
using SuBienApp.ViewModels;

namespace SuBienApp.Models
{
    public class GalleryImage : BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int ImageId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Uri { get; set; }

        public int Order { get; set; }

        public string Type { get; set; }
        [Ignore]
        public PropertyUploadPhotoItemViewModel ParentModel { get; set; }
        [Ignore]
        public bool IsGoUpVisible { get; set; }
        [Ignore]
        public bool IsGetLowVisible { get; set; }
    }
}
