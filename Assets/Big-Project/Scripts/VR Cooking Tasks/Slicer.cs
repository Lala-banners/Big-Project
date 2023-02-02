using UnityEngine;
using EzySlice;

using UnityEngine.XR.Interaction.Toolkit;

public class Slicer : MonoBehaviour
{
    public Material materialAfterSlice;
    public LayerMask sliceMask;
    public bool isTouched;

    private void Update()
    {
        if (isTouched)
        {
            isTouched = false;

            Collider[] objectsToBeSliced = Physics.OverlapBox(transform.position, new Vector3(1, 0.1f, 0.1f), transform.rotation, sliceMask);

            foreach (Collider objectToBeSliced in objectsToBeSliced)
            {
                SlicedHull slicedObject = SliceObject(objectToBeSliced.gameObject, materialAfterSlice);

                GameObject upperHullGO = slicedObject.CreateUpperHull(objectToBeSliced.gameObject, materialAfterSlice);
                GameObject lowerHullGO = slicedObject.CreateLowerHull(objectToBeSliced.gameObject, materialAfterSlice);

                upperHullGO.transform.position = objectToBeSliced.transform.position;
                lowerHullGO.transform.position = objectToBeSliced.transform.position;

                MakeItPhysical(upperHullGO);
                MakeItPhysical(lowerHullGO);

                Destroy(objectToBeSliced.gameObject);
            }
        }
    }

    private void MakeItPhysical(GameObject obj)
    {
        obj.AddComponent<MeshCollider>().convex = true;
        obj.AddComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
        obj.layer = LayerMask.NameToLayer("Sliceable");
        obj.AddComponent<XRGrabInteractable>();
    }

    private SlicedHull SliceObject(GameObject obj, Material crossSectionMaterial = null)
    {
        return obj.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
