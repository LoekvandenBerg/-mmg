using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntReward : MonoBehaviour
{

    public void GainHuntRewards(Rarity.RarityType rarity)
    {
        List<ResourceAmount> resourceLootList = CalculateRandomResourceLoot(rarity);

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

    public List<ResourceAmount> CalculateRandomResourceLoot(Rarity.RarityType rarity)
    {
        List<ResourceAmount> resourceLootList = new List<ResourceAmount>();

        int goldAmount = 0;
        int foodAmount = 0;
        int woodAmount = 0;
        int waterAmount = 0;
        int steelAmount = 0;

        switch (rarity)
        {
            case Rarity.RarityType.Common:
                goldAmount = Random.Range(25, 51);
                foodAmount = Random.Range(25, 51);
                steelAmount = Random.Range(25, 51);
                woodAmount = Random.Range(25, 51);
                waterAmount = Random.Range(25, 51);
                break;
            case Rarity.RarityType.Uncommon:
                goldAmount = Random.Range(50, 101);
                foodAmount = Random.Range(50, 101);
                steelAmount = Random.Range(50, 101);
                woodAmount = Random.Range(50, 101);
                waterAmount = Random.Range(50, 101);
                break;
            case Rarity.RarityType.Rare:
                goldAmount = Random.Range(100, 151);
                foodAmount = Random.Range(100, 151);
                steelAmount = Random.Range(100, 151);
                woodAmount = Random.Range(100, 151);
                waterAmount = Random.Range(100, 151);
                break;
            case Rarity.RarityType.Epic:
                goldAmount = Random.Range(150, 201);
                foodAmount = Random.Range(150, 201);
                steelAmount = Random.Range(150, 201);
                woodAmount = Random.Range(150, 201);
                waterAmount = Random.Range(150, 201);
                break;
            case Rarity.RarityType.Legendary:
                goldAmount = Random.Range(200, 251);
                foodAmount = Random.Range(200, 251);
                steelAmount = Random.Range(200, 251);
                woodAmount = Random.Range(200, 251);
                waterAmount = Random.Range(200, 251);
                break;
            default:
                break;
        }


        ResourceAmount goldLoot = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold);
        goldLoot.resourceAmount = goldAmount;
        ResourceAmount foodLoot = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Food);
        goldLoot.resourceAmount = foodAmount;
        ResourceAmount steelLoot = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Gold);
        goldLoot.resourceAmount = steelAmount;
        ResourceAmount woodLoot = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Wood);
        goldLoot.resourceAmount = woodAmount;
        ResourceAmount waterLoot = Resource.Instance.GetResourceAmount(Resource.ResourceTypes.Water);
        goldLoot.resourceAmount = waterAmount;

        resourceLootList.Add(goldLoot);
        resourceLootList.Add(foodLoot);
        resourceLootList.Add(woodLoot);
        resourceLootList.Add(waterLoot);
        resourceLootList.Add(steelLoot);

        return resourceLootList;
    }
}
