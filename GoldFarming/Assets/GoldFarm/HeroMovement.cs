using System.Collections;
using System.Collections.Generic;
using GoldFarm;
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
        if (context.canceled)
        {
            _hero.Interact();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            _hero.Attack();
        }
    }


}
