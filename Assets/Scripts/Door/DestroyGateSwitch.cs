using UnityEngine;

public class DestroyGateSwitch : MonoBehaviour
{
    public GameObject SwitchOne;
    public GameObject SwitchTwo;
    public GameObject SwitchThree;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SwitchTwo != null && SwitchThree != null)
        {
            RemoveGate();
        }
        else if (SwitchTwo == null && SwitchThree == null)
        {
            RemoveSoloGate();
        }
    }

    public void RemoveGate()
    {
        if (SwitchOne.GetComponent<Switch>().isActivated == true && SwitchTwo.GetComponent<Switch>().isActivated == true && SwitchThree.GetComponent<Switch>().isActivated == true)
        {
            Destroy(gameObject);
        }
    }

    public void RemoveSoloGate()
    {
        if (SwitchOne.GetComponent<Switch>().isActivated == true)
        {
            Destroy(gameObject);
        }
    }
}
