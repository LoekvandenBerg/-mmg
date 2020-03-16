using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class BuildingToolTip : MonoBehaviour
{
    public static BuildingToolTip Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI buildingCostText = null, buildingDescriptionText = null, buildingProductionText = null, requirementText = null;

    private void LateUpdate()
    {
        transform.position = Input.mousePosition;
    }

    public void Show(Building building)
    {
        gameObject.SetActive(true);
        string cost = string.Join("\n", building.resourceCosts.Select(c => string.Format("-{0} {1}", c.resourceAmount, c.resourceType)));
        string prod = string.Join("\n", building.productionModifiers.Select(m => string.Format("+{0} {1}", m.amount, m.modifierType)));
        string requirements = string.Join( "Required: \n", building.buildingRequirements.Select(r => string.Format("{0} {1}", r.buildingName, r.level)));
        requirementText.text = requirements;
        buildingCostText.text = cost;
        buildingProductionText.text = prod;
        //buildingDescriptionText.text = building.buildingDescription;
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
