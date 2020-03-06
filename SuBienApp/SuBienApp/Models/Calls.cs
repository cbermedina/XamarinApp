using System;

namespace SuBienApp.Models
{
    public class Calls
    {
        public string CallName { get; set; }//= "name";

        //   [Register("DATE")]
        public DateTime Date { get; set; }//= "date";

        // [Register("DURATION")]
        public string Duration { get; set; }// = "duration";
                                            // [Register("NUMBER")]
        public string Number { get; set; }//= "number";
                                          // [Register("NUMBER_PRESENTATION")]
        public string NumberPresentation { get; set; }//= "presentation";
    }
}
