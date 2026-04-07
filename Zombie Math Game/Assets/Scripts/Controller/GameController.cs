using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Normal,
    Pausing,
    End,
    Tutorial
}

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject pauseCanvas;
    public GameObject audioCanvas;
    public GameObject endCanvas;
    public GameState gameState {  get; private set; }

    private bool isMute = false;

    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (instance == null) { instance = this; }
    }

    private void Start()
    {
        bool finishedTutorial = PlayerPrefs.GetString("finishedTutorial") == "true";
        if (finishedTutorial)
        {
            gameState = GameState.Normal;
            Time.timeScale = 1f;
        }
        else
        {
            gameState = GameState.Tutorial;
            Time.timeScale = 0f;
            TutorialController.Instance.StartTutorial();
        }
    }

    public void Pause()
    {
        if (gameState != GameState.Normal) return;
        gameState = GameState.Pausing;
        pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
    public void AudioSettings()
    {
        audioCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }
    public void AudioBack()
    {
        audioCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void Return()
    {
        gameState = GameState.Normal;
        pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }
    public void Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void WatchTutorial()
    {
        gameState = GameState.Tutorial;
        pauseCanvas.SetActive(false);
        TutorialController.Instance.StartTutorial();
    }

    public void GameEnd()
    {
        gameState = GameState.End;
        LevelScoreController.Instance.UpdateScore();
        Time.timeScale = 0f;
        endCanvas.SetActive(true);
    }
}
