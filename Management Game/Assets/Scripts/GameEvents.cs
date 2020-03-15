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

    public static Action<Building> OnBuildingCompleteTroop;

    public static Action<Troop> OnTrainingStarted;
    public static Action<Building> OnTrainingCompleted;
    public static Action<Troop> OnTroopUnlocked;

    // TECH EVENTS

    public static void TechResearchStarted(Technology tech)
    {
        OnTechResearchStarted?.Invoke(tech);
    }
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
    public static void TrainingCompleted(Building building)
    {
        OnTrainingCompleted?.Invoke(building);
    }
    public static void TroopUnlocked(Troop troop)
    {
        OnTroopUnlocked?.Invoke(troop);
    }


    public static void BuildingCompletedForTroop(Building building)
    {
        OnBuildingCompleteTroop?.Invoke(building);
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
}
