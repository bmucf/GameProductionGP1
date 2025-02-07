using UnityEngine;

public class DestroyDoor : MonoBehaviour
{
    public GameObject TargetOne;
    public GameObject TargetTwo;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RemoveDoor();
    }

    public void RemoveDoor()
    {
        if(TargetOne.GetComponent<Target>().isActivated == true && TargetTwo.GetComponent<Target>().isActivated == true)
        {
            Destroy(gameObject);
        }

    }
}
