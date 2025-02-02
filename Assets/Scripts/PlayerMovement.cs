using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Movement speed
    public float rotationSpeed = 700f; // Rotation speed
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;

    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
    }

    
    void Movement()
    {
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 

        Vector3 Direction = new Vector3(horizontal, 0, vertical).normalized;

        if (Direction.magnitude >= 0.1f)
        {
            Quaternion Rotation = Quaternion.LookRotation(Direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotation, rotationSpeed * Time.deltaTime);

            Vector3 move = transform.forward * moveSpeed * Time.deltaTime;
            m_Rigidbody.MovePosition(m_Rigidbody.position + move);
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

}