namespace SDK.Tracking
{
    public interface ISessionStatsTracker
    {
        void SetParameter(string key, object value);
        T GetParameter<T>(string key);
        int IncreaseValue(string key);
    }
}