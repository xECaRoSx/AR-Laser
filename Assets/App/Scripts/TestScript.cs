using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TestScript : MonoBehaviour
{
    public TextMeshProUGUI testText;
    private ARTrackedImageManager m_TrackedImageManager;

    private void Awake()
    {
        if (m_TrackedImageManager == null)
        {
            m_TrackedImageManager = FindObjectOfType<ARTrackedImageManager>();
        }
    }
    public void NumTrackable()
    {
        Debug.Log($"Number of trackable object: {m_TrackedImageManager.trackables.count}");
        Debug.Log($"Trackable Name: {m_TrackedImageManager.trackables}");
    }
}