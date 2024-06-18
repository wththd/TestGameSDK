using SDK.Analytics;
using SDK.Tracking;
using UnityEngine;

namespace SDK
{
    public class GameSDK : MonoBehaviour
    {
        private static GameSDK _instance;
        private bool _inited;
        private int _sessionCount;
        private IAnalyticsFacade _analytics;
        
        public int SessionCount => _sessionCount;
        public IAnalyticsFacade Analytics => _analytics;
        public ISessionStatsTracker SessionTracker { get; private set; }
        public ILifetimeStatsTracker LifetimeTracker { get; private set; }

        public static GameSDK Instance
        {
            get
            {
                if (_instance == null)
                {
                    InstantiateAndInitialize();
                }
                return _instance;
            }
        }
        
        public void EnsureIsInitialized()
        {
            // For instancing
        }

        private void Init()
        {
            if (_inited)
            {
                return;
            }
            
            SetSessionNumber();
            CreateAnalytics();
            InitTrackers();
#if USE_CRASHLYTICS
            SetUpCrashlytics();
#endif
            _inited = true;
        }

        protected virtual void InitTrackers()
        {
            SessionTracker = new SessionStatsTracker();
            LifetimeTracker = new LifetimeStatsTracker();
        }

        protected virtual void CreateAnalytics()
        {
            var facade = new AnalyticsFacade();
            facade.Init(new FirebaseAnalyticsSender());
            _analytics = facade;
        }

#if USE_CRASHLYTICS
        protected virtual void SetUpCrashlytics()
        {
            Firebase.Crashlytics.Crashlytics.SetCustomKey(AnalyticsNames.SessionId, _sessionCount.ToString());
        }
#endif

        protected virtual void SetSessionNumber()
        {
            if (PlayerPrefs.HasKey("Session"))
            {
                _sessionCount = PlayerPrefs.GetInt("Session") + 1;
            }
            else
            {
                _sessionCount = 1;
            }

            PlayerPrefs.SetInt("Session", _sessionCount);
            PlayerPrefs.Save();
        }

        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private static void InstantiateAndInitialize()
        {
            var go = new GameObject();
            var sdk = go.AddComponent<GameSDK>();
            _instance = sdk;
            _instance.Init();
        }
    }
}