using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    [Header("Countdown Settings")]
    [SerializeField] private float countdownTime = 30f;
    [SerializeField] private string timeoutText = "Timeout";
    [SerializeField] private TextMeshProUGUI timerText;

    private float timeLeft;
    private bool isCounting = false;

    private void Start()
    {
        timerText = GetComponentInChildren<TextMeshProUGUI>();
        if (timerText == null) Debug.LogWarning("Timer Text is not assigned in the inspector.");

        ResetTimer();
        StartCountdown();
    }
    private void Update()
    {
        if (!isCounting) return;

        timeLeft -= Time.deltaTime;
        if (timeLeft > 0)
        {
            int minutes = Mathf.FloorToInt(timeLeft / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            timerText.text = timeoutText;
            isCounting = false;
        }
    }
    public void StartCountdown()
    {
        isCounting = true;
    }
    public void StopCountdown()
    {
        isCounting = false;
    }
    public void ResetTimer()
    {
        timeLeft = countdownTime;
        timerText.text = string.Format("{0:00}:{1:00}", Mathf.FloorToInt(countdownTime / 60), Mathf.FloorToInt(countdownTime % 60));
    }
}
