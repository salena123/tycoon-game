using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class money_manager : MonoBehaviour
{
    public static int money = 0;
    public float earnInterval = 1f;
    private float timer = 0f;
    public TMP_Text moneyText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer>=earnInterval)
        {
            money += 1;
            timer = 0f;
        }

        moneyText.text = "Δενόγθ: " + money.ToString();
    }
}
