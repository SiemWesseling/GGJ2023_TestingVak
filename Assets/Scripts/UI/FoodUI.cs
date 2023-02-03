using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodUI : MonoBehaviour
{
    private Slider foodSlider;
    // Start is called before the first frame update
    void Start()
    {
        foodSlider = GetComponent<Slider>();
    }


    public void UpdateBar(int food, int requiredFood)
    {
        float progress = (float)food / (float)requiredFood;
        foodSlider.value = progress;
    }

}
