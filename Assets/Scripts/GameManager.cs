using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;

    [SerializeField] public GameObject player;

    public static GameManager Instance
    {
        get
        {
            if (instance) { return instance; }
            else
            {
                Debug.LogWarning("There is no gamemanager in the scene but it is referred to. ");
                return null;
            }
        }
        private set { instance = value; }
    }

    private void Start()
    {
        if (!instance)
        {
            instance = this;
        }
        else { Debug.LogWarning("There seem to be multiple gamemanagers in the scene "); }

    }
}
