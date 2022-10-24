using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConsole.Model
{
    public class ConversionInput
    {
        public string BaseCur { get; set; }
        public string ExchangeCur { get; set; }
        public double Amount { get; set; }
        public string Date { get; set; }
    }
}
