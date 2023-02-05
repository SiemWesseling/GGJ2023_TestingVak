using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDiesAnim : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite deadPlayer;
    
    
    
    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        if(spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void StartDeathAnim()
    {
        animator.SetTrigger("playerDies");
    }
    
    private void RunAfterDeathAnimPlayer()
    {
        spriteRenderer.sprite = deadPlayer;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
