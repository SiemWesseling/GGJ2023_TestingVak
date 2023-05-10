using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Unity.Services.Analytics;
using Unity.Services.Core;


public class PlayerCollision : MonoBehaviour
{

    private int amountOfFoodPickedUp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Food")
        {
            gameObject.GetComponent<Food>().AddFood();

            amountOfFoodPickedUp++;
            OnAnalyticsInitializedSucces();

            Destroy(collision.gameObject);
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
