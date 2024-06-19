namespace SDK.Tracking
{
    public interface ISessionStatsTracker
    {
        void SetParameter(string key, int value);
        int GetParameter(string key);
        int IncreaseValue(string key);
    }
}