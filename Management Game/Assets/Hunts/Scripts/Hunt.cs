using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Hunt
{
    public enum AvailabilityState
    {
        Locked,
        Unlocked,
        Hunting
    }

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public AvailabilityState availabilityState;
    public Rarity rarity;
    public string huntName;
    public int neededArmyAmount;
    public Troop.TroopType requiredTroopType;
    public float huntedTime;
    public float requiredHuntTime;
    public List<Resource> resourcesToLoot;
    public HuntRequirements huntRequirement;

    //public Loot[] loot;


    public Hunt()
    {
        if (availabilityState == AvailabilityState.Locked)
        {
            GameEvents.OnHuntCompleted += CheckHuntRequirements;
        }
    }

    public void HuntCompleted()
    {
        GameEvents.HuntCompleted(huntRequirement.technology);
        GameEvents.OnTimePassed -= CheckHuntingTime;

        availabilityState = AvailabilityState.Unlocked;
        Debug.Log(string.Format("Hunt was finished {0}", huntName));
    }

    public void CheckHuntingTime()
    {
        if (availabilityState == AvailabilityState.Hunting)
        {
            if (huntedTime >= requiredHuntTime)
            {
                HuntCompleted();
            }
        }
    }

    //Fired when huntcompleted 
    void CheckHuntRequirements(Technology completedTech)
    {
        if (availabilityState == AvailabilityState.Locked)
        {
            if (completedTech != null && !huntRequirement.completed && huntRequirement.technology.techName == completedTech.techName)
            {
                huntRequirement.completed = true;
                if (huntRequirement.completed)
                {
                    HuntUnlock();
                }
            }
        }
    }

    public void HuntUnlock()
    {
        availabilityState = AvailabilityState.Unlocked;
        GameEvents.HuntUnlocked(this);
        GameEvents.OnHuntCompleted -= CheckHuntRequirements;
        Debug.Log("Hunt unlocked " + huntName);
    }

}
[Serializable]
public class HuntRequirements
{
    public Technology technology;
    public bool completed;
}
