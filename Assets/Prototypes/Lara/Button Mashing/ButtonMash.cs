using System;

using TMPro;

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

/// <summary>
/// Interaction that requires multiple taps (press and release within tapTime) spaced no more than tapDelay seconds apart.
/// </summary>
public class ButtonMash : MonoBehaviour
{
    private LalaTestControls controls;

    [Tooltip("The maximum time (in seconds) allowed to elapse between pressing and releasing a control for it to register as a tap.")]
    public float tapTime;

    [Tooltip("The maximum delay (in seconds) allowed between each tap. If this time is exceeded, the multi-tap is canceled.")]
    public float tapDelay;

    [Tooltip("How many taps need to be performed in succession. Two means double-tap, three means triple-tap, and so on.")]
    public int tapCount;

    private bool hasStarted;
    private bool hasPressed;
    public TMP_Text timeText;
    
    private void Awake()
    {
        tapTime = tapDelay;
        
        //Created controls object
        controls = new LalaTestControls();

        //Do something when mash A action start
        controls.GameplayActions.EscapeGrip.performed += ctx => EscapeVrGrip();
    }
    
    /// <summary>
    /// Performed - button press down
    /// </summary>
    private void EscapeVrGrip()
    {
        //Add to tap count
        tapCount++;

        if(tapCount >= 10)
        {
            print("Tap count reached!");
        }
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
