using Environment;
using Items;
using Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace PlayerMovement
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        
        private Camera _mainCamera;
        private Vector3 _targetPosition;
        private bool _isMoving;

        private void Start()
        {
            _mainCamera = Camera.main;
            _targetPosition = transform.position;
        }

        public void OnTap(InputAction.CallbackContext context)
        {
            if (!context.performed || BaseMenu.IsMenuOpen) return;

            Vector2 inputPosition;

            if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.isPressed)
            {
                inputPosition = Touchscreen.current.primaryTouch.position.ReadValue();
            }
            else
            {
                inputPosition = Mouse.current.position.ReadValue();
            }

            var ray = _mainCamera.ScreenPointToRay(inputPosition);
            
            if (!Physics.Raycast(ray, out var hit)) return;
            
            if (hit.collider.TryGetComponent(out Ground ground))
            {
                _targetPosition = hit.point;
                _isMoving = true;
            }
            else if (hit.collider.TryGetComponent<ItemFactory>(out var building))
            {
                _targetPosition = building.transform.position;
                _isMoving = true;
            }
        }

        private void Update()
        {
            if (!_isMoving) return;
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                _targetPosition = transform.position;
                return; 
            }

            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);
            
            if (Mathf.Abs(transform.position.x - _targetPosition.x) < 0.02f && 
                Mathf.Abs(transform.position.z - _targetPosition.z) < 0.02f)
            {
                _targetPosition = transform.position;
                _isMoving = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<ItemFactory>(out var building)) return;
            building.CollectResources();
            
            _isMoving = false;
        }
    }
}