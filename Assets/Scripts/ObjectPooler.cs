using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public GameObject prefab;
    public int amount;

    [HideInInspector]
    public List<GameObject> items;
}

public class ObjectPooler : MonoBehaviour
{
    public List<PoolItem> pool;

    Dictionary<GameObject, PoolItem> poolItems;

    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        poolItems = new Dictionary<GameObject, PoolItem>();

        foreach (var item in pool)
        {
            for (int i = 0; i < item.amount; i++)
            {
                item.items.Add(Instantiate(item.prefab));
                item.items[i].SetActive(false);
            }

            poolItems.Add(item.prefab, item);
        }
    }

    public GameObject GetPooledItem(GameObject gameObject)
    {
        PoolItem poolItem = poolItems[gameObject];
        for (int i = 0; i < poolItem.items.Count; i++)
        {
            if (!poolItem.items[i].activeInHierarchy)
            {
                poolItem.items[i].SetActive(true);
                return poolItem.items[i];
            }
        }

        return null;
    }


}
