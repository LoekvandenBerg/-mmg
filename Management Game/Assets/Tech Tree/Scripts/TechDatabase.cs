using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechDatabase : MonoBehaviour
{
    public List<TechGroup> techGroups;
    private float checkResearchTimeRate = 1.0f;
    private float offset = 0.1f;

    private void Start()
    {
        InvokeRepeating("CheckResearchTime", 0, checkResearchTimeRate);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            techGroups[0].technologies[0].StartResearching();
        } 

        if (Input.GetKeyDown(KeyCode.S))
        {
            techGroups[1].technologies[0].StartResearching();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //GameEvents.TurnPassed();
        }
    }

    void CheckResearchTime()
    {
        for (int i = 0; i < techGroups.Count; i++)
        {
            for (int j = 0; j < techGroups[i].technologies.Count; j++)
            {
                if(techGroups[i].technologies[j].availabilityState == Technology.AvailabilityState.Researching)
                {
                    techGroups[i].technologies[j].researchedTime += (checkResearchTimeRate + offset);
                }
            }
        }
        GameEvents.TimePassed();
    }
}

[System.Serializable]
public class TechGroup
{
    public List<Technology> technologies;

}
