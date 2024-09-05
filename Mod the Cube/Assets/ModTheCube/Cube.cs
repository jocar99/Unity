using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    private float changeSpeed = 10.0f;

    private Material material;

    private float rotationSpeed = 5.0f;

    private int rotationAxis = 0;

    void Start()
    {
        transform.position = new Vector3(3, 4, 1);
        transform.localScale = Vector3.one * 1.3f;
        
        material = Renderer.material;
        
        material.color = new Color(0.4f, 0.1f, 0.8f, 1.0f);
    }
    
    void Update()
    {

        float newRed = material.color.r + Random.Range(0.0f, 0.01f) * Time.deltaTime * changeSpeed;
        if (newRed > 1)
        {
            newRed -= 1;
            rotationAxis++;
            rotationAxis %= 3;
        }

        float newGreen = material.color.g + Random.Range(0.0f, 0.01f) * Time.deltaTime * changeSpeed;
        if (newGreen > 1)
        {
            newGreen -= 1;
            rotationAxis++;
            rotationAxis %= 3;
        }

        float newBlue = material.color.b + Random.Range(0.0f, 0.01f) * Time.deltaTime * changeSpeed;
        if (newBlue > 1)
        {
            newBlue -= 1;
            rotationAxis++;
            rotationAxis %= 3;
        }

        material.color = new Color(newRed, newGreen, newBlue, 1.0f);

        if (rotationAxis == 0)
        {
            transform.Rotate(10.0f * Time.deltaTime * rotationSpeed, 0.0f, 0.0f);
        }
        if(rotationAxis == 1)
        {
            transform.Rotate(0.0f, 10.0f * Time.deltaTime * rotationSpeed, 0.0f);
        }
        if(rotationAxis == 2)
        {
            transform.Rotate(0.0f, 0.0f, 10.0f * Time.deltaTime * rotationSpeed);
        }
    }
}
