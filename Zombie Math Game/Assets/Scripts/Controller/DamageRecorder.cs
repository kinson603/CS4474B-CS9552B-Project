using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static MainMenuController;

public class DamageRecorder : MonoBehaviour
{
    public struct DamageRecord
    {
        public int num1;
        public int num2;
        public int sign;
        public int level;

        public DamageRecord(int num1, int num2, int sign, int level)
        {
            this.num1 = num1;
            this.num2 = num2;
            this.sign = sign;
            this.level = level;
        }
    }

    public static DamageRecorder instance;

    public GameObject contentParent;
    public GameObject recordPrefab;
    private List<DamageRecord> damageRecords = new List<DamageRecord>();

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void AddRecord(int num1, int num2, int sign)
    {
        LevelScoreController levelScoreController = LevelScoreController.Instance;
        damageRecords.Add(new DamageRecord(num1, num2, sign, levelScoreController.currentLevel));
    }

    public void DisplayRecords()
    {
        foreach (Transform child in contentParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (DamageRecord damageRecord in damageRecords)
        {
            GameObject record = Instantiate(recordPrefab, contentParent.transform, contentParent);
            record.SetActive(true);
            record.transform.localScale = Vector3.one;
            TMP_Text[] recordTexts = record.GetComponentsInChildren<TMP_Text>();
            foreach (TMP_Text recordText in recordTexts)
            {
                switch (recordText.gameObject.name)
                {
                    case "Level":
                        recordText.text = damageRecord.level.ToString();
                        break;
                    case "Equation":
                        string sign = damageRecord.sign == 0 ? "+" : "-";
                        recordText.text = $"{damageRecord.num1} {sign} {damageRecord.num2}";
                        break;
                    case "Answer":
                        int answer = damageRecord.sign == 0 ? damageRecord.num1 + damageRecord.num2 : damageRecord.num1 - damageRecord.num2;
                        recordText.text = answer.ToString();
                        break;
                }
            }
        }
    }

}
