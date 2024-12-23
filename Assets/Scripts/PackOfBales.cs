using UnityEngine;

public class PackOfBales : PackOfSupplies
{
    protected override void Update()
    {
        if (_isPlayerClose && Input.GetKeyDown(KeyCode.E))
            _player.PickUpBaleOfHay();
    }
}
