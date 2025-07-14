using UnityEngine;

public enum AppState { Title, Scanning, Training, Finished }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private AppState currentState;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        SwitchState(AppState.Title);
    }

    public void SwitchState(AppState state)
    {
        currentState = state;
        UIManager.Instance.UpdateUI(state);

        Debug.Log($"[GameManager] Current state: {state}");
    }

    public void StartTraining()
    {
        TaskManager.Instance.ResetAll();
        SwitchState(AppState.Training);
        TaskManager.Instance.LoadStep();
    }

    public void GoToTitle() => SwitchState(AppState.Title);
    public void GoToScanning() => SwitchState(AppState.Scanning);
    public void GoToFinish() => SwitchState(AppState.Finished);
}
