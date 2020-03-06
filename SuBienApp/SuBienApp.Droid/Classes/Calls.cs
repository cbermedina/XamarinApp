using Android.App;
using Android.Net;
using Android.Provider;
using SuBienApp.Interfaces;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Linq;
using static Android.Resource;
using System;
using System.Globalization;
using Android.Content;
using Java.Lang;

[assembly: Dependency(typeof(SuBienApp.Droid.Classes.Calls))]
namespace SuBienApp.Droid.Classes
{
    public class Calls : ICall
    {
        public List<SuBienApp.Models.Calls> CallsLog
        {
            get
            {
                // ContentResolver values = new ContentResolver();
                //var dd = getAllCallLogs(values);
                return GetCallLogs();
            }
        }
        public List<SuBienApp.Models.Calls> GetCallLogs()
        {
            // filter in desc order limit by no
            string querySorter = string.Format("{0} desc ", CallLog.Calls.Date);
            List<SuBienApp.Models.Calls> calls = new List<SuBienApp.Models.Calls>();
            // CallLog.Calls.ContentUri is the path where data is saved
            //ICursor queryData = Android.Content.ContentResolver.Query(CallLog.Calls.ContentUri, null, null, null, querySorter);
            try
            {
                Android.Database.ICursor queryData = ((Activity)Forms.Context).ManagedQuery(CallLog.Calls.ContentUri, null, null, null, querySorter);
                while (queryData.MoveToNext())
                {
                    string callNumber = queryData
                                                 .GetString(queryData
                                                 .GetColumnIndex(CallLog.Calls.Number));

                    string callName = queryData
                                              .GetString(queryData
                                                             .GetColumnIndex(CallLog.Calls.CachedName));

                    string callDate = queryData
                                             .GetString(queryData
                                                            .GetColumnIndex(CallLog.Calls.Date));

                    Java.Text.SimpleDateFormat formatter = new Java.Text.SimpleDateFormat(
                            "dd/MMM/yyyy HH:mm");

                    DateTime dateString = Convert.ToDateTime(formatter.Format(new Java.Sql.Date(Long
                                                      .ParseLong(callDate))));

                    string callType = queryData
                                            .GetString(queryData
                                            .GetColumnIndex(CallLog.Calls.Type));

                    string isCallNew = queryData
                                               .GetString(queryData
                                               .GetColumnIndex(CallLog.Calls.New));

                    string duration = queryData
                                            .GetString(queryData
                                            .GetColumnIndex(CallLog.Calls.Duration));

                    calls.Add(new SuBienApp.Models.Calls
                    {
                        CallName = callName + " Fecha: " + Convert.ToString(dateString, CultureInfo.InvariantCulture),
                        Number = callNumber,
                        Date = dateString,
                        Duration = duration,
                    });

                }
                var lessdays = DateTime.Now.AddDays(-8);
                return calls.Where(c => c.Date > lessdays).ToList();//.GroupBy(p=> new { p.CachedName,p.Date,p.Duration,p.Number}).Select(p=>new { p.Key.CachedName,p.Key.Date,p.Key.Duration,p.Key.Number}).ToList() ;
            }
            catch (System.Exception ex)
            {
                return calls;
            }
        }

        private object getAllCallLogs(ContentResolver cr)
        {
            // reading all data in descending order according to DATE
            // String strOrder = android.provider.CallLog.Calls.DATE + " DESC";
            string strOrder = string.Format("{0} desc ", CallLog.Calls.Date);
            Android.Net.Uri callUri = Android.Net.Uri.Parse("content://call_log/calls");
            var cur = cr.Query(callUri, null, null, null, strOrder);
            // loop through cursor
            while (cur.MoveToNext())
            {
                string callNumber = cur.GetString(cur
                        .GetColumnIndex(CallLog.Calls.Number));
                string callName = cur
                        .GetString(cur
                                .GetColumnIndex(CallLog.Calls.CachedName));
                string callDate = cur.GetString(cur
                        .GetColumnIndex(CallLog.Calls.Date));
                Java.Text.SimpleDateFormat formatter = new Java.Text.SimpleDateFormat(
                        "dd-MMM-yyyy HH:mm");
                string dateString = formatter.Format(new Java.Sql.Date(Long
                        .ParseLong(callDate)));
                string callType = cur.GetString(cur
                        .GetColumnIndex(CallLog.Calls.Type));
                string isCallNew = cur.GetString(cur
                        .GetColumnIndex(CallLog.Calls.New));
                string duration = cur.GetString(cur
                        .GetColumnIndex(CallLog.Calls.Duration));
                // process log data...
            }
            return null;
        }
    }
}