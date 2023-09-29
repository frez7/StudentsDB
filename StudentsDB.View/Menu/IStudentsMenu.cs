using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDB.View.Menu
{
    public interface IStudentsMenu
    {
        void StudentsMainMenu();
        void FindByLastNameMenu();
        void DeleteStudentMenu();
        void EditStudentMenu();
        void AddStudentMenu();
        void GetStudentsMenu();
    }
}
