using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp15
{
    public partial class Teacher
    {
        public Teacher(int Id, string FullName)
        {
            this.Id = Id;
            this.FullName = FullName;
        }
        public int Id { get; set; }
        public string FullName { get; set; }
    }
}
