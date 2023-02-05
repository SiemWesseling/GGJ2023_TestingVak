using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int food = 0;
    public int foodRequired = 20;

    [SerializeField] private FoodUI foodBar;

    private void Start()
    {
        foodBar.UpdateBar(food, foodRequired);
    }

    public void AddFood()
    {
        food += (int)(GameManager.Instance.wave);
        
        Debug.Log(food);
        
        if (food >= foodRequired)
        {
            UpgradeManager.Instance.onLevelUp.Invoke();
            //Level up
            GameManager.Instance.pausingManager.PauseGame();
            
            food = 0;
            foodRequired = (int)(foodRequired * 1.25f);
        }
        
        //Change foodbar
        foodBar.UpdateBar(food, foodRequired);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.pausingManager.UnPauseGame();
        }
    }
}
