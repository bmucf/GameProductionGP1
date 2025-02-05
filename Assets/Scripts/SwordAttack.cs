using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float cooldown = 0.5f;
    public float spinHoldKeyTime = 1.5f;
    public Transform slashAttackPoint;
    public Transform spinAttackPoint;
    public float slashAttackRange = 1f;
    public int slashAttackDmg = 2;
    public float spinAttackRange = 2f;
    public int spinAttackDmg = 1;

    public LayerMask attackable;

    private float nextAttackTime = 0f;
    private float buttonHoldTime = 0f;
    private bool isHoldingAttack = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                isHoldingAttack = true;
                buttonHoldTime = Time.time;
            }
            if (Input.GetButtonUp("Fire1"))
            {
                isHoldingAttack = false;

                // Check if slash or spin
                if (Time.time - buttonHoldTime >= spinHoldKeyTime)
                {
                    SpinAttack();
                    nextAttackTime = Time.time + cooldown;
                }
                else
                {
                    Slash();
                    nextAttackTime = Time.time + cooldown;
                }
            }
            if (isHoldingAttack)
            {
                Debug.Log("Holding Attack");
            }
        }
    }

    void Slash()
    {
        Debug.Log("Slash");
        Collider[] hitObjects = Physics.OverlapSphere(slashAttackPoint.position, slashAttackRange, attackable);

        foreach (Collider obj in hitObjects)
        {
            // Enemy Check
           if (obj.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                enemy.TakeDamage(slashAttackDmg);
            }

            // Switch Check
            if (obj.TryGetComponent<Switch>(out Switch switchComponent))
            {
                switchComponent.Activate();
            }
        }
    }

    void SpinAttack()
    {
        Debug.Log("Spin");
        Collider[] hitObjects = Physics.OverlapSphere(spinAttackPoint.position, spinAttackRange, attackable);

        foreach (Collider obj in hitObjects)
        {
            // Enemy Check
            if (obj.TryGetComponent<EnemyHealth>(out EnemyHealth enemy))
            {
                enemy.TakeDamage(spinAttackDmg);
            }

            //Switch Check
            if (obj.TryGetComponent<Switch>(out Switch switchComponent))
            {
                switchComponent.Activate();
            }
        }
    }

    //Draw Gizmos for Debugging
    void OnDrawGizmosSelected()
    {
        if (slashAttackPoint != null)
        {
            // slash gizmo
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(slashAttackPoint.position, slashAttackRange);
        }

        if (spinAttackPoint != null)
        {
            // spin gizmo
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(spinAttackPoint.position, spinAttackRange);
        }
    }
}
