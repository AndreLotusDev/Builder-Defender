using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManagerUI : MonoBehaviour
{

    private BuildingTypeListSO buildingTypeListSOs;
    [SerializeField] private Sprite spriteMouse;

    private Dictionary<string, UIBuildingHelper> uiButtonsBuilding;

    private class UIBuildingHelper
    {
        public UIBuildingHelper(Transform fatherUIBuiling, Transform selectedUIBuilding)
        {
            FatherUIBuiling = fatherUIBuiling;
            SelectedUIBuilding = selectedUIBuilding;
        }

        public Transform FatherUIBuiling { get; set; }
        public Transform SelectedUIBuilding { get; set; }
    }

    private void Awake()
    {
        uiButtonsBuilding = new Dictionary<string, UIBuildingHelper>();

        buildingTypeListSOs = SOHelper.GetBuildingTypeListSO();

        var indexResource = 1;
        var offsetPositionBetweenEachOther = 170;

        var buildingTypeUI = transform.Find("btnTemplate");
        buildingTypeUI.Find("buildingImage").GetComponent<Image>().sprite = spriteMouse;

        var xAnchor = buildingTypeUI.GetComponent<RectTransform>().anchoredPosition.x;
        var yAnchor = buildingTypeUI.GetComponent<RectTransform>().anchoredPosition.y;

        buildingTypeUI.GetComponent<Button>().onClick.AddListener(() => OnClickHandleBuilding(null, true));

        AddMouseToDictionary(buildingTypeUI);

        foreach (var buildingType in buildingTypeListSOs.BuildingTypeList)
        {
            AddANewButtonWithBuilding(indexResource, offsetPositionBetweenEachOther, buildingTypeUI, xAnchor, yAnchor, buildingType);

            indexResource++;
        }

    }

    private void AddANewButtonWithBuilding(int indexResource, int offsetPositionBetweenEachOther, Transform buildingTypeUI, float xAnchor, float yAnchor, BuildingTypeSO buildingType)
    {
        Transform templateResourceUITemp = Instantiate(buildingTypeUI, transform);

        var anchorNewPosition = xAnchor + (offsetPositionBetweenEachOther * indexResource);
        templateResourceUITemp.GetComponent<RectTransform>().anchoredPosition = new Vector2(anchorNewPosition, yAnchor);

        templateResourceUITemp.Find("buildingImage").GetComponent<Image>().sprite = buildingType.sprite;

        templateResourceUITemp.gameObject.SetActive(true);

        templateResourceUITemp.GetComponent<Button>().onClick.AddListener(() => OnClickHandleBuilding(buildingType));

        var buildingReferenceTemp = new UIBuildingHelper(templateResourceUITemp, templateResourceUITemp.Find("selected"));
        uiButtonsBuilding.Add(buildingType.nameString, buildingReferenceTemp);

        templateResourceUITemp.Find("selected").gameObject.SetActive(false);
    }

    private void AddMouseToDictionary(Transform buildingTypeUI)
    {
        var buildingReference = new UIBuildingHelper(buildingTypeUI, buildingTypeUI.Find("selected"));
        uiButtonsBuilding.Add("mouse", buildingReference);
    }

    private void OnClickHandleBuilding(BuildingTypeSO prefabOfTheBuilding, bool imMouse = false)
    {
        if(prefabOfTheBuilding != null)
        {
            DeselectAllAnSetNewBuildingType(prefabOfTheBuilding);
        }
        else if(imMouse)
        {
            DeselectAllAndSetNullToMousePointer();
        }
    }

    private void DeselectAllAnSetNewBuildingType(BuildingTypeSO prefabOfTheBuilding)
    {
        foreach (var buildingUI in uiButtonsBuilding.Values)
        {
            buildingUI.SelectedUIBuilding.gameObject.SetActive(false);
        }

        uiButtonsBuilding[prefabOfTheBuilding.nameString].SelectedUIBuilding.gameObject.SetActive(true);

        BuildingManager.Instance.SetBuildingActualToUse(prefabOfTheBuilding);
    }

    private void DeselectAllAndSetNullToMousePointer()
    {
        foreach (var buildingUI in uiButtonsBuilding.Values)
        {
            buildingUI.SelectedUIBuilding.gameObject.SetActive(false);
        }

        uiButtonsBuilding["mouse"].SelectedUIBuilding.gameObject.SetActive(true);

        BuildingManager.Instance.SetBuildingActualToUse(null);
    }
}
