using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]

public class SC_FPSController : MonoBehaviour
{
    [Header("Imports")]
    [SerializeField]
    private BP_GameManager _gameManager;
    [SerializeField]
    private Animation _anim;
    [SerializeField]
    private Camera _playerCamera;
    [SerializeField]
    private PlayerInput _pInput;

    [Header("Values")]

    public bool _canMove = true;
    [SerializeField]
    private int _playerId;
    [SerializeField]
    private float _walkingSpeed = 7.5f;
    [SerializeField]
    private float _runningSpeed = 11.5f;
    [SerializeField]
    private float _jumpSpeed = 8.0f;
    [SerializeField]
    private float _gravity = 20.0f;
    [SerializeField]
    private float _lookSpeed = 2.0f;
    [SerializeField]
    private float _lookXLimit = 45.0f;
    [SerializeField,Range(0.05f,3)]
    private float _timeBeforeShot;

    [SerializeField, Range(0.1f, 3)]
    private float _lagDelay;

    private Vector3 _movementSum;

    private bool _fireReady = true;

    private bool _lagResetting = true;

    private enum STATUS {
        Lag,
        Dead,
        Wallhack,
        RespawnPower,
        Innof,
        Neutral
    }

    [SerializeField]
    private STATUS _currentStatus;

    private CharacterController _characterController;
    private Vector3 _moveDirection = Vector3.zero;
    private float _rotationX = 0;
    
    void Start()
    {
        //anim = gameObject.GetComponent<Animation>();

        //Vector3 direction = Vector3.forward;

        _characterController = GetComponent<CharacterController>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {

        Fire();
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = _canMove ? (isRunning ? _runningSpeed : _walkingSpeed) * _pInput.actions["Move"].ReadValue<Vector2>().y : 0;
        float curSpeedY = _canMove ? (isRunning ? _runningSpeed : _walkingSpeed) * _pInput.actions["Move"].ReadValue<Vector2>().x : 0;
        float movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (_pInput.actions["Jump"].WasPressedThisFrame() && _canMove && _characterController.isGrounded)
        {
            _moveDirection.y = _jumpSpeed;
        }
        else
        {
            _moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        if (_pInput.actions["Power"].WasPressedThisFrame())
        {
            if(_currentStatus == STATUS.RespawnPower)
            {
                RespawnPower();   
            }
        }

        //Debug.Log(BP_GameManager.Instance.P1.transform.position);

        

        // Player and Camera rotation
        if (_canMove)
        {
            ToggleCharacterController(true);
            if (_currentStatus != STATUS.Lag)
                // Move the controller
                _characterController.Move(_moveDirection * Time.deltaTime);
            else
                Lag(_moveDirection);

            float lookSpeedUpdated = _lookSpeed / 10;
            _rotationX += -_pInput.actions["Look"].ReadValue<Vector2>().y * lookSpeedUpdated;
            _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);
            _playerCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, _pInput.actions["Look"].ReadValue<Vector2>().x * lookSpeedUpdated, 0);
        }
        else
        {
            ToggleCharacterController(false);
        }
    }

    private void ToggleCharacterController(bool b)
    {
        if (_characterController.enabled == b) return;
        _characterController.enabled = b;
        Debug.Log("This is true");
    }

    private void Fire()
    {
        if (!_fireReady || _currentStatus == STATUS.Innof)
            return;

        if (_pInput.actions["Fire"].WasPressedThisFrame())
        {
            //Debug.Log("SUUUUUUUUUU");
            Vector3 characterPosition = _playerCamera.transform.position;
            Quaternion characterRotation = _playerCamera.transform.rotation;

            // Crée un rayon depuis le centre du personnage orienté vers l'avant
            Ray ray = new Ray(characterPosition, characterRotation * Vector3.forward);

            RaycastHit hit;


            if (Physics.Raycast(ray, out hit)) // Effectue le raycast et vérifie s'il y a une collision
            {
                Debug.DrawRay(ray.origin, ray.direction * 100f, Color.green, 1f);
                if (hit.collider.CompareTag("P1")) // Vérifie si l'objet touché a le tag "Player"
                {
                    _gameManager.P1Die = true;
                    Scores.Instance.IncreaseScore(1);
                    Debug.Log("Objet touché : " + hit.collider.gameObject.name); // Affiche le nom de l'objet touché dans la console
                    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
                }

                if (hit.collider.CompareTag("P2")) // Vérifie si l'objet touché a le tag "Player"
                {
                    _gameManager.P2Die = true;
                    Scores.Instance.IncreaseScore(0);
                    Debug.Log("Objet touché : " + hit.collider.gameObject.name); // Affiche le nom de l'objet touché dans la console
                    Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red, 1f);
                }


            }
            StartCoroutine(ShotCooldown());
        }
    }

    private IEnumerator ShotCooldown()
    {
        _fireReady = false;
        yield return new WaitForSeconds(_timeBeforeShot);
        _fireReady = true;
    }

    private void Lag(Vector3 pos)
    {
        if (_lagResetting)
        {
            StartCoroutine(LagTimer());
            _characterController.Move(_movementSum * Time.deltaTime);
            _movementSum = Vector3.zero;
            Debug.Log("Lag resetting");
        }

        _movementSum += pos;
    }

    private IEnumerator LagTimer()
    {
        _lagResetting = false;
        yield return new WaitForSeconds(_lagDelay);
        _lagResetting = true;
    }

    private void RespawnPower()
    {
        if (_playerId == 1)
        {
            BP_GameManager.Instance.P2.GetComponent<CharacterController>().enabled = false;
            BP_GameManager.Instance.P2.transform.position = BP_GameManager.Instance.SpawnP2.transform.position;
            BP_GameManager.Instance.P2.GetComponent<CharacterController>().enabled = true;
        }
        else
        {
            BP_GameManager.Instance.P1.GetComponent<CharacterController>().enabled = false;
            BP_GameManager.Instance.P1.transform.position = BP_GameManager.Instance.SpawnP1.transform.position;
            BP_GameManager.Instance.P1.GetComponent<CharacterController>().enabled = true;
        }
    }
}