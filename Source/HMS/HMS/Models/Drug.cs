using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Ignore these classes
namespace HMS.Models
{
    public class Drug
    {
        //[PrimaryKey, AutoIncrement]
        public string DrugName { get; set; }
        public string DrugForm { get; set; }
        public string Brand { get; set; }
        public string GenericName { get; set; }
        public string Route { get; set; }
        public string Frequency { get; set; }

        public Drug()
        {

        } 
    }
}
