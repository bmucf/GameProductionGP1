using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isActivated = false;
    public float DeactivateTime = 5f;

    public Material activeMaterial; 
    public Material notActiveMaterial;
    private Renderer objectRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();  // Get the Renderer component
        if (objectRenderer != null && notActiveMaterial != null)
        {
            objectRenderer.material = notActiveMaterial;  // Set initial material
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        if(!isActivated)
        {
            Debug.Log("Activated Switch");
            isActivated = true;
            Invoke("Deactivate", DeactivateTime);
            ChangeMaterial(activeMaterial);
        }
        
    }

    public void Deactivate()
    {
        Debug.Log("Deactivated Switch");
        isActivated = false;
        ChangeMaterial(notActiveMaterial);
    }

    void ChangeMaterial(Material newMaterial)
    {
        if (objectRenderer != null && newMaterial != null)
        {
            objectRenderer.material = newMaterial;
        }
    }
}
