using UnityEngine;
using UnityEngine.Events;

namespace Long18.Events
{
    public static class UnityActionExtensions
    {
        private const string DEFAULT_LOG = "Event was raised but no one was listening.";

        public static void SafeInvoke<T>(this UnityAction<T> action, T value, string log = DEFAULT_LOG)
        {
            if (action == null)
            {
                Debug.LogWarning(log);
                return;
            }

            action.Invoke(value);
        }

        public static void SafeInvoke(this UnityAction action, string log = DEFAULT_LOG)
        {
            if (action == null)
            {
                Debug.LogWarning(log);
                return;
            }

            action.Invoke();
        }
    }
}