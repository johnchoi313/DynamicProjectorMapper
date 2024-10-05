using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationSaveLoad : MonoBehaviour
{
    public ProjectorSize ps;
    public TrilinearInterpolator ti;
    public OptitrackProjectorTranslator opt;

    public void SaveCalibration() {
        Debug.Log("Calibration Saved.");
    }
    public void LoadCalibration() {
        Debug.Log("Calibration Loaded.");
    }
}
