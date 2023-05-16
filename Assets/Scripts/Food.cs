using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class Food : MonoBehaviour
{
    public int food = 0;
    public int foodRequired = 20;

    [SerializeField] private FoodUI foodBar;
    [SerializeField] string upgradeSound;
    [SerializeField] string eatFoodSound;
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

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.pausingManager.UnPauseGame();
        }
    }
}
