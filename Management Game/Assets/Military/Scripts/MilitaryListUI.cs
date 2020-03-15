using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryListUI : MonoBehaviour
{
    [SerializeField]
    private Transform militaryPanel = null;
    [SerializeField]
    private MilitaryNodeUI militaryHolderPrefab = null;
    private List<Troop> troops = new List<Troop>();
    [HideInInspector]
    public List<MilitaryNodeUI> troopHolders = new List<MilitaryNodeUI>();

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
}
