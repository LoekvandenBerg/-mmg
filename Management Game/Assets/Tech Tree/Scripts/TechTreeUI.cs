using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TechTreeUI : MonoBehaviour
{
    private List<TechGroup> techGroups = null;
    [SerializeField]
    private GameObject techColumn = null;
    [SerializeField]
    private TechNodeUI techNode = null;
    [SerializeField]
    private Transform techPanel = null;
    private List<TechNodeUI> techNodes = new List<TechNodeUI>();

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
}
