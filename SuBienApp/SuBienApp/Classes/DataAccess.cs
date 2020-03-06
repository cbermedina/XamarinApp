using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using SuBienApp.Interfaces;
using SuBienApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SuBienApp.Classes
{
    public class DataAccess : IDisposable
    {

        #region Properties

        private SQLiteConnection connection;

        #endregion

        #region Constructors
        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();
            connection = new SQLiteConnection(config.Platform,
                System.IO.Path.Combine(config.DirectoryDB, "SuBien.db3"));
            CreateTables();
        }
        #endregion

        #region Management Methods

        public void Insert<T>(T model)
        {
            connection.Insert(model);
        }
        public T InsertIdentity<T>(T model)
        {
            connection.Insert(model);
            return model;
        }

        public void Update<T>(T model)
        {
            connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            connection.Delete(model);
        }

        public T First<T>(bool withChildren) where T : class
        {
            if (withChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault();
            }
            else
            {
                return connection.Table<T>().FirstOrDefault();
            }
        }

        public List<T> GetList<T>(bool withChildren) where T : class
        {
            if (withChildren)
            {
                return connection.GetAllWithChildren<T>().ToList();
            }
            else
            {
                return connection.Table<T>().ToList();
            }
        }

        public T Find<T>(int pk, bool withChildren) where T : class
        {
            if (withChildren)
            {
                return connection.GetAllWithChildren<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
            else
            {
                return connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
            }
        }

        public List<T> GetMany<T>(Expression<Func<T, bool>> where, bool wihtChildren) where T : class
        {
            if (wihtChildren)
            {
                return connection.GetAllWithChildren<T>(where).ToList();
            }
            return connection.Table<T>().Where(where).ToList();
        }

        #endregion

        #region Persistence Methods

        private void CreateTables()
        {

           // connection.DropTable<Property>();
            //connection.DropTable<Customer>();

            connection.CreateTable<Property>();
            connection.CreateTable<PropertyType>();
            connection.CreateTable<Customer>();
            connection.CreateTable<GalleryImage>();

            connection.CreateTable<User>();
            var list = GetList<PropertyType>(false);
            if (list.Count() == 0)
            {
                Insert<PropertyType>(new PropertyType { Name = "Apartamento", Created = DateTime.Now, PropertyTypeId = 1, Updated = DateTime.Now });
                Insert<PropertyType>(new PropertyType { Name = "Casa", Created = DateTime.Now, PropertyTypeId = 2, Updated = DateTime.Now });
            }

        }

        public void Dispose()
        {
            connection.Dispose();
        }

        #endregion
    }
}
