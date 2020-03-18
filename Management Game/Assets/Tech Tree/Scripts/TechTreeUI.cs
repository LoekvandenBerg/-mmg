using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTreeUI : MonoBehaviour
{
    public static TechTreeUI Instance { get; private set; }

    private List<TechGroup> techGroups = null;
    [SerializeField]
    private GameObject techColumn = null;
    [SerializeField]
    private TechNodeUI techNode = null;
    [SerializeField]
    private Transform techPanel = null;
    private List<TechNodeUI> techNodes = new List<TechNodeUI>();


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
        techGroups = FindObjectOfType<TechDatabase>().techGroups;
        StartCoroutine(BuildTree());
    }

    IEnumerator BuildTree()
    {
        for (int i = 0; i < techGroups.Count; i++)
        {
            Transform col = Instantiate(techColumn, techPanel).transform;
            for (int j = 0; j < techGroups[i].technologies.Count; j++)
            {
                TechNodeUI node = Instantiate(techNode, col);
                techNodes.Add(node); 
                node.Initialize(techGroups[i].technologies[j]);
            }
        }

        yield return 0;

        for (int i = 0; i < techNodes.Count; i++)
        {
            techNodes[i].Connect(techNodes, techPanel);
        }
    }

    public void LockAllTechButtons()
    {
        foreach (TechNodeUI node in techNodes)
        {
            node.researchButton.interactable = false;
        }
    }

    public void UnlockAllTechButtons()
    {
        foreach (TechNodeUI node in techNodes)
        {
            node.researchButton.interactable = true;
        }
    }
}
