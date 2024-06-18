using System.Collections.Generic;

namespace SDK.Tracking
{
    public class SessionStatsTracker : ISessionStatsTracker
    {
        private Dictionary<string, object> _values = new();

        public void SetParameter(string key, object value)
        {
            _values[key] = value;
        }

        public T GetParameter<T>(string key)
        {
            if (!_values.ContainsKey(key))
            {
                return default;
            }

            return (T)_values[key];
        }

        public int IncreaseValue(string key)
        {
            var value = GetParameter<int>(key) + 1;
            SetParameter(key, value);
            return value;
        }
    }
}