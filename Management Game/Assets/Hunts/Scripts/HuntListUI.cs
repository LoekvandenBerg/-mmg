using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntListUI : MonoBehaviour
{
    public static HuntListUI Instance { get; private set; }

    [SerializeField]
    private Transform huntPanel = null;
    [SerializeField]
    private HuntNodeUI huntHolderPrefab = null;
    private List<Hunt> hunts = new List<Hunt>();
    [HideInInspector]
    public List<HuntNodeUI> huntNodes = new List<HuntNodeUI>();

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
        hunts = FindObjectOfType<HuntDatabase>().hunts;
        BuildList();
    }

    void BuildList()
    {
        for (int i = 0; i < hunts.Count; i++)
        {
            HuntNodeUI huntHolderUIObj = Instantiate(huntHolderPrefab, huntPanel);
            huntNodes.Add(huntHolderUIObj);
            huntHolderUIObj.Initialize(hunts[i]);
        }
    }

    public void LockAllHuntButtons()
    {
        foreach (HuntNodeUI node in huntNodes)
        {
            node.huntButton.interactable = false;
        }
    }

    public void UnlockAllHuntButtons()
    {
        foreach (HuntNodeUI node in huntNodes)
        {
            node.huntButton.interactable = true;
        }
    }
}
