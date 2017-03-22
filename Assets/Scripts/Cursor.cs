using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    private CameraRaycaster raycaster;

    // Use this for initialization
    private void Start()
    {
        raycaster = GetComponent<CameraRaycaster>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(Utilities.LeftMouseButton))
        {
            //print(raycaster.layerHit);
        }
    }
}