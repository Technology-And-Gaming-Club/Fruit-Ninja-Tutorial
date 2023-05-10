using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour
{
    private TouchControls inputs;
    private Camera mainCamera;

    public delegate void StartTouch(Vector2 position);
    public event StartTouch onTouchStarted;
    public delegate void EndTouch(Vector2 position);
    public event EndTouch onTouchEnded;
    public delegate void DeltaTouch(Vector2 deltaPosition);
    public event DeltaTouch onTouchDelta;


    public static InputManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        inputs = new TouchControls();
    }

    private void Start()
    {

        inputs.TouchMap.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        inputs.TouchMap.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
        inputs.TouchMap.PrimaryDelta.performed += ctx => DeltaTouchPrimary(ctx);

        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (onTouchStarted != null)
        {
            onTouchStarted(PrimaryPosition());
        }
    }

    void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (onTouchEnded != null)
        {
            onTouchEnded(PrimaryPosition());
        }
    }

    void DeltaTouchPrimary(InputAction.CallbackContext context)
    {
        if (onTouchDelta != null)
        {
            onTouchDelta(inputs.TouchMap.PrimaryDelta.ReadValue<Vector2>());
        }
    }

    public Vector3 PrimaryPosition()
    {
        return Utils.ScreenToWorld(mainCamera, inputs.TouchMap.PrimaryPosition.ReadValue<Vector2>());
    }
}
