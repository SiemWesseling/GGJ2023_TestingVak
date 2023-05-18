using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using Unity.Services.Core;
using System.Collections;

public class Food : MonoBehaviour
{
    public int food = 0;
    public int foodRequired = 20;

    [SerializeField] private FoodUI foodBar;
    [SerializeField] string upgradeSound;
    [SerializeField] string eatFoodSound;

    [field: Header("OverlapCircle Parameters")]
    [SerializeField]
    private Transform detectorOrigin;   //origin of blood cell
    public float detectorSize;  //size of the radius around the blood cell
    public Vector2 detectorOriginOffset = Vector2.zero;

    public float detectionDelay = 0.3f; //when does the bloodcell start detecting player

    public LayerMask detectorLayerMask; //which layer does the detector use

    public GameObject Target;   //what actor does the food want to target


    private void Start()
    {
        foodBar.UpdateBar(food, foodRequired);
    }

    public void AddFood()
    {
        food += GameManager.Instance.wave <= 1 ? 1 : GameManager.Instance.wave;

        if (food >= foodRequired)
        {
            UpgradeManager.Instance.onLevelUp.Invoke();
            //Level up
            GameManager.Instance.pausingManager.PauseGame();
            AudioManager.Instance.PlaySound(upgradeSound);
            food = 0;
            foodRequired = (int)(foodRequired * 1.25f);
        }
        AudioManager.Instance.PlaySound(eatFoodSound);
        //Change foodbar
        foodBar.UpdateBar(food, foodRequired);
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    /// <summary>
    /// Function to detect if player is in the radius to make the food start magnetizing to the player.
    /// </summary>
    public void PerformDetection()
    {
        Collider2D collider = 
            Physics2D.OverlapCircle(
                (Vector2)detectorOrigin.position + detectorOriginOffset, detectorSize, 0, detectorLayerMask);   
                //Blood cell draws a detection circle around itself


        if(collider != null)
        {
            Target = collider.gameObject;   //Blood cell has target, will try to detect within its radius
        }
        else
        {
            Target = null;  //Blood cell does not have a target, will not try to detect
        }
    }
    
    /// <summary>
    /// Function that follows the position of the player
    /// </summary>
    private Vector2 FollowPlayer()
    {
        Vector2 directionToPlayer = new Vector2();
        directionToPlayer = (this.transform.position - GameManager.Instance.player.transform.position).normalized;
        return -directionToPlayer;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.pausingManager.UnPauseGame();
        }
    }
}
