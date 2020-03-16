using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameEvents : MonoBehaviour
{
    public static Action<Technology> OnTechResearchStarted;
    public static Action<Technology> OnTechResearchCompleted;
    public static Action<Technology> OnTechUnlocked;

    //public static Action OnTurnPassed;
    public static Action OnTimePassed;

    public static Action<Building> OnBuildingStarted;
    public static Action<Building> OnBuildingCompleted;
    public static Action<Building> OnBuildingUnlocked;

    public static Action<Troop> OnTrainingStarted;
    public static Action<Troop> OnTrainingCompleted;
    public static Action<Troop> OnTroopUnlocked;

    public static Action<Hunt> OnHuntStarted;
    public static Action<Hunt> OnHuntCompleted;
    public static Action<Hunt> OnHuntUnlocked;

    // TECH EVENTS

    public static void TechResearchStarted(Technology tech)
    {
        OnTechResearchStarted?.Invoke(tech);
    }
    //should fire when tech completes to unlock something
    public static void TechResearchCompleted(Technology tech)
    {
        OnTechResearchCompleted?.Invoke(tech);
    }
    public static void TechUnlocked(Technology tech)
    {
        OnTechUnlocked?.Invoke(tech);
    }

    /// BUILDING EVENTS

    public static void BuildingStarted(Building building)
    {
        OnBuildingStarted?.Invoke(building);
    }
    //should fire when building completes to unlock something
    public static void BuildingCompleted(Building building)
    {
        OnBuildingCompleted?.Invoke(building);
    }
    public static void BuildingUnlocked(Building building)
    {
        OnBuildingUnlocked?.Invoke(building);
    }

    // MILITARY EVENTS

    public static void TrainingStarted(Troop troop)
    {
        OnTrainingStarted?.Invoke(troop);
    }
    public static void TrainingCompleted(Troop troop)
    {
        OnTrainingCompleted?.Invoke(troop);
    }
    public static void TroopUnlocked(Troop troop)
    {
        OnTroopUnlocked?.Invoke(troop);
    }

    // HUNT EVENTS


    public static void HuntStarted(Hunt hunt)
    {
        OnHuntStarted?.Invoke(hunt);
    }
    //should fire when hunt completes to gain rewards
    public static void HuntCompleted(Hunt hunt)
    {
        OnHuntCompleted?.Invoke(hunt);
    }
    public static void HuntUnlocked(Hunt hunt)
    {
        OnHuntUnlocked?.Invoke(hunt);
    }


    //GENERAL EVENTS

    //public static void TurnPassed()
    //{
    //    OnTurnPassed?.Invoke();
    //}
    public static void TimePassed()
    {
        OnTimePassed?.Invoke();
    }

    //public static void TimerDone(Troop troop)
    //{
    //    OnTrainingCompleted?.Invoke(troop);
    //}
}
