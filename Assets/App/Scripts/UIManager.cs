using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject titleUI;
    public GameObject scanningUI, trainingUI, finishedUI;

    [Header("Step Elements")]
    public TextMeshProUGUI taskText;
    public TextMeshProUGUI stepText, descriptionText, numberText, warningText;
    public Image stepImage;
    public RawImage stepVideo;
    public Animator taskBoxAnimator;

    [Header("Video Player")]
    public VideoPlayer videoPlayer;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void UpdateUI(AppState state)
    {
        titleUI.SetActive(state == AppState.Title);
        scanningUI.SetActive(state == AppState.Scanning);
        trainingUI.SetActive(state == AppState.Training);
        finishedUI.SetActive(state == AppState.Finished);
    }

    public void ShowStepInfo(string task, string stepName, int taskIndex, int totalTasks, TaskData.StepData step)
    {
        taskText.text = task;
        stepText.text = stepName;
        numberText.text = $"Task {taskIndex}/{totalTasks}";
        descriptionText.text = step.description;

        if (step.image != null)
        {
            stepImage.sprite = step.image;
            stepImage.gameObject.SetActive(true);
            stepVideo.gameObject.SetActive(false);
        }
        else if (step.video != null)
        {
            videoPlayer.clip = step.video;
            videoPlayer.Play();

            stepVideo.texture = videoPlayer.targetTexture;

            stepVideo.gameObject.SetActive(true);
            stepImage.gameObject.SetActive(false);
        }
        else
        {
            stepImage.gameObject.SetActive(false);
            stepVideo.gameObject.SetActive(false);
        }

        bool hasWarning = !string.IsNullOrEmpty(step.warning);
        warningText.text = hasWarning ? step.warning : "";
        warningText.transform.parent.gameObject.SetActive(hasWarning);

        if (taskBoxAnimator.GetBool("IsHiding"))
            taskBoxAnimator.SetBool("IsHiding", false);
    }
}