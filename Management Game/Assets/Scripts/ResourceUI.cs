
#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI goldText, waterText, foodText, woodText;


    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("UpdateResourceUI", 1.0f, 2.0f);
    }

    public void UpdateResourceUI()
    {
        goldText.text = Resources.Instance.GetResourceAmount(Resources.ResourceTypes.Gold).resourceAmount.ToString();
        foodText.text = Resources.Instance.GetResourceAmount(Resources.ResourceTypes.Food).resourceAmount.ToString();
        waterText.text = Resources.Instance.GetResourceAmount(Resources.ResourceTypes.Water).resourceAmount.ToString();
        woodText.text = Resources.Instance.GetResourceAmount(Resources.ResourceTypes.Wood).resourceAmount.ToString();
    }
}
