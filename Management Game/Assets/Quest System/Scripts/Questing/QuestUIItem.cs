using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIItem : MonoBehaviour 
{
    [SerializeField]
    private UnityEngine.UI.Text questName, questProgress;
    private Quest quest;

    private void Start()
    {
        EventController.OnQuestCompleted += QuestCompleted;
        EventController.OnQuestProgressChanged += UpdateProgress;
    }

    public void Setup(Quest questToSetup)
    {
        quest = questToSetup;
        questName.text = questToSetup.questName;
        questProgress.text = questToSetup.goal.countCurrent + "/" + questToSetup.goal.countNeeded;
    }

    public void UpdateProgress(Quest quest)
    {
        if (this.quest == quest)
        {
            questProgress.text = quest.goal.countCurrent + "/" + quest.goal.countNeeded;
        }
    }

    public void QuestCompleted(Quest quest)
    {
        if (this.quest == quest)
        {
            Destroy(this.gameObject, 1f);
        }
    }
}
