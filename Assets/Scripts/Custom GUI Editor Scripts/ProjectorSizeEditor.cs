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
        ps = (ProjectorSize)target;

        ProjectorElements();
        BackgroundElements();
        CameraElements();
        CornerElements();
    }
    private void ProjectorElements() {
        // Length field (Linked meters and feet)
        GUILayout.Label("Projector Width and Height (Meters and Feet)", EditorStyles.boldLabel);
        ps.projectorPlane = (Transform)EditorGUILayout.ObjectField("Projector Plane ",  ps.projectorPlane, typeof(Transform), true);
        ps.projectorScaleFactor = EditorGUILayout.FloatField("Projector Scale Factor", ps.projectorScaleFactor);
        if (EditorGUI.EndChangeCheck()) { ps.UpdatePlaneScale(); }

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
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        // Meters field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the meters field
        ps.projectorDepth = EditorGUILayout.FloatField("Projector Depth (m)", ps.projectorDepth);
        if (EditorGUI.EndChangeCheck()) {
            ps.projectorHeightIn = ps.projectorHeight * 39.3701f;
            ps.UpdatePlaneScale();
        }
        // Feet field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the feet field
        ps.projectorDepthIn = EditorGUILayout.FloatField("Projector Depth (in)", ps.projectorDepthIn);
        if (EditorGUI.EndChangeCheck()) {
            ps.projectorDepth = ps.projectorDepthIn / 39.3701f;
            ps.UpdatePlaneScale();
        }
        GUILayout.EndHorizontal();

        GUILayout.Space(10); 
    }

    private void BackgroundElements() {
        // Length field (Linked meters and feet)
        GUILayout.Label("Background Width and Height (Meters and Feet)", EditorStyles.boldLabel);
        ps.backgroundPlane = (Transform)EditorGUILayout.ObjectField("Background Plane ",  ps.backgroundPlane, typeof(Transform), true);
        
        GUILayout.BeginHorizontal(); 
        // Meters field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the meters field
        ps.backgroundWidth = EditorGUILayout.FloatField("Background Width (m)", ps.backgroundWidth);
        if (EditorGUI.EndChangeCheck()) { 
            ps.backgroundWidthIn = ps.backgroundWidth * 39.3701f; 
            ps.UpdateBackgroundScale();
        }
        // Feet field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the feet field
        ps.backgroundWidthIn = EditorGUILayout.FloatField("Background Width (in)", ps.backgroundWidthIn);
        if (EditorGUI.EndChangeCheck()) { 
            ps.backgroundWidth = ps.backgroundWidthIn / 39.3701f; 
            ps.UpdateBackgroundScale();
        }
        GUILayout.EndHorizontal();  

        GUILayout.BeginHorizontal(); 
        // Meters field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the meters field
        ps.backgroundHeight = EditorGUILayout.FloatField("Background Height (m)", ps.backgroundHeight);
        if (EditorGUI.EndChangeCheck()) { 
            ps.backgroundHeightIn = ps.backgroundHeight * 39.3701f; 
            ps.UpdateBackgroundScale();
        }
        // Feet field
        EditorGUI.BeginChangeCheck();  // Start tracking changes to the feet field
        ps.backgroundHeightIn = EditorGUILayout.FloatField("Background Height (in)", ps.backgroundHeightIn);
        if (EditorGUI.EndChangeCheck()) { 
            ps.backgroundHeight = ps.backgroundHeightIn / 39.3701f; 
            ps.UpdateBackgroundScale();
        }
        GUILayout.EndHorizontal();  
        GUILayout.Space(10);  
    }

    private void CameraElements() {
        GUILayout.Label("Camera Settings", EditorStyles.boldLabel);
        ps.camera = (Camera)EditorGUILayout.ObjectField("Camera",  ps.camera, typeof(Camera), true);
        
        ps.cameraDistanceToBackground = EditorGUILayout.FloatField("Camera Distance to Background (m)", ps.cameraDistanceToBackground);
        if (EditorGUI.EndChangeCheck()) { ps.UpdateBackgroundScale(); }

        GUILayout.BeginHorizontal(); 
        
        ps.cameraFOV = EditorGUILayout.FloatField("Camera FOV (degrees)", ps.cameraFOV);
        if (EditorGUI.EndChangeCheck()) { ps.UpdateCamera(); }

        if (GUILayout.Button("Calculate Camera FOV")) { 
            ps.cameraFOV = Mathf.Atan((ps.backgroundWidth*0.5f) / ps.cameraDistanceToBackground) * 57.2958f;
            ps.UpdateBackgroundScale();
            ps.UpdateCamera();
        }
        
        GUILayout.EndHorizontal();  
        GUILayout.Space(10);  
    }

    private bool showCornerList = false;
    private void CornerElements() {
        // First row of buttons (top corners of the cube)
        GUILayout.Label("Calibrate Back Corners", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Hide Calibration")) { ps.HideCalibration(); }
        GUI.backgroundColor = Color.yellow;
        if (GUILayout.Button("Show Calibration")) { ps.ShowCalibration(); }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);  

        //Corner Planes List
        GUILayout.Label("Corner Planes", EditorStyles.boldLabel);
        showCornerList = EditorGUILayout.Foldout(showCornerList, "List of Corner Planes");
        if (showCornerList) {
            // Display each plane in the list with a field and remove button
            for (int i = 0; i < ps.cornerPlanes.Count; i++) {
                GUILayout.BeginHorizontal();
                ps.cornerPlanes[i] = (Transform)EditorGUILayout.ObjectField("Corner Plane " + i,  ps.cornerPlanes[i], typeof(Transform), true);
                if (GUILayout.Button("Remove", GUILayout.Width(70))) {
                    ps.cornerPlanes.RemoveAt(i);
                    break;  // To avoid modifying the list while iterating
                }
                GUILayout.EndHorizontal();
            }
            // Button to add a new default null cornerPlane to the list     
            if (GUILayout.Button("Add New Corner Plane Object")) {
                ps.cornerPlanes.Add(null);  
            }
        }
        GUILayout.Space(10);  
    }
}