using System;
using UnityEngine;

public class Mill : MonoBehaviour
{
    [SerializeField] private Transform MillstoneTransform;

    [SerializeField] private float RotationSpeed = 5f;

    private void Update()
    {
        MillstoneTransform.Rotate(Vector3.right, RotationSpeed * Time.deltaTime);
    }
}
