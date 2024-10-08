using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;
using SFB;

[System.Serializable]
public class CalibrationSaveLoad : MonoBehaviour
{
    public ProjectorSize ps;
    public TrilinearInterpolator ti;
    public OptitrackProjectorTranslator opt;

    public void SaveCalibration() {
        var path = StandaloneFileBrowser.SaveFilePanel("Save Calibration", "", "calibration", "txt");
        if (!string.IsNullOrEmpty(path)) {
            string calibrationString = GetCalibrationString();
            File.WriteAllText(path, calibrationString);
            Debug.Log("Calibration saved to path: " + path);
        }
    }
    public void LoadCalibration() {
        var paths = StandaloneFileBrowser.OpenFilePanel("Load Calibration", "", "txt", false);
        if (paths.Length > 0) {
            if (File.Exists(paths[0])) {
                string calibrationString = File.ReadAllText(paths[0]);
                ApplyCalibrationString(calibrationString);
                Debug.Log("Calibration loaded to path: " + paths[0]);
            }
        }
    }

    private string GetCalibrationString() {
        CalibrationData calibration = new CalibrationData();

        //Projector Size Stuff
        calibration.projectorWidth = ps.projectorWidth;
        calibration.projectorHeight = ps.projectorHeight;
        calibration.projectorDepth = ps.projectorDepth;
        calibration.projectorWidthIn = ps.projectorWidthIn;
        calibration.projectorHeightIn = ps.projectorHeightIn;
        calibration.projectorDepthIn = ps.projectorDepthIn;
        calibration.projectorScaleFactor = ps.projectorScaleFactor;

        calibration.backgroundWidth = ps.backgroundWidth;
        calibration.backgroundHeight = ps.backgroundHeight;
        calibration.backgroundWidthIn = ps.backgroundWidthIn;
        calibration.backgroundHeightIn = ps.backgroundHeightIn;

        calibration.cameraDistanceToBackground = ps.cameraDistanceToBackground;
        calibration.cameraFOV = ps.cameraFOV;

        //Trilinear Interpolator Stuff
        calibration.flipPosX = ti.flipPosX;
        calibration.flipPosY = ti.flipPosY;
        calibration.flipPosZ = ti.flipPosZ;

        calibration.fudgeMaxX = ti.fudgeMaxX;
        calibration.fudgeMinX = ti.fudgeMinX;
        calibration.fudgeMaxY = ti.fudgeMaxY;
        calibration.fudgeMinY = ti.fudgeMinY;
        calibration.fudgeMaxZ = ti.fudgeMaxZ;
        calibration.fudgeMinZ = ti.fudgeMinZ;

        calibration.sourceCubeCorners = ti.sourceCubeCorners;
        calibration.destinationCubeCorners = ti.destinationCubeCorners;

        //Optitrack Projector Translator Stuff
        calibration.rotateOffset = opt.rotateOffset;

        calibration.flipRotX = opt.flipRotX;
        calibration.flipRotY = opt.flipRotY;
        calibration.flipRotZ = opt.flipRotZ;
        
        //Save this
        string calibrationString = JsonUtility.ToJson(calibration);
        Debug.Log(calibrationString);
        return calibrationString;
    }

    private void ApplyCalibrationString(string calibrationString) {
        Debug.Log(calibrationString);
        CalibrationData calibration = JsonUtility.FromJson<CalibrationData>(calibrationString);

        //Projector Size Stuff
        ps.projectorWidth = calibration.projectorWidth;
        ps.projectorHeight = calibration.projectorHeight;
        ps.projectorDepth = calibration.projectorDepth;
        ps.projectorWidthIn = calibration.projectorWidthIn;
        ps.projectorHeightIn = calibration.projectorHeightIn;
        ps.projectorDepthIn = calibration.projectorDepthIn;
        ps.projectorScaleFactor = calibration.projectorScaleFactor;

        ps.backgroundWidth = calibration.backgroundWidth;
        ps.backgroundHeight = calibration.backgroundHeight;
        ps.backgroundWidthIn = calibration.backgroundWidthIn;
        ps.backgroundHeightIn = calibration.backgroundHeightIn;

        ps.cameraDistanceToBackground = calibration.cameraDistanceToBackground;
        ps.cameraFOV = calibration.cameraFOV;

        //Trilinear Interpolator Stuff
        ti.flipPosX = calibration.flipPosX;
        ti.flipPosY = calibration.flipPosY;
        ti.flipPosZ = calibration.flipPosZ;

        ti.fudgeMaxX = calibration.fudgeMaxX;
        ti.fudgeMinX = calibration.fudgeMinX;
        ti.fudgeMaxY = calibration.fudgeMaxY;
        ti.fudgeMinY = calibration.fudgeMinY;
        ti.fudgeMaxZ = calibration.fudgeMaxZ;
        ti.fudgeMinZ = calibration.fudgeMinZ;

        ti.sourceCubeCorners = calibration.sourceCubeCorners;
        ti.destinationCubeCorners = calibration.destinationCubeCorners;

        //Optitrack Projector Translator Stuff
        opt.rotateOffset = calibration.rotateOffset;

        opt.flipRotX = calibration.flipRotX;
        opt.flipRotY = calibration.flipRotY;
        opt.flipRotZ = calibration.flipRotZ;

        //Apply functions
        ps.UpdatePlaneScale();
        ps.UpdateCamera();
        ps.UpdateBackgroundScale();
        opt.ApplyCornerTransforms();
    }
}

[System.Serializable]
public class CalibrationData {
    //Projector Size Stuff
    public float projectorWidth = 0.355f;
    public float projectorHeight = 0.28f;
    public float projectorDepth = 0.01f;
    public float projectorWidthIn = 14f;
    public float projectorHeightIn = 11f;
    public float projectorDepthIn = 0.39f;
    public float projectorScaleFactor = 1f;

    public float backgroundWidth = 4.44f;
    public float backgroundHeight = 2.5f;
    public float backgroundWidthIn = 174.8f;
    public float backgroundHeightIn = 98.4f;

    public float cameraDistanceToBackground = 5f;
    public float cameraFOV = 25f;

    //Trilinear Interpolator stuff
    public bool flipPosX = false;
    public bool flipPosY = false;
    public bool flipPosZ = false;

    public float fudgeMaxX = 0;
    public float fudgeMinX = 0;
    public float fudgeMaxY = 0;
    public float fudgeMinY = 0;
    public float fudgeMaxZ = 0;
    public float fudgeMinZ = 0;

    public Vector3[] sourceCubeCorners = new Vector3[8];
    public Vector3[] destinationCubeCorners = new Vector3[8];

    //Translator stuff
    public Vector3 rotateOffset = new Vector3(0,0,0);

    public bool flipRotX = false;
    public bool flipRotY = false;
    public bool flipRotZ = false;
}