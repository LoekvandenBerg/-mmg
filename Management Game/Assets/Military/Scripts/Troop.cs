﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Troop
{
    public enum TroopType
    {
        Infantry,
        Cavalry,
        Spear,
        Archer,
        Magic
    }

    public enum AvailabilityState { Locked, Unlocked, Training }

    public TroopType troopType;
    public AvailabilityState availabilityState;
    public string troopName;
    public Sprite troopSprite;
    public TroopRequirement troopRequirement;
    public List<ResourceAmount> resourceCosts = new List<ResourceAmount>();
    public float trainedTime;
    public float requiredTrainTime;

    public Troop()
    {
        if (availabilityState == AvailabilityState.Locked)
        {
            GameEvents.OnBuildingCompleted += CheckTrainingRequirements;
        }
    }

    public void Train()
    {
        GameEvents.TrainingCompleted(this);
        GameEvents.OnTimePassed -= CheckTrainingTime;

        availabilityState = AvailabilityState.Unlocked;
        Debug.Log("Troops were trained " + troopName);
    }

    public void CheckTrainingTime()
    {
        if (availabilityState == AvailabilityState.Training)
        {
            if (trainedTime >= requiredTrainTime)
            {
                Train();
            }
        }
    }

    public void TroopUnlock()
    {
        availabilityState = AvailabilityState.Unlocked;
        GameEvents.TroopUnlocked(this);
        GameEvents.OnBuildingCompleted -= CheckTrainingRequirements;
        Debug.Log("Troop unlocked " + troopName);
    }

    //Fired when trainingcompleted 
    void CheckTrainingRequirements(Building completedBuilding)
    {
        if (availabilityState == AvailabilityState.Locked)
        {
            if (completedBuilding != null && !troopRequirement.completed && troopRequirement.building.buildingName == completedBuilding.buildingName)
            {
                troopRequirement.completed = true;
                if (troopRequirement.completed)
                {
                    TroopUnlock();
                }
            }
        }
    }

    public bool CanTrain(int amountToTrain)
    {
        bool hasEnoughResources = false;
        List<ResourceAmount> resourceAmounts = new List<ResourceAmount>();
        for (int i = 0; i < resourceCosts.Count; i++)
        {
            ResourceAmount currentAmount = Resource.Instance.GetResourceAmount(resourceCosts[i].resourceType);
            ResourceAmount cost = resourceCosts[i];
            if (currentAmount.resourceAmount >= (cost.resourceAmount * amountToTrain))
            {
                resourceAmounts.Add(currentAmount);
            }
        }
        if (resourceAmounts.Count >= resourceCosts.Count)
        {
            hasEnoughResources = true;
            // resourceAmounts has the same order as resourceCosts
            for (int i = 0; i < resourceCosts.Count; i++)
            {
                resourceAmounts[i].resourceAmount -= (resourceCosts[i].resourceAmount * amountToTrain);
            }
            availabilityState = Troop.AvailabilityState.Training;
            GameEvents.TrainingStarted(this);
            GameEvents.OnTimePassed += CheckTrainingTime;
        }
        return hasEnoughResources;
    }
}

[System.Serializable]
public class TroopRequirement
{
    public Building building;
    public bool completed;
}
