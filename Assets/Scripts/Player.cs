using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5f;

    [SerializeField] private Transform CameraTransform;
    [SerializeField] private Vector3 CameraOffset;
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");  
        
        var movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        if (movementDirection != Vector3.zero)
        {
            _rb.MovePosition(transform.position + MoveSpeed * Time.deltaTime * movementDirection);
            transform.rotation = Quaternion.LookRotation(movementDirection);
        }
    }

    private void LateUpdate()
    {
        CameraTransform.transform.position = transform.position + CameraOffset;
    }
}
