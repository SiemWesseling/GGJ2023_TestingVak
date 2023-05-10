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

    private int amountOfFoodPickedUp;

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

        amountOfFoodPickedUp++;
        OnAnalyticsInitializedSucces();

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

    private void OnAnalyticsInitializedSucces()
    {
        //// Unsubscribe from the event
        //TestingConnect.AnalitycsInitializedSucces -= OnAnalyticsInitializedSucces;
        Debug.Log("Sending picking up bloodcell event");

        // Now you can log events to the Analytics service
        AnalyticsService.Instance.CustomData("redBloodCellsDropped", new Dictionary<string, object> {
            { "totalRedBloodCellsGotten", amountOfFoodPickedUp }
        });
        AnalyticsService.Instance.Flush();
    }
}
