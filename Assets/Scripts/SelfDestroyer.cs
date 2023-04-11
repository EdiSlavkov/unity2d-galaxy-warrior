using System.Collections;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    private void Awake()
    {
        StartCoroutine(SelfDestroy());
    }

    private IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}