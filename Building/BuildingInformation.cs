using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class BuildingInformation : MonoBehaviour
{
    private BuildingEachInstanceSpecific informationAbouthTheFather;

    private TextMeshPro txtAboutResourceGenerating;
    private const string TWO_DECIMAL_HOUSES = "0.00";

    private SpriteRenderer spriteResource;

    private Transform transformSpriteTimer;
    private Vector3 originalSize = Vector3.zero;

    private decimal howManyResourcesHeProduces = 0;


    void Start()
    {
        informationAbouthTheFather = transform.GetComponentInParent<BuildingEachInstanceSpecific>();

        MountTextInformation();
        MountSpriteResourceInformation();
        MountSpriteTimerRepeaterInformation();
    }

    private void MountTextInformation()
    {
        txtAboutResourceGenerating = transform.Find(GlobalGameObjectNamesHelper.TXT_ABOUT_HOW_MUCH_RESOURCE_ARE_BEING_GENERATED).GetComponent<TextMeshPro>();
        howManyResourcesHeProduces = (decimal)informationAbouthTheFather.HowManyResourcesThisBuildGenerate;
        txtAboutResourceGenerating.text = howManyResourcesHeProduces.ToString(TWO_DECIMAL_HOUSES);
    }

    private void MountSpriteResourceInformation()
    {
        spriteResource = transform.Find(GlobalGameObjectNamesHelper.SPRITE_ITEM_PRODUCE_NAME).GetComponent<SpriteRenderer>();
        spriteResource.sprite = informationAbouthTheFather.ItemThatThisBuildingProduce.sprite;
    }

    private void MountSpriteTimerRepeaterInformation()
    {
        transformSpriteTimer = transform.Find(GlobalGameObjectNamesHelper.TIMER_COUNTING_NAME).GetComponent<Transform>();
        originalSize = transformSpriteTimer.localScale;

        ResetSpriteTimerTicker();
    }


    decimal secondsPassed = 0;
    decimal percentagePassed = 0;
    decimal intraSeconds = 0;

    private const int ONE_SECOND = 1;
    private const int ONE_HUNDRED_PERCENT = 100;
    private const int ZERO_PERCENT = 0;
    private const int ZERO = 0;

    void Update()
    {
        if(howManyResourcesHeProduces > 0)
        {
            CalcResourcesResizingSprite();
        }
    }

    private void CalcResourcesResizingSprite()
    {
        decimal timePassedTemp = (decimal)Time.deltaTime;
        intraSeconds += timePassedTemp;
        secondsPassed += timePassedTemp;

        percentagePassed = (intraSeconds * howManyResourcesHeProduces * 100) * howManyResourcesHeProduces;
        percentagePassed = (decimal)Mathf.Clamp((float)percentagePassed, ZERO_PERCENT, ONE_HUNDRED_PERCENT);

        if (percentagePassed > 0)
        {
            var xValueActual = MathUtils.CalculsAPercentagemOfANumber((float)percentagePassed, originalSize.x);
            transformSpriteTimer.transform.localScale = new Vector3(xValueActual, originalSize.y, originalSize.z);
        }

        if (intraSeconds > ONE_SECOND / howManyResourcesHeProduces)
        {
            intraSeconds = 0;
            ResetSpriteTimerTicker();
        }

        if (secondsPassed >= ONE_SECOND)
        {
            intraSeconds = 0;
            percentagePassed = 0;
            secondsPassed = 0;
        }
    }

    private void ResetSpriteTimerTicker()
    {
        transformSpriteTimer.localScale = new Vector3(ZERO, originalSize.y, originalSize.z);
    }

}
