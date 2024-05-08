using System.Text.Json;

namespace SommerhusSite.Services
{
    public static class SessionHelper
{
    public static T Get<T>(HttpContext context)
    {
        string sessionName = typeof(T).Name;
        string s = context.Session.GetString(sessionName);
        if (string.IsNullOrEmpty(s))
        {
            throw new ArgumentException($"No session data found for {sessionName}");
        }
        return JsonSerializer.Deserialize<T>(s);
    }

    public static void Set<T>(T data, HttpContext context)
    {
        string sessionName = typeof(T).Name;
        string s = JsonSerializer.Serialize(data);
        context.Session.SetString(sessionName, s);
    }

    public static void Clear<T>(HttpContext context)
    {
        string sessionName = typeof(T).Name;
        context.Session.Remove(sessionName);
    }
}
}
