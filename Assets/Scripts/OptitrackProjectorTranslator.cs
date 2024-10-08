using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptitrackProjectorTranslator : MonoBehaviour
{
    public TrilinearInterpolator trinterp;

    //Projector Stuff
    public Transform projector;
    public Vector3 projectorPos; //Diagnostic only

    //Optitrack Stuff
    public Transform optitrack;
    public Vector3 optitrackPos; //Diagnostic only

    //Calibration Corners
    public Transform corner0; //Left Lower Front  (-1, -1, -1) [Alpha1]
    public Transform corner1; //Left Lower Back   (-1, -1,  1) [Alpha2]
    public Transform corner2; //Left Upper Front  (-1,  1, -1) [Alpha3]
    public Transform corner3; //Left Upper Back   (-1,  1,  1) [Alpha4]
    public Transform corner4; //Right Lower Front ( 1, -1, -1) [Alpha5]
    public Transform corner5; //Right Lower Back  ( 1, -1,  1) [Alpha6]
    public Transform corner6; //Right Upper Front ( 1,  1, -1) [Alpha7]
    public Transform corner7; //Right Upper Back  ( 1,  1,  1) [Alpha8]

    //Rotation Offset
    public Vector3 rotateOffset;

    //Flip Rotation Options
    public bool flipRotX = false;
    public bool flipRotY = false;
    public bool flipRotZ = false;

    //Calibrate Corner
    public void CalibrateCorner(int corner) {
        trinterp.sourceCubeCorners[corner] = optitrack.position;
        switch (corner) {
            case 0: trinterp.destinationCubeCorners[0] = corner0.position; break;
            case 1: trinterp.destinationCubeCorners[1] = corner1.position; break;
            case 2: trinterp.destinationCubeCorners[2] = corner2.position; break;
            case 3: trinterp.destinationCubeCorners[3] = corner3.position; break;
            case 4: trinterp.destinationCubeCorners[4] = corner4.position; break;
            case 5: trinterp.destinationCubeCorners[5] = corner5.position; break;
            case 6: trinterp.destinationCubeCorners[6] = corner6.position; break;
            case 7: trinterp.destinationCubeCorners[7] = corner7.position; break;
        }
        Debug.Log("Calibrated corner [" + corner + "].");
    }
    public void ApplyCornerTransforms() {
        corner0.position = trinterp.destinationCubeCorners[0];
        corner1.position = trinterp.destinationCubeCorners[1];
        corner2.position = trinterp.destinationCubeCorners[2];
        corner3.position = trinterp.destinationCubeCorners[3];
        corner4.position = trinterp.destinationCubeCorners[4];
        corner5.position = trinterp.destinationCubeCorners[5];
        corner6.position = trinterp.destinationCubeCorners[6];
        corner7.position = trinterp.destinationCubeCorners[7];
    
        Debug.Log("Loaded corner transforms.");
    }

    //Rotation Helper Functions
    public void SetRotationOffset() { 
        rotateOffset = optitrack.eulerAngles; 
        Debug.Log("Calibrated Rotation Offset.");
    }
    public void ResetRotationOffset() { 
        rotateOffset = new Vector3(0,0,0); 
        Debug.Log("Reset Rotation Offset.");
    }
    //Quick Calibration Clamping Helper Functions
    public void ClampCubeCorners() {
        trinterp.ClampCubeCorners();
        ApplyCornerTransforms();
        Debug.Log("Clamped source and destination cube corners.");
    }
    public void ClampCubeCornersZ() {
        trinterp.ClampCubeCornersZ();
        ApplyCornerTransforms();
        Debug.Log("Clamped source and destination cube corners with Z offset from keystone angle.");
    }

    void Update() {
        //Map Projector Position to Optitrack
        projector.position = trinterp.TrilinearInterpolate(optitrack.position);
        //Update Diagnostic Vectors
        projectorPos = projector.position;
        optitrackPos = optitrack.position;
        //Reset Rotation
        projector.rotation = Quaternion.identity;
        //Map Projector Rotation to Optitrack with flips
        projector.Rotate(-optitrack.eulerAngles.x * (flipRotX ? 1 : -1), 
                         -optitrack.eulerAngles.y * (flipRotY ? 1 : -1), 
                         -optitrack.eulerAngles.z * (flipRotZ ? 1 : -1));
        //Account for offsets with flips
        projector.Rotate(rotateOffset.x * (flipRotX ? 1 : -1), 
                         rotateOffset.y * (flipRotY ? 1 : -1), 
                         rotateOffset.z * (flipRotZ ? 1 : -1));
    }
}
