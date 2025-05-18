using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class FinishDialogAction : MonoBehaviour
{

    public GameObject dialogCanvas;
    public TypewriterEffect typewriter;
    public TMP_Text dialogText;
    public Button continueButton;

    [TextArea]
    public string dialogLine = "Поздравляю! Ты построил всё, что можно на этом острове! Держи свою награду, ты заслужил её!";

    public GameObject[] objectsToEnable;

    public string saveKey = "DialogTrigger_1";

    private bool hasTriggered = false;

    private void Start()
    {
        if (PlayerPrefs.GetInt(saveKey, 0) == 1)
        {
            EnableSavedObjects();
            Destroy(gameObject);
            return;
        }

        dialogCanvas.SetActive(false);
        continueButton.gameObject.SetActive(false);
        continueButton.onClick.AddListener(OnContinueClicked);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(StartDialog());
        }
    }

    IEnumerator StartDialog()
    {
        dialogCanvas.SetActive(true);
        continueButton.gameObject.SetActive(false);
        typewriter.uiText = dialogText;
        typewriter.ShowText(dialogLine);

        yield return new WaitUntil(() => typewriter.IsFinished);

        yield return new WaitForSeconds(0.5f);
        continueButton.gameObject.SetActive(true);
    }

    void OnContinueClicked()
    {
        dialogCanvas.SetActive(false);

        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            if (objectsToEnable[i] != null)
            {
                objectsToEnable[i].SetActive(true);
                PlayerPrefs.SetInt(saveKey + "_Obj" + i, 1);
            }
        }

        PlayerPrefs.SetInt(saveKey, 1);
        PlayerPrefs.Save();

        Destroy(gameObject);
    }

    void EnableSavedObjects()
    {
        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            if (objectsToEnable[i] != null)
            {
                bool wasEnabled = PlayerPrefs.GetInt(saveKey + "_Obj" + i, 0) == 1;
                objectsToEnable[i].SetActive(wasEnabled);
            }
        }
    }
}
