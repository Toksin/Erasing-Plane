using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObstacleRemoval : MonoBehaviour
{
    public static ObstacleRemoval Instance { get; private set; }
    public event EventHandler onClick;

    public Button deleteModeButton;     
    private bool isInDeleteMode = false;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {       
        deleteModeButton.onClick.AddListener(() => {
            onClick?.Invoke(this, EventArgs.Empty);
            ToggleDeleteMode();
        });
    }

    void Update()
    {
        if (isInDeleteMode)
        {           
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                    if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
                    {
                        Destroy(hit.collider.gameObject);
                        ExitDeleteMode();
                    }
                }
            }           
            else if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

                if (hit.collider != null && hit.collider.CompareTag("Obstacle"))
                {
                    Destroy(hit.collider.gameObject);
                    ExitDeleteMode();
                }
            }
        }
    }

    void ToggleDeleteMode()
    {
        isInDeleteMode = !isInDeleteMode;
        UpdateDeleteModeVisuals();
    }   
    void ExitDeleteMode()
    {
        isInDeleteMode = false;
        UpdateDeleteModeVisuals();
    }    
    void UpdateDeleteModeVisuals()
    {
        if (isInDeleteMode)
        {
            deleteModeButton.GetComponent<Image>().color = Color.red; 
        }
        else
        {
            deleteModeButton.GetComponent<Image>().color = Color.white; 
        }
    }
}
