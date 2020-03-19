using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public List<Quest> assignedQuests = new List<Quest>();

    [SerializeField]
    private QuestUIItem questUIItem;
    [SerializeField]
    private Transform questUIParent;
    private Quest quest;
    private QuestDatabase questDatabase;

    private void Awake()
    {
        questDatabase = GetComponent<QuestDatabase>();
    }

    public Quest AssignQuest(string questName)
    {
        if (assignedQuests.Find(quest => quest.questName == questName))
        {
            Debug.LogWarning("Quest already assigned!");
            return null;
        }
        quest = (Quest)gameObject.AddComponent(System.Type.GetType(questName));
        if (quest == null)
            Debug.LogError("questToAdd = null");
        else
        {
            assignedQuests.Add(quest);
            questDatabase.AddQuest(quest);
            Invoke("InstantiateQuestUIItem", 2.0f);
            return quest;
        }

        return null;
    }

    void InstantiateQuestUIItem()
    {
        QuestUIItem questUI = Instantiate(questUIItem, questUIParent);
        questUI.Setup(quest);
    }
}
