using SuBienApp.Classes;
using SuBienApp.Models;
using SuBienApp.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SuBienApp.Services
{
    public class DataService
    {
        #region Properties
        DataAccess da = new DataAccess();
        #endregion

        #region Methods

        #region Specific Methods

        public List<Property> GetAllProperties(string filter)
        {
            return da.GetList<Property>(false)
              .OrderBy(c => c.Name)
              .Where(c => c.Name.ToUpper().Contains(filter.ToUpper()) ||
                                  c.Contact.ToUpper().Contains(filter.ToUpper()))
              .ToList();
        }

        public List<Customer> GetAllCustomers(string filter)
        {
            return da.GetList<Customer>(false)
                .OrderBy(c => c.Name)
                .ThenBy(c => c.LastName)
                .Where(c => c.Name.ToUpper().Contains(filter.ToUpper()) ||
                                    c.LastName.ToUpper().Contains(filter.ToUpper()))
                .ToList();
        }

        public async Task<Response> InserUser<T>(T model, string controller) where T : class
        {
            try
            {
                var oldUser = da.First<T>(false);

                if (oldUser != null)
                {
                    da.Delete(oldUser);
                }

                model = da.InsertIdentity(model);

                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "IsSuccess"),
                    Type = Resource.TypeInformation,
                    Result = model
                };
                
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Resource.ResourceManager.GetString(controller + "IsFailed"),
                    Type = Resource.TypeError,
                    Result = model
                };
            }
        }

        public async Task<List<Calls>> GetCalls()
        {
            DataAccessCalls dataAccessCalls = new DataAccessCalls();
            return dataAccessCalls.GetHistoryCalls();
        }

        #endregion

        #region General Methods

        public async Task<List<T>> GetMany<T>(Expression<Func<T, bool>> where, bool wihtChildren) where T : class
        {
            return da.GetMany<T>(where, wihtChildren).ToList();
        }

        public List<T> Get<T>(bool wihtChildren) where T : class
        {
            return da.GetList<T>(wihtChildren).ToList();
        }

        public T GetFirst<T>(bool wihtChildren) where T : class
        {
            return da.First<T>(wihtChildren);
        }

        public void Save<T>(List<T> list) where T : class
        {
            var oldRecors = da.GetList<T>(false);
            foreach (var record in oldRecors)
            {
                da.Delete<T>(record);
            }
            foreach (var record in list)
            {
                da.Insert<T>(record);
            }
        }

        public async Task<Response> Insert<T>(T model, string controller)
        {
            try
            {
                da.Insert<T>(model);
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "AddSuccess"),
                    Type = Resource.TypeInformation,
                    Result = model
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "AddFailed"),
                    Type = Resource.TypeError,
                    Result = model
                };
            }
        }

        public T Find<T>(int pk, bool withChildren) where T : class
        {
            return da.Find<T>(pk, withChildren);
        }

        public async Task<Response> Update<T>(T model, string controller)
        {
            try
            {
                da.Update<T>(model);
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "UpdateSuccess"),
                    Type = Resource.TypeInformation,
                    Result = model
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "UpdateFailed"),
                    Type = Resource.TypeError,
                    Result = model
                };
            }
        }

       

        public async Task<Response> Delete<T>(T model, string controller)
        {
            try
            {
                da.Delete<T>(model);
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "DeleteSuccess"),
                    Type = Resource.TypeInformation,
                    Result = model
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "DeleteFailed"),
                    Type = Resource.TypeError,
                    Result = model
                };
            }
        } 
        #endregion

        #endregion
    }
}
