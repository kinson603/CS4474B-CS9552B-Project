using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public static TutorialController Instance { get; private set; }

    [System.Serializable]
    public struct TutorialStruct
    {
        public string key;
        public Canvas uiCanvas;
        public GameObject tutorialObject;
    }

    public GameObject darkPanel;
    public TutorialStruct[] tutorialStructs;
    private int index = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void StartTutorial()
    {
        darkPanel.SetActive(true);
        index = 0;
        DisplayTutorial();
    }

    public void ProceedNextStep(bool finish = false)
    {
        if (tutorialStructs[index].uiCanvas != null)
        {
            tutorialStructs[index].uiCanvas.overrideSorting = false;
            tutorialStructs[index].uiCanvas.sortingOrder = 0;
        }
        tutorialStructs[index].tutorialObject.SetActive(false);
        index++;
        if (finish) EndTutorial();
        else DisplayTutorial();
    }

    private void DisplayTutorial()
    {
        if (tutorialStructs[index].uiCanvas != null)
        {
            tutorialStructs[index].uiCanvas.overrideSorting = true;
            tutorialStructs[index].uiCanvas.sortingOrder = 1;
        }
        tutorialStructs[index].tutorialObject.SetActive(true);
    }

    public void EndTutorial()
    {
        darkPanel.SetActive(false);
        PlayerPrefs.SetString("finishedTutorial", "true");
        GameController.instance.Return();
    }
}
