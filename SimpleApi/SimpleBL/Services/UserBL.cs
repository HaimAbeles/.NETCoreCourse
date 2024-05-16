using SimpleBL.Interfaces;
using SimpleEntites;

namespace SimpleBL.Services
{
    public class UserBL : IUserBL
    {
        private static List<User> users = new List<User>();

        public UserBL()
        {
            users.Add(new User()
            {
                Id = 1,
                userName = "Haim",
                classId = 1,
            });
            users.Add(new User()
            {
                Id = 2,
                userName = "Yosef",
                classId = 1,
            });
            users.Add(new User()
            {
                Id = 3,
                userName = "Moshe",
                classId = 2,
            });
            users.Add(new User()
            {
                Id = 4,
                userName = "Simcha",
                classId = 2,
            });
        }

        public string GetUserFirstName()
        {
            return "Haim";
        }

        public string GetUserFirstNameByIdQuery(string id)
        {
            return users[int.Parse(id) - 1].userName;
        }

        public string GetUserFirstNameByIdRoute(string id)
        {
            return users[int.Parse(id) - 1].userName;
        }

        public List<string> GetAllUserNames()
        {
            List<string> list = new List<string>();
            list = users.Select(x => x.userName).ToList();
            return list;
        }

        public void AddUserName(User user)
        {
            users.Add(user);
        }

        public User Test()
        {
            return users.SingleOrDefault(x => x.Id == 1);
        }
    }
}
