using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float topBound = 30.0f;
    private float lowerBound = -10.0f;
    private float xBound = 40.0f;

    private GameObject canvas;
    private UIController controllerRef;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
        controllerRef = canvas.GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > xBound)
        {
            Destroy(gameObject);
            controllerRef.lives--;
            controllerRef.updateUI();
        }
        if (transform.position.x < -xBound)
        {
            Destroy(gameObject);
            controllerRef.lives--;
            controllerRef.updateUI();
        }
        if (transform.position.z > topBound)
        {
            Destroy(gameObject);
        }
        else if (transform.position.z < lowerBound)
        {
            Destroy(gameObject);
            controllerRef.lives--;
            controllerRef.updateUI();
        }
    }
}
