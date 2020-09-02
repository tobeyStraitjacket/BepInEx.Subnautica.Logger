using BepInEx.Logging;
#if SUBNAUTICA
using Oculus.Newtonsoft.Json;
using Oculus.Newtonsoft.Json.Converters;
#elif BELOWZERO
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
#endif
using QModManager.API;

namespace BepInEx
{
    internal static class Logger
    {
        private static ManualLogSource _logger;
        public static ManualLogSource logger
            => _logger ??= Logging.Logger.CreateLogSource(QModServices.Main.GetMyMod().DisplayName);

        public static void Log(LogLevel level, object data) => logger.Log(level, data);

        public static void LogDebug(object data) => logger.LogDebug(data);

        public static void LogError(object data) => logger.LogError(data);

        public static void LogFatal(object data) => logger.LogFatal(data);

        public static void LogInfo(object data) => logger.LogInfo(data);

        public static void LogMessage(object data) => logger.LogMessage(data);

        public static void LogWarning(object data) => logger.LogWarning(data);
    }
}
