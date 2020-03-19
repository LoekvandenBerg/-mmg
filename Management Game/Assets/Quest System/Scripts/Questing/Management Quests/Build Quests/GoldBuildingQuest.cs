using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldBuildingQuest : Quest
{
    private void Awake()
    {
        slug = "GoldBuildingQuest";
        questName = "Getting some gold";
        description = "Build a mint";
        //itemRewards = new List<string>() { "Arrow" };
        goal = new BuildGoal(2, "Mint", this);
    }

    public override void Complete()
    {
        //GrantReward();
        base.Complete();
    }
}
