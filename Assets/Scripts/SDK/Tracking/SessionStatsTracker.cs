using System.Collections.Generic;

namespace SDK.Tracking
{
    public class SessionStatsTracker : ISessionStatsTracker
    {
        private Dictionary<string, int> _values = new();

        public void SetParameter(string key, int value)
        {
            _values[key] = value;
        }

        public int GetParameter(string key)
        {
            if (!_values.ContainsKey(key))
            {
                return 0;
            }

            return _values[key];
        }

        public int IncreaseValue(string key)
        {
            var value = GetParameter(key) + 1;
            SetParameter(key, value);
            return value;
        }
    }
}