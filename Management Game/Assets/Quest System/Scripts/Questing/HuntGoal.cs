using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntGoal : Goal
{
    public string huntName;

    public HuntGoal(int amountNeeded, string huntName, Quest quest)
    {
        countCurrent = 0;
        countNeeded = amountNeeded;
        completed = false;
        this.huntName = huntName;
        this.quest = quest;
        GameEvents.OnHuntQuestCompleted += HuntQuestCompleted;
    }

    void HuntQuestCompleted(string huntName)
    {
        if (this.huntName == huntName)
        {
            Increment();
            if (this.completed)
            {
                GameEvents.OnHuntQuestCompleted -= HuntQuestCompleted;
            }
        }
    }
}
