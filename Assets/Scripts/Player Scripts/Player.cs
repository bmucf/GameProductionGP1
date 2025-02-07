using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public bool isWalking = false;
    public float rotationSpeed = 5000f;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    public float health;
    public GameObject arrow;
    public float projectileSpawnDistance = 2f;
    public bool isBlocking = false;
    public bool hasCrossbow = false;
    public GameObject Shield;
    public float shieldSpawnDistance = 1f;
    private GameObject CreatedShield;
    public float arrowShotCooldown = 1f;
    private float shootingTime = 0f;

    public GameObject Bow;
    public float bowSpawnDistance = 1f;
    private GameObject CreatedBow;

    public bool isInvulnerable = false;
    private float invulnerabilityDuration = 1;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (isBlocking == false)
        {
            Movement();
        }
        
        if (hasCrossbow == true)
        {
            Shooting();
        }
        
        Blocking();
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            isWalking = true;
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0, Space.World);
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, -90, 0);
            transform.rotation = rotation;
        }
        else 
        {
            isWalking = false;
        }

        if (Input.GetKey(KeyCode.D))
        {
            isWalking = true;
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0, Space.World);
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 90, 0);
            transform.rotation = rotation;
        }

        if (Input.GetKey(KeyCode.W))
        {
            isWalking = true;
            transform.Translate(0, 0, moveSpeed * Time.deltaTime, Space.World);
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, 0);
            transform.rotation = rotation;
        }

        if (Input.GetKey(KeyCode.S))
        {
            isWalking = true;
            transform.Translate(0, 0, -moveSpeed * Time.deltaTime, Space.World);
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 180, 0);
            transform.rotation = rotation;
        }

        /*float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 Direction = new Vector3(horizontal, 0, vertical);

        if (Direction.magnitude >= 0.1f)
        {
            Quaternion Rotation = Quaternion.LookRotation(Direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, Rotation) < 5f)
            {
                Vector3 move = transform.forward * moveSpeed * Time.deltaTime;
                m_Rigidbody.MovePosition(m_Rigidbody.position + move);
            }
        }

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;*/

        if (isWalking)
        {
            if (!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }
        }

    public void GainHealth()
    {
        if (health < 3)
        {
            health++;
            GameObject.Find("GameManager").GetComponent<GameManager>().HealthText(health);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;
        GameObject.Find("GameManager").GetComponent<GameManager>().HealthText(health);

        if (health <= 0)
        {
            Debug.Log("Player is dead");
        }
        else
        {
            Debug.Log("Player health: " + health);
            StartCoroutine(Invulnerability());
        }
    }

    private IEnumerator Invulnerability()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= shootingTime)
        {

            shootingTime = Time.time + arrowShotCooldown;
            Vector3 spawnPosition = transform.position + (transform.forward * projectileSpawnDistance);
            // Debug.Log("spawn Position: " + spawnPosition + "projectileSpawnDist: " + projectileSpawnDist);
            Instantiate(arrow, spawnPosition, (transform.rotation * arrow.transform.rotation));

            Vector3 bowPosition = transform.position + (transform.forward * bowSpawnDistance);
            GameObject createdBow = Instantiate(Bow, bowPosition, transform.rotation * Bow.transform.rotation);
            Destroy(createdBow, 0.25f);
        }
    }

    void Blocking()
    {
        if (Input.GetButton("Fire2"))
        {

            if(!isBlocking)
            {
                isBlocking = true;
                MakeAShield();
            }
        }
        else
        {
            if(isBlocking)
            {
                isBlocking = false;
                DestroyAShield();
            }
        }

        if(isBlocking == true && m_AudioSource.isPlaying)
        {
            m_AudioSource.Stop();
        }
    }

    void MakeAShield()
    {
        Vector3 shieldPosition = transform.position + transform.up * 2 + transform.forward * shieldSpawnDistance;
        CreatedShield = Instantiate(Shield, shieldPosition, transform.rotation * Shield.transform.rotation);
        CreatedShield.transform.SetParent(transform); 
    }

    void DestroyAShield()
    {
        if (CreatedShield != null)
        {
            Destroy(CreatedShield);
            CreatedShield = null;
        }
    }
}