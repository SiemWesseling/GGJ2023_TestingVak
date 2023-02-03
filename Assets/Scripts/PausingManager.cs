using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausingManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
        GameManager.Instance.paused = true;
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        GameManager.Instance.paused = false;
        Time.timeScale = 1;
    }
}
