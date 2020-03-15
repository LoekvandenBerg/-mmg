using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HuntNodeUI : MonoBehaviour
{
    [HideInInspector]
    public Hunt hunt;
    [SerializeField]
    private Image huntImg;
    [SerializeField]
    private Button huntButton;
    [SerializeField]
    private TextMeshProUGUI requiredTroopAmountText = null, huntNameText = null;

    public void Initialize(Hunt hunt)
    {
        this.hunt = hunt;
        huntNameText.text = hunt.huntName;

        if (hunt.availabilityState == Hunt.AvailabilityState.Locked)
        {
            huntImg.color = Color.red;
            huntButton.interactable = false;
            GameEvents.OnHuntUnlocked += UnlockedHunt;
        }
    }

    public void UnlockedHunt(Hunt hunt)
    {
        if (this.hunt == hunt)
        {
            huntImg.color = Color.white;
            huntButton.interactable = true;
            GameEvents.OnHuntUnlocked -= UnlockedHunt;
        }
    }

    void OnHuntButton()
    {
        if (hunt.availabilityState == Hunt.AvailabilityState.Unlocked && CanHunt())
        {
            hunt.huntedTime = 0.0f;
        }
    }

    bool CanHunt()
    {
        bool canHunt = false;

        switch (hunt.requiredTroopType)
        {
            case Troop.TroopType.Infantry:
                if (MilitaryManager.Instance.infantryAmount >= hunt.neededArmyAmount)
                    canHunt = true;
                break;
            case Troop.TroopType.Cavalry:
                if (MilitaryManager.Instance.cavalryAmount >= hunt.neededArmyAmount)
                    canHunt = true;
                break;
            case Troop.TroopType.Spear:
                if (MilitaryManager.Instance.spearAmount >= hunt.neededArmyAmount)
                    canHunt = true;
                break;
            case Troop.TroopType.Archer:
                if (MilitaryManager.Instance.archerAmount >= hunt.neededArmyAmount)
                    canHunt = true;
                break;
            default:
                break;
        }

        return canHunt;
    }
}
