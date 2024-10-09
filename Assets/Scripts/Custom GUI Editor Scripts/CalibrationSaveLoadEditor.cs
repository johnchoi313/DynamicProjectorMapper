using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CalibrationSaveLoad))]
public class CalibrationSaveLoadEditor : Editor
{
    CalibrationSaveLoad csl;

    private bool showConnections = false;
    public override void OnInspectorGUI() { csl = (CalibrationSaveLoad)target;
        //Script Connections to Save/Load
        GUILayout.Label("Script Connections", EditorStyles.boldLabel);
        showConnections = EditorGUILayout.Foldout(showConnections, "Connections");
        if (showConnections) {
            csl.ps = (ProjectorSize)EditorGUILayout.ObjectField("Projector Size Script",  csl.ps, typeof(ProjectorSize), true);
            csl.ti = (TrilinearInterpolator)EditorGUILayout.ObjectField("Trilinear Interpolator Script",  csl.ti, typeof(TrilinearInterpolator), true);
            csl.opt = (OptitrackProjectorTranslator)EditorGUILayout.ObjectField("Optitrack Projector Translator Script",  csl.opt, typeof(OptitrackProjectorTranslator), true);
        }
        GUILayout.Space(10);  

        //Save/Load Calibration Buttons
        GUILayout.Label("Save/Load Calibration Buttons", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Save Cube Calibration")) { csl.SaveCalibration(); }
        if (GUILayout.Button("Load Cube Calibration")) { csl.LoadCalibration(); }
        GUILayout.EndHorizontal();
        GUILayout.Space(10);  
    }
}
