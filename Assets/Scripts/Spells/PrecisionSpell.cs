using System;
using UnityEngine;
using UnityEngine.UI;

public class PrecisionSpell : Spell
{
    public Slider attackSliderUI;
    public float sliderSpeed;
    private bool movingRight = true;
    private bool settingPrecision = false;
    public float precisionAmount;
    private void Update()
    {
        if (settingPrecision)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                settingPrecision = false;
                precisionAmount = 1 - MathF.Abs(0.5f - attackSliderUI.value);
                attackSliderUI.gameObject.SetActive(false);
                Debug.Log(precisionAmount);
                AfterMechanic();
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

    protected override void AfterMechanic()
    {
        target.TakeDamage(damage * precisionAmount);
        target = null;
        combatManager.SwitchTurn(); //I DO NOT CARE, RIDER
    }

    protected override void AttackMechanic()
    {
        settingPrecision = true;
        attackSliderUI.gameObject.SetActive(true);
    }
}
