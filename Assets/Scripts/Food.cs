using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int food = 0;
    public int foodRequired = 20;

    [SerializeField] private FoodUI foodBar;

    public void AddFood()
    {
        food++;

        //Change foodbar
        foodBar.UpdateBar(food, foodRequired);


        if (food >= foodRequired)
        {
            //Level up
            GameManager.Instance.pausingManager.PauseGame();
        }
    }
}
