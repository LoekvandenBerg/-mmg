using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class MilitaryNodeUI : MonoBehaviour
{
    [HideInInspector]
    public Troop troop = null;
    [SerializeField]
    private Image troopImg = null;
    [SerializeField]
    private Button trainButton = null;
    [SerializeField]
    private TextMeshProUGUI troopNameText = null, haveTrainedAmountText = null;
    [SerializeField]
    private GameObject inputContainer = null;
    private int amountTraining;

    [Header("Timer")]
    [SerializeField]
    private GameObject timerContainer = null;
    [SerializeField]
    private Image timerBar = null;
    private CountdownTimer countdown;


    public void Initialize(Troop troop)
    {
        countdown = GetComponent<CountdownTimer>();

        this.troop = troop;
        troopNameText.text = troop.troopName;

        timerBar.fillAmount = 0.0f;

        timerContainer.SetActive(false);

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
        amountTraining = int.Parse(amountToTrain.text);
        if (troop.availabilityState == Troop.AvailabilityState.Unlocked && troop.CanTrain(int.Parse(amountToTrain.text)))
        {
            troop.trainedTime = 0.0f;
            troop.requiredTrainTime *= int.Parse(amountToTrain.text);

            troopImg.color = Color.yellow;
            trainButton.interactable = false;

            StartTimerBar();

            GameEvents.OnTrainingCompleted += CompletedTraining;

        }
    }

    private void CompletedTraining(Troop troop)
    {
        switch (troop.troopType)
        {
            case Troop.TroopType.Infantry:
                MilitaryManager.Instance.infantryAmount += amountTraining;
                haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.infantryAmount.ToString();
                break;
            case Troop.TroopType.Cavalry:
                MilitaryManager.Instance.cavalryAmount += amountTraining;
                haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.cavalryAmount.ToString();
                break;
            case Troop.TroopType.Spear:
                MilitaryManager.Instance.spearAmount += amountTraining;
                haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.spearAmount.ToString();
                break;
            case Troop.TroopType.Archer:
                MilitaryManager.Instance.archerAmount += amountTraining;
                haveTrainedAmountText.text = "Trained: " + MilitaryManager.Instance.archerAmount.ToString();
                break;
            default:
                break;
        }

        GameEvents.OnTrainingCompleted -= CompletedTraining;
    }

    void StartTimerBar()
    {
        inputContainer.SetActive(false);
        timerContainer.SetActive(true);

        float timeLeft = troop.requiredTrainTime;
        countdown.StartTimer(timeLeft);

        timerBar.fillAmount = troop.trainedTime / troop.requiredTrainTime;

        GameEvents.OnTrainingCompleted += StopTimerBar;
    }

    void StopTimerBar(Troop troop)
    {
        inputContainer.SetActive(true);
        timerContainer.SetActive(false);

        trainButton.interactable = true;

        GameEvents.OnTrainingCompleted -= StopTimerBar;
    }
}
