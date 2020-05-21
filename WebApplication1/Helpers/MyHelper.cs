using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Helpers
{
    public class MyHelper
    {
        public string Message { get; set; }
        public Doctor doctor { get; set; }
        public IEnumerable<Doctor> doctors { get; set; }
        public int status { get; set; }
    }
}
