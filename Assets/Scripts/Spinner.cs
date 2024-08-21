using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    public Vector3 rotator;

    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Time.deltaTime * rotator);
    }
}
