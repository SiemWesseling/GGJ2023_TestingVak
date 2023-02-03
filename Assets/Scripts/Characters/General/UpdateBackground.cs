using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBackground : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Renderer _background;
    private static readonly int Position = Shader.PropertyToID("_Position");

    void Start()
    {
        if(_player == null)
        {
            _player = GameObject.Find("Player").transform;
        }
        
        if (_background == null)
        {
            _background = GameObject.Find("Background").GetComponent<Renderer>();
        }
    }

    void Update()
    {
        _background.sharedMaterial.SetVector(Position, _player.position);
    }
}
