#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class TechToolTip : MonoBehaviour
{
    public static TechToolTip Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI techCostText, techDescriptionText, techModText;

    private void LateUpdate()
    {
        transform.position = Input.mousePosition;
    }

    public void Show(Technology tech)
    {
        gameObject.SetActive(true);
        string cost = string.Join("\n", tech.resourceCosts.Select(c => string.Format("-{0} {1}", c.resourceAmount, c.resourceType)));
        string mods = string.Join("\n", tech.techModifiers.Select(m => string.Format("+{0} {1}", m.amount, m.modifierType)));
        techCostText.text = cost;
        techModText.text = mods;
        techDescriptionText.text = tech.techDescription;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

        gameObject.SetActive(false);
    }


}
