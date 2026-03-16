using Celeste.Mod;

namespace KongtiaoToolbox.Extensions
{
    public static class Extensions
    {
        public static void Log(this string message, LogLevel level = LogLevel.Debug) => Logger.Log(level, "KongtiaoToolbox", message);
    }
}