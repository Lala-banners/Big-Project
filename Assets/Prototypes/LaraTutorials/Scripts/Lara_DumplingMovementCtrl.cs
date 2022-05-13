using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/// <summary>
/// Script test for PC/Dumpling movement
/// </summary>
public class Lara_DumplingMovementCtrl : NetworkBehaviour
{
    //Movement 
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private CharacterController controller = null;

    //Stop players from spamming keys
    private Vector2 prevInput;

    private Lara_Controls controls;
    private Lara_Controls Controls
    {
        get
        {
            if(controls != null) { return controls; }
            return controls = new Lara_Controls();
        }
    }

    public override void OnStartAuthority()
    {
        enabled = true;

        Controls.Lara_Player.Move.performed += ctx => SetMovement(ctx.ReadValue<Vector2>());
        //Reset movement when stop moving
        Controls.Lara_Player.Move.canceled += ctx => ResetMovement();
    }

    //Enable & Disable input
    [ClientCallback]
    private void OnEnable() => Controls.Enable();

    [ClientCallback]
    private void OnDisable() => Controls.Disable();
    //Client authorative movement (will be fixed as server authorative movement later)
    [ClientCallback]
    private void Update() => Move();

    [Client]
    private void SetMovement(Vector2 movement) => prevInput = movement;

    [Client]
    private void ResetMovement() => prevInput = Vector2.zero;

    //Get inputs of player and handle moving
    [Client]
    private void Move()
    {
        Vector3 right = controller.transform.right;
        Vector3 forward = controller.transform.forward;
        right.y = 0f;
        forward.y = 0f;

        //Moves player left and right, backwards and forward
        Vector3 movement = right.normalized * prevInput.x + forward.normalized * prevInput.y;

        controller.Move(movement * moveSpeed * Time.deltaTime);
    }
}
