using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LevelScoreController : MonoBehaviour
{
    public static LevelScoreController Instance;

    public int currentLevel = 1;
    public float timeToNextLevel = 60f;
    public int baseScore = 10;
    [HideInInspector]
    public bool isNewRecord = false;
    public TMP_Text levelText;
    public TMP_Text currentScoreText;
    public TMP_Text highestScoreText;

    public TMP_Text endScoreText;
    public GameObject newRecordText;

    private int currentScore = 0;
    private int highestScore = 0;
    private float levelCooldown = 0f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    void Start()
    {
        highestScore = PlayerPrefs.GetInt("highestScore");
        highestScoreText.text = $"Highest: {highestScore.ToString("0000")}";
    }

    void Update()
    {
        GameController gameController = GameController.instance;
        if (gameController == null || gameController.gameState != GameState.Normal) return;
        levelCooldown += Time.deltaTime;
        if (levelCooldown > timeToNextLevel)
        {
            levelCooldown = 0f;
            NextLevel();
        }
        if (currentScore > highestScore)
        {
            highestScore = currentScore;
            isNewRecord = true;
        }  
    }

    public void NextLevel()
    {
        currentLevel++;  
        levelText.text = $"Level: {currentLevel}";
    }
    public void AddScore()
    {
        currentScore += baseScore;
        currentScoreText.text = $"Current: {currentScore.ToString("0000")}";
    }

    public void UpdateScore()
    {
        PlayerPrefs.SetInt("highestScore", currentScore);
        endScoreText.text = $"Your score: {currentScore.ToString("0000")}";
        newRecordText.SetActive(isNewRecord);
        int scoreIndex = PlayerPrefs.GetInt("scoreIndex");
        PlayerPrefs.SetString($"score{scoreIndex}", $"{currentLevel},{currentScore}");
        PlayerPrefs.SetInt("scoreIndex", scoreIndex + 1);
    }

}
