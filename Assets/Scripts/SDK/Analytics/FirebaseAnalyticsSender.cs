using System;
using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;

namespace SDK.Analytics
{
    public class FirebaseAnalyticsSender : IAnalyticsSender
    {
        public void SendEvent(AnalyticEvent analyticsAnalyticEvent)
        {
            FirebaseAnalytics.LogEvent(analyticsAnalyticEvent.EventName, GetParametersFromEventPayload(analyticsAnalyticEvent.Payload));
        }

        private Parameter[] GetParametersFromEventPayload(Dictionary<string, object> values)
        {
            if (values == null || values.Count == 0)
            {
                return Array.Empty<Parameter>();
            }

            var result = new List<Parameter>();
            foreach (var value in values)
            {
                result.Add(new Parameter(value.Key, value.Value.ToString()));
            }

            return result.ToArray();
        }
    }
}