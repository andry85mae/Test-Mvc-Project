using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magneti_Marelli_Test.Models
{
    public class LookupValue<T, U>
    {
        public T Key { get; set; }

        public U Value { get; set; }

        public LookupValue() { }

        public LookupValue(T Key, U Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }
}