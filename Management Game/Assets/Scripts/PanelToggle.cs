using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggle : MonoBehaviour
{
    private GameObject panelToToggle;
    private bool currentToggle;
    [SerializeField]
    private KeyCode keyToToggle;

    // Start is called before the first frame update
    void Awake()
    {
        panelToToggle = transform.GetChild(0).gameObject;
        currentToggle = panelToToggle.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToToggle))
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        currentToggle = !currentToggle;
        panelToToggle.SetActive(currentToggle);
    }
}
