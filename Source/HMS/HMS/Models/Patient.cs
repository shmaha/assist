using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ignore these classes
namespace HMS.Models
{
    public class Patient
    {
        public int RegNo { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public DateTime DoB { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Village { get; set; }
        public string Consultant { get; set; }
        public int Fee { get; set; }

        public Patient()
        {

        }
    }
}
