using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    private Vector3 originalPosition;

    private void Awake()
    {
        originalPosition = transform.position;
    }

    public IEnumerator Shake()
    {
        float timer = 0;
        while (duration > timer)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(offsetX, offsetY, originalPosition.z);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPosition;
    }
}