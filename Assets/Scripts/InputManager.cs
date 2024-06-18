using SDK;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Camera targetCamera;
    public Paddle leftPaddle;
    public Paddle rightPaddle;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        var mouseScreenPosition = Input.mousePosition;

        mouseScreenPosition.z = targetCamera.WorldToScreenPoint(transform.position).z;
        var clickPosition = targetCamera.ScreenToWorldPoint(mouseScreenPosition);

        if (clickPosition.x > 0f)
        {
            rightPaddle.MoveToY(clickPosition.y);
            GameSDK.Instance.SessionTracker.IncreaseValue("right_move_count");
        }
        else
        {
            leftPaddle.MoveToY(clickPosition.y);
            GameSDK.Instance.SessionTracker.IncreaseValue("left_move_count");
        }
    }
}