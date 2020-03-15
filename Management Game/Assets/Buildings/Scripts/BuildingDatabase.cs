using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDatabase : MonoBehaviour
{
    public List<Building> buildings;
    private float checkBuildTimeRate = 1.0f;
    private float offset = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CheckBuildTime", 0, checkBuildTimeRate);
    }

    void CheckBuildTime()
    {
        for (int i = 0; i < buildings.Count; i++)
        {
                if (buildings[i].buildingState == Building.BuildingState.Building)
                {
                    buildings[i].buildingTime += (checkBuildTimeRate + offset);
                }
            
        }
        GameEvents.TimePassed();
    }
}
