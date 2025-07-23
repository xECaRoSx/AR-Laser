using UnityEngine;

public class GuideObjectAutoRegister : MonoBehaviour
{
    [Header("Step Binding (Starts at 1)")]
    [Tooltip("Task number (e.g., Task 1 = 1). Internally converted to index 0.")]
    public int taskNumber = 1;

    [Tooltip("Step number (e.g., Step 1 = 1). Internally converted to index 0.")]
    public int stepNumber = 1;

    private void Start()
    {
        int taskIndex = taskNumber - 1;
        int stepIndex = stepNumber - 1;

        var taskList = TaskManager.Instance.GetAllTasks();

        if (taskIndex < 0 || taskIndex >= taskList.Count || stepIndex < 0 || stepIndex >= taskList[taskIndex].steps.Count)
        {
            Debug.LogWarning($"{gameObject.name} has invalid task/step number. Task {taskNumber}, Step {stepNumber}");
            return;
        }

        var step = taskList[taskIndex].steps[stepIndex];
        if (!step.guideObject.Contains(gameObject))
        {
            step.guideObject.Add(gameObject);
            gameObject.SetActive(false); // Default inactive
        }
    }
}
