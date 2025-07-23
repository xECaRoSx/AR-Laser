using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    [Header("Floating Settings")]
    public float amplitude = 0.2f;
    public float frequency = 1f;

    private Vector3 localStartOffset;
    private Vector3 worldStartPos;

    void Start()
    {
        localStartOffset = Vector3.zero;
        worldStartPos = transform.TransformPoint(localStartOffset);
    }

    void Update()
    {
        float offset = Mathf.Abs(Mathf.Sin(Time.time * frequency)) * amplitude;

        Vector3 worldOffset = transform.up * offset;
        transform.position = worldStartPos + worldOffset;
    }
}
