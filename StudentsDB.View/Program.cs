using StudentsDB.BL;
using StudentsDB.View.Menu;

IGroupManager groupManager = new GroupManager();
IStudentManager studentManager = new StudentManager(groupManager);
IGroupMenu groupMenu = new GroupMenu(groupManager);
IStudentsMenu studentsMenu = new StudentsMenu(groupManager, studentManager);

while (true)
{
    Console.Clear();
    Console.WriteLine("\nНажмите на соотвествующую цифру пункта меню.");
    Console.WriteLine("\nГлавное меню:");
    Console.WriteLine("1. Управление группами.");
    Console.WriteLine("2. Управление студентами.");
    Console.WriteLine("3. Выйти из программы.");

    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
    char mainChoice = keyInfo.KeyChar;

    switch (mainChoice)
    {
        case '1':
            groupMenu.GroupMainMenu();
            break;
        case '2':
            studentsMenu.StudentsMainMenu();
            break;
        case '3':
            return;
    }
    
}