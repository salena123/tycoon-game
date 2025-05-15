using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoconutUI : MonoBehaviour
{
    public TMP_Text coconutText;

    void Update()
    {
        coconutText.text = "Кокосы: " + CoconutManager.coconuts;
    }
}
