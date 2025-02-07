using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 5000f;
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    public float health;
    public GameObject arrow;
    public float projectileSpawnDist;
    public bool isBlocking = false;

    public bool isInvulnerable = false;
    private float invulnerabilityDuration = 1;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
        Shooting();
        Blocking();
    }

    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
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
        bool isWalking = hasHorizontalInput || hasVerticalInput;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 spawnPosition = transform.position + (transform.forward * projectileSpawnDist);
            Instantiate(arrow, spawnPosition, (transform.rotation * arrow.transform.rotation));
        }
    }

    void Blocking()
    {
        if (Input.GetButton("Fire2"))
        {
            isBlocking = true;
        }
        else
        {
            isBlocking = false;
        }
    }
}