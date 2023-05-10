using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using Unity.Services.Core;

[RequireComponent(typeof(HealthManager))]
public class DropFoodOnDeath : MonoBehaviour
{
    private int amountOfFoodDropped;
    HealthManager healthManager;
    [SerializeField] GameObject foodPrefab;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
        healthManager.onDeath.AddListener(SpawnFood);
    }

    void SpawnFood()
    {
        var newFood = Instantiate(foodPrefab);
        newFood.transform.position = transform.position;

        amountOfFoodDropped++;
        OnAnalyticsInitializedSucces();
    }

    private void OnAnalyticsInitializedSucces()
    {
        //// Unsubscribe from the event
        //TestingConnect.AnalitycsInitializedSucces -= OnAnalyticsInitializedSucces;
        Debug.Log("Sending dropping bloodcell event");

        // Now you can log events to the Analytics service
        AnalyticsService.Instance.CustomData("redBloodCellsDropped", new Dictionary<string, object> {
            { "totalRedBloodCellsDropped", amountOfFoodDropped }
        });
        AnalyticsService.Instance.Flush();
    }
}
