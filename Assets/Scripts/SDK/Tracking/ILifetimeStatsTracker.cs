namespace SDK.Tracking
{
    public interface ILifetimeStatsTracker
    {
        void SetParameter(string key, object value);
        T GetParameter<T>(string key);
        int IncreaseValue(string key);
    }
}