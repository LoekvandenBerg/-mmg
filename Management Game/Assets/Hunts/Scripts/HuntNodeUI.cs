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
    private Image huntImg = null;
    [SerializeField]
    private Button huntButton = null;
    [SerializeField]
    private TextMeshProUGUI requiredTroopAmountText = null, huntNameText = null;
    [SerializeField]
    private HuntReward huntReward;
    private Hunt.Rarity rarity;

    [Header("Timer")]
    [SerializeField]
    private GameObject timerContainer = null;
    [SerializeField]
    private Image timerBar = null;
    private CountdownTimer countdown;

    public void Initialize(Hunt hunt)
    {
        this.hunt = hunt;
        huntReward = GetComponent<HuntReward>();
        rarity = hunt.rarity;

        requiredTroopAmountText.text = string.Format("Needed: {0} {1}", hunt.neededArmyAmount, hunt.requiredTroopType.ToString());
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

    public void OnHuntButton()
    {
        if (hunt.availabilityState == Hunt.AvailabilityState.Unlocked && hunt.CanHunt())
        {
            hunt.huntedTime = 0.0f; 
            huntImg.color = Color.cyan;
            huntButton.interactable = false;
            GameEvents.OnHuntCompleted += CompletedHunt;
        }
    }

    public void CompletedHunt(Hunt hunt)
    {
        huntReward.GainHuntRewards(rarity);
        GameEvents.OnHuntCompleted -= CompletedHunt;
        Destroy(gameObject, 1.5f);
    }

    void StartTimerBar()
    {
        huntImg.gameObject.SetActive(false);
        timerContainer.SetActive(true);

        float timeLeft = hunt.requiredHuntTime;
        countdown.StartTimer(timeLeft);

        timerBar.fillAmount = hunt.huntedTime / hunt.requiredHuntTime;

        GameEvents.OnHuntCompleted += StopTimerBar;
    }

    void StopTimerBar(Hunt hunt)
    {
        huntImg.gameObject.SetActive(true);
        timerContainer.SetActive(false);

        huntButton.interactable = true;

        GameEvents.OnHuntCompleted -= StopTimerBar;
    }
}
