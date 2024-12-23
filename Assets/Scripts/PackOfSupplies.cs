using UnityEngine;

public abstract class PackOfSupplies : MonoBehaviour
{
    protected bool _isPlayerClose = false;

    protected Player _player;

    protected abstract void Update();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out _player)) return;
        
        _isPlayerClose = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent(out _player)) return;
        
        _isPlayerClose = false;
        _player = null;
    }
}
