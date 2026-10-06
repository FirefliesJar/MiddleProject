using UnityEngine;

public class upanddown : MonoBehaviour
{
    public float speed = 1;
    public float height = 0.2f;

    Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * height;

        transform.position = startPos + new Vector3(0, y, 0);
    }
}