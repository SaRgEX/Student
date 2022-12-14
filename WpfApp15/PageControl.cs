using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp15
{
    public class PageControl
    {
        public static GroupPage group;
        public static SpecialtyPage specialty;
        public static StudentPage student;
        public static MainPage main;
        public static LoginPage login;
        public static RegistrationPage registration;
        public static TeacherPage teacher;
        public static TimetablePage timetable;
        public static GroupPage GetGroupPage
        {
            get
            {
                if (group == null)
                {
                    group = new GroupPage();
                }
                return group;
            }
        }

        public static SpecialtyPage GetSpecialtyPage
        {
            get
            {
                if (specialty == null)
                {
                    specialty = new SpecialtyPage();
                }
                return specialty;
            }
        }

        public static StudentPage GetStudentPage
        {
            get
            {
                if (student == null)
                {
                    student = new StudentPage();
                }
                return student;
            }
        }

        public static MainPage GetMainPage
        {
            get
            {
                if (main == null)
                {
                    main = new MainPage();
                }
                return main;
            }
        }

        public static RegistrationPage GetRegistrationPage
        {
            get
            {
                if (registration == null)
                {
                    registration = new RegistrationPage();
                }
                return registration;
            }
        }

        public static LoginPage GetLoginPage
        {
            get
            {
                if (login == null)
                {
                    login = new LoginPage();
                }
                return login;
            }
        }

        public static TeacherPage GetTeacherPage
        {
            get
            {
                if (teacher == null)
                {
                    teacher = new TeacherPage();
                }
                return teacher;
            }
        }

        public static TimetablePage GetTimetablePage
        {
            get
            {
                if (timetable == null)
                {
                    timetable = new TimetablePage();
                }
                return timetable;
            }
        }
    }
}
