using SimpleBL.Interfaces;
using SimpleEntites;

namespace SimpleBL.Services
{
    public class UserBL : IUserBL
    {
        private static List<string> names = new List<string>()
        {
            "Haim1",
            "Haim2",
            "Haim3",
        };

        public string GetUserFirstName()
        {
            return "Haim";
        }

        public string GetUserFirstNameByIdQuery(string id)
        {
            return names[int.Parse(id) - 1];
        }

        public string GetUserFirstNameByIdRoute(string id)
        {
            return names[int.Parse(id) - 1];
        }

        public List<string> GetAllUserNaems() 
        {
            return names;
        }

        public void AddUserName(User user)
        {
            names.Add(user.userName);
        }
    }
}
