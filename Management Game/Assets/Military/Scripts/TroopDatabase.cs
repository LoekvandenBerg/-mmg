using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopDatabase : MonoBehaviour
{
    public List<Troop> troops;
    private float checkTrainTimeRate = 1.0f;
    private float offset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckTrainTime", 0, checkTrainTimeRate);
    }

    void CheckTrainTime()
    {
        for (int i = 0; i < troops.Count; i++)
        {
            if (troops[i].availabilityState == Troop.AvailabilityState.Training)
            {
                troops[i].trainedTime += (checkTrainTimeRate + offset);
            }

        }
        GameEvents.TimePassed();
    }
}
