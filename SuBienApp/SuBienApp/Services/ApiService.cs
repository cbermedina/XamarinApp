using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.IO;
using SuBienApp.Classes;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using SuBienApp.Resources;
using SuBienApp.Classes;
using SuBienApp.Utils;

namespace SuBienApp.Services
{
    public class ApiService
    {

        #region Properties
        private DataAccess dataAccess;
        private Utilities utilities;
        private HttpClient client;
        #endregion

        #region Constructors

        public ApiService()
        {
            dataAccess = new DataAccess();
            utilities = new Utilities();
            client = new HttpClient();
        }

        #endregion

        #region Methods
        public async Task<Response> Login<T>(T model, string controller)
        {
            try
            {
               // model =   dataAccess.InsertIdentity(model);
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "IsSuccess"),
                    Type = Resource.TypeInformation,
                    Result = model
                };
                //client.BaseAddress = new Uri(Resource.BaseAddress);
                //var jsonRequest = utilities.SerializeObject<T>(model);
                //var httpContent = utilities.HttpContent(jsonRequest);
                //var url = controller + "/Post/";
                //var response = await client.PostAsync(url, httpContent);
                //if (!response.IsSuccessStatusCode)
                //{
                //    return new Response
                //    {
                //        IsSuccess = true,
                //        Message = Resource.ResourceManager.GetString(controller + "IsNotValid"),
                //        Type = Resource.TypeInformation,
                //        Result = model
                //    };
                //}
                //var result = await response.Content.ReadAsStringAsync();
                //return new Response
                //{
                //    IsSuccess = true,
                //    Message = Resource.ResourceManager.GetString(controller + "IsSuccess"),
                //    Type = Resource.TypeInformation,
                //    Result = JsonConvert.DeserializeObject<T>(result)
                //};
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


        public async Task<Response> Get<T>(string controller) where T : class
        {
            try
            {
                client.BaseAddress = new Uri(Resource.BaseAddress);
                var url = controller + "/Get/";
                var response = await client.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Resource.ResourceManager.GetString("GetFaild"),
                        Type = Resource.TypeInformation,
                        Result = null
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "GetSuccess"),
                    Type = Resource.TypeInformation,
                    Result = JsonConvert.DeserializeObject(result)
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "GetFaild"),
                    Type = Resource.TypeInformation,
                    Result = null
                };
            }
        }

        public async Task<Response> Insert<T>(T model, string controller)
        {
            try
            {
                client.BaseAddress = new Uri(Resource.BaseAddress);
                var jsonRequest = utilities.SerializeObject<T>(model);
                var httpContent = utilities.HttpContent(jsonRequest);
                var url = controller + "/Post/";
                var response = await client.PostAsync(url, httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Resource.ResourceManager.GetString(controller + "AddFailed"),
                        Type = Resource.TypeInformation,
                        Result = model
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "AddSuccess"),
                    Type = Resource.TypeInformation,
                    Result = JsonConvert.DeserializeObject<T>(result)
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Resource.ResourceManager.GetString(controller + "AddFailed"),
                    Type = Resource.TypeError,
                    Result = model
                };
            }
        }

        public async Task<Response> Update<T>(T model, string controller)
        {
            try
            {
                client.BaseAddress = new Uri(Resource.BaseAddress);
                var jsonRequest = utilities.SerializeObject<T>(model);
                var httpContent = utilities.HttpContent(jsonRequest);
                var url = controller + "/Put/";
                var response = await client.PutAsync(url, httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Resource.ResourceManager.GetString(controller + "UpdateFailed"),
                        Type = Resource.TypeInformation,
                        Result = model
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "UpdateSuccess"),
                    Type = Resource.TypeInformation,
                    Result = JsonConvert.DeserializeObject<T>(result)
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

        public async Task<Response> SetPhoto<T>(int id, T model, string controller)
        {
            try
            {
                client.BaseAddress = new Uri(Resource.BaseAddress);
                var jsonRequest = utilities.SerializeObject<T>(model);
                var httpContent = utilities.HttpContent(jsonRequest);
                var url = controller + "/Post/";
                var response = await client.PostAsync(url, httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = true,
                        Message = Resource.ResourceManager.GetString(controller + "AddFailed"),
                        Type = Resource.TypeInformation,
                        Result = model
                    };
                }
                var result = await response.Content.ReadAsStringAsync();
                return new Response
                {
                    IsSuccess = true,
                    Message = Resource.ResourceManager.GetString(controller + "AddSuccess"),
                    Type = Resource.TypeInformation,
                    Result = JsonConvert.DeserializeObject<T>(result)
                };
                //var array = ReadFully(stream);

                //var photoRequest = new PhotoRequest
                //{
                //    Id = id,
                //    Array = array,
                //};

                //var request = JsonConvert.SerializeObject(photoRequest);
                //var body = new StringContent(request, Encoding.UTF8, "application/json");
                //var client = new HttpClient
                //{
                //    BaseAddress = new Uri("http://www.google.com")
                //};
                //var url = controller + "/SetPhoto";
                //var response = await client.PostAsync(url, body);

                //if (!response.IsSuccessStatusCode)
                //{
                //    return new Response
                //    {
                //        IsSuccess = false,
                //        Message = Resource.ResourceManager.GetString(controller + "AddFailed"),
                //        Type = Resource.TypeError,
                //        Result = response
                //    };
                //}

                //return new Response
                //{
                //    IsSuccess = true,
                //    Message = Resource.ResourceManager.GetString(controller + "AddSuccess"),
                //    Type = Resource.TypeInformation,
                //    Result = response
                //};
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Resource.ResourceManager.GetString(controller + "AddFailed"),
                    Type = Resource.TypeInformation,
                    Result = model
                };
            }

        }
        #endregion

        //public static byte[] ReadFully(Stream input)
        //{
        //    byte[] buffer = new byte[16 * 1024];
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        int read;
        //        while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            ms.Write(buffer, 0, read);
        //        }
        //        return ms.ToArray();
        //    }
        //}
    }
}
