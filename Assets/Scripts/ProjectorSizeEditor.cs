using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ProjectorSize))]
public class ProjectorSizeEditor : Editor
{
    ProjectorSize ps;

    // Start is called before the first frame update
    public override void OnInspectorGUI() {

        // Get the target Player instance
        ps = (ProjectorSize)target;

        // Length field (Linked meters and feet)
        GUILayout.Label("Projector Width and Height (Meters and Feet)", EditorStyles.boldLabel);
        ps.projectorPlane = (Transform)EditorGUILayout.ObjectField("Projector Plane ",  ps.projectorPlane, typeof(Transform), true);
        ps.projectorScaleFactor = EditorGUILayout.FloatField("Projector Scale Factor", ps.projectorScaleFactor);
        
        GUILayout.BeginHorizontal(); 
        // Meters field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the meters field
        ps.projectorWidth = EditorGUILayout.FloatField("Projector Width (m)", ps.projectorWidth);
        if (EditorGUI.EndChangeCheck()) { 
            ps.projectorWidthIn = ps.projectorWidth * 39.3701f; 
            ps.UpdatePlaneScale();
        }
        // Feet field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the feet field
        ps.projectorWidthIn = EditorGUILayout.FloatField("Projector Width (in)", ps.projectorWidthIn);
        if (EditorGUI.EndChangeCheck()) { 
            ps.projectorWidth = ps.projectorWidthIn / 39.3701f; 
            ps.UpdatePlaneScale();
        }
        // End the horizontal layout
        GUILayout.EndHorizontal();  

        GUILayout.BeginHorizontal(); 
        // Meters field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the meters field
        ps.projectorHeight = EditorGUILayout.FloatField("Projector Height (m)", ps.projectorHeight);
        if (EditorGUI.EndChangeCheck()) { 
            ps.projectorHeightIn = ps.projectorHeight * 39.3701f; 
            ps.UpdatePlaneScale();
        }
        // Feet field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the feet field
        ps.projectorHeightIn = EditorGUILayout.FloatField("Projector Height (in)", ps.projectorHeightIn);
        if (EditorGUI.EndChangeCheck()) { 
            ps.projectorHeight = ps.projectorHeightIn / 39.3701f; 
            ps.UpdatePlaneScale();
        }
        // End the horizontal layout
        GUILayout.EndHorizontal();  


        GUILayout.Space(10);  // Add psacing after the section




        // Length field (Linked meters and feet)
        GUILayout.Label("Background Width and Height (Meters and Feet)", EditorStyles.boldLabel);
        ps.backgroundPlane = (Transform)EditorGUILayout.ObjectField("Background Plane ",  ps.backgroundPlane, typeof(Transform), true);
        
        GUILayout.BeginHorizontal(); 
        // Meters field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the meters field
        ps.backgroundWidth = EditorGUILayout.FloatField("Background Width (m)", ps.backgroundWidth);
        if (EditorGUI.EndChangeCheck()) { 
            ps.backgroundWidthIn = ps.backgroundWidth * 39.3701f; 
            ps.UpdatePlaneScale();
        }
        // Feet field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the feet field
        ps.backgroundWidthIn = EditorGUILayout.FloatField("Background Width (in)", ps.backgroundWidthIn);
        if (EditorGUI.EndChangeCheck()) { 
            ps.backgroundWidth = ps.backgroundWidthIn / 39.3701f; 
            ps.UpdatePlaneScale();
        }
        // End the horizontal layout
        GUILayout.EndHorizontal();  

        GUILayout.BeginHorizontal(); 
        // Meters field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the meters field
        ps.backgroundHeight = EditorGUILayout.FloatField("Background Height (m)", ps.backgroundHeight);
        if (EditorGUI.EndChangeCheck()) { 
            ps.backgroundHeightIn = ps.backgroundHeight * 39.3701f; 
            ps.UpdatePlaneScale();
        }
        // Feet field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the feet field
        ps.backgroundHeightIn = EditorGUILayout.FloatField("Background Height (in)", ps.backgroundHeightIn);
        if (EditorGUI.EndChangeCheck()) { 
            ps.backgroundHeight = ps.backgroundHeightIn / 39.3701f; 
            ps.UpdatePlaneScale();
        }
        // End the horizontal layout
        GUILayout.EndHorizontal();  
        GUILayout.Space(10);  // Add psacing after the section


        //Camera settings
        GUILayout.Label("Camera Settings", EditorStyles.boldLabel);

        ps.camera = (Camera)EditorGUILayout.ObjectField("Camera",  ps.camera, typeof(Camera), true);
        
        GUILayout.BeginHorizontal(); 
        
        ps.cameraDistanceToBackground = EditorGUILayout.FloatField("Camera Distance to Background (m)", ps.cameraDistanceToBackground);
        ps.cameraFOV = EditorGUILayout.FloatField("Camera FOV (degrees)", ps.cameraFOV);
        

        GUILayout.EndHorizontal();  
        



        GUILayout.Space(10);  // Add psacing after the section




        // First row of buttons (top corners of the cube)
        GUILayout.Label("Calibrate Back Corners", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Hide Calibration")) { ps.HideCalibration(); }
        GUI.backgroundColor = Color.yellow;
        if (GUILayout.Button("Show Calibration")) { ps.ShowCalibration(); }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);  // Add psacing after the section


            

        // List of Vector3s
        GUILayout.Label("List of Corner Planes", EditorStyles.boldLabel);

        // Dipslay each Vector3 in the list with a field and remove button
        for (int i = 0; i < ps.cornerPlanes.Count; i++) {
            GUILayout.BeginHorizontal();
            
            ps.cornerPlanes[i] = (Transform)EditorGUILayout.ObjectField("Corner Plane " + i,  ps.cornerPlanes[i], typeof(Transform), true);
             
            if (GUILayout.Button("Remove", GUILayout.Width(70))) {
                ps.cornerPlanes.RemoveAt(i);
                break;  // To avoid modifying the list while iterating
            }
            GUILayout.EndHorizontal();
        }

        // Button to add a new Vector3 to the list
        if (GUILayout.Button("Add New Corner Plane Object")) {
            ps.cornerPlanes.Add(null);  // Add a default zero Vector3
        }
        GUILayout.Space(10);  // Add psacing after the section

    }
}
