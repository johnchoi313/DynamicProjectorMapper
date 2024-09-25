using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptitrackProjectorTranslator : MonoBehaviour
{
    public float projectorWidth;
    public float projectorHeight;

    public KeyCode[] AlphaNum = new KeyCode[8];

    public Transform projector;
    public Transform optitrack;
    public Transform reference;

    public Transform projectorPlane;
    public Transform referencePlane;

    public TrilinearInterpolator trinterp;

    public Vector3 projectorPos; //Diagnostic only
    public Vector3 optitrackPos; //Diagnostic only
    public Vector3 rotateOffset;

    public bool flipRotX = false;
    public bool flipRotY = false;
    public bool flipRotZ = false;

    // Start is called before the first frame update
    void Start()
    {
        projectorPlane.localScale = new Vector3(projectorWidth, projectorHeight, 0.01f);
        referencePlane.localScale = new Vector3(projectorWidth, projectorHeight, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        //Calibration keys Alpha1 to Alpha 8: Calibration Order Matters!
        for (int i = 0; i< 8; i++) {
            if (Input.GetKeyDown(AlphaNum[i])) {
                trinterp.sourceCubeCorners[i] = optitrack.position;
                trinterp.destinationCubeCorners[i] = reference.position;
                Debug.Log("Calibrated corner [" + i + "].");
            }
        }

        if(Input.GetKeyDown(KeyCode.R)) //Get rotation offset
        {
            rotateOffset = optitrack.eulerAngles;
            Debug.Log("Calibrated Rotation Offset.");
        }

        if (Input.GetKeyDown(KeyCode.C)) //Get rotation offset
        {
            trinterp.ClampCubeCorners();
            Debug.Log("Clamped source and destination cube corners.");
        }

        if (Input.GetKeyDown(KeyCode.Z)) //Get rotation offset
        {
            trinterp.ClampCubeCornersZ();
            Debug.Log("Clamped source and destination cube corners with Z offset from keystone angle.");
        }


        projector.position = trinterp.TrilinearInterpolate(optitrack.position);

        projectorPos = projector.position;
        optitrackPos = optitrack.position;


        projector.rotation = Quaternion.identity;
        //projector.rotation = Quaternion.Inverse(optitrack.rotation);

        projector.Rotate(-optitrack.eulerAngles.x * (flipRotX ? 1 : -1), 
                         -optitrack.eulerAngles.y * (flipRotY ? 1 : -1), 
                         -optitrack.eulerAngles.z * (flipRotZ ? 1 : -1));

        projector.Rotate(rotateOffset.x * (flipRotX ? 1 : -1), 
                         rotateOffset.y * (flipRotY ? 1 : -1), 
                         rotateOffset.z * (flipRotZ ? 1 : -1));
    }
}
