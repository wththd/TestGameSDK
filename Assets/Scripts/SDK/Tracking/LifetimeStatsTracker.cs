using System.Collections.Generic;
using UnityEngine;

namespace SDK.Tracking
{
    public class LifetimeStatsTracker : ILifetimeStatsTracker
    {
        private Dictionary<string, int> _objects;
        private const string SaveKey = "LifetimeStats";

        public LifetimeStatsTracker()
        {
            if (PlayerPrefs.HasKey(SaveKey))
            {
                _objects =
                    Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, int>>(
                        PlayerPrefs.GetString(SaveKey));
            }
            else
            {
                _objects = new Dictionary<string, int>();
            }
        }
        
        public void SetParameter(string key, int value)
        {
            _objects[key] = value;
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(_objects);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }

        public int GetParameter(string key)
        {
            if (!_objects.ContainsKey(key))
            {
                return 0;
            }

            return _objects[key];
        }

        public int IncreaseValue(string key)
        {
            var value = GetParameter(key) + 1;
            SetParameter(key, value);
            return value;
        }
    }
}