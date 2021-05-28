using BepInEx.Logging;
using QModManager.API;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BepInEx.Subnautica
{
    internal class Logger : MonoBehaviour
    {
        private static Logger instance;
        private static Logger Instance
            => instance ??= new GameObject("BepInEx.Subnautica.Logger").AddComponent<Logger>();

        private static ManualLogSource logSource;
        public static ManualLogSource LogSource
            => logSource ??= Logging.Logger.CreateLogSource(QModServices.Main.GetMyMod().DisplayName);

        private bool updateExecuting = false;
        private readonly List<string> DisplayMessages = new List<string>();
        private readonly List<object> LogDebugs = new List<object>();
        private readonly List<object> LogErrors = new List<object>();
        private readonly List<object> LogFatals = new List<object>();
        private readonly List<object> LogInfos = new List<object>();
        private readonly List<object> LogMessages = new List<object>();
        private readonly List<object> LogWarnings = new List<object>();

#pragma warning disable IDE0051 // Remove unused private members
        private void Update()
#pragma warning restore IDE0051 // Remove unused private members
        {
            updateExecuting = true;

            foreach (var message in DisplayMessages)
                ErrorMessage.AddError(message);
            foreach (var debug in LogDebugs)
                LogDebug(debug);
            foreach (var error in LogErrors)
                LogError(error);
            foreach (var fatal in LogFatals)
                LogFatal(fatal);
            foreach (var info in LogInfos)
                LogInfo(info);
            foreach (var message in LogMessages)
                LogMessage(message);
            foreach (var warning in LogWarnings)
                LogWarning(warning);

            DisplayMessages.Clear();
            LogDebugs.Clear();
            LogErrors.Clear();
            LogFatals.Clear();
            LogInfos.Clear();
            LogMessages.Clear();
            LogWarnings.Clear();

            updateExecuting = false;
        }

        public static void Log(LogLevel level, object data) => LogSource.Log(level, data);

        public static void LogDebug(object data) => LogSource.LogDebug(data);

        public static void LogError(object data) => LogSource.LogError(data);

        public static void LogFatal(object data) => LogSource.LogFatal(data);

        public static void LogInfo(object data) => LogSource.LogInfo(data);

        public static void LogMessage(object data) => LogSource.LogMessage(data);

        public static void LogWarning(object data) => LogSource.LogWarning(data);

        private const int delay = 500;
        private static async Task LogAsync(object data, List<object> list)
        {
            while (Instance.updateExecuting)
                await Task.Delay(delay);
            list.Add(data);
        }

        public static async Task LogDebugAsync(object data) => await LogAsync(data, Instance.LogDebugs);

        public static async Task LogErrorAsync(object data) => await LogAsync(data, Instance.LogErrors);

        public static async Task LogFatalAsync(object data) => await LogAsync(data, Instance.LogFatals);

        public static async Task LogInfoAsync(object data) => await LogAsync(data, Instance.LogInfos);

        public static async Task LogMessageAsync(object data) => await LogAsync(data, Instance.LogMessages);

        public static async Task LogWarningAsync(object data) => await LogAsync(data, Instance.LogWarnings);

        public static async Task DisplayMessageAsync(object data)
        {
            while (Instance.updateExecuting)
                await Task.Delay(delay);
            Instance.DisplayMessages.Add(data.ToString());
        }
    }
}
