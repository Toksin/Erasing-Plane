using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AirplaneController : MonoBehaviour
{
    public static AirplaneController Instance { get; private set; }
    public LineRenderer trajectoryLine;
    public Rigidbody2D rb;

    public event EventHandler onAirplanePush;

    [SerializeField] private float maxStretch = 3.0f;
    [SerializeField] private float launchPower = 4f;
    [SerializeField] private float friction = 0.99f;
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float boostPower = 5f;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button boostButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button startButton;

    public MaterialSelection.MaterialData currentMaterial;

    private Vector2 initialTouchPosition;
    [SerializeField] private bool isDragging = false;
    private bool isLaunched = false;
    private bool canInteract = true;
    private bool isRotatingLeft = false;
    private bool isRotatingRight = false;
    private bool isBoosting = false;

    public GameObject[] playerSkins;
    [SerializeField] private float[] boostPowers;
    private int activeSkinIndex;

    private Vector2 launchDirection;
    private float launchStrength;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb.gravityScale = 0f;

        AddButtonListeners(leftButton, PointerDownLeft, PointerUpLeft);
        AddButtonListeners(boostButton, PointerDownBoost, PointerUpBoost);
        AddButtonListeners(rightButton, PointerDownRight, PointerUpRight);

        activeSkinIndex = SaveSystem.Load().activeSkinIndex;
        SetActiveSkin(activeSkinIndex);      
    }

  

    private void SetActiveSkin(int index)
    {
        if (index < 0 || index >= playerSkins.Length)
        {
            Debug.LogWarning("Invalid skin index.");
            return;
        }

        for (int i = 0; i < playerSkins.Length; i++)
        {
            playerSkins[i].SetActive(i == index);
        }

        activeSkinIndex = index;
        boostPower = boostPowers[activeSkinIndex]; // ������������� �������� boostPower ��� �������� �����
        Debug.Log($"Skin {index} is now active with boostPower = {boostPower}");
    }

    void Update()
    {
        if (canInteract)
        {
            if (Input.touchCount > 0)
            {
                HandleTouchInput();
            }


            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    initialTouchPosition = mousePosition;
                }
            }

            if (isDragging && Input.GetMouseButton(0))
            {
                Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 stretchVector = initialTouchPosition - currentMousePosition;

                if (stretchVector.magnitude > maxStretch)
                {
                    stretchVector = stretchVector.normalized * maxStretch;
                }

                DrawTrajectory(stretchVector);

                float angle = Mathf.Atan2(stretchVector.y, stretchVector.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if (isDragging && Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                LaunchObject(initialTouchPosition - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
        }

        if (isLaunched)
        {
            ReduceSpeedOverTime();
        }

        if (isRotatingLeft)
        {
            RotateLeft();
        }

        if (isRotatingRight)
        {
            RotateRight();
        }

        if (isBoosting)
        {
            Boost();
        }
    }

    private void HandleTouchInput()
    {
       
            if (Input.touchCount == 0)
            {
                return;
            }

            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                    initialTouchPosition = touchPosition;
                }
            }

            if (isDragging && (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary))
            {
                Vector2 currentTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
                Vector2 stretchVector = initialTouchPosition - currentTouchPosition;

                if (stretchVector.magnitude > maxStretch)
                {
                    stretchVector = stretchVector.normalized * maxStretch;
                }

                DrawTrajectory(stretchVector);

                float angle = Mathf.Atan2(stretchVector.y, stretchVector.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }

            if (isDragging && touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
                Vector2 stretchVector = initialTouchPosition - (Vector2)Camera.main.ScreenToWorldPoint(touch.position);

                if (stretchVector.magnitude > 0)
                {
                    LaunchObject(stretchVector);
                }
            }
        
    }

    private void LaunchObject(Vector2 stretchVector)
    {
        launchDirection = stretchVector.normalized;
        launchStrength = stretchVector.magnitude * launchPower;


        rb.velocity = launchDirection * launchStrength;

        ClearTrajectory();

        isLaunched = true;
        canInteract = false;
        onAirplanePush?.Invoke(this, EventArgs.Empty);
    }

    private void ReduceSpeedOverTime()
    {
        rb.velocity *= friction;

        if (rb.velocity.magnitude < minSpeed)
        {
            rb.velocity = rb.velocity.normalized * minSpeed;
        }
    }

    private void DrawTrajectory(Vector2 stretchVector)
    {
        trajectoryLine.positionCount = 2;
        trajectoryLine.SetPosition(0, transform.position);
        trajectoryLine.SetPosition(1, (Vector2)transform.position + stretchVector);
    }

    private void ClearTrajectory()
    {
        trajectoryLine.positionCount = 0;
    }

    private void RotateLeft()
    {
        if (isLaunched)
        {
            transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            UpdateVelocity();
        }
    }

    private void RotateRight()
    {
        if (isLaunched)
        {
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
            UpdateVelocity();
        }
    }
    private void Boost()
    {
        if (isLaunched)
        {
            rb.velocity += (Vector2)transform.right * boostPower * Time.deltaTime;
        }
    }
    private void UpdateVelocity()
    {
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity = Quaternion.Euler(0, 0, transform.eulerAngles.z) * Vector2.right * rb.velocity.magnitude;
        }
    }
    private void AddButtonListeners(Button button, Action onPointerDown, Action onPointerUp)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryDown = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        entryDown.callback.AddListener((data) => { onPointerDown(); });
        trigger.triggers.Add(entryDown);

        EventTrigger.Entry entryUp = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        entryUp.callback.AddListener((data) => { onPointerUp(); });
        trigger.triggers.Add(entryUp);
    }
    private void PointerDownLeft() { isRotatingLeft = true; }
    private void PointerUpLeft() { isRotatingLeft = false; }

    private void PointerDownRight() { isRotatingRight = true; }
    private void PointerUpRight() { isRotatingRight = false; }

    private void PointerDownBoost() { isBoosting = true; }
    private void PointerUpBoost() { isBoosting = false; }

    public void SetMaterial(MaterialSelection.MaterialData material)
    {
        currentMaterial = material;
        AdjustSpeedBasedOnMaterial();
        Debug.Log("materialChanged");
    }

    private void AdjustSpeedBasedOnMaterial()
    {
        if (currentMaterial.materialType == null)
        {
            return;
        }

        float emptyMaterialLaunchPower = 4f;
        float emptyMaterialFriction = 0.99f;

        if (currentMaterial.materialType.Equals("Empty"))
        {
            launchPower = emptyMaterialLaunchPower;
            friction = emptyMaterialFriction;
        }
        else
        {
            launchPower = 4f / currentMaterial.weight;
            friction = 0.99f / currentMaterial.weight;
        }

        Debug.Log($"Material Changed: launchPower = {launchPower}, friction = {friction}");
    }


}