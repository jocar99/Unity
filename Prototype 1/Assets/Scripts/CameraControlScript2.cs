using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlScript2 : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;

    private float swapCameraInput;
    private bool refreshCameraSwap = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        swapCameraInput = Input.GetAxis("Jump");

        if (swapCameraInput == 1 && refreshCameraSwap)
        {
            refreshCameraSwap = false;
            camera1.SetActive(!camera1.activeInHierarchy);
            camera2.SetActive(!camera2.activeInHierarchy);
        }

        if (swapCameraInput == 0 && !refreshCameraSwap)
        {
            refreshCameraSwap = true;
        }
    }
}
