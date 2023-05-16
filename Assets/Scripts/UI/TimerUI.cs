using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Random = Unity.Mathematics.Random;

public class TimerUI : MonoBehaviour
{
    
    [SerializeField] public TextMeshProUGUI timerText;
    public float timer = 0;

    private void Start()
    {
        if(timerText == null)
        {
            timerText = GetComponent<TextMeshProUGUI>();
        }
    }
    
    private void Update()
    {
        timer += Time.deltaTime;
        float minutes = Mathf.Floor(timer / 60);
        float seconds = Mathf.Floor(timer % 60);
        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
