using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public enum ResourceTypes
    {
        Gold,
        Steel,
        Wood,
        Water,
        Food
    }

    public List<ResourceAmount> resourceAmounts;

    public static Resources Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    public ResourceAmount GetResourceAmount(ResourceTypes resource)
    {
        return resourceAmounts.Find(r => r.resourceType == resource);
    }
}

[System.Serializable]
public class ResourceAmount
{
    public Resources.ResourceTypes resourceType;
    public int resourceAmount;
}
