using UnityEngine;
using BreadAndButter.Mobile; //Butter on Toast

public class MobileTest : MonoBehaviour
{
    [SerializeField]
    private bool testJoystick = false;
    [SerializeField]
    private bool testSwipe = false;

    // Start is called before the first frame update
    void Start()
    {
        MobileInput.Initialise();
    }

    // Update is called once per frame
    void Update()
    {
        if (testJoystick)
        {
            //Test joystick
            transform.position += transform.forward * MobileInput.GetJoystickAxis(JoystickAxis.Vertical) * Time.deltaTime;
            transform.position += transform.right * MobileInput.GetJoystickAxis(JoystickAxis.Horizontal) * Time.deltaTime;
        }

        if(testSwipe)
        {
            //Platform directives - similar to regions but as an if statement
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR //Platform specific
            //Mobile Input HERE

#else
            //Touch start emulation
            if(Input.GetMouseButtonDown(0)) //Left click
            {

            }

            //Touch update emulation
            if (Input.GetMouseButtonDown(0))
            {

            }

            //Touch end emulation
            if (Input.GetMouseButtonUp(0))
            {

            }

            //Touch position emulator
            Vector2 touchPos = Input.mousePosition;
#endif

        }
    }
}
