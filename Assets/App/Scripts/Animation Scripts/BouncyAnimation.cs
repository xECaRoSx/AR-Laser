using UnityEngine;

public class BouncyAnimation : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float scaleAmount = 0.2f;
    public float frequency = 2f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scaleOffset = Mathf.Abs(Mathf.Sin(Time.time * frequency)) * scaleAmount;
        transform.localScale = originalScale + Vector3.one * scaleOffset;
    }
}
