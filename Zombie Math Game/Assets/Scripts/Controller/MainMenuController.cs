using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject leaderboardCanvas;
    public List<RecordStruct> records = new List<RecordStruct>();

    public GameObject recordContent;
    public GameObject entryPrefab;

    public struct RecordStruct
    {
        public int level;
        public float score;
        public RecordStruct(int level, float score)
        {
            this.level = level;
            this.score = score;
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

    public void Leaderboard()
    {
        LoadScore();
        mainCanvas.SetActive(false);
        leaderboardCanvas.SetActive(true);
    }

    public void Return()
    {
        mainCanvas.SetActive(true);
        leaderboardCanvas.SetActive(false);
    }

    private void LoadScore()
    {
        int scoreIndex = PlayerPrefs.GetInt("scoreIndex");
        if (scoreIndex > 0)
        {
            for (int i = 0; i < scoreIndex; i++)
            {
                string value = PlayerPrefs.GetString("score" + i);
                Debug.Log("value: " + value);
                int level = int.Parse(value.Split(",")[0]);
                float score = float.Parse(value.Split(",")[1]);
                records.Add(new RecordStruct(level, score));
            }
            records.Sort(delegate (RecordStruct x, RecordStruct y)
            {
                return y.score.CompareTo(x.score);
            });
            int index = 1;
            foreach (RecordStruct record in records)
            {
                GameObject entry = Instantiate(entryPrefab, recordContent.transform, recordContent);
                entry.SetActive(true);
                entry.transform.localScale = Vector3.one;
                TMP_Text[] entryTexts = entry.GetComponentsInChildren<TMP_Text>();
                foreach (TMP_Text entryText in entryTexts)
                {
                    switch (entryText.gameObject.name)
                    {
                        case "Rank":
                            entryText.text = index.ToString();
                            index++;
                            break;
                        case "Level":
                            entryText.text = record.level.ToString();
                            break;
                        case "Score":
                            entryText.text = record.score.ToString();
                            break;
                    }
                }
            }
            
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
