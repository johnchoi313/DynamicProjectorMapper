using UnityEngine;
using UnityEditor;
using System.Collections.Generic;  // Import required for List

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    Player player;
    // Variables to hold the references for Transforms and GameObjects
    private GameObject gameObjectSlot1;
    private GameObject gameObjectSlot2;

    // Variables for length in meters and feet
    private float lengthInMeters = 1f;
    private float lengthInFeet = 3.28084f;  // 1 meter = 3.28084 feet

    // Default Vector3 variable
    private Vector3 defaultVector3 = Vector3.zero;

    // List of Vector3s
    private List<Vector3> vector3List = new List<Vector3>();

    // Boolean toggles for flipping
    private bool flipX = false;
    private bool flipY = false;
    private bool flipZ = false;

    public override void OnInspectorGUI()
    {
        // Get the target Player instance
        player = (Player)target;

        // Title for the cube buttons
        GUILayout.Label("Colored Cube Corner Buttons", EditorStyles.boldLabel);
        GUILayout.Space(10);  // Add spacing after the header

        // Add slots for Transforms
        GUILayout.Label("Transform Slots", EditorStyles.boldLabel);
        player.transformSlot1 = (Transform)EditorGUILayout.ObjectField("Transform Slot 1", player.transformSlot1, typeof(Transform), true);
        player.transformSlot2 = (Transform)EditorGUILayout.ObjectField("Transform Slot 2", player.transformSlot2, typeof(Transform), true);
        GUILayout.Space(10);  // Add spacing after the section

        // Add slots for GameObjects
        GUILayout.Label("GameObject Slots", EditorStyles.boldLabel);
        gameObjectSlot1 = (GameObject)EditorGUILayout.ObjectField("GameObject Slot 1", gameObjectSlot1, typeof(GameObject), true);
        gameObjectSlot2 = (GameObject)EditorGUILayout.ObjectField("GameObject Slot 2", gameObjectSlot2, typeof(GameObject), true);
        GUILayout.Space(10);  // Add spacing after the section

        // Length field (Linked meters and feet)
        GUILayout.Label("Length Conversion (Meters and Feet)", EditorStyles.boldLabel);

        // Start a horizontal layout for length fields
        GUILayout.BeginHorizontal(); 

        // Meters field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the meters field
        lengthInMeters = EditorGUILayout.FloatField("Length in Meters", lengthInMeters);
        if (EditorGUI.EndChangeCheck())  // If meters field was changed, update the feet field
        {
            lengthInFeet = lengthInMeters * 3.28084f;
        }

        // Feet field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the feet field
        lengthInFeet = EditorGUILayout.FloatField("Length in Feet", lengthInFeet);
        if (EditorGUI.EndChangeCheck())  // If feet field was changed, update the meters field
        {
            lengthInMeters = lengthInFeet / 3.28084f;
        }

        // End the horizontal layout
        GUILayout.EndHorizontal();  
        GUILayout.Space(10);  // Add spacing after the section

        // Default Vector3 slot
        GUILayout.Label("Default Vector3 Slot", EditorStyles.boldLabel);
        defaultVector3 = EditorGUILayout.Vector3Field("Default Vector3", defaultVector3);
        GUILayout.Space(10);  // Add spacing after the section

        // List of Vector3s
        GUILayout.Label("List of Vector3s", EditorStyles.boldLabel);

        // Display each Vector3 in the list with a field and remove button
        for (int i = 0; i < vector3List.Count; i++)
        {
            GUILayout.BeginHorizontal();
            vector3List[i] = EditorGUILayout.Vector3Field("Vector3 " + i, vector3List[i]);
            if (GUILayout.Button("Remove", GUILayout.Width(70)))
            {
                vector3List.RemoveAt(i);
                break;  // To avoid modifying the list while iterating
            }
            GUILayout.EndHorizontal();
        }

        // Button to add a new Vector3 to the list
        if (GUILayout.Button("Add New Vector3"))
        {
            vector3List.Add(Vector3.zero);  // Add a default zero Vector3
        }
        GUILayout.Space(10);  // Add spacing after the section

        // Boolean toggles for flipping on the same horizontal line
        GUILayout.Label("Flip Options", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();  // Start horizontal layout

        GUILayout.Space(10);  // Add space before the first toggle (Flip X)
        flipX = EditorGUILayout.Toggle("Flip X", flipX);

        // Add spacing between toggles
        GUILayout.Space(10);  // Space between Flip X and Flip Y
        flipY = EditorGUILayout.Toggle("Flip Y", flipY);

        GUILayout.Space(10);  // Space between Flip Y and Flip Z
        flipZ = EditorGUILayout.Toggle("Flip Z", flipZ);

        GUILayout.EndHorizontal();  // End horizontal layout
        GUILayout.Space(10);  // Add spacing after the section

        // First row of buttons (top corners of the cube)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.red;  // Color for Corner 1
        if (GUILayout.Button("Corner 1"))
        {
            Debug.Log("Corner 1 clicked");
        }

        GUI.backgroundColor = Color.green;  // Color for Corner 2
        if (GUILayout.Button("Corner 2"))
        {
            Debug.Log("Corner 2 clicked");
        }
        GUILayout.EndHorizontal();

        // Second row of buttons (middle level - represent space between top and bottom)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.blue;  // Color for Corner 3
        if (GUILayout.Button("Corner 3"))
        {
            Debug.Log("Corner 3 clicked");
        }

        GUI.backgroundColor = Color.yellow;  // Color for Corner 4
        if (GUILayout.Button("Corner 4"))
        {
            Debug.Log("Corner 4 clicked");
        }
        GUILayout.EndHorizontal();

        // Third row of buttons (bottom corners of the cube)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.cyan;  // Color for Corner 5
        if (GUILayout.Button("Corner 5"))
        {
            Debug.Log("Corner 5 clicked");
        }

        GUI.backgroundColor = Color.magenta;  // Color for Corner 6
        if (GUILayout.Button("Corner 6"))
        {
            Debug.Log("Corner 6 clicked");
        }
        GUILayout.EndHorizontal();

        // Fourth row of buttons (final bottom corners of the cube)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.grey;  // Color for Corner 7
        if (GUILayout.Button("Corner 7"))
        {
            Debug.Log("Corner 7 clicked");
        }

        GUI.backgroundColor = Color.white;  // Color for Corner 8
        if (GUILayout.Button("Corner 8"))
        {
            Debug.Log("Corner 8 clicked");
        }
        GUILayout.EndHorizontal();

        // Reset the GUI background color to default
        GUI.backgroundColor = Color.white;

        // Display selected Transform and GameObject information
        if (player.transformSlot1 != null)
        {
            GUILayout.Label("Transform Slot 1 is set to: " + player.transformSlot1.name);
        }

        if (player.transformSlot2 != null)
        {
            GUILayout.Label("Transform Slot 2 is set to: " + player.transformSlot2.name);
        }

        if (gameObjectSlot1 != null)
        {
            GUILayout.Label("GameObject Slot 1 is set to: " + gameObjectSlot1.name);
        }

        if (gameObjectSlot2 != null)
        {
            GUILayout.Label("GameObject Slot 2 is set to: " + gameObjectSlot2.name);
        }

        // Display the current Vector3 value
        GUILayout.Label("Current Vector3: " + defaultVector3.ToString());
    }

    // Draw the wireframe line between the two transforms in the Scene view
    private void OnSceneGUI()
    {
        // Check if both transforms are assigned
        if (player.transformSlot1 != null && player.transformSlot2 != null)
        {
            // Set the color of the line
            Handles.color = Color.yellow;

            // Draw a line between the two transforms
            Handles.DrawLine(player.transformSlot1.position, player.transformSlot2.position);
        }
    }
}
