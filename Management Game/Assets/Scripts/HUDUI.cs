
#pragma warning disable 0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDUI : MonoBehaviour
{
    public TextMeshProUGUI goldText, waterText, foodText, woodText;
    public TextMeshProUGUI HUDText;
    private bool currentToggle;
    [SerializeField]
    private Button[] unusedButtons;

    // Update is called once per frame
    void Start()
    {
        InvokeRepeating("UpdateResourceUI", 1.0f, 2.0f);
    }

    public void UpdateResourceUI()
    {
        goldText.text = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold).resourceAmount.ToString();
        foodText.text = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Food).resourceAmount.ToString();
        waterText.text = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Water).resourceAmount.ToString();
        woodText.text = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Wood).resourceAmount.ToString();
    }

    public void OnUnImplementedButton()
    {
        StartCoroutine("HUDNotImplementedCoRoutine");
    }

    IEnumerator HUDNotImplementedCoRoutine()
    {
        currentToggle = !currentToggle;
        HUDText.transform.gameObject.SetActive(currentToggle);
        HUDText.text = "This is not yet implemented";

        yield return new WaitForSeconds(5.0f);

        currentToggle = !currentToggle;
        HUDText.transform.gameObject.SetActive(currentToggle);
    }
}
