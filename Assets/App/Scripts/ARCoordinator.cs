using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class ARCoordinator : MonoBehaviour
{
    public static ARCoordinator Instance;

    [Header("AR References")]
    [SerializeField] private ARSession arSession;
    [SerializeField] private ARTrackedImageManager trackedImageManager;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ResetAndEnableImageTracking()
    {
        Debug.Log("[ARCoordinator] Reset & Enable Image Tracking");

        arSession.Reset();

        ClearTrackedImages();
        trackedImageManager.enabled = true;
    }

    public void DisableImageTracking()
    {
        Debug.Log("[ARCoordinator] Disable Image Tracking");

        trackedImageManager.enabled = false;
        ClearTrackedImages();
    }

    public void DisableAllTracking()
    {
        Debug.Log("[ARCoordinator] Disable All Tracking");

        trackedImageManager.enabled = false;
        ClearTrackedImages();
    }

    private void ClearTrackedImages()
    {
        var images = new List<ARTrackedImage>();

        foreach (var img in trackedImageManager.trackables)
        {
            if (img) images.Add(img);
        }

        foreach (var img in images)
        {
            if (img) Destroy(img.gameObject);
        }
    }
}