using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.TryGetComponent<IPickup>(out var pickup))
        {
            pickup.PickMeUp();
        }
    }
}
