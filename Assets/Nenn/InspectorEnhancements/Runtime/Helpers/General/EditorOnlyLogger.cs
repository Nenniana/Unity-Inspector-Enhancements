using System;
using UnityEngine;

namespace Nenn.InspectorEnhancements.Runtime.Helpers.General
{
    public static class EditorOnlyLogger
    {
        public static bool EnableLogging = true;
        public static bool EnableWarnings = true;
        public static bool EnableErrors = true;
        public static bool EnableExceptions = true;
        
        private const string Prefix = "[InspectorEnhancements]";

        public static void Log(string message)
        {
#if UNITY_EDITOR
            if (EnableLogging)
            {
                Debug.Log($"{Prefix} {message}");
            }
#endif
        }

        public static void LogWarning(string message)
        {
#if UNITY_EDITOR
            if (EnableWarnings)
            {
                Debug.LogWarning($"{Prefix} {message}");
            }
#endif
        }

        public static void LogError(string message)
        {
#if UNITY_EDITOR
            if (EnableErrors)
            {
                Debug.LogError($"{Prefix} {message}");
            }
#endif
        }

        public static void LogException(Exception ex)
        {
#if UNITY_EDITOR
            if (EnableExceptions)
            {
                Debug.LogException(ex);
            }
#endif
        }
    }
}