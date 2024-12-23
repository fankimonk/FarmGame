using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed = 5f;

    [SerializeField] private GameObject BaleOfHay;
    [SerializeField] private GameObject BagOfGrain;

    [SerializeField] private Transform BalePoint;
    [SerializeField] private Transform BagPoint;
    
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private Vector3 CameraOffset;
    
    [SerializeField] private Animator Animator;
    
    private Rigidbody _rb;

    private bool _hasBagOfGrain = false;
    private bool _hasBaleOfHay = false;

    private bool _isAnimationPlaying = false;
    
    public Supply Supply = null;
    
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
            if (!_isAnimationPlaying)
            {
                Animator.SetBool("IsWalking", true);
                _isAnimationPlaying = true;
            }

            _rb.MovePosition(transform.position + MoveSpeed * Time.deltaTime * movementDirection);
            transform.rotation = Quaternion.LookRotation(movementDirection);
        }
        else
        {
            if (_isAnimationPlaying)
            {
                Animator.SetBool("IsWalking", false);
                _isAnimationPlaying = false;
            }
            
            _rb.linearVelocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }
    }

    public void PickUpBaleOfHay()
    {
        if (_hasBaleOfHay) return;
        
        if (_hasBagOfGrain) DropBagOfGrain();
        
        Debug.Log("Picked up bale of hay");
        
        var supplyGo = Instantiate(BaleOfHay, BalePoint.position, Quaternion.Euler(90, 0, 90), transform);
        Supply = supplyGo.GetComponent<Supply>();
        
        _hasBaleOfHay = true;
        Animator.SetBool("HasBale", true);
    }

    public void DropBaleOfHay()
    {
        Debug.Log("Dropped bale of hay");
        
        if (_hasBaleOfHay) Destroy(Supply.gameObject);
        _hasBaleOfHay = false;
        
        Animator.SetBool("HasBale", false);
    }
    
    public void PickUpBagOfGrain()
    {
        if (_hasBagOfGrain) return;
        
        if (_hasBaleOfHay) DropBaleOfHay();

        Debug.Log("Picked up bag of grain");
        
        var supplyGo = Instantiate(BagOfGrain, BagPoint.position, Quaternion.Euler(-90, 0, 0), transform);
        Supply = supplyGo.GetComponent<Supply>();
        
        _hasBagOfGrain = true;
        Animator.SetBool("HasBag", true);
    }

    public void DropBagOfGrain()
    {
        Debug.Log("Dropped bag of grain");
        
        if (_hasBagOfGrain) Destroy(Supply.gameObject);
        _hasBagOfGrain = false;
        
        Animator.SetBool("HasBag", false);
    }
    
    private void LateUpdate()
    {
        CameraTransform.transform.position = transform.position + CameraOffset;
    }
}
