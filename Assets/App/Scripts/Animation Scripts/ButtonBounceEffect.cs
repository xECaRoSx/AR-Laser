using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBounceEffect : MonoBehaviour, IPointerDownHandler
{
    [Header("Bounce Settings")]
    public float scaleDown = 0.85f;
    public float bounceDuration = 0.2f;
    public AnimationCurve bounceCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private Vector3 originalScale;
    private Coroutine currentRoutine;

    void Awake()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentRoutine != null) StopCoroutine(currentRoutine);
        currentRoutine = StartCoroutine(Bounce());
    }

    private System.Collections.IEnumerator Bounce()
    {
        float time = 0f;
        Vector3 targetScale = originalScale * scaleDown;

        while (time < bounceDuration)
        {
            float t = time / bounceDuration;
            transform.localScale = Vector3.Lerp(originalScale, targetScale, bounceCurve.Evaluate(t));
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        time = 0f;
        while (time < bounceDuration)
        {
            float t = time / bounceDuration;
            transform.localScale = Vector3.Lerp(targetScale, originalScale, bounceCurve.Evaluate(t));
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}