using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "NewTaskData", menuName = "ARLaser/Task Data")]
public class TaskData : ScriptableObject
{
    public string taskName;
    public List<StepData> steps;

    [System.Serializable]
    public class StepData
    {
        public string stepName;
        [TextArea(4, 5)]
        public string description;
        public List<AudioClip> voiceovers;

        public List<GameObject> guideObject;
        public Sprite image;
        public VideoClip video;
        [TextArea(2, 3)]
        public string warning;

        public float duration = 10f;
    }
}