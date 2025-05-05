using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmartDialogAction : MonoBehaviour
{
    [Header("Диалог")]
    public TypewriterEffect typewriter;
    [TextArea] public string[] dialogLines;
    public GameObject dialogCanvas;
    public Button continueButton;

    [Header("Объекты")]
    public GameObject objectToDestroy;
    public GameObject[] objectsToEnable;

    [Header("Поворот")]
    public GameObject[] rotatingObjects;
    public Vector3[] targetEulerRotations;
    public float rotationSpeed = 90f;

    private int currentLine = 0;

    // Ключи для сохранения
    private const string DialogPlayedKey = "SmartDialog_Played";
    private const string ObjectDestroyedKey = "Object_Destroyed";
    private const string ObjectEnabledPrefix = "Enabled_";
    private const string ObjectRotatedPrefix = "Rotated_";

    void Start()
    {
        if (PlayerPrefs.GetInt(DialogPlayedKey, 0) == 1)
        {
            // Диалог уже был — восстановить состояние
            RestoreState();
            Destroy(gameObject); // Удалить триггер
            return;
        }

        // Показываем диалог, если он ещё не был проигран
        dialogCanvas.SetActive(true);
        continueButton.gameObject.SetActive(false);
        StartCoroutine(PlayDialog());
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
        });
    }

    void PerformAction()
    {
        // Удаление объекта
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy);
            PlayerPrefs.SetInt(ObjectDestroyedKey, 1);
        }

        // Включение объектов
        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            if (objectsToEnable[i] != null)
            {
                objectsToEnable[i].SetActive(true);
                PlayerPrefs.SetInt(ObjectEnabledPrefix + i, 1);
            }
        }

        // Поворот объектов
        for (int i = 0; i < rotatingObjects.Length && i < targetEulerRotations.Length; i++)
        {
            if (rotatingObjects[i] != null)
            {
                rotatingObjects[i].transform.rotation = Quaternion.Euler(targetEulerRotations[i]);
                PlayerPrefs.SetInt(ObjectRotatedPrefix + i, 1);
            }
        }

        // Отметить, что диалог пройден
        PlayerPrefs.SetInt(DialogPlayedKey, 1);
        PlayerPrefs.Save();
    }

    void RestoreState()
    {
        // Восстановить включённые объекты
        for (int i = 0; i < objectsToEnable.Length; i++)
        {
            if (objectsToEnable[i] != null)
            {
                int enabled = PlayerPrefs.GetInt(ObjectEnabledPrefix + i, 0);
                objectsToEnable[i].SetActive(enabled == 1);
            }
        }

        // Восстановить повороты
        for (int i = 0; i < rotatingObjects.Length && i < targetEulerRotations.Length; i++)
        {
            if (rotatingObjects[i] != null)
            {
                int rotated = PlayerPrefs.GetInt(ObjectRotatedPrefix + i, 0);
                if (rotated == 1)
                {
                    rotatingObjects[i].transform.rotation = Quaternion.Euler(targetEulerRotations[i]);
                }
            }
        }

        // Удалить объект, если он уже уничтожен
        if (PlayerPrefs.GetInt(ObjectDestroyedKey, 0) == 1 && objectToDestroy != null)
        {
            Destroy(objectToDestroy);
        }
    }
}
