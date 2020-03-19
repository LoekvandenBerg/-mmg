using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchHunterQuest : Quest {

    void Awake()
    {
        questName = "Witch Hunter";
        description = "Hunt some witches";
        //resourceRewards = null;
        goal = new KillGoal(2, 1, this);
    }

    public override void Complete()
    {
        base.Complete();
    }

}
