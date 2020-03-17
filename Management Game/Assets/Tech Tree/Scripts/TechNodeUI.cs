using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class TechNodeUI : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField]
    private Image techImg = null;
    [SerializeField]
    private TextMeshProUGUI techTitleText = null;
    [HideInInspector]
    public Technology technology = null;
    [SerializeField]
    private Image nodeImg = null;
    [SerializeField]
    private TechNodeConnector connector = null;
    [SerializeField]
    private Outline outline = null;

    [Header("Timer")]
    [SerializeField]
    private GameObject timerContainer = null;
    [SerializeField]
    private Image timerBar = null;
    private CountdownTimer countdown;

    public void Initialize(Technology tech)
    {
        countdown = GetComponent<CountdownTimer>();

        technology = tech;
        techTitleText.text = tech.techName;
        techImg.sprite = tech.techImage;
        outline.effectColor = tech.techUIColor;

        if(tech.availabilityState == Technology.AvailabilityState.Locked)
        {
            nodeImg.color = Color.grey;
            GameEvents.OnTechUnlocked += UnlockedTechnology;
        }
        GetComponent<Button>().onClick.AddListener(TryResearching);
    }

    public void TryResearching()
    {
        if(technology.availabilityState == Technology.AvailabilityState.Unlocked && technology.StartResearching())
        {
            technology.researchedTime = 0.0f;
            nodeImg.color = Color.blue;

            StartTimerBar();

            GameEvents.OnTechResearchCompleted += CompletedResearch;
        }
    }

    public void Connect(List<TechNodeUI> nodes, Transform parent)
    {
        foreach (RequiredTech tech in technology.techRequirements)
        {
            TechNodeUI connectedNode = nodes.Find(t => t.technology.techName == tech.techName);
            TechNodeConnector nodeConnector = Instantiate(connector, parent);
            nodeConnector.MakeConnections(transform.position, connectedNode.transform.position, connectedNode.technology.techUIColor);
        }
    }

    public void CompletedResearch(Technology tech)
    {
        nodeImg.color = Color.green;
        GameEvents.OnTechResearchCompleted -= CompletedResearch;

    }

    public void UnlockedTechnology(Technology tech)
    {
        if (tech == technology)
        {
            nodeImg.color = Color.yellow;
            GameEvents.OnTechUnlocked -= UnlockedTechnology;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TechToolTip.Instance.Show(this.technology); 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TechToolTip.Instance.Hide();
    }

    void StartTimerBar()
    {
        timerContainer.SetActive(true);

        timerBar.fillAmount = technology.researchedTime / technology.requiredResearchTime;

        float timeLeft = technology.requiredResearchTime;
        float timeDone = technology.researchedTime;
        countdown.StartTimer(timeLeft, timeDone);

        GameEvents.OnTechResearchCompleted += StopTimerBar;
    }

    void StopTimerBar(Technology tech)
    {
        timerContainer.SetActive(false);

        //trainButton.interactable = true;

        GameEvents.OnTechResearchCompleted -= StopTimerBar;
    }
}
