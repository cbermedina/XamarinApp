using SuBienApp.Interfaces;
using SuBienApp.Models;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SuBienApp.Classes
{
   public class DataAccessCalls
    {
        private List<Calls> Calls;
        public DataAccessCalls()
        {
            var contact = DependencyService.Get<ICall>();
            Calls = contact.CallsLog;
        }
        public List<Calls> GetHistoryCalls()
        {
            return Calls;
        }
    }
}
