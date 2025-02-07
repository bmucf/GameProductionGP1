using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 10f;
    public bool inContact;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0, Space.World);
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, -90, 0);
            transform.rotation = rotation;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0, Space.World);
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 90, 0);
            transform.rotation = rotation;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, moveSpeed * Time.deltaTime, Space.World);
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = rotation;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 180, 0);
            transform.rotation = rotation;
        }
    }

    void pickUpObject()
    {
        if (Input.GetMouseButton(1))
        {
            ;
        }
        else
        {
            
        }
    }
}