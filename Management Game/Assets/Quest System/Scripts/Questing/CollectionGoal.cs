using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGoal : Goal {
    public int itemID;

    public CollectionGoal(int amountNeeded, int itemID, Quest quest)
    {
        countCurrent = 0;
        countNeeded = amountNeeded;
        completed = false;
        this.quest = quest;
        this.itemID = itemID;
        EventController.OnItemFound += ItemFound;
    }

    void ItemFound(int itemID)
    {
        if (this.itemID == itemID)
        {
            Increment();
            if (this.completed)
            {
                EventController.OnItemFound -= ItemFound;
            }
        }
    }
}
