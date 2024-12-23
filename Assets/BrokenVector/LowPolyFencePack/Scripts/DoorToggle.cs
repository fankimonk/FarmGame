using System;
using UnityEngine;

namespace BrokenVector.LowPolyFencePack
{
    /// <summary>
    /// This class toggles the door animation.
    /// The gameobject of this script has to have the DoorController script which needs an Animator component
    /// and some kind of Collider which detects your mouse click applied.
    /// </summary>
    [RequireComponent(typeof(DoorController))]
	public class DoorToggle : MonoBehaviour
    {
        private DoorController _doorController;

        private BoxCollider _boxCollider;

        private bool _isPlayerClose = false;
        
        private void Awake()
        {
            _doorController = GetComponent<DoorController>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        private void Update()
        {
	        if (_isPlayerClose && Input.GetKeyDown(KeyCode.E))
	        {
		        _doorController.ToggleDoor();
		        _boxCollider.enabled = !_boxCollider.enabled;
	        }
        }

        private void OnTriggerEnter(Collider other)
	    {
		    if (other.TryGetComponent<Player>(out Player player))
			    _isPlayerClose = true;
	    }

	    private void OnTriggerExit(Collider other)
	    {
		    if (other.TryGetComponent<Player>(out Player player))
			    _isPlayerClose = false;
	    }
    }
}