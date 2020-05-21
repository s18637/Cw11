using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Helpers;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DbService : IDbService
    {
        private readonly MyDbContext myDb;
        public DbService(MyDbContext _myDb)
        {
            myDb = _myDb;
        }

        public MyHelper AddDoctor(Doctor doctor)
        {
            try
            {
                MyHelper helper = new MyHelper();
                myDb.Add(doctor);
                helper.Message = "Added";
                helper.doctor = doctor;
                myDb.SaveChanges();
                helper.status = 0;
                return helper;
            }
            catch(Exception ex)
            {
                MyHelper helper = new MyHelper();
                helper.Message = ex.ToString();
                helper.status = -1;
                return helper;
            }
        }

        public MyHelper DeleteDoctor(string LastName)
        {
            MyHelper helper = new MyHelper();
            try
            {
                var doc = myDb.Doctor.Where(x => x.LastName == LastName).First();
                myDb.Doctor.Remove(doc);
                myDb.SaveChanges();
                helper.Message = "deleted";
                helper.status = 0;
                return helper;
            }
            catch(Exception ex)
            {
                helper.Message = ex.ToString();
                helper.status = -1;
                return helper;
            }
        }

        public MyHelper GetDoctors()
        {
            MyHelper helper = new MyHelper();
            try
            {
                var res = myDb.Doctor.ToList();
                helper.doctors = res;
                helper.status = 0;
                return helper;

            }catch(Exception ex)
            {
                helper.Message = ex.ToString();
                helper.status = -1;
                return helper;
            }
        }

        public MyHelper UpdateDoctor(Doctor doctor)
        {
            MyHelper helper = new MyHelper();
            try
            {
                myDb.Doctor.Attach(doctor);
                myDb.Entry(doctor).State = EntityState.Modified;
                myDb.SaveChanges();
                helper.Message = "updated";
                helper.status = 0;
                return helper;
            }
            catch (Exception ex)
            {
                helper.Message = ex.ToString();
                helper.status = -1;
                return helper;
            }
        }
    }
}
