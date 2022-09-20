using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp15
{
    public partial class Group
    {
        public Group(int IdGroup, int IdSpeciality, int IdCourse)
        {
            this.IdCourse = IdCourse;
            this.IdGroup = IdGroup;
            this.IdSpeciality = IdSpeciality;
        }
        public int IdGroup { get; set; }
        public int IdSpeciality { get; set; }
        public int IdCourse { get; set; }
    }
}
