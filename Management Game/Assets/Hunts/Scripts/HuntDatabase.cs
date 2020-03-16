using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntDatabase : MonoBehaviour
{
    public List<Hunt> hunts;
    private float checkHuntTimeRate = 1.0f;
    private float offset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckHuntingTime", 0, checkHuntTimeRate);
    }

    void CheckHuntingTime()
    {
        for (int i = 0; i < hunts.Count; i++)
        {
            if (hunts[i].availabilityState == Hunt.AvailabilityState.Hunting)
            {
                hunts[i].huntedTime += (checkHuntTimeRate + offset);
            }

        }
        GameEvents.TimePassed();
    }
}
