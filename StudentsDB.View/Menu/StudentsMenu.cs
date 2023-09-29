using StudentsDB.BL;

namespace StudentsDB.View.Menu
{
    public class StudentsMenu : IStudentsMenu
    {
        private readonly IGroupManager _groupManager;
        private readonly IStudentManager _studentManager;

        public StudentsMenu(IGroupManager groupManager, IStudentManager studentManager)
        {
            _groupManager = groupManager;
            _studentManager = studentManager;
        }
        public void StudentsMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nМеню управления студентами:");
                Console.WriteLine("1. Посмотреть список студентов.");
                Console.WriteLine("2. Добавить нового студента.");
                Console.WriteLine("3. Редактировать информацию о студенте.");
                Console.WriteLine("4. Удалить студента.");
                Console.WriteLine("5. Найти студента по фамилии.");
                Console.WriteLine("6. Выйти из меню управления студентами.");
                ConsoleKeyInfo keyInfoStudent = Console.ReadKey(intercept: true);
                char studentChoice = keyInfoStudent.KeyChar;
                switch (studentChoice)
                {
                    case '1':
                        GetStudentsMenu();
                        break;
                    case '2':
                        AddStudentMenu();
                        break;
                    case '3':
                        EditStudentMenu();
                        break;
                    case '4':
                        DeleteStudentMenu();
                        break;
                    case '5':
                        FindByLastNameMenu();
                        break;
                    case '6':
                        return;
                }
            }
        }
        public void FindByLastNameMenu()
        {
            Console.Clear();
            Console.Write("Введите фамилию, по которой хотите выполнить поиск студентов: ");
            string lastName = Console.ReadLine();
            _studentManager.SearchStudentsByLastName(lastName);
            Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
            Console.ReadKey();
        }
        public void DeleteStudentMenu()
        {
            int deleteStudentId;
            Console.Clear();
            _studentManager.ViewAllStudents();
            do
            {
                Console.Write("\nВведите ID студента, которого хотите удалить: ");
            } while (!Int32.TryParse(Console.ReadLine(), out deleteStudentId));
            _studentManager.DeleteStudent(deleteStudentId);
            Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
            Console.ReadKey();
        }
        public void EditStudentMenu()
        {
            int editStudentId;
            Console.Clear();
            _studentManager.ViewAllStudents();
            do
            {
                Console.Write("\nВведите ID студента, информацию о котором хотите отредактировать: ");
            } while (!Int32.TryParse(Console.ReadLine(), out editStudentId));

            while (true)
            {
                var student = _studentManager.GetStudentById(editStudentId);
                var group = _groupManager.GetGroupById(student.GroupId);

                Console.Clear();
                Console.WriteLine("\nВыберите пункт, который вы хотите отредактировать у студента: ");
                Console.WriteLine($"1. Имя (Текущее: {student.FirstName})");
                Console.WriteLine($"2. Фамилия (Текущее: {student.LastName})");
                Console.WriteLine($"3. Группа (Текущее: {group.Name})");
                Console.WriteLine("4. Выйти из меню редактирования студента.");

                ConsoleKeyInfo keyInfoStudentEdit = Console.ReadKey(intercept: true);
                char studentEditChoice = keyInfoStudentEdit.KeyChar;

                switch (studentEditChoice)
                {
                    case '1':
                        Console.Clear();
                        Console.Write("\nВведите новое имя студента: ");
                        string newFirstName = Console.ReadLine();
                        _studentManager.UpdateStudent(editStudentId, newFirstName, student.LastName, student.GroupId);
                        Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
                        Console.ReadKey();
                        break;
                    case '2':
                        Console.Clear();
                        Console.Write("\nВведите новую фамилию студента: ");
                        string newLastName = Console.ReadLine();
                        _studentManager.UpdateStudent(editStudentId, student.FirstName, newLastName, student.GroupId);
                        Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
                        Console.ReadKey();
                        break;
                    case '3':
                        int newGroupId;
                        Console.Clear();
                        _groupManager.ViewAllGroups();
                        do
                        {
                            Console.Write("\nВведите ID новой группы студента: ");
                        } while (!Int32.TryParse(Console.ReadLine(), out newGroupId));
                        _studentManager.UpdateStudent(editStudentId, student.FirstName, student.LastName, newGroupId);
                        Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
                        Console.ReadKey();
                        break;
                    case '4':
                        return;
                }
            }
        }
        public void AddStudentMenu()
        {
            int selectGroupId;
            Console.Clear();
            Console.Write("Введите имя студента: ");
            string studentFirstName = Console.ReadLine();
            Console.Write("Введите фамилию студента: ");
            string studentLastName = Console.ReadLine();
            Console.Clear();
            _groupManager.ViewAllGroups();
            do
            {
                Console.Write("\nВведите ID группы, в которую хотите добавить студента: ");
            } while (!Int32.TryParse(Console.ReadLine(), out selectGroupId));
            Console.Clear();
            _studentManager.CreateStudent(studentFirstName, studentLastName, selectGroupId);
            Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
            Console.ReadKey();
        }
        public void GetStudentsMenu()
        {
            int chooseStudentGroupId;
            Console.Clear();
            _groupManager.ViewAllGroups();
            do
            {
                Console.Write("\nВведите ID группы, в которой хотите посмотреть студентов: ");
            } while (!Int32.TryParse(Console.ReadLine(), out chooseStudentGroupId));
            Console.Clear();
            _studentManager.GetStudentsByGroupId(chooseStudentGroupId);
            Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
            Console.ReadKey();
        }
    }
}
