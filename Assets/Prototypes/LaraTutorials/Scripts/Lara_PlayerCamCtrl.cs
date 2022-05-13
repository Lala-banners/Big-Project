using UnityEngine;
using Mirror;
using Cinemachine;

public class Lara_PlayerCamCtrl : NetworkBehaviour
{
    [Header("Camera")]
    [SerializeField, Tooltip("Limits of camera movement")] private Vector2 maxFollowOffset = new Vector2(-1f, 6f);
    [SerializeField, Tooltip("How fast the camera rotates")] private Vector2 camVelocity = new Vector2(4f, 0.25f);
    [SerializeField, Tooltip("Reference to Dumpling rotation")] private Transform playerTransform = null;
    [SerializeField, Tooltip("Reference to virtual camera")] private CinemachineVirtualCamera virtualcamera = null;

    private Lara_Controls lControls;
    private Lara_Controls LControls
    {
        get
        {
            if(lControls != null) { return lControls; }
            return lControls = new Lara_Controls();
        }
    }

    private CinemachineTransposer transposer;

    /// <summary>
    /// Ensures each player has own camera.
    /// </summary>
    public override void OnStartAuthority()
    {
        //Find transposer element
        transposer = virtualcamera.GetCinemachineComponent<CinemachineTransposer>();

        //Activate camera
        virtualcamera.gameObject.SetActive(true);

        enabled = true;

        //Take in context and get the value of mouse rotation (Vector2)
        LControls.Lara_Player.Look.performed += ctx => Look(ctx.ReadValue<Vector2>());
    }

    //Only gets called on client - enables and disables controls 
    [ClientCallback]
    private void OnEnable() => LControls.Enable();

    [ClientCallback]
    private void OnDisable() => LControls.Disable();

    /// <summary>
    /// New input system functionality for rotating player with mouse
    /// </summary>
    private void Look(Vector2 lookAxis)
    {
        float deltaTime = Time.deltaTime;

        //Limits of camera rotation to not go outside max follow offset
        float followOffset = Mathf.Clamp(
            transposer.m_FollowOffset.y - (lookAxis.y * camVelocity.y * deltaTime),
            maxFollowOffset.x,
            maxFollowOffset.y);

        transposer.m_FollowOffset.y = followOffset;

        //Rotate player according to mouse controls
        playerTransform.Rotate(0f, lookAxis.x * camVelocity.x * deltaTime, 0f);
    }
}
