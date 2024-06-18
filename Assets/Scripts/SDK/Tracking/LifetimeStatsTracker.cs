using System.Collections.Generic;
using UnityEngine;

namespace SDK.Tracking
{
    public class LifetimeStatsTracker : ILifetimeStatsTracker
    {
        private Dictionary<string, object> _objects;
        private const string SaveKey = "LifetimeStats";

        public LifetimeStatsTracker()
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                _objects =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(
                        PlayerPrefs.GetString(SaveKey));
            }
            else
            {
                _objects = new Dictionary<string, object>();
            }
        }
        
        public void SetParameter(string key, object value)
        {
            _objects[key] = value;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_objects);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public T GetParameter<T>(string key)
        {
            if (!_objects.ContainsKey(key))
            {
                return default;
            }

            return (T)_objects[key];
        }

        public int IncreaseValue(string key)
        {
            var value = GetParameter<int>(key) + 1;
            SetParameter(key, value);
            return value;
        }
    }
}