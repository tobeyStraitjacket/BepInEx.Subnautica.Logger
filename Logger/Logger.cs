using BepInEx.Logging;
using QModManager.API;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BepInEx.Subnautica
{
    internal class Logger : MonoBehaviour
    {
        private static Logger _instance;
        private static Logger instance
            => _instance ??= new GameObject("BepInEx.Subnautica.Logger").AddComponent<Logger>();

        private static ManualLogSource _logger;
        public static ManualLogSource logger
            => _logger ??= Logging.Logger.CreateLogSource(QModServices.Main.GetMyMod().DisplayName);

        private bool updateExecuting = false;
        private List<string> DisplayMessages = new List<string>();
        private List<object> LogDebugs = new List<object>();
        private List<object> LogErrors = new List<object>();
        private List<object> LogFatals = new List<object>();
        private List<object> LogInfos = new List<object>();
        private List<object> LogMessages = new List<object>();
        private List<object> LogWarnings = new List<object>();
        private void Update()
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

        public static void Log(LogLevel level, object data) => logger.Log(level, data);

        public static void LogDebug(object data) => logger.LogDebug(data);

        public static void LogError(object data) => logger.LogError(data);

        public static void LogFatal(object data) => logger.LogFatal(data);

        public static void LogInfo(object data) => logger.LogInfo(data);

        public static void LogMessage(object data) => logger.LogMessage(data);

        public static void LogWarning(object data) => logger.LogWarning(data);

        private const int delay = 500;
        private static async Task LogAsync(object data, List<object> list)
        {
            while (instance.updateExecuting)
                await Task.Delay(delay);
            list.Add(data);
        }

        public static async Task LogDebugAsync(object data) => await LogAsync(data, instance.LogDebugs);

        public static async Task LogErrorAsync(object data) => await LogAsync(data, instance.LogErrors);

        public static async Task LogFatalAsync(object data) => await LogAsync(data, instance.LogFatals);

        public static async Task LogInfoAsync(object data) => await LogAsync(data, instance.LogInfos);

        public static async Task LogMessageAsync(object data) => await LogAsync(data, instance.LogMessages);

        public static async Task LogWarningAsync(object data) => await LogAsync(data, instance.LogWarnings);

        public static async Task DisplayMessageAsync(object data)
        {
            while (instance.updateExecuting)
                await Task.Delay(delay);
            instance.DisplayMessages.Add(data.ToString());
        }
    }
}
