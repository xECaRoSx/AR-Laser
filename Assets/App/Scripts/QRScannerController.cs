using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class QRScannerController : MonoBehaviour
{
    [Header("AR Components")]
    public ARTrackedImageManager trackedImageManager;
    public GameObject lockedAnchor;
    public string targetImageName = "MyQRCode";

    private bool isLocked = false;

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs args)
    {
        if (isLocked) return;

        foreach (var trackedImage in args.added)
        {
            TryLock(trackedImage);
        }

        foreach (var trackedImage in args.updated)
        {
            TryLock(trackedImage);
        }
    }

    private void TryLock(ARTrackedImage trackedImage)
    {
        if (trackedImage.referenceImage.name == targetImageName && trackedImage.trackingState == TrackingState.Tracking)
        {
            lockedAnchor.transform.position = trackedImage.transform.position;

            float yaw = trackedImage.transform.eulerAngles.y;
            lockedAnchor.transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        }
    }

    public void ConfirmPlacement()
    {
        isLocked = true;
        trackedImageManager.enabled = false;
        Debug.Log("QR Locked at pose: " + lockedAnchor.transform.position);
    }

    public void Rescan()
    {
        isLocked = false;
        trackedImageManager.enabled = true;
        Debug.Log("QR Scan restarted");
    }
}
