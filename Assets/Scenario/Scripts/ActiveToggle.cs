using TMPro;
using UnityEngine;

public class ActiveToggle : MonoBehaviour
{
    [Header("Target to Toggle")]
    [SerializeField] private GameObject targetObject;
    [Header("Status Text")]
    [SerializeField] private TextMeshProUGUI statusText;
    private bool isActive;

    void Start()
    {
        statusText = GetComponentInChildren<TextMeshProUGUI>();
        if (targetObject != null)
        {
            isActive = targetObject.activeSelf;
            UpdateStatusText();
        }
    }

    public void SetActiveToggle()
    {
        if (targetObject == null) return;

        isActive = !isActive;
        targetObject.SetActive(isActive);
        UpdateStatusText();
    }
    private void UpdateStatusText()
    {
        if (statusText != null)
        {
            statusText.text = isActive ? "On" : "Off";
        }
        else
        {
            Debug.LogWarning("Status Text is not assigned in the inspector.");
        }
    }
}
