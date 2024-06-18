namespace SDK.Analytics
{
    public class AnalyticsFacade : IAnalyticsFacade
    {
        private IAnalyticsSender[] _analyticsSenders;


        public void Init(params IAnalyticsSender[] providers)
        {
            _analyticsSenders = providers;
        }

        public void SendEvent(AnalyticEvent analyticsAnalyticEvent)
        {
            foreach (var sender in _analyticsSenders)
            {
                sender.SendEvent(analyticsAnalyticEvent);
            }
        }

        public void SendEvent(string eventName)
        {
            foreach (var sender in _analyticsSenders)
            {
                sender.SendEvent(new AnalyticEvent(eventName));
            }
        }
    }
}