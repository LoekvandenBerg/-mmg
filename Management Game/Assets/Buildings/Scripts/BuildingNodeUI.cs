﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class BuildingNodeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    public Building building;
    [SerializeField]
    private Image buildingImg = null;
    [SerializeField]
    private TextMeshProUGUI buildingNameText = null, levelText = null;
    private Color imgColor = new Color32(124, 17, 217, 255);


    public void Initialize(Building building)
    {
        this.building = building;
        buildingNameText.text = building.buildingName;
        levelText.text = "Level: " + building.level.ToString();

        if(building.buildingState == Building.BuildingState.Locked)
        {
            buildingImg.color = Color.grey;
            GameEvents.OnBuildingUnlocked += UnlockedBuilding;
        }
        GetComponent<Button>().onClick.AddListener(TryBuilding);
    }

    public void TryBuilding()
    {
        if(building.buildingState == Building.BuildingState.Unlocked && building.CanBuild())
        {
            building.buildingTime = 0.0f;
            buildingImg.color = Color.cyan;
            GameEvents.OnBuildingCompleted += CompletedBuilding;
        }
    }

    public void CompletedBuilding(Building building)
    {
        UpdateLevelUI();
        GameEvents.OnBuildingCompleted -= CompletedBuilding;
    }

    public void UnlockedBuilding(Building build)
    {
        if (build == building)
        {
            buildingImg.color = imgColor;
            GameEvents.OnBuildingUnlocked -= UnlockedBuilding;
        }
    }

    void UpdateLevelUI()
    {
        levelText.text = "Level: " + building.level;
        buildingImg.color = imgColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        BuildingToolTip.Instance.Show(this.building);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BuildingToolTip.Instance.Hide();
    }
}
