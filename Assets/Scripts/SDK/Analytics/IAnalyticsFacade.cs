namespace SDK.Analytics
{
    public interface IAnalyticsFacade
    {
        void SendEvent(AnalyticEvent analyticsAnalyticEvent);
        void SendEvent(string eventName);
    }
}