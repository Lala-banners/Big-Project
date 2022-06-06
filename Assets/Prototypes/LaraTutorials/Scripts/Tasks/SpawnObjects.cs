using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SpawnObject();
    }

    public void SpawnObject()
    {
        //To spawn specific pooled object with the tagged Fuse
        GameObject fuseObject = ObjectPool.instance.GetPooledObject("");
        if (fuseObject != null)
        {
            fuseObject.transform.position = ObjectPool.instance.spawnPos.transform.position;
            fuseObject.transform.rotation = ObjectPool.instance.spawnPos.transform.rotation;
            fuseObject.SetActive(true);
        }
    }
}
