using UnityEngine;

public class Hen : Animal
{
    [SerializeField] private float RotationCoefficient = 1.0f;

    protected override void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rotationCoefficient = RotationCoefficient;
    }
    
    
}