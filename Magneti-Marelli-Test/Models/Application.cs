using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magneti_Marelli_Test.Models
{
    public class Application
    {

        public Application() {
            this.Groups = new List<string>();
        }
        public string Name { get; set; }

        public List<string> Groups { get; set; }

        public string OU { get; set; }


    }
}