using System;
using SDK;
using SDK.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GoalZone leftGoalZone;
    public GoalZone rightGoalZone;
    public WinPanel winPanel;

    private void Start()
    {
        var levelsPlayedTotal = GameSDK.Instance.LifetimeTracker.GetParameter<int>(AnalyticsNames.LevelsPlayedTotal);
        var levelInSession = GameSDK.Instance.SessionTracker.GetParameter<int>(AnalyticsNames.LevelInSession) + 1;
        var levelsPlayedSession = GameSDK.Instance.SessionTracker.GetParameter<int>(AnalyticsNames.LevelsPlayedSession);
        
#if USE_CRASHLYTICS
        SetUpCrashlytics(levelInSession);
#endif
        GameSDK.Instance.SessionTracker.SetParameter(AnalyticsNames.LevelInSession, levelInSession);
        
        Application.targetFrameRate = 60;
        
        leftGoalZone.OnGoalReached += () => OnGoalReached("RIGHT");
        rightGoalZone.OnGoalReached += () => OnGoalReached("LEFT");

        winPanel.OnRestartClicked += OnRestartClicked;
        winPanel.Hide();
        
        GameSDK.Instance.SessionTracker.SetParameter(AnalyticsNames.Playtime, DateTime.Now.Ticks);

        GameSDK.Instance.Analytics.SendEvent(new AnalyticEvent(AnalyticsNames.LevelStart)
            .AddParameter(AnalyticsNames.SessionId, GameSDK.Instance.SessionCount)
            .AddParameter(AnalyticsNames.LevelInSession, levelInSession)
            .AddParameter(AnalyticsNames.LevelsPlayedTotal, levelsPlayedTotal)
            .AddParameter(AnalyticsNames.LevelsPlayedSession, levelsPlayedSession));
    }
    
#if USE_CRASHLYTICS
    protected virtual void SetUpCrashlytics(int levelInSession)
    {
        Firebase.Crashlytics.Crashlytics.SetCustomKey(AnalyticsNames.SessionId, levelInSession.ToString());
    }
#endif

    private static void OnRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnGoalReached(string winner)
    {
        winPanel.SetText($"{winner} WINS!");
        winPanel.Show();
        
        GameSDK.Instance.LifetimeTracker.IncreaseValue(AnalyticsNames.LevelsPlayedTotal);
        var levelInSession = GameSDK.Instance.SessionTracker.GetParameter<int>(AnalyticsNames.LevelInSession);
        GameSDK.Instance.SessionTracker.IncreaseValue(AnalyticsNames.LevelsPlayedSession);
        var leftMoveCount = GameSDK.Instance.SessionTracker.GetParameter<int>(AnalyticsNames.LeftMoveCount);
        var rightMoveCount = GameSDK.Instance.SessionTracker.GetParameter<int>(AnalyticsNames.RightMoveCount);
        var playTime =
            new TimeSpan(DateTime.Now.Ticks - GameSDK.Instance.SessionTracker.GetParameter<long>(AnalyticsNames.Playtime));

        GameSDK.Instance.Analytics.SendEvent(new AnalyticEvent(AnalyticsNames.LevelComplete)
            .AddParameter(AnalyticsNames.SessionId, GameSDK.Instance.SessionCount)
            .AddParameter(AnalyticsNames.LevelInSession, levelInSession)
            .AddParameter(AnalyticsNames.Winner, winner)
            .AddParameter(AnalyticsNames.LeftMoveCount, leftMoveCount)
            .AddParameter(AnalyticsNames.RightMoveCount, rightMoveCount)
            .AddParameter(AnalyticsNames.Playtime, playTime.TotalSeconds));

        ClearSessionValues();
    }

    private void ClearSessionValues()
    {
        GameSDK.Instance.SessionTracker.SetParameter(AnalyticsNames.RightMoveCount, 0);
        GameSDK.Instance.SessionTracker.SetParameter(AnalyticsNames.LeftMoveCount, 0);
        GameSDK.Instance.SessionTracker.SetParameter(AnalyticsNames.Playtime, 0);
    }

    private void OnDestroy()
    {
#if USE_CRASHLYTICS
        Firebase.Crashlytics.Crashlytics.SetCustomKey(AnalyticsNames.SessionId, string.Empty);
#endif
    }
}