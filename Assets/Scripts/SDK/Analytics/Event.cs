using System.Collections.Generic;

namespace SDK.Analytics
{
    public class AnalyticEvent
    {
        public readonly string EventName;
        public readonly Dictionary<string, object> Payload = new();
        
        public AnalyticEvent(string name)
        {
            EventName = name;
        }

        public AnalyticEvent AddParameter(string paramName, string value)
        {
            return AddParamImplementation(paramName, value);
        }
        
        public AnalyticEvent AddParameter(string paramName, int value)
        {
            return AddParamImplementation(paramName, value);
        }
        
        public AnalyticEvent AddParameter(string paramName, double value)
        {
            return AddParamImplementation(paramName, value);
        }
        
        private AnalyticEvent AddParamImplementation(string paramName, object value)
        {
            Payload.Add(paramName, value);
            return this;
        }
    }
}