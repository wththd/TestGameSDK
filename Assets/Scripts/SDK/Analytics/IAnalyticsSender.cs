namespace SDK.Analytics
{
    public interface IAnalyticsSender
    {
        void SendEvent(AnalyticEvent analyticsAnalyticEvent);
    }
}