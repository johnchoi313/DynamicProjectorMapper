using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OptitrackProjectorTranslator))]
public class OptitrackProjectorTranslatorEditor : Editor
{
    OptitrackProjectorTranslator opt;
    

    public override void OnInspectorGUI() {

        // Get the target Player instance
        opt = (OptitrackProjectorTranslator)target;

        // Add slots for Corners
        GUILayout.Label("Corners", EditorStyles.boldLabel);
        opt.corner1 = (Transform)EditorGUILayout.ObjectField("Corner 1 [-1, -1, -1] (Left Lower Front)",  opt.corner1, typeof(Transform), true);
        opt.corner2 = (Transform)EditorGUILayout.ObjectField("Corner 2 [-1, -1,  1] (Left Lower Back)",   opt.corner2, typeof(Transform), true);
        opt.corner3 = (Transform)EditorGUILayout.ObjectField("Corner 3 [-1,  1, -1] (Left Upper Front)",  opt.corner3, typeof(Transform), true);
        opt.corner4 = (Transform)EditorGUILayout.ObjectField("Corner 4 [-1,  1,  1] (Left Upper Back)",   opt.corner4, typeof(Transform), true);
        opt.corner5 = (Transform)EditorGUILayout.ObjectField("Corner 5 [ 1, -1, -1] (Right Lower Front)", opt.corner5, typeof(Transform), true);
        opt.corner6 = (Transform)EditorGUILayout.ObjectField("Corner 6 [ 1, -1,  1] (Right Lower Back)",  opt.corner6, typeof(Transform), true);
        opt.corner7 = (Transform)EditorGUILayout.ObjectField("Corner 7 [ 1,  1, -1] (Right Upper Front)", opt.corner7, typeof(Transform), true);
        opt.corner8 = (Transform)EditorGUILayout.ObjectField("Corner 8 [ 1,  1,  1] (Right Upper Back)",  opt.corner8, typeof(Transform), true);
        GUILayout.Space(10);  // Add spacing after the section

        //

        // First row of buttons (top corners of the cube)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.red;  // Color for Corner 1
        if (GUILayout.Button("Corner 3")) {
            Debug.Log("Corner 3 clicked");
        }

        GUI.backgroundColor = Color.yellow;  // Color for Corner 2
        if (GUILayout.Button("Corner 7")) {
            Debug.Log("Corner 7 clicked");
        }
        GUILayout.EndHorizontal();

        // Second row of buttons (middle level - represent space between top and bottom)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;  // Color for Corner 3
        if (GUILayout.Button("Corner 1")) {
            Debug.Log("Corner 1 clicked");
        }

        GUI.backgroundColor = Color.blue;  // Color for Corner 4
        if (GUILayout.Button("Corner 5")) {
            Debug.Log("Corner 5 clicked");
        }
        GUILayout.EndHorizontal();

        // Third row of buttons (bottom corners of the cube)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.magenta;  // Color for Corner 5
        if (GUILayout.Button("Corner 4")) {
            Debug.Log("Corner 4 clicked");
        }

        GUI.backgroundColor = Color.cyan;  // Color for Corner 6
        if (GUILayout.Button("Corner 8")) {
            Debug.Log("Corner 8 clicked");
        }
        GUILayout.EndHorizontal();

        // Fourth row of buttons (final bottom corners of the cube)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.grey;  // Color for Corner 7
        if (GUILayout.Button("Corner 2")) {
            Debug.Log("Corner 2 clicked");
        }

        GUI.backgroundColor = Color.white;  // Color for Corner 8
        if (GUILayout.Button("Corner 6")) {
            Debug.Log("Corner 6 clicked");
        }
        GUILayout.EndHorizontal();

        // Reset the GUI background color to default
        GUI.backgroundColor = Color.white;
    }

}