using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    public GameObject titleUI;
    public GameObject scanningUI;
    public GameObject trainingUI;
    public GameObject finishedUI;
    public VideoPlayer videoPlayer;
    public AudioSource audioPlayer;
    public TimeBarSqueeze timeBar;

    [Header("Task Box Elements")]
    public GameObject taskBox;
    public TextMeshProUGUI taskName;
    public TextMeshProUGUI stepDescription;
    public Image stepImage;
    public RawImage stepVideo;

    [Header("Warning Box Elements")]
    public TextMeshProUGUI warningDescription;

    public Button nextButton;
    public Button previousButton;

    [Header("Task & Step Data")]
    public List<TaskData> allTasks;

    private Coroutine stepTimerCoroutine;    // Store coroutine time for each step
    private float currentTime;
    private int currentTaskIndex = 0;
    private int currentStepIndex = 0;
    private List<GameObject> trackedObjects = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        StartTitle();  // Open the title screen
    }

    // ========================= WORKFLOW CONTROL =========================
    // --------------------------- Title Screen ---------------------------
    public void StartTitle()
    {
        titleUI.SetActive(true);
        scanningUI.SetActive(false);
        trainingUI.SetActive(false);
        finishedUI.SetActive(false);
    }
    // ----------------------------- QR Code ------------------------------
    public void StartScanning()
    {
        titleUI.SetActive(false);
        scanningUI.SetActive(true);
        trainingUI.SetActive(false);
        finishedUI.SetActive(false);
    }
    // ----------------------------- Training -----------------------------
    public void StartTraining()
    {
        titleUI.SetActive(false);
        scanningUI.SetActive(false);
        trainingUI.SetActive(true);
        finishedUI.SetActive(false);
        LoadStep();
    }
    public void Finishing()
    {
        titleUI.SetActive(false);
        scanningUI.SetActive(false);
        trainingUI.SetActive(false);
        finishedUI.SetActive(true);
    }
    // ====================================================================


    // =========================== STEP CONTROL ===========================

    // ----------------------------- Next Step ----------------------------
    public void NextStep()
    {
        currentStepIndex++;
        if (currentStepIndex >= allTasks[currentTaskIndex].steps.Count)
        {
            currentTaskIndex++;
            currentStepIndex = 0;
            if (currentTaskIndex >= allTasks.Count)
            {
                //TaskFinishing();
                return;
            }
        }
        LoadStep();
    }
    // -------------------------- Previous Step ---------------------------
    public void PreviousStep()
    {
        if (currentStepIndex > 0)
            currentStepIndex--;
        else if (currentTaskIndex > 0)
        {
            currentTaskIndex--;
            currentStepIndex = allTasks[currentTaskIndex].steps.Count - 1;
        }
        LoadStep();
    }
    // ----------------------------- Load Step ----------------------------
    private void LoadStep()
    {
        if (stepTimerCoroutine != null) StopCoroutine(stepTimerCoroutine);          // Check if a previous step timer coroutine is still running, stop it

        StepData step = allTasks[currentTaskIndex].steps[currentStepIndex];         // Fetch the current step data (stepName, guideObject, duration, etc.)

        taskName.text = allTasks[currentTaskIndex].taskName;                        // Change Task Name (TextMeshPro)
        stepDescription.text = step.description;                                    // Change Step Description (TextMeshPro)
        warningDescription.text = step.hasWarning ? step.warningText : "";

        foreach (GameObject obj in trackedObjects)                                  // Deactivate all registered objects first
        {
            obj.SetActive(false);
        }
        if (step.guideObject != null && step.guideObject.Count > 0)                 // Activate the guide objects for the current step
        {
            foreach (GameObject obj in step.guideObject)
            {
                if (obj != null) obj.SetActive(true);
            }
        }

        if (step.instructionImage != null)                                          // Check if the step has an instruction image
        {
            stepImage.sprite = step.instructionImage;
            stepImage.gameObject.SetActive(true);
            stepVideo.gameObject.SetActive(false);
        }
        else if (step.instructionVideo != null)                                     // If the step has an instruction video instead
        {
            videoPlayer.clip = step.instructionVideo;
            stepImage.gameObject.SetActive(false);
            stepVideo.gameObject.SetActive(true);
        }
        else                                                                        // If neither, hide both
        {
            stepImage.gameObject.SetActive(false);
            stepVideo.gameObject.SetActive(false);
        }

        audioPlayer.PlayOneShot(step.voiceover);                                    // Play the voiceover

        if (step.hasWarning)                                                        // Check if the step has a warning
        {
            warningDescription.text = step.warningText;                             // Set the warning text
            warningDescription.transform.parent.gameObject.SetActive(true);         // WarningBox SetActive True
        }
        else                                                                        // If the step doesn't have a warning
        {
            warningDescription.text = "";                                           // Set the warning text to empty
            warningDescription.transform.parent.gameObject.SetActive(false);        // WarningBox SetActive False
        }

        if (taskBox.GetComponent<Animator>().GetBool("IsHiding") == true)           // If task box is hiding, play unhide animation
        {
            taskBox.GetComponent<Animator>().SetBool("IsHiding", false);            // Set IsHiding (animator bool parameter) to false
        }

        stepTimerCoroutine = StartCoroutine(StepTimer(step.duration));              // Start countdown and time bar
        timeBar.StartTimer(step.duration);
    }
    private IEnumerator StepTimer(float duration)
    {
        float timer = duration;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        NextStep();                                                                 // Auto skip to next step when time's up
    }
    // ====================================================================
    public void RegisterGuideObject(GameObject obj, int taskIndex, int stepIndex)   // Register guide object from AR tracked image prefab
    {
        trackedObjects.Add(obj);

        if (taskIndex < allTasks.Count && stepIndex < allTasks[taskIndex].steps.Count)
        {
            allTasks[taskIndex].steps[stepIndex].guideObject.Add(obj);
            obj.SetActive(false); // default inactive until that step is active
        }
        else
        {
            Debug.LogWarning($"Invalid task/step index for {obj.name}: Task {taskIndex}, Step {stepIndex}");
        }
    }
}
