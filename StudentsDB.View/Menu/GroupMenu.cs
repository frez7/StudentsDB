using StudentsDB.BL;

namespace StudentsDB.View.Menu
{
    public class GroupMenu : IGroupMenu
    {
        private readonly IGroupManager _groupManager;
        public GroupMenu(IGroupManager groupManager)
        {
            _groupManager = groupManager;
        }
        public void GroupMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\nМеню управления группами:");
                Console.WriteLine("1. Список всех групп.");
                Console.WriteLine("2. Создать группу.");
                Console.WriteLine("3. Редактировать группу.");
                Console.WriteLine("4. Удалить группу.");
                Console.WriteLine("5. Выйти из меню управления группами.");
                ConsoleKeyInfo keyInfoGroup = Console.ReadKey(intercept: true);
                char groupChoice = keyInfoGroup.KeyChar;
                switch (groupChoice)
                {
                    case '1':
                        GroupViewMenu();
                        break;
                    case '2':
                        GroupAddMenu();
                        break;
                    case '3':
                        GroupEditMenu();
                        break;
                    case '4':
                        GroupDeleteMenu();
                        break;
                    case '5':
                        return;
                }
            }
        }
        public void GroupAddMenu()
        {
            Console.Clear();
            Console.Write("\nВведите название новой группы: ");
            var groupName = Console.ReadLine();
            Console.Write("\nВведите доп.информацию: ");
            var groupInfo = Console.ReadLine();
            _groupManager.CreateGroup(groupName, groupInfo);
            Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
            Console.ReadKey();
        }
        public void GroupViewMenu()
        {
            Console.Clear();
            _groupManager.ViewAllGroups();
            Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
            Console.ReadKey();
        }
        public void GroupEditMenu()
        {
            int editGroupId;
            Console.Clear();
            _groupManager.ViewAllGroups();
            do
            {
                Console.Write("\nВведите ID группы, которую хотите отредактировать: ");
            } while (!Int32.TryParse(Console.ReadLine(), out editGroupId));
            var group = _groupManager.GetGroupById(editGroupId);
            if (group != null)
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("\nВыберите что хотите поменять у группы: ");
                    Console.WriteLine($"1. Название (Текущее: {group.Name})");
                    Console.WriteLine($"2. Доп.информация (Текущая: {group.Info}).");
                    Console.WriteLine("3. Выйти из меню редактирования группы.");

                    ConsoleKeyInfo keyInfoGroupEdit = Console.ReadKey(intercept: true);
                    char groupChoiceEdit = keyInfoGroupEdit.KeyChar;
                    switch (groupChoiceEdit)
                    {
                        case '1':
                            Console.Clear();
                            Console.Write("\nВведите новое название группы: ");
                            var newGroupName = Console.ReadLine();
                            _groupManager.UpdateGroup(editGroupId, newGroupName, group.Info);
                            Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
                            Console.ReadKey();
                            break;
                        case '2':
                            Console.Clear();
                            Console.Write("\nВведите новую доп.информацию группы: ");
                            var newGroupInfo = Console.ReadLine();
                            _groupManager.UpdateGroup(editGroupId, group.Name, newGroupInfo);
                            Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
                            Console.ReadKey();
                            break;
                        case '3':
                            return;
                    }
                }
            }
        }
        public void GroupDeleteMenu()
        {
            int deleteGroupId;
            Console.Clear();
            _groupManager.ViewAllGroups();
            do
            {
                Console.Write("\nВведите ID удаляемой группы: ");
            } while (!Int32.TryParse(Console.ReadLine(), out deleteGroupId));
            _groupManager.DeleteGroup(deleteGroupId);
            Console.WriteLine("\nНажмите любую кнопку, чтобы вернуться в меню.");
            Console.ReadKey();
        }
    }
}
