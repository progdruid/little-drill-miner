using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public float ParalaxFactor;
    public Vector2 startPoint;

    private void Start()
    {
        startPoint = transform.position;
    }

    private void Update()
    {
        float layerZ = transform.position.z;
        float width = transform.localScale.x;

        Vector2 dist = ((Vector2)Camera.main.transform.position - startPoint) * ParalaxFactor;
        Vector2 camdist = ((Vector2)Camera.main.transform.position - startPoint) * (1f - ParalaxFactor);

        float x = startPoint.x + dist.x;
        float y = startPoint.y + dist.y;

        transform.position = new Vector3(x, y, layerZ);

        if (camdist.x > width)
        {
            startPoint = Camera.main.transform.position;
            transform.position = new Vector3(transform.position.x + width, transform.position.y, transform.position.z);
        }
        else if (camdist.x < - width)
        {
            startPoint = Camera.main.transform.position;
            transform.position = new Vector3(transform.position.x - width, transform.position.y, transform.position.z);
        }
    }
}
