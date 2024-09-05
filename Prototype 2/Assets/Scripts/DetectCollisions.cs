using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectCollisions : MonoBehaviour
{
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
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "Projectile" && other.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
            GameObject canvasChild = other.gameObject.transform.Find("Canvas").gameObject;
            GameObject sliderChild = canvasChild.transform.Find("Slider").gameObject;
            Slider sliderComp = sliderChild.GetComponent<Slider>();
            sliderComp.value++;
            if (sliderComp.value == sliderComp.maxValue)
            {
                Destroy(other.gameObject);
                controllerRef.score++;
                controllerRef.updateUI();
            }
        }
        else if (gameObject.tag == "Enemy" && other.gameObject.tag == "Player")
        {
            controllerRef.lives--;
            controllerRef.updateUI();
        }
    }
}
