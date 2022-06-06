using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObject()
    {
        //To spawn specific pooled object with the tagged Fuse
        GameObject fuseObject = ObjectPool.instance.GetPooledObject("Fuse");
        if (fuseObject != null)
        {
            fuseObject.transform.position = ObjectPool.instance.spawnPos.transform.position;
            fuseObject.transform.rotation = ObjectPool.instance.spawnPos.transform.rotation;
            fuseObject.SetActive(true);
        }
    }
}
