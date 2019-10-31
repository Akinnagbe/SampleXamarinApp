using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SampleApp.Enums
{
    public enum CurrencyCode
    {
        [Description(description: "Naira")]
        NGN = 566,
        [Description(description: "Dollars")]
        USD = 840,
        [Description(description: "Euro")]
        EUR = 978,
        [Description(description: "Pound")]
        GBP = 826
    }
}
