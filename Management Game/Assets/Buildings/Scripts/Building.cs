using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class Building
{
    public enum BuildingState
    {
        Locked,
        Unlocked,
        Building
    }

    public string buildingName;
    public BuildingState buildingState;
    public int initProductionAmount;
    public int level;
    public List<ProductionModifiers> productionModifiers;
    public List<ResourceAmount> resourceCosts;
    public List<BuildingRequirements> buildingRequirements;
    public float buildingTime;
    public float requiredBuildingTime;

    public Building()
    {
        if(buildingState == BuildingState.Locked)
        {
            GameEvents.OnBuildingCompleted += CheckBuildingRequirements;
        }
    }

    public void Build()
    {
        GameEvents.BuildingCompleted(this);
        GameEvents.OnTimePassed -= CheckBuildingTime;
        
        buildingState = BuildingState.Unlocked;
        Debug.Log("Building was build " + buildingName);
    }

    public void CheckBuildingTime()
    {
        if (buildingState == BuildingState.Building)
        {
            if (buildingTime >= requiredBuildingTime)
            {
                level++;
                Build();
            }
        }
    }
    
    public void BuildingUnlock()
    {
        buildingState = BuildingState.Unlocked;
        GameEvents.BuildingUnlocked(this);
        GameEvents.OnBuildingCompleted -= CheckBuildingRequirements;
        Debug.Log("Building unlocked " + buildingName);
    }

      //Fired when buildingcompleted 
    void CheckBuildingRequirements(Building completedBuilding)
    {
            if (buildingState == BuildingState.Locked)
            {
                BuildingRequirements requirements = buildingRequirements.FirstOrDefault(br => br.buildingName == completedBuilding.buildingName && br.level == completedBuilding.level);
                if (requirements != null && !requirements.completed)
                {
                    requirements.completed = true;
                    if (!buildingRequirements.Any(t => t.completed == false))
                    {
                        BuildingUnlock();
                    }
                }
            }
    }

    public bool CanBuild()
    {
        bool hasEnoughResources = false;
        List<ResourceAmount> resourceAmounts = new List<ResourceAmount>();
        for (int i = 0; i < resourceCosts.Count; i++)
        {
            ResourceAmount currentAmount = Resources.Instance.GetResourceAmount(resourceCosts[i].resourceType);
            ResourceAmount cost = resourceCosts[i];
            if (currentAmount.resourceAmount >= cost.resourceAmount)
            {
                resourceAmounts.Add(currentAmount);
            }
        }
        if (resourceAmounts.Count >= resourceCosts.Count )
        {
            hasEnoughResources = true;
            // resourceAmounts has the same order as resourceCosts
            for (int i = 0; i < resourceCosts.Count; i++)
            {
                resourceAmounts[i].resourceAmount -= resourceCosts[i].resourceAmount;
            }
            buildingState = BuildingState.Building;
            GameEvents.BuildingStarted(this);
            GameEvents.OnTimePassed += CheckBuildingTime;
        }
        return hasEnoughResources;
    }

    //bool IsBuilding()
    //{
    //    bool amIBuilding;
    //    List<bool> amIbuildingList = new List<bool>();

    //    foreach (BuildingNodeUI node in buildings)
    //    {
    //        if (node.building.buildingState == Building.BuildingState.Building)
    //            amIbuildingList.Add(true);
    //        else amIbuildingList.Add(false);
    //    }

    //    if (amIbuildingList.Contains(true))
    //        amIBuilding = true;
    //    else
    //        amIBuilding = false;

    //    return amIBuilding;
    //}
}

[System.Serializable]
public class BuildingRequirements
{
    public string buildingName;
    public int level;
    public bool completed;
}

[System.Serializable]
public class ProductionModifiers
{
    public enum ModifierType { Wood, Food, Steel, Gold, Water}
    public ModifierType modifierType;
    public int amount;
}
