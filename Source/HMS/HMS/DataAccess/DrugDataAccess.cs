using HMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DataAccess
{
    public class DrugDataAccess
    {
        private Entities _context;
        public DrugDataAccess()
        {
            _context = new Entities();
        }

        public List<Drug> GetAll()
        {
            return _context.Drugs.ToList();
        }

        public List<Drug> GetByName(string Name)
        {
            return _context.Drugs.Where(x => x.DrugName == Name).ToList();
        }

        public List<Drug> GetByGeneric(string Name)
        {
            return _context.Drugs.Where(x => x.GenericName == Name).ToList();
        }

        public Drug Add(Drug drug)
        {
            var add = _context.Drugs.Where(x => x.DrugName == drug.DrugName && x.Brand == drug.Brand).FirstOrDefault();
            if (add == null)
            {
                _context.Drugs.Add(drug);
                _context.SaveChanges();
            }
            else
            {
                drug = null;
            }
            return drug;
        }

        public void Delete(Drug drug)
        {
            
           _context.Drugs.Remove(drug);
           _context.SaveChanges();
        }
    }
}
