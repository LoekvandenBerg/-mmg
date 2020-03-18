using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryListUI : MonoBehaviour
{
    public static MilitaryListUI Instance { get; private set; }

    [SerializeField]
    private Transform militaryPanel = null;
    [SerializeField]
    private MilitaryNodeUI militaryHolderPrefab = null;
    private List<Troop> troops = new List<Troop>();
    [HideInInspector]
    public List<MilitaryNodeUI> troopHolders = new List<MilitaryNodeUI>();

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
        troops = FindObjectOfType<TroopDatabase>().troops;
        BuildList();
    }

    void BuildList()
    {
        for (int i = 0; i < troops.Count; i++)
        {
            MilitaryNodeUI militaryHolderUIObj = Instantiate(militaryHolderPrefab, militaryPanel);
            troopHolders.Add(militaryHolderUIObj);
            militaryHolderUIObj.Initialize(troops[i]);
        }
    }

    public void LockAllTrainingButtons()
    {
        foreach (MilitaryNodeUI node in troopHolders)
        {
            node.trainButton.interactable = false;
        }
    }

    public void UnlockAllTrainingButtons()
    {
        foreach (MilitaryNodeUI node in troopHolders)
        {
            node.trainButton.interactable = true;
        }
    }
}
