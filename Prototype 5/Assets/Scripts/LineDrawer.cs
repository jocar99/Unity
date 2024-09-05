using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineDrawer : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject brush;
    public GameManager gameManager;

    private LineRenderer currentLineRenderer;

    private GameObject brushInstance;

    private Vector2 lastPosition;

    private float lastTime;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.isGameActive)
        {
            Drawing();
        }
    }

    void Drawing()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreateBrush();
        }
        else if (Input.GetMouseButton(0))
        {
            PointToMousePos();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DeleteBrush();
        }
    }

    void CreateBrush()
    {
        brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
    }

    void AddAPoint(Vector2 pointPos)
    {
        if(currentLineRenderer.positionCount > 20)
        {
            for(int i = 0; i < currentLineRenderer.positionCount - 1; i++)
            {
                currentLineRenderer.SetPosition(i, currentLineRenderer.GetPosition(i + 1));
            }
        }
        else
        {
            currentLineRenderer.positionCount++;
        }
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
        lastTime = Time.time;
    }

    void ReducePoints()
    {
        for (int i = 0; i < currentLineRenderer.positionCount - 1; i++)
        {
            currentLineRenderer.SetPosition(i, currentLineRenderer.GetPosition(i + 1));
        }
        if(currentLineRenderer.positionCount > 0)
        {
            currentLineRenderer.positionCount--;
        }
        lastTime = Time.time;
    }

    void PointToMousePos()
    {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if(lastPosition != mousePos)
        {
            AddAPoint(mousePos);
            lastPosition = mousePos;
            
        }
        else if (Time.time - lastTime > 0.025)
        {
            ReducePoints();
        }
    }

    void DeleteBrush()
    {
        Destroy(brushInstance);
    }
}
