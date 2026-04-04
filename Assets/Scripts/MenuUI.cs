using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public TMP_InputField nameInput;
    public TMP_Text bestScoreText;

    private void Start()
    {
        UpdateUI();

        if (nameInput != null)
        {
            nameInput.onEndEdit.AddListener(OnNameChanged);
        }
        Debug.Log("MenuManager: " + MenuManager.Instance);
        Debug.Log("BestScoreText: " + bestScoreText);
    }

    void OnNameChanged(string value)
    {
        MenuManager.Instance.SetUserName(value);
    }

    void UpdateUI()
    {
        if (MenuManager.Instance == null)
        {
            Debug.LogError("MenuManager no existe");
            return;
        }

        if (bestScoreText == null)
        {
            Debug.LogError("bestScoreText no asignado");
            return;
        }

        bestScoreText.text =
            $"Best Score: {MenuManager.Instance.topScorePoints} - {MenuManager.Instance.topScoreName}";
    }

    public void StartGame()
    {
        if (MenuManager.Instance.userName == null ||MenuManager.Instance.userName =="")
        {
            MenuManager.Instance.userName = "Player";
        }
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}