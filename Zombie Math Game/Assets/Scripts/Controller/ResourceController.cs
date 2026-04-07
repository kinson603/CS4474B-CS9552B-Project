using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour
{
    public static ResourceController instance;
    public GameObject healthObject;
    public Sprite depletedHealthSprite;

    private Image[] healthImgs;
    private int maxHealth = 3;
    private int currentHealth = 3;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        healthImgs = healthObject.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        if (currentHealth == 0)
        {
            GameController gameController = GameController.instance;
            if (gameController.gameState != GameState.End)
            {
                GameController.instance.GameEnd();
            }
        }
    }

    public void TakeDamage()
    {
        if (currentHealth > 0) currentHealth--;
        healthImgs[currentHealth].sprite = depletedHealthSprite;
    }
}
