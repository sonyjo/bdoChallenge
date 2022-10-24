using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace CurrencyDailyUpdate.Model
{
    public class FixerApiResponse
    {
        public class Error
        {
            public int code { get; set; }
            public string type { get; set; }

            public string info { get; set; }
        }


        public bool success { get; set; }
        public int timestamp { get; set; }

        [Display(Name = "base")]
        public string Base { get; set; }
        public DateTime date { get; set; }
        // public Rates rates { get; set; }

        public IDictionary<string, double> rates { get; set; }
        public Error error { get; set; }

    }
}

