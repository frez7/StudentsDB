
using StudentsDB.Model;

namespace StudentsDB.BL
{
    public class GroupManager : IGroupManager
    {
        private static string _groupFilePath = "groups_db.txt";
        private List<Group> _groups;
        public GroupManager() 
        {
            _groups = LoadGroups();
        }
        private List<Group> LoadGroups()
        {
            List<Group> loadedGroups = new List<Group>();

            if (File.Exists(_groupFilePath))
            {
                string[] lines = File.ReadAllLines(_groupFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 3 && int.TryParse(parts[0], out int id))
                    {
                        loadedGroups.Add(new Group()
                        {
                            Id = id,
                            Name = parts[1],
                            Info = parts[2]
                        });
                    }
                }
            }

            return loadedGroups;
        }
        private void SaveGroupsToDatabase()
        {
            using (StreamWriter writer = File.CreateText(_groupFilePath))
            {
                for (int i = 0; i < _groups.Count; i++)
                {
                    writer.WriteLine($"{_groups[i].Id},{_groups[i].Name},{_groups[i].Info}");
                }
            }
        }
        public void CreateGroup(string name, string info)
        {
            int newId = 1;
            if (_groups.Any())
            {
                newId = _groups.Max(g => g.Id) + 1;
            }
            Group group = new Group()
            {
                Id = newId,
                Info = info,
                Name = name,
            };
            _groups.Add(group);
            SaveGroupsToDatabase();
            Console.WriteLine("Вы успешно создали новую группу!");
        }
        public List<Group> GetGroups()
        {
            return _groups;
        }
        public Group GetGroupById(int id)
        {
            var group = _groups.Find(group => group.Id == id);
            return group;
        }
        public void UpdateGroup(int id, string name, string info)
        {
            Group groupToUpdate = GetGroupById(id);
            if (groupToUpdate != null)
            {
                groupToUpdate.Name = name;
                groupToUpdate.Info = info;
                SaveGroupsToDatabase();
                Console.WriteLine("Вы успешно обновили данные о группе!");
                return;
            }
            Console.WriteLine("Группы с таким ID не существует!");
        }
        public void DeleteGroup(int id)
        {
            Group groupToDelete = GetGroupById(id);
            if (groupToDelete != null)
            {
                _groups.Remove(groupToDelete);
                SaveGroupsToDatabase();
                Console.WriteLine("Вы успешно удалили группу!");
                return;
            }
            Console.WriteLine("Группы с таким ID не существует!");
        }
        public void ViewAllGroups()
        {
            Console.WriteLine("\nСписок существующих групп: ");
            foreach (var group in _groups)
            {
                Console.WriteLine($"ID: {group.Id}, Название: {group.Name}, Доп.информация: {group.Info}");
            }
        }
    }
}
