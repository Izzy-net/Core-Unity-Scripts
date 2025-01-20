using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimGun : MonoBehaviour
{
    [SerializeField] GameObject gun;
    Vector2 mousePosition;
    Vector2 direction;
    void Update()
    {
        HandleGunAim();
    }

    private void HandleGunAim()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (mousePosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;
    }
}
