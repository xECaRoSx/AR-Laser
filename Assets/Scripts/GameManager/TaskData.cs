using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class TaskData
{
    public string taskName;
    public List<StepData> steps;
}

[System.Serializable]
public class StepData
{
    public string stepName;

    [Tooltip("Guideline object for tutorial - Direction arrow, Hand coach, etc.")]
    public List<GameObject> guideObject;

    [Tooltip("Time limit for each step. (in seconds)")]
    public float duration = 20f;    // 20 seconds

    [TextArea(2, 5)]
    public string description;
    public AudioClip voiceover;

    public Sprite instructionImage;
    public VideoClip instructionVideo;

    public bool hasWarning;
    [TextArea(2, 3)]
    public string warningText;
}