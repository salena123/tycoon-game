using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextPulseEffect : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Color baseColor = Color.cyan;       // ÷вет в спокойном состо€нии
    public Color glowColor = Color.white;      // ÷вет при вспышке
    public float pulseSpeed = 2f;              // —корость пульсации

    void Update()
    {
        float t = (Mathf.Sin(Time.time * pulseSpeed) + 1f) / 2f; // [0..1]
        text.color = Color.Lerp(baseColor, glowColor, t);
    }
}
