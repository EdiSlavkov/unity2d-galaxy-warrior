using System.Collections.Generic;
using UnityEngine;

public class CollectablesDrop : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects;
    [Range(0, 1)]
    [SerializeField] private float chanceToDrop;

    public void DropCollectables()
    {
        float chance = Random.value;
        if (chanceToDrop >= chance)
        {
            GameObject obj = objects[Random.Range(0, objects.Count)];
            Instantiate(obj, transform.position, Quaternion.identity);
        }
    }
}