using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartDialogAction : MonoBehaviour
{
    [Header("������")]
    public TypewriterEffect typewriter;
    [TextArea] public string[] dialogLines;
    public GameObject dialogCanvas;
    public Button continueButton;

    [Header("�������")]
    public GameObject objectToDestroy;
    public GameObject[] objectsToEnable;

    [Header("�������")]
    public GameObject[] rotatingObjects;
    public Vector3[] targetEulerRotations;
    public float rotationSpeed = 90f;

    private int currentLine = 0;
    private bool dialogStarted = false;

    // ����� ��� ����������
    private const string DialogPlayedKey = "SmartDialog_Played";
    private const string ObjectDestroyedKey = "Object_Destroyed";
    private const string ObjectEnabledPrefix = "Enabled_";
    private const string ObjectRotatedPrefix = "Rotated_";

    void Start()
    {
        if (PlayerPrefs.GetInt(DialogPlayedKey, 0) == 1)
        {
            RestoreState();
            Destroy(gameObject); // ������� �������
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (dialogStarted || PlayerPrefs.GetInt(DialogPlayedKey, 0) == 1) return;

        if (other.CompareTag("Player"))
        {
            dialogStarted = true;
            dialogCanvas.SetActive(true);
            continueButton.gameObject.SetActive(false);
            StartCoroutine(PlayDialog());
        }
    }

    IEnumerator PlayDialog()
    {
        while (currentLine < dialogLines.Length)
        {
            typewriter.ShowText(dialogLines[currentLine]);
            yield return new WaitUntil(() => typewriter.IsFinished);
            yield return new WaitForSeconds(1f);
            currentLine++;
        }

        continueButton.gameObject.SetActive(true);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() =>
        {
            PerformAction();
            dialogCanvas.SetActive(false);
            Destroy(gameObject); // ������� ������� ����� ����������
        });
    }

    void PerformAction()
    {
        // ��������� ��������
        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            if (objectsToEnable[i] != null)
            {
                objectsToEnable[i].SetActive(true);
                PlayerPrefs.SetInt(ObjectEnabledPrefix + i, 1);
            }
        }

        PlayerPrefs.SetInt(DialogPlayedKey, 1);
        PlayerPrefs.Save();
    }

    void RestoreState()
    {
        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            if (objectsToEnable[i] != null)
            {
                int enabled = PlayerPrefs.GetInt(ObjectEnabledPrefix + i, 0);
                objectsToEnable[i].SetActive(enabled == 1);
            }
        }
    }
}
