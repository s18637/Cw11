using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface IDbService
    {
        public MyHelper GetDoctors();
        public MyHelper AddDoctor(Doctor doctor);
        public MyHelper UpdateDoctor(Doctor doctor);
        public MyHelper DeleteDoctor(string LastName);
    }
}
