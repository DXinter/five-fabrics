using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; 
    [SerializeField] private Vector2 xLimits = new(-10f, 10f); 
    [SerializeField] private Vector2 zLimits = new(-10f, 10f); 

    private Vector3 _lastMousePosition;
    private bool _isDragging;

    private void Update()
    {
        HandleTouchInput();
        HandleMouseInput();
    }

    private void HandleTouchInput()
    {
        if (Touchscreen.current == null || Touchscreen.current.primaryTouch.press.isPressed == false)
            return;

        var touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();

        if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began)
        {
            _lastMousePosition = touchPosition;
            _isDragging = true;
        }
        else if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Moved &&
                 _isDragging)
        {
            var delta = (Vector3)(touchPosition - (Vector2)_lastMousePosition);
            MoveCamera(-delta);
            _lastMousePosition = touchPosition;
        }
        else if (Touchscreen.current.primaryTouch.phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended)
        {
            _isDragging = false;
        }
    }

    private void HandleMouseInput()
    {
        if (Mouse.current == null)
            return;

        if (Mouse.current.rightButton.isPressed)
        {
            Vector3 mousePosition = Mouse.current.position.ReadValue();
            var delta = mousePosition - _lastMousePosition;
            
            if (!_isDragging)
            {
                _lastMousePosition = mousePosition;
                _isDragging = true;
            }
            else
            {
                MoveCamera(-delta);
                _lastMousePosition = mousePosition;
            }
        }
        else
        {
            _isDragging = false;
        }
    }

    private void MoveCamera(Vector3 delta)
    {
        var moveDirection = new Vector3(delta.x, 0, delta.y) * moveSpeed * Time.deltaTime;
        var newPosition = transform.position + moveDirection;
        
        newPosition.x = Mathf.Clamp(newPosition.x, xLimits.x, xLimits.y);
        newPosition.z = Mathf.Clamp(newPosition.z, zLimits.x, zLimits.y);

        transform.position = newPosition;
    }
}