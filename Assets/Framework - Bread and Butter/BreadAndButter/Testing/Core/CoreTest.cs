using UnityEngine;
using BreadAndButter;

public class CoreTest : MonoSingleton<CoreTest>
{
    [SerializeField]
    private RunnableTest runnableTest;

    [SerializeField]
    private bool testEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        CreateInstance();
        FlagAsPersistant();

        RunnableHelper.Setup(ref runnableTest, gameObject, "Sally", new Vector3(1, 1, 1)); //Should set runnableTest variable
    }

    // Update is called once per frame
    void Update()
    {
        runnableTest.Enabled = testEnabled;
        RunnableHelper.Run(ref runnableTest, gameObject);
    }
}
