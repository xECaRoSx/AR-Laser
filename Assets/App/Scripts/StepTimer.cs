using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepTimer : MonoBehaviour
{
    public static StepTimer Instance;
    private Coroutine countdown;

    [Header("Step Timer Settings")]
    public bool autoNext = true;
    public TimeBarSqueeze timeBar;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void StartCountdown(float duration)
    {
        if (countdown != null) StopCoroutine(countdown);
        countdown = StartCoroutine(Countdown(duration));
        timeBar.StartTimer(duration);
    }

    private IEnumerator Countdown(float time)
    {
        while (time > 0f)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        if (autoNext) TaskManager.Instance.NextStep();
    }
}
