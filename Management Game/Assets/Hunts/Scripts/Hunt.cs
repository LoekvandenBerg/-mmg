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
        Hunting,
        Completed
    }

    public AvailabilityState availabilityState;
    public Rarity.RarityType rarity;
    public string huntName;
    public int neededArmyAmount;
    public Troop.TroopType requiredTroopType;
    public float huntedTime;
    public float requiredHuntTime;
    public List<ResourceAmount> resourcesToLoot;
    public HuntRequirements huntRequirement;

    //public Loot[] loot;


    public Hunt()
    {
        if (availabilityState == AvailabilityState.Locked)
        {
            GameEvents.OnTechResearchCompleted += CheckHuntRequirements;
        }
    }

    public void HuntUnlock()
    {
        availabilityState = AvailabilityState.Unlocked;
        GameEvents.HuntUnlocked(this);
        GameEvents.OnTechResearchCompleted -= CheckHuntRequirements;
        Debug.Log("Hunt unlocked " + huntName);
    }

    public void HuntComplete()
    {
        GameEvents.HuntCompleted(this);
        GameEvents.HuntQuestCompleted(huntName);
        GameEvents.OnTimePassed -= CheckHuntingTime;

        availabilityState = AvailabilityState.Completed;
        Debug.Log(string.Format("Hunt was finished {0}", huntName));
    }

    public void CheckHuntingTime()
    {
        if (availabilityState == AvailabilityState.Hunting)
        {
            if (huntedTime >= requiredHuntTime)
            {
                HuntComplete();
            }
        }
    }

    //Fired when huntcompleted for more hunts
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

    public bool CanHunt()
    {
        bool canHunt = false;

        switch (requiredTroopType)
        {
            case Troop.TroopType.Infantry:
                if (MilitaryManager.Instance.infantryAmount >= neededArmyAmount)
                    canHunt = true;
                break;
            case Troop.TroopType.Cavalry:
                if (MilitaryManager.Instance.cavalryAmount >= neededArmyAmount)
                    canHunt = true;
                break;
            case Troop.TroopType.Spear:
                if (MilitaryManager.Instance.spearAmount >= neededArmyAmount)
                    canHunt = true;
                break;
            case Troop.TroopType.Archer:
                if (MilitaryManager.Instance.archerAmount >= neededArmyAmount)
                    canHunt = true;
                break;
            case Troop.TroopType.Magic:
                if (MilitaryManager.Instance.mageAmount >= neededArmyAmount)
                    canHunt = true;
                break;
            default:
                break;
        }

        availabilityState = AvailabilityState.Hunting;
        GameEvents.OnTimePassed += CheckHuntingTime;
        return canHunt;
    }
}
[Serializable]
public class HuntRequirements
{
    public Technology technology;
    public bool completed;
}
