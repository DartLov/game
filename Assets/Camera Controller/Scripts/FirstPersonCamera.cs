using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private int _lookSpeedMouse;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _jumpHeight;
    [SerializeField] private float _sprint;
    [SerializeField] private float _gravity;

    private Vector2 _rotation;
    private CharacterController _characterController;
    private float _velocity = 0f;

    private void OnValidate()
    {
        _sprint = _moveSpeed >= _sprint ? _moveSpeed * 1.5f : _sprint;
    }

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MouseLook();
        Move();
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * _lookSpeedMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _lookSpeedMouse * Time.deltaTime;

        _rotation.y += mouseX;
        _rotation.x -= mouseY;

        _rotation.x = Mathf.Clamp(_rotation.x, -90, 90);

        _camera.transform.eulerAngles = new Vector3(_rotation.x, _rotation.y, 0);

    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal") * _moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * Time.deltaTime;

        if (_characterController.isGrounded)
        {
            _velocity = 0;
        }

        _velocity += Input.GetKeyDown(KeyCode.Space) ? Mathf.Sqrt(_jumpHeight * _gravity) : -_gravity * Time.deltaTime;
        vertical *= Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift) ? _sprint : _moveSpeed;

        _characterController.Move((_camera.transform.right * horizontal + _camera.transform.forward * vertical + new Vector3(0, _velocity, 0)) * Time.deltaTime);
    }
}
