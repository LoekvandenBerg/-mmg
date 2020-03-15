using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechNodeConnector : MonoBehaviour
{
    private Image image;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponent<Image>();
    }

    public void MakeConnections(Vector3 fromPoint , Vector3 toPoint, Color color)
    {
        image.color = color;
        Vector3 centerPos = (fromPoint + toPoint) / 2;
        Vector3 dir = Vector3.Normalize(fromPoint - toPoint);
        transform.right = dir;
        transform.position = centerPos;
        transform.localScale = new Vector3(Vector3.Distance(fromPoint, toPoint)/10, 1, 1);
        transform.SetAsFirstSibling();
    }
}
