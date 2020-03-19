using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinorWyvernHuntQuest : Quest
{
    private void Awake()
    { 
        slug = "MinorWyvernHuntQuest";
        questName = "Minor Wyvern Hunt";
        description = "Kill some minor wyverns";

        //itemRewards = new List<string>() { "Arrow" };
        goal = new HuntGoal(1, "Minor Wyvern Hunt", this);
    }

    public override void Complete()
    {
        //GrantReward();
        base.Complete();
    }
}
