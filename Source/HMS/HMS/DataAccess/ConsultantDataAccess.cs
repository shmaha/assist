using HMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.DataAccess
{
    public class ConsultantDataAccess
    {
        private Entities _context;
        public ConsultantDataAccess()
        {
            _context = new Entities();
        }

        public List<Consultant> GetAll()
        {
            return _context.Consultants.ToList();
        }

        public Consultant GetDefault()
        {
            return _context.Consultants.Where(x => x.Default == 1).FirstOrDefault();
        }

        public Consultant GetByMci(string mci)
        {
            return _context.Consultants.Where(x => x.MCI == mci).FirstOrDefault();
        }

        public Consultant Add(Consultant consultant)
        {
            var add = _context.Consultants.Where(x => x.MCI == consultant.MCI).FirstOrDefault();

            if (add == null)
            {
                _context.Consultants.Add(consultant);
                _context.SaveChanges();
            }
            else
            {
                consultant = null;
            }
            return consultant;
        }

    }
}
