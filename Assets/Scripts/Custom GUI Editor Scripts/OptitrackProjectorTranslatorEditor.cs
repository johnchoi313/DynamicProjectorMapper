using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OptitrackProjectorTranslator))]
public class OptitrackProjectorTranslatorEditor : Editor
{
    OptitrackProjectorTranslator opt;
    
    public override void OnInspectorGUI() { 
        opt = (OptitrackProjectorTranslator)target;
        
        Connections();
        CornerTransforms();
        RotationOptions();
        CalibrateCornerButtons();
        ClampCornerCalibrations();
    }

    public void Connections() {
        // Trilinear Interpolator
        GUILayout.Label("TriLinear Interpolator", EditorStyles.boldLabel);
        opt.trinterp = (TrilinearInterpolator)EditorGUILayout.ObjectField("TriLinear Interpolator",  opt.trinterp, typeof(TrilinearInterpolator), true);
        GUILayout.Space(10);  

        GUILayout.Label("Projector/Optitrack Link", EditorStyles.boldLabel);
        //Show Projector and its position
        opt.projector = (Transform)EditorGUILayout.ObjectField("Projector", opt.projector, typeof(Transform), true);
        GUI.enabled = false;
        opt.projectorPos = EditorGUILayout.Vector3Field("Projector Position", opt.projectorPos);
        GUI.enabled = true;
        //Show Optitrack and its position
        opt.optitrack = (Transform)EditorGUILayout.ObjectField("Optitrack", opt.optitrack, typeof(Transform), true);
        GUI.enabled = false;
        opt.optitrackPos = EditorGUILayout.Vector3Field("Optitrack Position", opt.optitrackPos);
        GUI.enabled = true;
        GUILayout.Space(10); 
    }

    private bool showCornerList = false;
    public void CornerTransforms() {
        // Add slots for Corners
        GUILayout.Label("Corner Transforms", EditorStyles.boldLabel);
        showCornerList = EditorGUILayout.Foldout(showCornerList, "List of Corner Transforms");
        if (showCornerList) {
            opt.corner0 = (Transform)EditorGUILayout.ObjectField("Corner 0 [-1, -1, -1] (Left Lower Front)",  opt.corner0, typeof(Transform), true);
            opt.corner1 = (Transform)EditorGUILayout.ObjectField("Corner 1 [-1, -1,  1] (Left Lower Back)",   opt.corner1, typeof(Transform), true);
            opt.corner2 = (Transform)EditorGUILayout.ObjectField("Corner 2 [-1,  1, -1] (Left Upper Front)",  opt.corner2, typeof(Transform), true);
            opt.corner3 = (Transform)EditorGUILayout.ObjectField("Corner 3 [-1,  1,  1] (Left Upper Back)",   opt.corner3, typeof(Transform), true);
            opt.corner4 = (Transform)EditorGUILayout.ObjectField("Corner 4 [ 1, -1, -1] (Right Lower Front)", opt.corner4, typeof(Transform), true);
            opt.corner5 = (Transform)EditorGUILayout.ObjectField("Corner 5 [ 1, -1,  1] (Right Lower Back)",  opt.corner5, typeof(Transform), true);
            opt.corner6 = (Transform)EditorGUILayout.ObjectField("Corner 6 [ 1,  1, -1] (Right Upper Front)", opt.corner6, typeof(Transform), true);
            opt.corner7 = (Transform)EditorGUILayout.ObjectField("Corner 7 [ 1,  1,  1] (Right Upper Back)",  opt.corner7, typeof(Transform), true);
        }
        GUILayout.Space(10);  // Add spacing after the section
    }

    public void RotationOptions() {
        //Rotation Offsets
        GUILayout.Label("Rotation Offset", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        opt.rotateOffset = EditorGUILayout.Vector3Field("Rotate Offset", opt.rotateOffset);
        if (GUILayout.Button("Set Optitrack Offset")) { opt.SetRotationOffset(); }
        if (GUILayout.Button("Reset")) { opt.ResetRotationOffset(); }
        GUILayout.EndHorizontal();
        GUILayout.Space(10); 

        //Rotation Offset Flips
        GUILayout.Label("Flip Options", EditorStyles.boldLabel);
        opt.flipRotX = EditorGUILayout.Toggle("Flip Rot X", opt.flipRotX);
        opt.flipRotY = EditorGUILayout.Toggle("Flip Rot Y", opt.flipRotY);
        opt.flipRotZ = EditorGUILayout.Toggle("Flip Rot Z", opt.flipRotZ);
        GUILayout.Space(10);
    }

    public void CalibrateCornerButtons() {
        //Front rows
        GUILayout.Label("Calibrate Front Corners", EditorStyles.boldLabel);

        // First row of buttons (top corners of the cube)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Corner 2")) { opt.CalibrateCorner(2); }
        GUI.backgroundColor = Color.yellow;
        if (GUILayout.Button("Corner 6")) { opt.CalibrateCorner(6); }
        GUILayout.EndHorizontal();

        // Second row of buttons (middle level - represent space between top and bottom)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Corner 0")) { opt.CalibrateCorner(0); }
        GUI.backgroundColor = Color.blue; 
        if (GUILayout.Button("Corner 4")) { opt.CalibrateCorner(4); }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);  

        //Back rows
        GUILayout.Label("Calibrate Back Corners", EditorStyles.boldLabel);

        // Third row of buttons (bottom corners of the cube)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.magenta;  
        if (GUILayout.Button("Corner 3")) { opt.CalibrateCorner(3); }
        GUI.backgroundColor = Color.cyan;  
        if (GUILayout.Button("Corner 7")) { opt.CalibrateCorner(7); }
        GUILayout.EndHorizontal();

        // Fourth row of buttons (final bottom corners of the cube)
        GUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.grey;  
        if (GUILayout.Button("Corner 1")) { opt.CalibrateCorner(1); }
        GUI.backgroundColor = Color.white;  
        if (GUILayout.Button("Corner 5")) { opt.CalibrateCorner(5); }
        GUILayout.EndHorizontal();

        // Reset the GUI background color to default
        GUI.backgroundColor = Color.white;
        GUILayout.Space(10);  
    }
    
    public void ClampCornerCalibrations() {
        //Clamp Corner Calibration Quick Helpers
        GUILayout.Label("Clamp Corner Calibration", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Clamp Cube Corners")) { opt.ClampCubeCorners(); }
        if (GUILayout.Button("Clamp Cube Corners Z")) { opt.ClampCubeCornersZ(); }
        GUILayout.EndHorizontal();
        GUILayout.Space(10); 
    }

}