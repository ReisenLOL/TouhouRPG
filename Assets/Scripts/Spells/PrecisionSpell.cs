using System;
using UnityEngine;
using UnityEngine.UI;

public class PrecisionSpell : Spell
{
    public Slider attackSliderUI;
    public float sliderSpeed;
    private bool movingRight = true;
    private bool settingPrecision = true;
    public float precisionAmount;
    private void Update()
    {
        if (settingPrecision)
        {
            if (Input.anyKeyDown)
            {
                //settingPrecision = false;
                precisionAmount = 1 - MathF.Abs(0.5f - attackSliderUI.value);
                attackSliderUI.gameObject.SetActive(false);
                Debug.Log(precisionAmount);
                //...sure? i dunno!
            }
            if (movingRight)
            {
                attackSliderUI.value += Time.deltaTime * sliderSpeed;
                if (attackSliderUI.value >= 1)
                {
                    movingRight = false;
                }
            }
            else
            {
                attackSliderUI.value -= Time.deltaTime * sliderSpeed;
                if (attackSliderUI.value <= 0)
                {
                    movingRight = true;
                }
            }
        }
        //wow!
    }

    protected override void AttackMechanic()
    {
        settingPrecision = true;
        attackSliderUI.gameObject.SetActive(true);
    }
}
