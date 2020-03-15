using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
public class Technology
{
    public enum AvailabilityState
    {
        Locked,
        Unlocked,
        Researching,
        Learned
    }
    public string techName;
    public AvailabilityState availabilityState;
    public string techDescription;
    public Sprite techImage;
    public List<RequiredTech> techRequirements;
    public List<ResourceAmount> resourceCosts;
    public List<TechModifier> techModifiers;
    [SerializeField]
    private int researchedTurns;
    public int requiredResearchTurns;
    public float researchedTime;
    public float requiredResearchTime;
    public Color techUIColor;

    public Technology()
    {
        if (availabilityState == AvailabilityState.Locked)
        {
            GameEvents.OnTechResearchCompleted += CheckRequirements;
        }
    }

    public void TechUnlock()
    {
        availabilityState = AvailabilityState.Unlocked;
        GameEvents.TechUnlocked(this);
        GameEvents.OnTechResearchCompleted -= CheckRequirements;
        Debug.Log("Tech unlocked " + this.techName);
    }

    public void Learn()
    {
        GameEvents.TechResearchCompleted(this);
        //GameEvents.OnTurnPassed -= NewTurn;
        GameEvents.OnTimePassed -= CheckTime;

        availabilityState = AvailabilityState.Learned;
        Debug.Log("Tech was learned " + this.techName);
    }

    //public void NewTurn()
    //{
    //    if (availabilityState == AvailabilityState.Researching)
    //    {
    //        researchedTurns++;
    //        if (researchedTurns >= requiredResearchTurns)
    //        {
    //            Learn();
    //        }
    //    }
    //}

    public void CheckTime()
    {
        if (availabilityState == AvailabilityState.Researching)
        {
            if (researchedTime >= requiredResearchTime)
            {
                Learn();
            }
        }
    }

    void CheckRequirements(Technology tech)
    {
        if(availabilityState == AvailabilityState.Locked)
        {
            RequiredTech requiredTech = techRequirements.FirstOrDefault(rt => rt.techName == tech.techName);
            if(requiredTech != null && !requiredTech.completed)
            {
                requiredTech.completed = true;
                if (!techRequirements.Any(t => t.completed == false))
                {
                    TechUnlock();
                }
            }
        }
    }

    public bool StartResearching()
    {
        bool hasEnoughResources = false;
        List<ResourceAmount> resourceAmounts = new List<ResourceAmount>();
        for (int i = 0; i < resourceCosts.Count; i++)
        {
            ResourceAmount currentAmount = Resource.Instance.GetResourceAmount(resourceCosts[i].resourceType);
            ResourceAmount cost = resourceCosts[i];
            if(currentAmount.resourceAmount >= cost.resourceAmount)
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
                resourceAmounts[i].resourceAmount -= resourceCosts[i].resourceAmount;
            }
            availabilityState = AvailabilityState.Researching;
            GameEvents.TechResearchStarted(this);
            //GameEvents.OnTurnPassed += NewTurn;
            GameEvents.OnTimePassed += CheckTime;
        }
        return hasEnoughResources;
    }

}

[System.Serializable]
public class RequiredTech
{
    public string techName;
    public bool completed;
}

[System.Serializable]
public class TechModifier
{
    public enum ModifierType{ Gold, Wood, Water, Steel, Population, Food }
    public ModifierType modifierType;
    public int amount;
}