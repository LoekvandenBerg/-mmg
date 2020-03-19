using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour {
    // set the slugs in inspector to add quests
    [SerializeField] private List<string> questSlugs;
    private QuestController questController;
    private Quest quest;
    private int index = 0;
    //public List<Quest> quests;

	// Use this for initialization
	void Start () {
        questController = FindObjectOfType<QuestController>();
        EventController.OnQuestCompleted += Completed;
        GiveQuest(null);
        EventController.OnQuestCompleted += GiveQuest;
    }

    public void GiveQuest(Quest quest)
    {
        //give first quest
        if(index == 0)
            this.quest = questController.AssignQuest(questSlugs[index]);
        // if first quest is completed add the next
        else if (index < questSlugs.Count)
            this.quest = questController.AssignQuest(questSlugs[index]);
        else
            Debug.Log("Out of quests");
    }

    public void Completed(Quest quest)
    {
        if (this.quest != null && quest == this.quest)
        {
            // make sure next quest is added
            index++;
        }
    }
}
