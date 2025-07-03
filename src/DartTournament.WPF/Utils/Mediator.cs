using System;
using System.Collections.Generic;

namespace DartTournament.WPF.Utils
{
    public static class Mediator
    {
        private static readonly Dictionary<string, List<Action<object>>> _subscribers = new();

        public static void Subscribe(string message, Action<object> callback)
        {
            if (!_subscribers.ContainsKey(message))
                _subscribers[message] = new List<Action<object>>();
            _subscribers[message].Add(callback);
        }

        public static void Unsubscribe(string message, Action<object> callback)
        {
            if (_subscribers.ContainsKey(message))
                _subscribers[message].Remove(callback);
        }

        public static void Notify(string message, object args = null)
        {
            if (_subscribers.ContainsKey(message))
                foreach (var callback in _subscribers[message])
                    callback(args);
        }
    }
}