using System.Diagnostics;
using SDK;
using SDK.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public string gameSceneName = "Game";
    public float loadTime = 1.2f;
    private Stopwatch _stopwatch = new();

    private void Awake()
    {
        GameSDK.Instance.EnsureIsInitialized();
        _stopwatch.Start();
    }

    private void Start()
    {
        Invoke(nameof(LoadGameScene), loadTime);
    }

    private void LoadGameScene()
    {
        _stopwatch.Stop();
        GameSDK.Instance.Analytics.SendEvent(
            new AnalyticEvent(AnalyticsNames.GameStart)
                .AddParameter(AnalyticsNames.SessionId, GameSDK.Instance.SessionCount)
                .AddParameter(AnalyticsNames.LoadTime, _stopwatch.Elapsed.TotalSeconds));
        
        SceneManager.LoadScene(gameSceneName);
    }
}