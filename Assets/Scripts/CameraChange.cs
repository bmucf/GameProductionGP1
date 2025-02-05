using UnityEngine;

public class CameraChange : MonoBehaviour
{
    public GameObject maincamera;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == Player)
        {
            Debug.Log("Entered Trigger");
            if(maincamera.transform.position != cam2.transform.position)
            {
                maincamera.transform.position = cam2.transform.position;
                maincamera.transform.rotation = cam2.transform.rotation;
                Debug.Log("Moved to Camera 2");
            }
            else if(maincamera.transform.position == cam2.transform.position)
            {
                maincamera.transform.position = cam1.transform.position;
                maincamera.transform.rotation = cam1.transform.rotation;
                Debug.Log("Moved to Camera 1");
            }
        }
    }
}
