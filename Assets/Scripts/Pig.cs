using UnityEngine;

public class Pig : Animal
{
    [SerializeField] private float RotationCoefficient = 1.0f;

    protected override void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rotationCoefficient = RotationCoefficient;
    }
}