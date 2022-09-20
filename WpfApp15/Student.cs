using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp15
{
    public class Student
    {
        public Student(int IdStudent, string FullName, int IdGroup)
        {
            this.IdStudent = IdStudent;
            this.FullName = FullName;
            this.IdGroup = IdGroup;
        }
        public int IdStudent { get; set; }
        public string FullName { get; set; }
        public int IdGroup { get; set; }
    }
}
