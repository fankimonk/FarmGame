using UnityEngine;

public class PackOfBags : PackOfSupplies
{
    protected override void Update()
    {
        if (_isPlayerClose && Input.GetKeyDown(KeyCode.E))
            _player.PickUpBagOfGrain();
    }
}
