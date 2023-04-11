using UnityEngine;

public class ParralaxEffect : MonoBehaviour
{
    [SerializeField] private float speed;
    private Material material;

    private void Start()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        material.mainTextureOffset += new Vector2(0f, speed * Time.deltaTime);
    }
}
