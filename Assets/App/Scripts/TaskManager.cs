using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance;
    [SerializeField] private List<TaskData> allTasks;

    private int currentTask = 0;
    private int currentStep = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public List<TaskData> GetAllTasks()
    {
        return allTasks;
    }

    public void ResetAll()
    {
        currentTask = 0;
        currentStep = 0;
    }

    public void LoadStep()
    {
        var step = allTasks[currentTask].steps[currentStep];

        UIManager.Instance.ShowStepInfo(
            allTasks[currentTask].taskName,
            step.stepName,
            currentTask + 1,
            allTasks.Count,
            step
        );
        AudioManager.Instance.PlayVoiceovers(step.voiceovers);
        GuideObjectController.Instance.ShowGuideObjects(step.guideObject);
        StepTimer.Instance.StartCountdown(step.duration);
    }

    public void NextStep()
    {
        currentStep++;
        if (currentStep >= allTasks[currentTask].steps.Count)
        {
            currentTask++;
            currentStep = 0;

            if (currentTask >= allTasks.Count)
            {
                AudioManager.Instance.StopVO();
                GameManager.Instance.GoToFinish();
                return;
            }
        }
        LoadStep();
    }

    public void PreviousStep()
    {
        if (currentStep > 0) currentStep--;
        else if (currentTask > 0)
        {
            currentTask--;
            currentStep = allTasks[currentTask].steps.Count - 1;
        }
        LoadStep();
    }
}
