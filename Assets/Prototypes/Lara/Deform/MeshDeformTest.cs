using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshDeformTest : MonoBehaviour
{
    //Need access to the mesh, then original vertex positions can be extracted
    private Mesh deformingMesh;
    private Vector3[] originalVertices, displacedVertices;
    
    //Vertices move as the mesh is deformed - store the velocity of each vertex
    private Vector3[] vertexVelocities;

    private void Start()
    {
        //Get the mesh & vertices and copy original to the displaced vertices
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];

        for(int i = 0; i < originalVertices.Length; i++)
        {
            displacedVertices[i] = originalVertices[i];
        }

        vertexVelocities = new Vector3[originalVertices.Length];
    }
}
