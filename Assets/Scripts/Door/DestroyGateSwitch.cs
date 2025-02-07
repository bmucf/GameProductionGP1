using UnityEngine;

public class DestroyGateSwitch : MonoBehaviour
{
    public GameObject SwitchOne;
    public GameObject SwitchTwo;
    public GameObject SwitchThree;
    public GameObject SwitchFour;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RemoveGate();
    }

    public void RemoveGate()
    {
        if (SwitchOne.GetComponent<Switch>().isActivated == true && SwitchTwo.GetComponent<Switch>().isActivated == true && SwitchThree.GetComponent<Switch>().isActivated == true && SwitchFour.GetComponent<Switch>().isActivated == true)
        {
            Destroy(gameObject);
        }
    }
}
