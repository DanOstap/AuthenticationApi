namespace AuthenticationApi.Models
{
    public class Roles
    {
        public Dictionary<string, int> Role = new Dictionary<string, int> {
            {"Admin", 1 },
            {"Manager", 2 },
            {"User", 3 }
        };
    }
}
