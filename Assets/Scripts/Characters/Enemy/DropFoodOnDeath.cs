using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class DropFoodOnDeath: MonoBehaviour
{
    HealthManager healthManager;
    [SerializeField] GameObject foodPrefab;

    private void Start()
    {
        healthManager = GetComponent<HealthManager>();
        healthManager.onDeath.AddListener(SpawnFood);
    }

    void SpawnFood()
    {
       var newFood =  Instantiate(foodPrefab);
        newFood.transform.position = transform.position;
    }

}
