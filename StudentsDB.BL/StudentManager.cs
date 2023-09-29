using StudentsDB.Model;
using System.Text.RegularExpressions;

namespace StudentsDB.BL
{
    public class StudentManager : IStudentManager
    {
        private static string _studentFilePath = "students_db.txt";
        private List<Student> _students;
        private readonly IGroupManager _groupManager;
        public StudentManager(IGroupManager groupManager)
        {
            _groupManager = groupManager;
            _students = LoadStudentsFromDatabase();
        }
        private List<Student> LoadStudentsFromDatabase()
        {
            List<Student> loadedStudents = new List<Student>();

            if (File.Exists(_studentFilePath))
            {
                string[] lines = File.ReadAllLines(_studentFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 4 && int.TryParse(parts[0], out int id) && int.TryParse(parts[3], out int groupId))
                    {
                        loadedStudents.Add(new Student()
                        {
                            Id = id,
                            FirstName = parts[1],
                            LastName = parts[2],
                            GroupId = groupId,
                        });
                    }
                }
            }
            return loadedStudents;
        }
        private void SaveStudentsToDatabase()
        {
            using (StreamWriter writer = File.CreateText(_studentFilePath))
            {
                foreach (Student student in _students)
                {
                    writer.WriteLine($"{student.Id},{student.FirstName},{student.LastName},{student.GroupId}");
                }
            }
        }
        public void ViewAllStudents()
        {
            var students = GetStudents();
            Console.WriteLine("Список всех студентов: ");
            foreach (var student in students)
            {
                var group = _groupManager.GetGroupById(student.GroupId);
                Console.WriteLine($"ID: {student.Id}, Имя: {student.FirstName}, Фамилия: {student.LastName}, Группа: {group.Name}");
            }
            Console.WriteLine("");
        }
        public List<Student> GetStudentsByGroupId(int groupId)
        {
            var group = _groupManager.GetGroupById(groupId);
            List<Student> groupStudents = _students.FindAll(s => s.GroupId == groupId);
            if (groupStudents.Count != 0)
            {
                Console.WriteLine($"Студенты группы \"{group.Name}\":");
                foreach (var student in groupStudents)
                {
                    Console.WriteLine($"ID: {student.Id}, Имя: {student.FirstName}, Фамилия: {student.LastName}");
                }
                Console.WriteLine("");
                return groupStudents;
            }
            Console.WriteLine("В данной группе пока нет студентов!");
            return null;

        }
        public List<Student> SearchStudentsByLastName(string lastName)
        {
            lastName = lastName.Trim();
            List<Student> foundStudents = _students.FindAll(s => s.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase));
            if (foundStudents.Count != 0)
            {
                Console.WriteLine("\nСписок найденных студентов: ");
                foreach (var student in foundStudents)
                {
                    var group = _groupManager.GetGroupById(student.GroupId);
                    Console.WriteLine($"ID: {student.Id}, Имя: {student.FirstName}, Фамилия: {student.LastName}, Группа: {group.Name}");
                }
                return foundStudents;
            }
            Console.WriteLine("Студенты с такой фамилией не найдены!");
            return null;
        }

        public void CreateStudent(string firstName, string lastName, int groupId)
        {
            var group = _groupManager.GetGroupById(groupId);
            int newId = 1;
            if (_students.Any())
            {
                newId = _students.Max(s => s.Id) + 1;
            }
            Student student = new Student()
            {
                Id = newId,
                FirstName = firstName,
                LastName = lastName,
                GroupId = groupId,
            };
            _students.Add(student);
            SaveStudentsToDatabase();
            Console.WriteLine($"Вы успешно добавили нового студента, в группу: \"{group.Name}\"!");
        }
        public List<Student> GetStudents()
        {
            return _students;
        }
        public Student GetStudentById(int id)
        {
            var student = _students.Find(student => student.Id == id);
            return student;
        }
        public void UpdateStudent(int id, string firstName, string lastName, int groupId)
        {
            Student studentToUpdate = GetStudentById(id);
            if (studentToUpdate != null)
            {
                studentToUpdate.FirstName = firstName;
                studentToUpdate.LastName = lastName;
                studentToUpdate.GroupId = groupId;
                SaveStudentsToDatabase();
                Console.WriteLine("Вы успешно обновили данные о студенте!");
                return;
            }
            Console.WriteLine("Студента с таким ID не существует!");
        }
        public void DeleteStudent(int id)
        {
            Student studentToDelete = GetStudentById(id);
            if (studentToDelete != null)
            {
                _students.Remove(studentToDelete);
                SaveStudentsToDatabase();
                Console.WriteLine("Вы успешно удалили студента!");
                return;
            }
            Console.WriteLine("Студента с таким ID не существует!");
        }
    }
}
