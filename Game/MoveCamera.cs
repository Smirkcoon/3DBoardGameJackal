using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveCamera : MonoBehaviour
{
    public float sensitivity = 1.5F;
    public GameObject go;
    
    private Vector3 MousePos;
    public float wheel_speed = 20f; // скорость зума
    private float movementSpeed = 0.7f;
    public GameObject table;
    private Vector3 defaultposition;
    public Vector3 Multitouch;
    public Vector3 PiratToMove;
    float Distance;
    void Start()
    {        
        defaultposition = new Vector3(0, 28,-9);
        table = GameObject.FindGameObjectWithTag("Table");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 1) Zoom();
        else if (Distance != 0) Distance = 0;

        if (Input.touchCount == 1 )  Swipe();
        
    }
    void Swipe()
    {       
        if (Input.GetTouch(0).deltaPosition != null)
        {
            Vector2 delta = Input.GetTouch(0).deltaPosition;
            if (delta.x > 0)
            {
                
                if (transform.position.x > -25)
                {
                    transform.Translate(Vector3.left * delta.x * movementSpeed * Time.deltaTime);
                }
            }
            if (delta.x < 0)
            {
                if (transform.position.x < 25)
                {
                    transform.Translate(Vector3.left * delta.x * movementSpeed * Time.deltaTime);
                }
            }
            
            if (delta.y > 0)
            {
                if (transform.position.z > -25)
                {
                    transform.Translate(Vector3.down * delta.y * movementSpeed * Time.deltaTime);
                }
            }
            if (delta.y < 0)
            {
                if (transform.position.z < 25)
                {
                    transform.Translate(Vector3.down * delta.y * movementSpeed * Time.deltaTime);
                }
            }

        }       
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
            if (delta > 3)
            {
                delta = 3;
                if (heading.sqrMagnitude > 1200f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, table.transform.position, delta + Time.deltaTime);
                }
            }
            else 
            {
                if (heading.sqrMagnitude > 1200f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, table.transform.position, delta + Time.deltaTime);
                }
            }
        }
        if (delta < 0.0f)
        {
            if (delta < -3)
            {
                delta = -3;                
                if (heading.sqrMagnitude < 3800f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, table.transform.position, delta + Time.deltaTime);
                }
            }
            else 
            {
                if (heading.sqrMagnitude < 3800f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, table.transform.position, delta + Time.deltaTime);
                }
            }
        }
        Distance = Vector2.Distance(finger1, finger2);

    }
    void FixedUpdate()
    {
        if (table != null)
        {
            var heading = table.transform.position - transform.position;
            if (Input.GetKeyDown("space"))
            {
                transform.position = defaultposition;
            }
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.right * horizontalInput * movementSpeed * Time.deltaTime);
            transform.Translate(Vector3.up * verticalInput * movementSpeed * Time.deltaTime);
            MousePos = Input.mousePosition;

            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (scroll > 0.0f)
            {
                if (heading.sqrMagnitude > 1200f)
                {
                    transform.position += transform.forward * scroll * wheel_speed;
                }
            }
            if (scroll < 0.0f)
            {
                if (heading.sqrMagnitude < 3800f)
                {
                    transform.position += transform.forward * scroll * wheel_speed;
                }
            }            
        }
    }
}
