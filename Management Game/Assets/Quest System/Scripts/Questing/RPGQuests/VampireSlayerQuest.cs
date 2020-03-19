using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireSlayerQuest : Quest {

    void Awake()
    {
        questName = "Vampire Slayer";
        description = "slay some vampires.";
        //resourceRewards = null;
        goal = new KillGoal(10, 0, this);
    }

    public override void Complete()
    {
        base.Complete();
    }

}
