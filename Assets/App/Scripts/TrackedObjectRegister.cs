using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackedObjectRegister : MonoBehaviour
{
    [System.Serializable]
    public class ObjectGuideInfo
    {
        public GameObject obj;
        public int taskIndex;
        public int stepIndex;
    }

    public ObjectGuideInfo[] objectToRegister;

    void Start()
    {
        foreach (var entry in objectToRegister)
        {
            GameManager.Instance.RegisterGuideObject(entry.obj, entry.taskIndex, entry.stepIndex);
        }
    }
}
