using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProjectorSize : MonoBehaviour
{
    public float projectorWidth = 0.355f; //(m)
    public float projectorHeight = 0.28f; //(m)
    public float projectorDepth = 0.01f;  //(m)
    public float projectorWidthIn = 14f;   //(in)
    public float projectorHeightIn = 11f;  //(in)
    public float projectorDepthIn = 0.39f; //(in)
    public float projectorScaleFactor = 1.0f;

    public float backgroundWidth = 4.44f;    //(m)
    public float backgroundHeight = 2.5f;    //(m)
    public float backgroundWidthIn = 174.8f; //(in)
    public float backgroundHeightIn = 98.4f; //(in)

    public Transform projectorPlane;
    public Transform backgroundPlane;
    public List<Transform> cornerPlanes;
    
    public Camera camera;
    public float cameraDistanceToBackground = 5f;
    public float cameraFOV = 25f;
    
    // Start is called before the first frame update
    public void UpdatePlaneScale() {
        foreach(Transform cornerPlane in cornerPlanes) {
            cornerPlane.localScale = new Vector3(projectorWidth, projectorHeight, projectorDepth);
        }
        projectorPlane.localScale = new Vector3(projectorWidth * projectorScaleFactor, projectorHeight * projectorScaleFactor, projectorDepth * projectorScaleFactor);
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
        Debug.Log("All calibration objects shown.");
    }
    
}