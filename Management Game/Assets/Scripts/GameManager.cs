using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private float gainRate = 2.0f;
    [SerializeField]
    private int goldToGet, waterToGet, foodToGet, woodToGet, steelToGet;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.OnTechResearchCompleted += CalculateResourceTechModifiers;
        GameEvents.OnBuildingCompleted += CalculateProductionAmount;

        InvokeRepeating("GainResources", 1.0f, gainRate);
    }

    void GainResources()
    {
        Resources.Instance.GetResourceAmount(Resources.ResourceTypes.Gold).resourceAmount += (1 + goldToGet);
        Resources.Instance.GetResourceAmount(Resources.ResourceTypes.Water).resourceAmount += (2 + waterToGet);
        Resources.Instance.GetResourceAmount(Resources.ResourceTypes.Food).resourceAmount += (3 + foodToGet);
        Resources.Instance.GetResourceAmount(Resources.ResourceTypes.Wood).resourceAmount += (1 + woodToGet);
    }

    void CalculateResourceTechModifiers(Technology tech)
    {
        foreach (TechModifier mod in tech.techModifiers)
        {
            switch (mod.modifierType)
            {
                case TechModifier.ModifierType.Gold:
                    goldToGet += mod.amount;
                    break;
                case TechModifier.ModifierType.Wood:
                    woodToGet += mod.amount;
                    break;
                case TechModifier.ModifierType.Water:
                    waterToGet += mod.amount;
                    break;
                case TechModifier.ModifierType.Steel:
                    break;
                case TechModifier.ModifierType.Population:
                    break;
                case TechModifier.ModifierType.Food:
                    foodToGet += mod.amount;
                    break;
                default:
                    break;
            }
        }
    }


    void CalculateProductionAmount(Building building)
    {
        foreach(ProductionModifiers mod in building.productionModifiers)
        {
            if(building.level > 0)
            {
                switch (mod.modifierType)
                {
                    case ProductionModifiers.ModifierType.Wood:
                        mod.amount += building.initProductionAmount + (building.level + (building.level * 2));
                        woodToGet += mod.amount;
                        break;
                    case ProductionModifiers.ModifierType.Food:
                        mod.amount += building.initProductionAmount + (building.level + (building.level * 2));
                        foodToGet += mod.amount;
                        break;
                    case ProductionModifiers.ModifierType.Steel:
                        mod.amount += building.initProductionAmount + (building.level + (building.level * 2));
                        steelToGet += mod.amount;
                        break;
                    case ProductionModifiers.ModifierType.Gold:
                        mod.amount += building.initProductionAmount + (building.level + (building.level * 2));
                        goldToGet += mod.amount;
                        break;
                    case ProductionModifiers.ModifierType.Water:
                        mod.amount += building.initProductionAmount + (building.level + (building.level * 2));
                        waterToGet += mod.amount;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
