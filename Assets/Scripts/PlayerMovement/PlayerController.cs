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
        private bool _isMove;

        private void Start()
        {
            _mainCamera = Camera.main;
            _targetPosition = transform.position;
        }

        public void OnTap(InputAction.CallbackContext context)
        {
            if (!context.performed || BackpackMenu.IsMenuOpen) return;

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
                _isMove = true;
            }
            else if (hit.collider.TryGetComponent<ItemFactory>(out var building))
            {
                _targetPosition = building.transform.position;
                _isMove = true;
            }
        }

        private void Update()
        {
            if (!_isMove) return;
            
            if (EventSystem.current.IsPointerOverGameObject())
            {
                _targetPosition = transform.position;
                return; 
            }

            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                _isMove = false;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<ItemFactory>(out var building)) return;
            building.CollectResources();
        }
    }
}