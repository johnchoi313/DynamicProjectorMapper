using UnityEngine;

public class DynamicQuadDeformer : MonoBehaviour
{
    public GameObject reference;

    public Transform pointYellow;
    public Transform pointGreen;
    public Transform pointBlue;
    public Transform pointRed;
    
    Mesh mesh;
    Vector3[] vertices;

    public Texture2D texture;
    public float speed = 1f;

    void Start() {
        reference.SetActive(false);
        
        // Create a new quad mesh
        gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        mesh = GetComponent<MeshFilter>().mesh;
        
        // Define vertices for the quad
        vertices = new Vector3[] {
            new Vector3(-0.5f, -0.5f, 0f),
            new Vector3(0.5f, -0.5f, 0f),
            new Vector3(-0.5f, 0.5f, 0f),
            new Vector3(0.5f, 0.5f, 0f)
        };
        mesh.vertices = vertices;

        // Define UVs for texture mapping
        mesh.uv = new Vector2[] {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(0, 1),
            new Vector2(1, 1)
        };

        // Define triangles
        mesh.triangles = new int[] { 0, 2, 1, 2, 3, 1 };

        // Assign texture
        if (texture != null) { GetComponent<Renderer>().material.mainTexture = texture; }
    }

    void Update() {
        // Update vertices positions in real-time
        //for (int i = 0; i < vertices.Length; i++) {
        //    vertices[i].x += Mathf.Sin(Time.time * speed) * 0.01f; // Example real-time movement
        //}

        vertices[3] = pointYellow.localPosition;
        vertices[0] = pointGreen.localPosition;
        vertices[1] = pointBlue.localPosition;
        vertices[2] = pointRed.localPosition;
        
        // Assign the updated vertices back to the mesh
        mesh.vertices = vertices;
    }
}
