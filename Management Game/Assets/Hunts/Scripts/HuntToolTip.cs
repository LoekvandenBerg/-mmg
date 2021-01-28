using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class HuntToolTip : MonoBehaviour
{
    public static HuntToolTip Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI huntTroopText = null, huntDescriptionText = null, huntLootText = null, requirementText = null;

    private void LateUpdate()
    {
        transform.position = Input.mousePosition;
    }

    public void Show(Hunt hunt)
    {
        gameObject.SetActive(true);
        string troopType = string.Join("\n", hunt.requiredTroopType);
        string loot = string.Join("\n", hunt.resourcesToLoot.Select(m => string.Format("+{0} {1}", m.resourceAmount, m.resourceType)));
        string requirements = string.Join("Required: \n", hunt.neededArmyAmount);
        requirementText.text = requirements;
        huntTroopText.text = troopType;
        huntLootText.text = loot;
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
