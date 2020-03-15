using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Troop
{
    public enum TroopType
    {
        Infantry,
        Cavalry,
        Spear,
        Archer
    }

    public enum AvailabilityState { Locked, Unlocked, Training }

    public TroopType troopType;
    public AvailabilityState availabilityState;
    public string troopName;
    public TroopRequirements troopRequirement;
    public List<ResourceAmount> resourceCosts;
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
        GameEvents.TrainingCompleted(troopRequirement.building);
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
        GameEvents.OnTrainingCompleted -= CheckTrainingRequirements;
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
}

[System.Serializable]
public class TroopRequirements
{
    public Building building;
    public bool completed;
}
