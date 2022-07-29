using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PCController : MonoBehaviour
{

    Vector2 iMovement;
    Vector2 iTurn;
    Vector2 mouseTurn;
    public float moveSpeed = .1f;
    public float rotateSpeed = 60f;
    Vector3 jump;
    public float jumpForce = 2f;
    public bool isGrounded;
    //bool mouse = false;
    public Rigidbody rb;
    GameObject cameraMan;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);

        cameraMan = this.transform.Find("Camera").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
       Move();
    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }
    private void Move()
    {
        cameraMan.transform.Rotate(new Vector3(0f, iTurn.x, 0f), Space.World);
        cameraMan.transform.Rotate(new Vector3(-iTurn.y, 0f, 0f), Space.Self);
        
        transform.Rotate(mouseTurn.x * rotateSpeed * Time.deltaTime, 0, mouseTurn.y * rotateSpeed * Time.deltaTime);

        Vector3 movement = new Vector3(iMovement.x, 0, iMovement.y) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    private void OnMove(InputValue value)
    {
        Debug.Log("Left Stick Input!");
        iMovement = value.Get<Vector2>();
    }

    private void OnJump()
    {
        Debug.Log("Button Input South!");
        rb.AddForce(jump * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    private void OnDash()
    {
        Debug.Log("Button Input West!");
       // Vector3 movement = new Vector3(iMovement.x, 0, iMovement.y) * moveSpeed * Time.deltaTime;
       // transform.Translate(movement);
    }

    private void OnTurnMouse(InputAction.CallbackContext context)
    {
        mouseTurn = context.ReadValue<Vector2>();
    }

    
    private void OnTurn(InputValue value)
    {
        iTurn = value.Get<Vector2>();
    }
    
}
