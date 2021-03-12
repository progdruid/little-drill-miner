using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public Camera camera;

    public float ParalaxFactor;
    public Vector2 Travel => (Vector2)camera.transform.position - startPoint;
    public Vector2 startPoint;


    private void Start()
    {
        startPoint = camera.transform.position;
    }

    private void Update()
    {
        Vector2 newPos = startPoint + Travel * ParalaxFactor;

        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);
    }
}
