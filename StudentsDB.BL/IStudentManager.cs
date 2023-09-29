using StudentsDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDB.BL
{
    public interface IStudentManager
    {
        void ViewAllStudents();
        List<Student> GetStudentsByGroupId(int groupId);
        List<Student> SearchStudentsByLastName(string lastName);
        void CreateStudent(string firstName, string lastName, int groupId);
        List<Student> GetStudents();
        Student GetStudentById(int id);
        void UpdateStudent(int id, string firstName, string lastName, int groupId);
        void DeleteStudent(int id);
    }
}
