using GoldFarm.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    private Vector2 direction;

    public void Movement(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
        _hero.SetDirection(direction);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hero.Interact();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hero.Attack();
        }
    }
    public void OnThrow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _hero.Throw();
        }
    }



}
