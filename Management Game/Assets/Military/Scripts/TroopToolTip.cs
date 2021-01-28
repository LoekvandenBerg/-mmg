using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TroopToolTip : MonoBehaviour
{
    public static TroopToolTip Instance { get; private set; }
    [SerializeField]
    private TextMeshProUGUI huntTroopText = null, huntDescriptionText = null, huntLootText = null, requirementText = null;

    private void LateUpdate()
    {
        transform.position = Input.mousePosition;
    }

    public void Show(Troop troop)
    {
        gameObject.SetActive(true);
        string troopType = string.Join("\n", troop.requiredTroopType);
        string loot = string.Join("\n", troop.resourcesToLoot.Select(m => string.Format("+{0} {1}", m.resourceAmount, m.resourceType)));
        string requirements = string.Join("Required: \n", troop.neededArmyAmount);
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
