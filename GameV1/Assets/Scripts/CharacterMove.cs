using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    //private float _playerInputH;
    //private float _playerInputV;
    private float _rotationInputH;
    private float _rotationInputV;
    private Vector3 _userRot;
    //private bool _userJumped;
    //private bool _isGrounded;
    private float _minRotV;
    private float _maxRotV;

    [SerializeField]
    public AudioSource sfx_PlayerObjectBump;

    private const float _horizontalSpeed = 1.0f;
    private const float _verticalSpeed = 1.0f;
    //private const float _inputScale = 0.2f;
    //private const float _jumpScale = 3.0f;
    private const float _maxTiltAngle = 50.0f;
    private int objectsPickedUp = 0;

    public float Speed = 1f;
    public float JumpHeight = 1.5f;
    private const float Gravity = -20.0f;
    //public LayerMask Ground;

    private CharacterController _controller;
    private Vector3 _velocity;
    private Vector3 _moveDirection;
    private bool _isGrounded = true;

    //private Rigidbody _rigidbody;
    private Transform _transform;
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        //_rigidbody = GetComponent<Rigidbody>();
        _transform = camera.transform;
        _userRot = _transform.rotation.eulerAngles;
        _minRotV = _userRot.x - _maxTiltAngle;
        _maxRotV = _userRot.x + _maxTiltAngle;

        _controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        //_playerInputV = Input.GetAxis("Vertical");
        //_playerInputH = Input.GetAxis("Horizontal");
        _rotationInputH = Input.GetAxis("Mouse X");
        _rotationInputV = Input.GetAxis("Mouse Y");
        //_userJumped = Input.GetButton("Jump");

        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0f;
        }


        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _moveDirection = _transform.TransformDirection(move);
        _controller.Move(_moveDirection * Time.deltaTime * Speed);


        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y += Mathf.Sqrt(JumpHeight * -2.0f * Gravity);
        }


        _velocity.y += Gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);

        if (Time.timeScale != 0) {
            _userRot += new Vector3(0, _rotationInputH * _horizontalSpeed, 0);
            _userRot -= new Vector3(_rotationInputV * _verticalSpeed, 0, 0);
            _userRot.x = Mathf.Clamp(_userRot.x, _minRotV, _maxRotV);

            _transform.rotation = Quaternion.Euler(_userRot);
        }
        
    }

    private void FixedUpdate()
    {

        
        //_rigidbody.MovePosition(_rigidbody.position + _transform.forward * _playerInputV * _inputScale * Time.fixedDeltaTime + _transform.right * _playerInputH * _inputScale * Time.fixedDeltaTime)
        //_rigidbody.velocity += _transform.forward * _playerInputV * _inputScale;
        //_rigidbody.velocity += _transform.right * _playerInputH * _inputScale;

        //if (_isGrounded && _userJumped)
        //{
            //_userJumped = false;
            //_rigidbody.AddForce(Vector3.up * _jumpScale, ForceMode.Impulse);
            
        //}
    }

    //void OnCollisionEnter(Collision other)
    //{
        //if (other.gameObject.tag == "Ground")
        //{
            //_isGrounded = true;
        //}
    //}

    //void OnCollisionExit(Collision other)
    //{
        //if (other.gameObject.tag == "Ground")
        //{
            //_isGrounded = false;
        //}
    //}

    // What to do when we collide with an object marked for pickup with the "Pick Up" tag
    // void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Pick Up"))
    //     {
    //         other.gameObject.SetActive(false);
    //         objectsPickedUp += 1;
    //         Debug.Log("Objects picked up: " + objectsPickedUp.ToString());
    //     }
        

    // }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            sfx_PlayerObjectBump.pitch = (Random.Range(0.6f, 0.9f));
            sfx_PlayerObjectBump.Play();
        }
    }
}
