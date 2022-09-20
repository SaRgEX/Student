using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp15
{
    public class Speciality
    {
        public Speciality(int IdSpeciality, string NameSpeciality)
        {
            this.IdSpeciality = IdSpeciality;
            this.NameSpeciality = NameSpeciality;
        } 
        public int IdSpeciality { get; set; }
        public string NameSpeciality { get; set; }
    }
}
