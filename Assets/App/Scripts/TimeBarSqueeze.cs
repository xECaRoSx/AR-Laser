using UnityEngine;
using UnityEngine.UI;

public class TimeBarSqueeze : MonoBehaviour
{
    [SerializeField] private GameObject leftBar;
    private RectTransform leftRect;
    private Image leftImage;

    [SerializeField] private GameObject rightBar;
    private RectTransform rightRect;
    private Image rightImage;

    private float totalTime;
    private float timer;
    private bool isRunning = false;
    private Color currentColor = Color.green;

    void Awake()
    {
        leftRect = leftBar.GetComponent<RectTransform>();
        leftImage = leftBar.GetComponent<Image>();

        rightRect = rightBar.GetComponent<RectTransform>();
        rightImage = rightBar.GetComponent<Image>();
}

    void Update()
    {
        if (!isRunning || timer <= 0f) return;

        timer -= Time.deltaTime;
        float t = Mathf.Clamp01(timer/totalTime);    // t -> between 0 and 1

        // ===== Bar Shrinking =====
        leftRect.anchorMin = new Vector2(0.5f * (1 - t), 0f);    // Left bar: move AnchorMin from 0 to 0.5
        leftRect.anchorMax = new Vector2(0.5f, 1f);

        rightRect.anchorMin = new Vector2(0.5f, 0f);    // Right bar: move AnchorMax from 1 to 0.5
        rightRect.anchorMax = new Vector2(0.5f + 0.5f * t, 1f);

        // ===== Bar Color =====
        Color barColor = GetInterpolatedColor(t);
        currentColor = Color.Lerp(currentColor, barColor, Time.deltaTime * 5f); // 5f = speed factor
        leftImage.color = barColor;
        rightImage.color = barColor;

        if (timer <= 0f) isRunning = false;    // Stop the timer when it reaches 0
    }

    public void StartTimer(float duration)
    {
        totalTime = duration;
        timer = duration;
        isRunning = true;

        leftRect.anchorMin = new Vector2(0f, 0f);
        leftRect.anchorMax = new Vector2(0.5f, 1f);
        rightRect.anchorMin = new Vector2(0.5f, 0f);
        rightRect.anchorMax = new Vector2(1f, 1f);
    }
    public void StopTimer()
    {
        isRunning = false;
    }
    Color GetInterpolatedColor(float t)
    {
        if (t > 0.75f)
        {
            float lerpT = Mathf.InverseLerp(1f, 0.75f, t);
            return Color.Lerp(Color.green, Color.yellow, lerpT);
        }
        else if (t > 0.5f)
        {
            float lerpT = Mathf.InverseLerp(0.75f, 0.5f, t);
            return Color.Lerp(Color.yellow, new Color(1f, 0.5f, 0f), lerpT); // orange
        }
        else if (t > 0.25f)
        {
            float lerpT = Mathf.InverseLerp(0.5f, 0.25f, t);
            return Color.Lerp(new Color(1f, 0.5f, 0f), Color.red, lerpT);
        }
        else
        {
            return Color.red;
        }
    }

}