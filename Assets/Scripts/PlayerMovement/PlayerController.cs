using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovement
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        private Camera _mainCamera;
        private Vector3 _targetPosition;
        private bool _isMove = false;

        private void Start()
        {
            _mainCamera = Camera.main;
            _targetPosition = transform.position;
        }

        public void OnTap(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

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

            if (Physics.Raycast(ray, out var hit) && hit.collider.TryGetComponent(out Ground ground))
            {
                _targetPosition = hit.point;
                _isMove = true;
            }
        }

        private void Update()
        {
            if (!_isMove) return;

            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPosition) < 0.1f)
            {
                _isMove = false;
            }
        }
    }
}