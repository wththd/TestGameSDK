namespace SDK.Tracking
{
    public interface ILifetimeStatsTracker
    {
        void SetParameter(string key, int value);
        int GetParameter(string key);
        int IncreaseValue(string key);
    }
}