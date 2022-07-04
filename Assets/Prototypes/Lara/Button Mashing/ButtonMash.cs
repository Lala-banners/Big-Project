using System;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

/// <summary>
/// Interaction that requires multiple taps (press and release within tapTime) spaced no more than tapDelay seconds apart.
/// </summary>
public class ButtonMash : MonoBehaviour
{
    private LalaTestControls controls;

    [SerializeField] private float mash;
    public float mashDelay = 0.5f;

    private bool hasPressed;
    private bool timerHasStarted = false;
    
    private void Awake()
    {
        mash = mashDelay;
        
        //Created controls object
        controls = new LalaTestControls();

        controls.GameplayActions.EscapeGrip.performed += EscapeGrip;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            timerHasStarted = true;
        }

        if(timerHasStarted)
        {
            //Decrease the timer
            mash -= Time.deltaTime;

            if(Input.GetKeyDown(KeyCode.Space) && !hasPressed)
            {
                hasPressed = true;
                mash = mashDelay;
            }
            else if(Input.GetKeyUp(KeyCode.Space))
            {
                hasPressed = false;
            }
            if(mash <= 0)
            {
                print("FAILED!");
                timerHasStarted = false;
            }
        }
    }


    public void EscapeGrip(InputAction.CallbackContext ctx)
    {
        //If action is tap, start the countdown timer
        /*if(ctx.interaction is TapInteraction)
        {
            timerHasStarted = true;
            print("Tapping");
        }*/
        
        //timerHasStarted = true;
        if(ctx.performed)
        {
            timerHasStarted = true;
            print("Timer started");
        }

        //Once the timer has started
        /*if(timerHasStarted)
        {
            //If mashing the button, activate button press bool
            // and reset the timer to the delay (start time).
            if(ctx.performed && !hasPressed)
            {
                Debug.Log(ctx);
                hasPressed = true;
                mash = mashDelay;
            }
            //If the mashing was aborted or too slow
            else if(ctx.canceled)
            {
                //Stop the button pressing and decrease the timer again
                hasPressed = false;
            }

            //If the timer reaches 0, game over
            if(mash <= 0)
            {
                print("Failed!");
            }
        }*/
    }

    private void OnEnable()
    {
        controls.GameplayActions.Enable();
    }

    private void OnDisable()
    {
        controls.GameplayActions.Disable();
    }
}
