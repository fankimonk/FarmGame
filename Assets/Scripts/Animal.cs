using System;
using System.Collections;
using UnityEngine;

public class Animal : MonoBehaviour
{
    [SerializeField] protected float MoveSpeed = 2.0f;

    [SerializeField] protected Animator Animator;
    
    protected Rigidbody _rb;

    protected float _rotationCoefficient = 1.0f;

    private bool _isMoving = false;
    
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rb.angularVelocity = Vector3.zero;
        
        if (!_isMoving)
            _rb.linearVelocity = Vector3.zero;
    }

    public IEnumerator Move(Vector3 moveTo)
    {
        _isMoving = true;
        Animator.SetBool("IsWalking", _isMoving);
        
        while (Vector3.Distance(transform.position, moveTo) > 0.01f)
        {
            var movementDirection = (moveTo - transform.position).normalized;

            _rb.MovePosition(transform.position + MoveSpeed * Time.deltaTime * movementDirection);
            transform.rotation = Quaternion.LookRotation(movementDirection * _rotationCoefficient);

            yield return null;
        }
        
        _isMoving = false;
        Animator.SetBool("IsWalking", _isMoving);
        
        yield return new WaitForSeconds(2.0f);
    }
}