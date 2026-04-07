using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections;

public class ShootingController : MonoBehaviour
{
    public TMP_InputField numberInputField;
    public GameObject bulletPrefab;
    public GameObject wrongInputPrompt;
    public GameObject correctInputPrompt;

    private Coroutine displayPromptCoroutine;

    public void submitNumber()
    {
        int number;
        if (int.TryParse(numberInputField.text, out number))
        {
            Debug.Log("Submitted number: " + number);

            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Enemy");
            if (displayPromptCoroutine != null) StopCoroutine(displayPromptCoroutine);
            wrongInputPrompt.SetActive(false);
            correctInputPrompt.SetActive(false);
            bool foundTarget = false;
            foreach (GameObject zombie in zombies)
            {
                Zombie zombieScript = zombie.GetComponent<Zombie>();
                if (zombieScript != null && zombieScript.answer == number)
                {
                    // Destroy(zombie);
                    foundTarget = true;
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<BulletController>().setTarget(zombie);
                    break;
                }
            }
            if (!foundTarget)
            {
                displayPromptCoroutine = StartCoroutine(DisplayPrompt(wrongInputPrompt));
            } else
            {
                displayPromptCoroutine = StartCoroutine(DisplayPrompt(correctInputPrompt));
            }
        }
        else
        {
            if (numberInputField.text != "") Debug.Log($"Invalid input. Please enter a valid integer: {numberInputField.text}");
        }

        numberInputField.text = "";

        setFocusOnInputField();
    }

    public void setFocusOnInputField()
    {
        EventSystem.current.SetSelectedGameObject(numberInputField.gameObject);
        numberInputField.ActivateInputField();
    }

    private IEnumerator DisplayPrompt(GameObject prompt)
    {
        prompt.SetActive(true);
        yield return new WaitForSeconds(2f);
        prompt.SetActive(false);
    }
}
