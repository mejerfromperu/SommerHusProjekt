using System.Text.Json;

namespace SommerhusSite.Services
{
    public class SessionHelper
    {

        public static T Get<T>(HttpContext context)
        {
            String sessionName = typeof(T).Name;
            String s = context.Session.GetString(sessionName);
            if (string .IsNullOrEmpty(s))
            {
                throw new ArgumentException($"No session name found sorry {sessionName}");
            }
            return JsonSerializer.Deserialize<T>(s);

        }
        public static void Set<T>(T t, HttpContext context)
        {
            String sessionName = typeof(T).Name;
            String s = JsonSerializer.Serialize(t);
            context.Session.SetString(sessionName, s);
        }

        public static void Clear<T>(HttpContext context)
        {
            context.Session.Remove(typeof(T).Name);
        }


    }
}
