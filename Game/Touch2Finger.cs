using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch2Finger : MonoBehaviour
{
    float Distance;
    public GameObject table;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 1) Zoom();
        else if (Distance != 0) Distance = 0;
    }
    void Zoom()
    {
        var heading = table.transform.position - transform.position;
        Vector2 finger1 = Input.GetTouch(0).position;
        Vector2 finger2 = Input.GetTouch(1).position;
        if (Distance == 0) Distance = Vector2.Distance(finger1, finger2);
        float delta = Vector2.Distance(finger1, finger2) - Distance;
        if (delta > 0.0f)
        {
            if (heading.sqrMagnitude > 1200f)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, delta + Time.deltaTime);
            }
        }
        if (delta < 0.0f)
        {
            if (heading.sqrMagnitude < 3800f)
            {
                transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, delta + Time.deltaTime);
            }
        }
        Distance = Vector2.Distance(finger1, finger2);
    }
}
