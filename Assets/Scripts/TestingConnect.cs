using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;

public class TestingConnect : MonoBehaviour
{
    public static EventHandler AnalitycsInitializedSucces;
    private static bool isInitialized;
    public static bool IsInitialized => isInitialized;

    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
            isInitialized = true;
            AnalitycsInitializedSucces?.Invoke(this, new EventArgs());
        }
        catch (ConsentCheckException e)
        {
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
        }
    }
}