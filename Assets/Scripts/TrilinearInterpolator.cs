using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrilinearInterpolator : MonoBehaviour
{
    // Define the corners of the source cube
    public Vector3[] sourceCubeCorners = new Vector3[8];

    // Define the corners of the destination cube
    public Vector3[] destinationCubeCorners = new Vector3[8];

    void Start()
    {
        // Example input coordinate
        Vector3 inputCoordinate = new Vector3(0.5f, 0.5f, 0.5f);

        // Perform trilinear interpolation
        Vector3 outputCoordinate = TrilinearInterpolate(inputCoordinate);
        Debug.Log($"Mapped Coordinate: {outputCoordinate}");
    }

    // Perform trilinear interpolation from source to destination cube
    public Vector3 TrilinearInterpolate(Vector3 input)
    {
        // Clamp the input coordinate if clamping is enabled
        input = ClampCoordinate(input, sourceCubeCorners);

        // Calculate interpolation factors for the input coordinate within the source cube
        Vector3 factors = GetInterpolationFactors(input, GetMinCorner(sourceCubeCorners), GetMaxCorner(sourceCubeCorners));

        // Interpolate the result using the destination cube corners
        return InterpolateCube(destinationCubeCorners, factors);
    }


    // Get interpolation factors for a given input coordinate
    private Vector3 GetInterpolationFactors(Vector3 input, Vector3 minCorner, Vector3 maxCorner)
    {
        float xd = 1-Mathf.InverseLerp(minCorner.x, maxCorner.x, input.x);
        float yd = Mathf.InverseLerp(minCorner.y, maxCorner.y, input.y);
        float zd = 1-Mathf.InverseLerp(minCorner.z, maxCorner.z, input.z);

        return new Vector3(xd, yd, zd);
    }

    // Perform trilinear interpolation within a cube given interpolation factors
    private Vector3 InterpolateCube(Vector3[] cubeCorners, Vector3 factors)
    {
        Vector3 p000 = cubeCorners[0]; //Left Lower Front
        Vector3 p001 = cubeCorners[1]; //Left Lower Back
        Vector3 p010 = cubeCorners[2]; //Left Upper Front
        Vector3 p011 = cubeCorners[3]; //Left Upper Back
        Vector3 p100 = cubeCorners[4]; //Right Lower Front
        Vector3 p101 = cubeCorners[5]; //Right Lower Back
        Vector3 p110 = cubeCorners[6]; //Right Upper Front
        Vector3 p111 = cubeCorners[7]; //Right Upper Back

        // Interpolate along the x-axis
        Vector3 c00 = Vector3.Lerp(p000, p100, factors.x);
        Vector3 c01 = Vector3.Lerp(p001, p101, factors.x);
        Vector3 c10 = Vector3.Lerp(p010, p110, factors.x);
        Vector3 c11 = Vector3.Lerp(p011, p111, factors.x);

        // Interpolate along the y-axis
        Vector3 c0 = Vector3.Lerp(c00, c10, factors.y);
        Vector3 c1 = Vector3.Lerp(c01, c11, factors.y);

        // Interpolate along the z-axis
        return Vector3.Lerp(c0, c1, factors.z);
    }

    // Clamp the input coordinate to be within the bounds of the source cube
    private Vector3 ClampCoordinate(Vector3 input, Vector3[] cubeCorners)
    {
        Vector3 minCorner = GetMinVector(cubeCorners);
        Vector3 maxCorner = GetMaxVector(cubeCorners);
        //Vector3 minCorner = GetMinCorner(cubeCorners);
        //Vector3 maxCorner = GetMaxCorner(cubeCorners);

        float clampedX = Mathf.Clamp(input.x, minCorner.x, maxCorner.x);
        float clampedY = Mathf.Clamp(input.y, minCorner.y, maxCorner.y);
        float clampedZ = Mathf.Clamp(input.z, minCorner.z, maxCorner.z);

        return new Vector3(clampedX, clampedY, clampedZ);
    }

    private Vector3 GetMinCorner(Vector3[] corners)
    {
        Vector3 minCorner = corners[0];
        foreach (var corner in corners)
        {
            minCorner = Vector3.Min(minCorner, corner);
        }
        return minCorner;
    }

    private Vector3 GetMaxCorner(Vector3[] corners)
    {
        Vector3 maxCorner = corners[0];
        foreach (var corner in corners)
        {
            maxCorner = Vector3.Max(maxCorner, corner);
        }
        return maxCorner;
    }

    // Function to get the minimum vector from an array of vectors
    public static Vector3 GetMinVector(Vector3[] vectors)
    {
        if (vectors == null || vectors.Length == 0)
        {
            Debug.LogError("The vector array is null or empty.");
            return Vector3.zero; // Return a default value or handle as needed
        }

        // Initialize minVector with the first vector in the array
        Vector3 minVector = vectors[0];

        // Iterate through the array to find the minimum vector
        foreach (var vector in vectors)
        {
            minVector.x = Mathf.Min(minVector.x, vector.x);
            minVector.y = Mathf.Min(minVector.y, vector.y);
            minVector.z = Mathf.Min(minVector.z, vector.z);
        }

        return minVector;
    }

    // Function to get the maximum vector from an array of vectors
    public static Vector3 GetMaxVector(Vector3[] vectors)
    {
        if (vectors == null || vectors.Length == 0)
        {
            Debug.LogError("The vector array is null or empty.");
            return Vector3.zero; // Return a default value or handle as needed
        }

        // Initialize maxVector with the first vector in the array
        Vector3 maxVector = vectors[0];

        // Iterate through the array to find the maximum vector
        foreach (var vector in vectors)
        {
            maxVector.x = Mathf.Max(maxVector.x, vector.x);
            maxVector.y = Mathf.Max(maxVector.y, vector.y);
            maxVector.z = Mathf.Max(maxVector.z, vector.z);
        }

        return maxVector;
    }

}