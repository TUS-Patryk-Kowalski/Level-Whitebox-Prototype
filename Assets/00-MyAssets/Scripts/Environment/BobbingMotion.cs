using UnityEngine;

public class BobbingMotion : MonoBehaviour
{
    public float amplitude = 0.25f;
    public float frequency = 0.5f;

    private float randomFrequencyOffset;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        randomFrequencyOffset = Random.Range(-0.2f, 0.2f);
    }

    void Update()
    {
        Vector3 tempPos = startPosition;
        tempPos.y += Mathf.Sin((Time.fixedTime + randomFrequencyOffset) * Mathf.PI * frequency) * amplitude;

        transform.position = tempPos;
    }
}