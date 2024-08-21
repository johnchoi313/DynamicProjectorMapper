using UnityEngine;

public class DynamicQuadDeformer : MonoBehaviour
{
    public GameObject reference;

    public string prepend = "1";

    public Transform pointYellow;
    public Transform pointGreen;
    public Transform pointBlue;
    public Transform pointRed;

    Mesh mesh;
    Vector3[] vertices;

    public Texture texture;
    public float speed = 1f;

    public Shader shader;

    void Start()
    {
        reference.SetActive(false);
        LoadPointPositions(prepend);

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

        // Changing  shader
        GetComponent<Renderer>().material.shader = shader;

        // Assign texture
        if (texture != null) { GetComponent<Renderer>().material.mainTexture = texture; }
    }

    void Update()
    {
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetPointPositions();
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application ending after " + Time.time + " seconds");
        SavePointPositions(prepend);

    }

    public void ResetPointPositions()
    {
        pointYellow.localPosition = new Vector3(0.5f, 0.5f, 0f);
        pointGreen.localPosition = new Vector3(-0.5f, -0.5f, 0f);
        pointBlue.localPosition = new Vector3(0.5f, -0.5f, 0f);
        pointRed.localPosition = new Vector3(-0.5f, 0.5f, 0f);
    }

    public void SavePointPositions(string prepend)
    {
        SavePointPosition(pointYellow.gameObject, prepend);
        SavePointPosition(pointGreen.gameObject, prepend);
        SavePointPosition(pointBlue.gameObject, prepend);
        SavePointPosition(pointRed.gameObject, prepend);
    }
    void SavePointPosition(GameObject obj, string prepend)
    {
        PlayerPrefs.SetFloat(prepend + "." + obj.name + ".position.x", obj.transform.position.x);
        PlayerPrefs.SetFloat(prepend + "." + obj.name + ".position.y", obj.transform.position.y);
        PlayerPrefs.SetFloat(prepend + "." + obj.name + ".position.z", obj.transform.position.z);
    }

    public void LoadPointPositions(string prepend)
    {
        LoadPointPosition(pointYellow.gameObject, prepend);
        LoadPointPosition(pointGreen.gameObject, prepend);
        LoadPointPosition(pointBlue.gameObject, prepend);
        LoadPointPosition(pointRed.gameObject, prepend);
    }
    void LoadPointPosition(GameObject obj, string prepend)
    {
        float x = PlayerPrefs.GetFloat(prepend + "." + obj.name + ".position.x", obj.transform.position.x);
        float y = PlayerPrefs.GetFloat(prepend + "." + obj.name + ".position.y", obj.transform.position.y);
        float z = PlayerPrefs.GetFloat(prepend + "." + obj.name + ".position.z", obj.transform.position.z);
        obj.transform.position = new Vector3(x, y, z);
    }

}
