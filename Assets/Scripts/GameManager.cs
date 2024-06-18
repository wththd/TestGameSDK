using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GoalZone leftGoalZone;
    public GoalZone rightGoalZone;
    public WinPanel winPanel;

    private void Start()
    {
        Application.targetFrameRate = 60;

        leftGoalZone.OnGoalReached += () => OnGoalReached("RIGHT");
        rightGoalZone.OnGoalReached += () => OnGoalReached("LEFT");

        winPanel.OnRestartClicked += OnRestartClicked;
        winPanel.Hide();
    }

    private static void OnRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnGoalReached(string winner)
    {
        winPanel.SetText($"{winner} WINS!");
        winPanel.Show();
    }
}