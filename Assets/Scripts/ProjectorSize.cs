using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectorSize : MonoBehaviour
{


    public float projectorWidth; //In Meters
    public float projectorHeight; //In Meters
    public float projectorWidthIn = 1f;
    public float projectorHeightIn = 1f;  // 1 meter = 3.28084 feet

    public float projectorScaleFactor = 1.0f;

    public float backgroundWidth;
    public float backgroundHeight;
    public float backgroundWidthIn;
    public float backgroundHeightIn;

    public Transform projectorPlane;
    public Transform backgroundPlane;
    public List<Transform> cornerPlanes;
    
    public Camera camera;
    public float cameraDistanceToBackground;
    public float cameraFOV;
    
    // Start is called before the first frame update
    public void UpdatePlaneScale() {
        foreach(Transform cornerPlane in cornerPlanes) {
            cornerPlane.localScale = new Vector3(projectorWidth, projectorHeight, 0.01f);
        }
        projectorPlane.localScale = new Vector3(projectorWidth * projectorScaleFactor, projectorHeight * projectorScaleFactor, 0.01f);
        Debug.Log("Planes scaled.");
    }

    public void UpdateCamera() {
        camera.fieldOfView = cameraFOV;
    }

    public void UpdateBackgroundScale() {
        backgroundPlane.localPosition = new Vector3(0,0,cameraDistanceToBackground);
        backgroundPlane.localScale = new Vector3(backgroundWidth, backgroundHeight, 1f);
    }

    public void HideCalibration() {
        foreach(Transform cornerPlane in cornerPlanes) {
            cornerPlane.gameObject.SetActive(false);
        }
        backgroundPlane.gameObject.SetActive(false);        
        Debug.Log("All calibration objects hidden.");
    }
    public void ShowCalibration() {
        foreach(Transform cornerPlane in cornerPlanes) {
            cornerPlane.gameObject.SetActive(true);
        }
        backgroundPlane.gameObject.SetActive(true);
        Debug.Log("Planes scaled.");
        Debug.Log("All calibration objects shown.");
    }
    
}