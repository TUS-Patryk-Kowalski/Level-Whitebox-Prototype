using UnityEngine;

public class BobbingMotion : MonoBehaviour
{
    public float amplitude = 0.25f;
    public float frequency = 0.5f;

    private float randomFrequencyOffset;
    private Vector3 startPosition;

    void Start()
    {
        StartupActions();
    }

    void Update()
    {
        CalculateAndApplyMotions();
    }

    private void StartupActions()
    {
        startPosition = transform.position;
        randomFrequencyOffset = Random.Range(-0.2f, 0.2f);
    }

    private void CalculateAndApplyMotions()
    {
        // Calculate offset
        Vector3 tempPos = startPosition;
        tempPos.y += Mathf.Sin((Time.fixedTime + randomFrequencyOffset) * Mathf.PI * frequency) * amplitude;

        // Apply offset
        transform.position = tempPos;
    }
}