using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour {
    public string slug;
    public string questName;
    public string description;
    public Goal goal;
    public bool completed;
    //public HuntReward resourceRewards;

    private void Awake()
    {
        //resourceRewards = GetComponent<HuntReward>();
    }

    public virtual void Complete()
    {
        Debug.Log("Quest completed!");
        EventController.QuestCompleted(this);
        //GrantReward();
    }

    public void GrantReward()
    {
        //resourceRewards
        Destroy(this);
    }
}
