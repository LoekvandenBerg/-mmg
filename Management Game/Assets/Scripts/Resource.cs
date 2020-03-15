using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public static Resource Instance { get; private set; }

    public enum ResourceTypes
    {
        Gold,
        Steel,
        Wood,
        Water,
        Food
    }

    public List<ResourceAmount> resourceAmounts;

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
    public Resource.ResourceTypes resourceType;
    public int resourceAmount;
}
