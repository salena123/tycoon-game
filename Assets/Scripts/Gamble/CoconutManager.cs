using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutManager : MonoBehaviour
{
    private const string CoconutKey = "PlayerCoconuts";

    public static int coconuts
    {
        get => PlayerPrefs.GetInt(CoconutKey, 0);
        private set => PlayerPrefs.SetInt(CoconutKey, Mathf.Max(0, value));
    }

    public static void AddCoconuts(int amount)
    {
        coconuts += amount;
        PlayerPrefs.Save();
    }

    public static void SetCoconuts(int amount)
    {
        coconuts = amount;
        PlayerPrefs.Save();
    }
}
