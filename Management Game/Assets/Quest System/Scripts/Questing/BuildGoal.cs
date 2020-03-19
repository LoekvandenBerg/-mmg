using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildGoal : Goal
{
    public string buildingName;

    public BuildGoal(int levelNeeded, string buildingName, Quest quest)
    {
        countCurrent = 0;
        countNeeded = levelNeeded;
        completed = false;
        this.buildingName = buildingName;
        this.quest = quest;
        GameEvents.OnBuildingCompletedQuestCheck += BuildingBuild;
    }

    void BuildingBuild(string buildingName, int level)
    {
        if (this.buildingName == buildingName)
        {
            Increment();
            if (this.completed)
            {
                GameEvents.OnBuildingCompletedQuestCheck -= BuildingBuild;
            }
        }
    }
}
