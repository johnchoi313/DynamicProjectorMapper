using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptitrackProjectorTranslator : MonoBehaviour
{
    public float projectorWidth;
    public float projectorHeight;

    public Transform projector;
    public Transform optitrack;
    public Transform reference;

    public Vector3 optitrackPos1;
    public Vector3 projectorPos1;

    public Vector3 optitrackPos2;
    public Vector3 projectorPos2;


    // Start is called before the first frame update
    void Start()
    {
        projector.localScale = new Vector3(projectorWidth, projectorHeight, 0.01f);
        reference.localScale = new Vector3(projectorWidth, projectorHeight, 0.01f);
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            optitrackPos1 = optitrack.position;
            projectorPos1 = reference.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            optitrackPos2 = optitrack.position;
            projectorPos2 = reference.position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
        }

        float x = Mathf.Lerp(optitrackPos1.x, optitrackPos2.x, (optitrack.position.x - optitrackPos1.x) / (optitrackPos2.x - optitrackPos1.x));
        float y = Mathf.Lerp(optitrackPos1.y, optitrackPos2.y, (optitrack.position.y - optitrackPos1.y) / (optitrackPos2.y - optitrackPos1.y));
        float z = Mathf.Lerp(optitrackPos1.z, optitrackPos2.z, (optitrack.position.z - optitrackPos1.z) / (optitrackPos2.z - optitrackPos1.z));

        projector.position =  ValueMapper.Map(new Vector3(x,y,z), optitrackPos1, optitrackPos2, projectorPos1, projectorPos2, false);


    }
}
