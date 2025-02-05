using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    public GameObject Heart;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Break()
    {
        Destroy(gameObject);
        Debug.Log("Destroyed!");
        Instantiate(Heart, transform.position, Quaternion.identity);
        Debug.Log("Spawned Heart");
    }
}
