using GoldFarm.Creatures;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    [SerializeField] private float _throwHold;
    private Vector2 direction;
    private float _throwStartTime;

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
        OnThrowStarted(context);
        
        if (context.canceled && Time.time - _throwStartTime >= _throwHold)
        {
           _hero.SuperThrow();
        } else if (context.canceled)
        {
            _hero.Throw();
        }
    }

    private void OnThrowStarted(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _throwStartTime = Time.time;
        }
    }
}
