using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float cooldown = 0.5f;
    public float spinHoldKeyTime = 1f;
    public Transform slashAttackPoint;
    public Transform spinAttackPoint;
    public float slashAttackRange = 1.5f;
    public int slashAttackDmg = 2;
    public float spinAttackRange = 2f;
    public int spinAttackDmg = 1;

    public LayerMask attackable;

    public GameObject swordPrefab;
    public float swordSpawnDistance = .5f;
    public float swordLifetime = .1f;
    public int numSpinSwords = 8;
    public float spinRadius = 1f;

    private float nextAttackTime = 0f;
    private float buttonHoldTime = 0f;

    void Update()
    {
        if (Time.time >= nextAttackTime)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                buttonHoldTime = Time.time;
            }

            if (Input.GetButtonUp("Fire1"))
            {
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
        }
    }

    void Slash()
    {
        Vector3 spawnPosition = slashAttackPoint.position + slashAttackPoint.forward * swordSpawnDistance;

        GameObject slashSword = Instantiate(swordPrefab, spawnPosition, Quaternion.identity);

        slashSword.transform.rotation = Quaternion.LookRotation(transform.forward);

        Destroy(slashSword, swordLifetime);

        Collider[] hitObjects = Physics.OverlapSphere(slashAttackPoint.position, slashAttackRange, attackable);

        foreach (Collider obj in hitObjects)
        {
            if (obj.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement))
            {
                enemyMovement.TakeDamage(slashAttackDmg);
            }

            if (obj.TryGetComponent<EnemyShooter>(out EnemyShooter enemyShooter))
            {
                enemyShooter.TakeDamage(slashAttackDmg);
            }

            if (obj.TryGetComponent<Boss>(out Boss boss))
            {
                boss.TakeDamage(slashAttackDmg);
            }

            if (obj.TryGetComponent<Switch>(out Switch switchComponent))
            {
                switchComponent.Activate();
            }

            if (obj.TryGetComponent<BreakableObject>(out BreakableObject breakable))
            {
                breakable.Break();
            }
        }
    }

    void SpinAttack()
    {
        for (int i = 0; i < numSpinSwords; i++)
        {
            float angle = i * Mathf.PI * 2f / numSpinSwords;
            Vector3 spawnPosition = spinAttackPoint.position + new Vector3(Mathf.Cos(angle) * spinRadius, 0f, Mathf.Sin(angle) * spinRadius);

            GameObject spinSword = Instantiate(swordPrefab, spawnPosition, Quaternion.identity);

            Vector3 directionToCenter = (spinAttackPoint.position - spawnPosition).normalized;

            Quaternion rotation = Quaternion.LookRotation(directionToCenter) * Quaternion.Euler(0f, 180f, 0f);

            spinSword.transform.rotation = rotation;

            Destroy(spinSword, swordLifetime);
        }

        Collider[] hitObjects = Physics.OverlapSphere(spinAttackPoint.position, spinAttackRange, attackable);

            foreach (Collider obj in hitObjects)
            {
                if (obj.TryGetComponent<EnemyMovement>(out EnemyMovement enemyMovement))
                {
                    enemyMovement.TakeDamage(spinAttackDmg);
                }

                if (obj.TryGetComponent<EnemyShooter>(out EnemyShooter enemyShooter))
                {
                    enemyShooter.TakeDamage(spinAttackDmg);
                }

                if (obj.TryGetComponent<Boss>(out Boss boss))
                {
                    boss.TakeDamage(spinAttackDmg);
                }

                if (obj.TryGetComponent<Switch>(out Switch switchComponent))
                {
                    switchComponent.Activate();
                }

                if (obj.TryGetComponent<BreakableObject>(out BreakableObject breakable))
                {
                    breakable.Break();
                }
            }
    }

    // Draw Gizmos for debugging
    void OnDrawGizmosSelected()
    {
        if (slashAttackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(slashAttackPoint.position, slashAttackRange);
        }

        if (spinAttackPoint != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(spinAttackPoint.position, spinAttackRange);
        }
    }
}