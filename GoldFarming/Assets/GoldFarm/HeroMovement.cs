using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private Hero _hero;
    private Vector2 direction;
    //private float directionY;

    //private void Update()
    //{
    //    Debug.Log(message: "Update");
    //    var horizontal = Input.GetAxis("Horizontal");

    //    _hero.SetDirection(horizontal,vertical);

    //    //if (Input.GetKey(KeyCode.A)){

    //    //    _hero.SetDirection(-1);
    //    //}
    //    //else if (Input.GetKey(KeyCode.D)){

    //    //    _hero.SetDirection(1);
    //    //}
    //    //else
    //    //{
    //    //    _hero.SetDirection(0);

    //    //}

    //}

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


}
