using HMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DataAccess
{
    public class PatientDataAccess
    {
        private Entities _context;
        public PatientDataAccess()
        {
            _context = new Entities();
        }

        public List<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public Patient GetById(int RegNo)
        {
            return _context.Patients.Where(x => x.RegNo == RegNo).FirstOrDefault();
        }

        public List<Patient> GetByName(string Name)
        {
            return _context.Patients.Where(x => x.Name == Name).ToList();
        }

        public bool Add(Patient patient)
        {
            var add = _context.Patients.Where(x => x.Name == patient.Name && x.Phone == patient.Phone).FirstOrDefault();
            if (add == null)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return true;
            }
            else
            {
                patient = null;
                return false;
            }
        }

        public bool Delete(int RegNo)
        {
            bool status;
            var remove = _context.Patients.Where(x => x.RegNo == RegNo).FirstOrDefault();
            if (remove != null)
            {
                _context.Patients.Remove(remove);
                _context.SaveChanges();
                status = true;
            }
            else
            {
                status = false;
            }
            return status;
        }
    }
}
