using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedObject
{
    public GameObject gameObject;
    public float weight;
}

public class RandomSelectorWeighted : MonoBehaviour
{
    private float totalWeight = 0;

    public List<WeightedObject> weightedObjects;

    public static RandomSelectorWeighted Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (var item in weightedObjects)
        {
            totalWeight += item.weight;
        }
    }

    public GameObject GetRandomObject()
    {
        float weight = Random.Range(0, totalWeight);
        float currentWeight = 0;
        foreach (var item in weightedObjects)
        {
            currentWeight += item.weight;
            if (weight < currentWeight)
            {
                return item.gameObject;
            }
        }

        return null;
    }
}
