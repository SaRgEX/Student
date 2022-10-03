using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp15
{
    public class Timetable
    {
        public Timetable(int IdGroup, string Cabinet, int ThisСlass, DateTime Date)
        {
            this.IdTimetable = IdTimetable;
            this.IdGroup = IdGroup;
            this.Cabinet = Cabinet;
            @Class = ThisСlass;
            this.Date = Date;
        }
        public int IdTimetable { get; set; }
        public int IdGroup { get; set; }
        public string Cabinet { get; set; }
        public int @Class { get; set; }
        public DateTime Date { get; set; }
    }
}
