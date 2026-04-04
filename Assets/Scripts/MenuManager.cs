using UnityEngine;
using System.IO;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public string userName;
    public string topScoreName;
    public int topScorePoints;

    private string savePath => Application.persistentDataPath + "/player_save.json";

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int points;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadPlayer();
            Debug.Log(Application.persistentDataPath);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetUserName(string name)
    {
        userName = string.IsNullOrEmpty(name) ? "Player" : name;
    }

    public void TrySetHighScore(int score)
    {
        if (score > topScorePoints)
        {
            topScorePoints = score;
            topScoreName = userName;
            SavePlayer();
        }
    }

    public void SavePlayer()
    {
        SaveData data = new SaveData
        {
            name = topScoreName,
            points = topScorePoints
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, json);
    }

    public void LoadPlayer()
    {
        if (!File.Exists(savePath)) return;

        string json = File.ReadAllText(savePath);
        SaveData data = JsonUtility.FromJson<SaveData>(json);

        topScoreName = data.name;
        topScorePoints = data.points;
    }
}