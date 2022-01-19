using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using NameHelper = GlobalGameObjectNamesHelper;

public class BuildingGhost : MonoBehaviour
{
    private GameObject thisGameObject;
    private SpriteRenderer thisSpriteRender;

    private GameObject gameObjectBuildingGhost;

    private const string HEXADECIMAL_INVALID_POSITION_TO_PLACE = "EA2020";
    private const string HEXADECIMAL_VALID_POSITION_TO_PLACE = "FFFFFF";
    private const string TWO_DECIMAL_HOUSES = "0.00";

    public static BuildingGhost Instance { get; private set; }

    private Transform fatherOfTheUIBuildingGhostInfo;
    private SpriteRenderer resourceTypeSpriteGhostBuildingHelperUI;
    private TextMeshPro textWithInformationAboutResources;

    private void Awake()
    {
        Instance = this;    

        thisGameObject = this.gameObject;
        thisSpriteRender = thisGameObject.GetComponent<SpriteRenderer>();

        thisSpriteRender.sprite = null;

        fatherOfTheUIBuildingGhostInfo = transform.Find(NameHelper.BUILDING_INFORMATION_NAME);
        fatherOfTheUIBuildingGhostInfo.gameObject.SetActive(false);

        resourceTypeSpriteGhostBuildingHelperUI = fatherOfTheUIBuildingGhostInfo
            .Find(NameHelper.SPRITE_ITEM_PRODUCE_NAME).GetComponent<SpriteRenderer>();

        textWithInformationAboutResources = fatherOfTheUIBuildingGhostInfo
            .Find(NameHelper.TXT_ABOUT_HOW_MUCH_RESOURCE_ARE_BEING_GENERATED).GetComponent<TextMeshPro>();
    }

    void Start()
    {
        BuildingManager.Instance.OnTypeBuildingChange += BuildingManager_OnTypeBuildingChange;

        BuildingManager.Instance.OnFoundNewAmountOfResources += Building_OnFoundNewAmountOfResources;
    }

    private void Building_OnFoundNewAmountOfResources(object sender, BuildingManager.OnFoundNewAmountOfResourcesEventArgs e)
    {
        textWithInformationAboutResources.text = e.quantityOfResourcesThatABuildingCanBring.ToString(TWO_DECIMAL_HOUSES);
    }

    private void BuildingManager_OnTypeBuildingChange(object sender, BuildingManager.OnTypeBuildingChangeEventArgs e)
    {
        if (e.activeBuildingType == null)
            Hide();
        else
            Show(e.activeBuildingType);
    }

    void Update()
    {
        transform.position = UtilsClass.GetMouseWorldPosition();

        if(gameObjectBuildingGhost != null)
            gameObjectBuildingGhost.transform.position = transform.position;
    }

    private void Show(BuildingTypeSO buildintTypeSO)
    {
        thisGameObject.SetActive(true);

        thisSpriteRender.sprite = buildintTypeSO.sprite;

        fatherOfTheUIBuildingGhostInfo.gameObject.SetActive(true);

        resourceTypeSpriteGhostBuildingHelperUI.sprite = buildintTypeSO.resourceGeneratorData.itemsTypesSO.sprite;
    }

    private void Hide()
    {
        thisGameObject.SetActive(false);

        fatherOfTheUIBuildingGhostInfo.gameObject.SetActive(false);
    }

    public void SetMouseSpriteAsInvalid()
    {
        var convertedWithoutProblem = ColorUtility.TryParseHtmlString("#" + HEXADECIMAL_INVALID_POSITION_TO_PLACE, out Color colorInvalid);

        if(convertedWithoutProblem)
            thisSpriteRender.color = colorInvalid;
    }

    public void SetMouseSpriteAsValid()
    {
        var convertedWithoutProblem = ColorUtility.TryParseHtmlString("#" + HEXADECIMAL_VALID_POSITION_TO_PLACE, out Color colorValid);

        if (convertedWithoutProblem)
            thisSpriteRender.color = colorValid;
    }
}
