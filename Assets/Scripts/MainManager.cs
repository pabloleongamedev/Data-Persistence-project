using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TMP_Text ScoreText;
    public TMP_Text bestScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private string m_name = "xxx";
    private string m_topScoreName;
    private int m_Points;
    private int m_topScorePoint;
    
    private bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        GenerateBricks();

        if (MenuManager.Instance != null)
        {
            m_name = MenuManager.Instance.userName;
            m_topScoreName = MenuManager.Instance.topScoreName;
            m_topScorePoint = MenuManager.Instance.topScorePoints;
        }

        UpdateScore(0);

        bestScoreText.text =
            $"Best Score: {m_topScorePoint} - {m_topScoreName}";
    }
    void GenerateBricks()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = {1,1,2,2,5,5};

        for (int i = 0; i < LineCount; i++)
        {
            for (int x = 0; x < perLine; x++)
            {
                Vector3 pos = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, pos, Quaternion.identity);

                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(UpdateScore);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    void UpdateScore(int points)
    {
        m_Points += points;
        ScoreText.text = $"Score: {m_Points} - {m_name}";
    }

    public void GameOver()
    {
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.TrySetHighScore(m_Points);
        }

        m_GameOver = true;
        GameOverText.SetActive(true);
    }
}
