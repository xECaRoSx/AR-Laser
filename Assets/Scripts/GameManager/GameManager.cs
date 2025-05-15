using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Panels")]
    [Tooltip("Text Mesh Pro - Touch to continue")] public GameObject TouchText;
    public GameObject titleUI;
    public GameObject scanningUI;

    [Header("Task Box Elements")]
    public TextMeshProUGUI taskName;
    public TextMeshProUGUI stepDescription;
    public Image stepImage;

    [Header("Warning Box Elements")]
    public TextMeshProUGUI warningDescription;

    public Button nextButton;
    public Button previousButton;
    public GameObject Media;

    [Header("Task & Step Data")]
    public List<TaskData> allTasks;

    private Coroutine stepTimerCoroutine;    // Store coroutine time for each step
    private float currentTime;
    private int currentTaskIndex = 0;
    private int currentStepIndex = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }
    private void Start()
    {
        OpenTitle();  // Open the title screen
    }

    // ========================= WORKFLOW CONTROL =========================
    // =========================== Title Screen ===========================
    public void OpenTitle()
    {
        StartCoroutine(TouchTextDelay(2f));    // Coroutine: Delay before showing the touch text
        scanningUI.SetActive(false);
    }
    private IEnumerator TouchTextDelay(float delay)
    {
        TouchText.SetActive(false);    // To be sure it is off at the start
        yield return new WaitForSeconds(delay);
        TouchText.SetActive(true);    // Set active true after delay
    }
    // ============================= QR Code ==============================
    public void ScanQRCode()
    {
        //TouchText.SetActive(true);
        //UI_QRCode.SetActive(true);
    }
    // ======================= Task 1: Introduction =======================
    public void TaskIntro()
    {
        //UI_QRCode.SetActive(false);
        //UI_Introduction.SetActive(true);
    }
    // ========================= Task 2: Power On =========================
    public void TaskPowerOn()
    {
        //UI_Introduction.SetActive(false);
        //UI_Training.SetActive(true);
        //currentTime = maxTrainingTime;
        //LoadStep();
    }
    // ======================= Task 3: PC Connection ======================
    public void TaskPCConnect()
    {
        
    }
    // ================== Task 4: Printing Configuration ==================
    public void TaskPrintConfig()
    {

    }
    // =================== Task 5: Material Preparation ===================
    public void TaskMatPrepare()
    {

    }
    // ===================== Task 6: After Finishing ======================
    public void TaskFinishing(string message = "คุณทำขั้นตอนเสร็จเรียบร้อยแล้ว!")
    {
        //UI_Training.SetActive(false);
        //UI_Finished.SetActive(true);
        //subtitleText.text = message;
        //audioSource.Stop();
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
        if (stepTimerCoroutine != null)
            StopCoroutine(stepTimerCoroutine);    // Stop the previous step timer

        StepData step = allTasks[currentTaskIndex].steps[currentStepIndex];

        taskName.text = $"Task {currentTaskIndex + 1}: {allTasks[currentTaskIndex].taskName}";
        stepDescription.text = step.questInfo;
        //warningText.text = step.hasWarning ? step.warningText : "";

        if (step.instructionImage != null) stepImage.sprite = step.instructionImage;

        // TODO: เพิ่มระบบเล่นเสียง voiceover ถ้าต้องการ
        // audioSource.clip = step.voiceover;
        // audioSource.Play();

        // SetActive guide object (If available)
        foreach (GameObject obj in step.guideObject)
            obj.SetActive(true);

        // Auto start the step timer
        stepTimerCoroutine = StartCoroutine(StepTimer(step.duration));
    }
    private IEnumerator StepTimer(float duration)
    {
        float timer = duration;
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }

        // Skip to the next step when time is up
        NextStep();
    }
    // ====================================================================
}
