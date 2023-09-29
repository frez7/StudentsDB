using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDB.View.Menu
{
    public interface IGroupMenu
    {
        void GroupMainMenu();
        void GroupAddMenu();
        void GroupViewMenu();
        void GroupEditMenu();
        void GroupDeleteMenu();
    }
}
