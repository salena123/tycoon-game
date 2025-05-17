using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public float delay = 0.05f; // задержка между символами
    public TMP_Text uiText;         // если используешь TMP, замени на TMP_Text

    [HideInInspector]
    public bool IsFinished { get; private set; } = false;

    private Coroutine typingCoroutine;

    public void ShowText(string fullText)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(fullText));
    }

    IEnumerator TypeText(string fullText)
    {
        IsFinished = false;
        uiText.text = "";

        foreach (char c in fullText)
        {
            uiText.text += c;
            yield return new WaitForSeconds(delay);
        }

        IsFinished = true;
    }
}
