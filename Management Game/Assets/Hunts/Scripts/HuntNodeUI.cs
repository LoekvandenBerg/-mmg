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

    [Header("Timer")]
    [SerializeField]
    private GameObject timerContainer = null;
    [SerializeField]
    private Image timerBar = null;
    private CountdownTimer countdown;

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
        GameEvents.OnHuntCompleted -= CompletedHunt;
        Destroy(gameObject, 1.5f);
    }

    public void GainHuntRewards()
    {
        List<ResourceAmount> resourceLootList = CalculateRandomResourceLoot();

        for (int i = 0; i < resourceLootList.Count; i++)
        {
            switch (resourceLootList[i].resourceType)
            {
                case Resource.ResourceTypes.Gold:
                    Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold).resourceAmount += resourceLootList[i].resourceAmount;
                    break;
                case Resource.ResourceTypes.Steel:
                    Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Steel).resourceAmount += resourceLootList[i].resourceAmount;
                    break;
                case Resource.ResourceTypes.Wood:
                    Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Water).resourceAmount += resourceLootList[i].resourceAmount;
                    break;
                case Resource.ResourceTypes.Water:
                    Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Food).resourceAmount += resourceLootList[i].resourceAmount;
                    break;
                case Resource.ResourceTypes.Food:
                    Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Wood).resourceAmount += resourceLootList[i].resourceAmount;
                    break;
                default:
                    break;
            }
        }
    }

    public List<ResourceAmount> CalculateRandomResourceLoot()
    {
        List<ResourceAmount> resourceLootList = new List<ResourceAmount>();

        int goldAmount = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold).resourceAmount + Random.Range(25, 101);
        int foodAmount = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold).resourceAmount + Random.Range(25, 101);
        //int steelAmount = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold).resourceAmount + Random.Range(25, 101);
        int woodAmount = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold).resourceAmount + Random.Range(25, 101);
        int waterAmount = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold).resourceAmount + Random.Range(25, 101);

        ResourceAmount goldLoot = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold);
        goldLoot.resourceAmount = goldAmount;
        ResourceAmount foodLoot = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Food);
        goldLoot.resourceAmount = foodAmount;
        //ResourceAmount steelLoot = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold);
        //goldLoot.resourceAmount = steelAmount;
        ResourceAmount woodLoot = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Wood);
        goldLoot.resourceAmount = woodAmount;
        ResourceAmount waterLoot = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Water);
        goldLoot.resourceAmount = waterAmount;

        resourceLootList.Add(goldLoot);
        resourceLootList.Add(foodLoot);
        resourceLootList.Add(woodLoot);
        resourceLootList.Add(waterLoot);
        //resourceLootList.Add(steelLoot);

        return resourceLootList;
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
