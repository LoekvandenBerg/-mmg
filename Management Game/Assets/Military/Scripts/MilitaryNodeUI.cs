using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MilitaryNodeUI : MonoBehaviour
{
    [HideInInspector]
    public Troop troop;
    [SerializeField]
    private Image troopImg;
    [SerializeField]
    private Button trainButton;
    [SerializeField]
    private TextMeshProUGUI troopNameText = null, haveTrainedAmountText = null;

    public void Initialize(Troop troop)
    {
        this.troop = troop;
        troopNameText.text = troop.troopName;

        switch (troop.troopType)
        {
            case Troop.TroopType.Infantry:
                haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.infantryAmount;
                break;
            case Troop.TroopType.Cavalry:
                haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.cavalryAmount;
                break;
            case Troop.TroopType.Spear:
                haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.spearAmount;
                break;
            case Troop.TroopType.Archer:
                haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.archerAmount;
                break;
            default:
                break;
        }

        if (troop.availabilityState == Troop.AvailabilityState.Locked)
        {
            troopImg.color = Color.grey;
            trainButton.interactable = false;
            GameEvents.OnTroopUnlocked += UnlockedTroop;
        }
    }

    public void UnlockedTroop(Troop troop)
    {
        if (this.troop == troop)
        {
            troopImg.color = Color.white;
            trainButton.interactable = true;
            GameEvents.OnTroopUnlocked -= UnlockedTroop;
        }
    }

    public void OnTrainButton(TMP_InputField amountToTrain)
    {
        if (troop.availabilityState == Troop.AvailabilityState.Unlocked && CanTrain())
        {
            troop.trainedTime = 0.0f;

            switch (troop.troopType)
            {
                case Troop.TroopType.Infantry:
                    MilitaryManager.Instance.infantryAmount += int.Parse(amountToTrain.text);
                    haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.infantryAmount.ToString();
                    break;
                case Troop.TroopType.Cavalry:
                    MilitaryManager.Instance.cavalryAmount += int.Parse(amountToTrain.text);
                    haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.cavalryAmount.ToString();
                    break;
                case Troop.TroopType.Spear:
                    MilitaryManager.Instance.spearAmount += int.Parse(amountToTrain.text);
                    haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.spearAmount.ToString();
                    break;
                case Troop.TroopType.Archer:
                    MilitaryManager.Instance.archerAmount += int.Parse(amountToTrain.text);
                    haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.archerAmount.ToString();
                    break;
                default:
                    break;
            }
        }
    }

    public bool CanTrain()
    {
        bool hasEnoughResources = false;
        List<ResourceAmount> resourceAmounts = new List<ResourceAmount>();
        for (int i = 0; i < troop.resourceCosts.Count; i++)
        {
            ResourceAmount currentAmount = Resource.Instance.GetResourceAmount(troop.resourceCosts[i].resourceType);
            ResourceAmount cost = troop.resourceCosts[i];
            if (currentAmount.resourceAmount >= cost.resourceAmount)
            {
                resourceAmounts.Add(currentAmount);
            }
        }
        if (resourceAmounts.Count >= troop.resourceCosts.Count)
        {
            hasEnoughResources = true;
            // resourceAmounts has the same order as resourceCosts
            for (int i = 0; i < troop.resourceCosts.Count; i++)
            {
                resourceAmounts[i].resourceAmount -= troop.resourceCosts[i].resourceAmount;
            }
            troop.availabilityState = Troop.AvailabilityState.Training;
            GameEvents.TrainingStarted(troop);
            GameEvents.OnTimePassed += troop.CheckTrainingTime;
        }
        return hasEnoughResources;
    }
}
