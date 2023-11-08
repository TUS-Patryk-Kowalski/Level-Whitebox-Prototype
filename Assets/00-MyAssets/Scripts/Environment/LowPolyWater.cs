using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class LowPolyWaves : MonoBehaviour
{
    public float primaryScale = 0.1f;
    public float secondaryScale = 0.05f;
    public float speed = 1.0f;
    public float primaryWaveHeight = 0.5f;
    public float secondaryWaveHeight = 0.25f;

    private Mesh mesh;
    private Vector3[] baseVertices;

    //---------------------------------------------------
    // UNITY FUNCTIONS
    //---------------------------------------------------

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
    }

    private void Update()
    {
        Vector3[] vertices = new Vector3[baseVertices.Length];
        float time = Time.time * speed;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];

            // Primary wave based on Perlin noise
            vertex.y += Mathf.PerlinNoise(vertex.x + time, vertex.z + Mathf.Sin(time * 0.1f)) * primaryScale * primaryWaveHeight;

            // Secondary wave for more variation
            vertex.y += Mathf.PerlinNoise(vertex.x + time * 0.5f, vertex.z + Mathf.Cos(time * 0.15f)) * secondaryScale * secondaryWaveHeight;

            // Additional sine wave for more randomness
            vertex.y += Mathf.Sin(time + vertex.x * vertex.z) * (primaryWaveHeight / 3);

            vertices[i] = vertex;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals(); // To update the mesh normals for proper lighting and shading
    }
}