using System;
using UnityEngine;

namespace Long18.Events
{
    public static class DelegateExtensions
    {
        private const string DEFAULT_LOG = "Event was raised but no one was listening.";

        public static void SafeInvoke<T>(this Delegate action, T value, string log = DEFAULT_LOG)
        {
            if (action == null)
            {
                Debug.LogWarning(log);
                return;
            }

            action.DynamicInvoke(value);
        }

        public static void SafeInvoke(this Delegate action, string log = DEFAULT_LOG)
        {
            if (action == null)
            {
                Debug.LogWarning(log);
                return;
            }

            action.DynamicInvoke();
        }

        public static void SafeInvoke(this Delegate action, params object[] args)
        {
            if (action == null)
            {
                Debug.LogWarning(DEFAULT_LOG);
                return;
            }

            action.DynamicInvoke(args);
        }
    }
}