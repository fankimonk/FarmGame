using UnityEngine;

public class Trough : MonoBehaviour
{
    [SerializeField] private MeshRenderer FoodRenderer;

    public void Fill()
    {
        FoodRenderer.enabled = true;
    }

    public void Empty()
    {
        FoodRenderer.enabled = false;
    }
}
