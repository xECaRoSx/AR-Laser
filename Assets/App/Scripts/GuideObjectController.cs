using UnityEngine;
using System.Collections.Generic;

public class GuideObjectController : MonoBehaviour
{
    public static GuideObjectController Instance;

    private List<GameObject> currentlyActive = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ShowGuideObjects(List<GameObject> guideObjects)
    {
        foreach (var obj in currentlyActive)
        {
            if (obj != null)
                obj.SetActive(false);
        }
        currentlyActive.Clear();

        foreach (var obj in guideObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
                currentlyActive.Add(obj);
            }
        }
    }
}
