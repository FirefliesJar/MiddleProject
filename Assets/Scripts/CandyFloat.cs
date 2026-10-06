using UnityEngine;

public class CandyFloat : MonoBehaviour
{
    public float speed = 2f;
    public float height = 0.5f;

    float startY;

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        float newY = startY + Mathf.Sin(Time.time * speed) * height;

        transform.position = new Vector3(
            transform.position.x,
            newY,
            transform.position.z
        );
    }
}