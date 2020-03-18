using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingListUI : MonoBehaviour
{
    public static BuildingListUI Instance { get; private set; }

    [SerializeField]
    private Transform buildingPanel = null;
    [SerializeField]
    private BuildingNodeUI buildingNodePrefab = null;
    private List<Building> buildings = null;
    [HideInInspector]
    public List<BuildingNodeUI> buildingNodes = new List<BuildingNodeUI>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        buildings = FindObjectOfType<BuildingDatabase>().buildings;
        BuildList();
    }

    void BuildList()
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            BuildingNodeUI buildingNodeObj = Instantiate(buildingNodePrefab, buildingPanel);
            buildingNodes.Add(buildingNodeObj);
            buildingNodeObj.Initialize(buildings[i]);
        }
    }

    public void LockAllBuildingButtons()
    {
        foreach(BuildingNodeUI node in buildingNodes)
        {
            node.buildButton.interactable = false;
        }
    }

    public void UnlockAllBuildingButtons()
    {
        foreach (BuildingNodeUI node in buildingNodes)
        {
            node.buildButton.interactable = true;
        }
    }
}
