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

    public TrilinearInterpolator trinterp;

    public Vector3 projectorPos; //Diagnostic only
    public Vector3 optitrackPos; //Diagnostic only
    public Vector3 rotateOffset;

    // Start is called before the first frame update
    void Start()
    {
        projector.localScale = new Vector3(projectorWidth, projectorHeight, 0.01f);
        reference.localScale = new Vector3(projectorWidth, projectorHeight, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        //Calibration keys Alpha1 to Alpha 8: Calibration Order Matters!
        for (int i = 0; i< 8; i++) {
            if (Input.GetKeyDown(AlphaNum[i])) {
                trinterp.sourceCubeCorners[i] = optitrack.position;
                trinterp.destinationCubeCorners[i] = reference.position;
            }
        }

        if(Input.GetKeyDown(KeyCode.Y)) //Duplicate Y corners
        {
            trinterp.sourceCubeCorners[2] = trinterp.sourceCubeCorners[0];
            trinterp.sourceCubeCorners[3] = trinterp.sourceCubeCorners[1];

            trinterp.sourceCubeCorners[6] = trinterp.sourceCubeCorners[4];
            trinterp.sourceCubeCorners[7] = trinterp.sourceCubeCorners[5];

            trinterp.destinationCubeCorners[2] = trinterp.destinationCubeCorners[0];
            trinterp.destinationCubeCorners[3] = trinterp.destinationCubeCorners[1];

            trinterp.destinationCubeCorners[6] = trinterp.destinationCubeCorners[4];
            trinterp.destinationCubeCorners[7] = trinterp.destinationCubeCorners[5];
        }

        if(Input.GetKeyDown(KeyCode.R)) //Get rotation offset
        {
            rotateOffset = optitrack.eulerAngles;
        }

        projector.position = trinterp.TrilinearInterpolate(optitrack.position);

        projectorPos = projector.position;
        optitrackPos = optitrack.position;

        projector.rotation = optitrack.rotation;
        projector.Rotate(-rotateOffset);
    }
}
