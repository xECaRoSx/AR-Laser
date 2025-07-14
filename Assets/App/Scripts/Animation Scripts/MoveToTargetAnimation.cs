using UnityEngine;
using System.Collections;

public class MoveToTargetAnimator : MonoBehaviour
{
    public enum LoopMode { None, Loop, PingPong }

    [Header("Target Settings")]
    public Transform target;
    public float moveSpeed = 2f;
    public float stopDistance = 0.01f;

    [Header("Playback Settings")]
    public bool autoStart = true;
    public LoopMode loopMode = LoopMode.None;

    [Header("Delay Settings")]
    public float delayAfterArrive = 0.5f;

    private Vector3 localStartPos;
    private bool movingForward = true;
    private bool isDelaying = false;

    private Coroutine animationCoroutine;

    void Start()
    {
        localStartPos = transform.localPosition;

        if (autoStart)
            animationCoroutine = StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        while (true)
        {
            Vector3 destination = movingForward ? target.localPosition : localStartPos;

            while (Vector3.Distance(transform.localPosition, destination) > stopDistance)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, destination, moveSpeed * Time.deltaTime);
                yield return null;
            }

            isDelaying = true;
            yield return new WaitForSeconds(delayAfterArrive);
            isDelaying = false;

            switch (loopMode)
            {
                case LoopMode.None:
                    yield break;

                case LoopMode.Loop:
                    transform.localPosition = localStartPos;
                    break;

                case LoopMode.PingPong:
                    movingForward = !movingForward;
                    break;
            }
        }
    }

    void OnEnable()
    {
        localStartPos = transform.localPosition;
        movingForward = true;
        isDelaying = false;

        if (autoStart)
        {
            if (animationCoroutine != null)
                StopCoroutine(animationCoroutine);

            animationCoroutine = StartCoroutine(Animate());
        }
    }

    void OnDisable()
    {
        if (animationCoroutine != null)
            StopCoroutine(animationCoroutine);
    }
}
