using UnityEngine;

public class ValueMapper : MonoBehaviour
{
    
    // Map a float value from one range to another
    public static float Map(float value, float fromMin, float fromMax, float toMin, float toMax, bool clampValues = true)
    {
        // Clamp the value if the flag is set
        if (clampValues)
        {
            value = Mathf.Clamp(value, fromMin, fromMax);
        }

        // Normalize the value to 0-1 range
        float normalizedValue = Mathf.InverseLerp(fromMin, fromMax, value);

        // Map the normalized value to the new range
        return Mathf.Lerp(toMin, toMax, normalizedValue);
    }

    // Map a Vector2 from one range to another
    public static Vector2 Map(Vector2 value, Vector2 fromMin, Vector2 fromMax, Vector2 toMin, Vector2 toMax, bool clampValues = true)
    {
        float x = Map(value.x, fromMin.x, fromMax.x, toMin.x, toMax.x, clampValues);
        float y = Map(value.y, fromMin.y, fromMax.y, toMin.y, toMax.y, clampValues);
        return new Vector2(x, y);
    }

    // Map a Vector3 from one range to another
    public static Vector3 Map(Vector3 value, Vector3 fromMin, Vector3 fromMax, Vector3 toMin, Vector3 toMax, bool clampValues = true)
    {
        float x = Map(value.x, fromMin.x, fromMax.x, toMin.x, toMax.x, clampValues);
        float y = Map(value.y, fromMin.y, fromMax.y, toMin.y, toMax.y, clampValues);
        float z = Map(value.z, fromMin.z, fromMax.z, toMin.z, toMax.z, clampValues);
        return new Vector3(x, y, z);
    }

    // Example usage in the Unity Editor
    void Start()
    {
        // Example float mapping
        float originalFloat = 15f;
        float mappedFloat = Map(originalFloat, 0f, 10f, -1f, 1f);
        Debug.Log("Mapped Float: " + mappedFloat);

        // Example Vector2 mapping
        Vector2 originalVector2 = new Vector2(12f, 8f);
        Vector2 mappedVector2 = Map(originalVector2, new Vector2(0f, 0f), new Vector2(10f, 10f), new Vector2(-1f, -1f), new Vector2(1f, 1f));
        Debug.Log("Mapped Vector2: " + mappedVector2);

        // Example Vector3 mapping
        Vector3 originalVector3 = new Vector3(5f, 15f, 25f);
        Vector3 mappedVector3 = Map(originalVector3, new Vector3(0f, 10f, 20f), new Vector3(10f, 20f, 30f), new Vector3(-1f, -2f, -3f), new Vector3(1f, 2f, 3f));
        Debug.Log("Mapped Vector3: " + mappedVector3);
    }
}