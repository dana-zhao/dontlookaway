using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private float _playerInputH;
    private float _playerInputV;
    private float _rotationInputH;
    private float _rotationInputV;
    private Vector3 _userRot;
    private bool _userJumped;
    private bool _isGrounded;

    [SerializeField]
    public AudioSource pickupsfx;

    private const float _horizontalSpeed = 2.0f;
    private const float _verticalSpeed = 2.0f;
    private const float _inputScale = 0.2f;
    private const float _jumpScale = 3.0f;
    private int objectsPickedUp = 0;

    private Rigidbody _rigidbody;
    private Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerInputV = Input.GetAxis("Vertical");
        _playerInputH = Input.GetAxis("Horizontal");
        _rotationInputH = Input.GetAxis("Mouse X");
        _rotationInputV = Input.GetAxis("Mouse Y");
        _userJumped = Input.GetButton("Jump");
    }

    private void FixedUpdate()
    {
        _userRot = _transform.rotation.eulerAngles;
        _userRot += new Vector3(0, _rotationInputH * _horizontalSpeed, 0);

        _transform.rotation = Quaternion.Euler(_userRot);
        _rigidbody.velocity += _transform.forward * _playerInputV * _inputScale;
        _rigidbody.velocity += _transform.right * _playerInputH * _inputScale;

        if (_isGrounded && _userJumped)
        {
            _userJumped = false;
            _rigidbody.AddForce(Vector3.up * _jumpScale, ForceMode.Impulse);
            
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = true;
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _isGrounded = false;
        }
    }

    // What to do when we collide with an object marked for pickup with the "Pick Up" tag
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            objectsPickedUp += 1;
            Debug.Log("Objects picked up: " + objectsPickedUp.ToString());
        }
        pickupsfx.Play();

    }
}
