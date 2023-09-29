using StudentsDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDB.BL
{
    public interface IGroupManager
    {
        void CreateGroup(string name, string info);
        Group GetGroupById(int id);
        void UpdateGroup(int id, string name, string info);
        void DeleteGroup(int id);
        void ViewAllGroups();
    }
}
