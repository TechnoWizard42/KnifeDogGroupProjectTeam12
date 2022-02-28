using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Audio;

public class PlayerDogScript : MonoBehaviour
{
    public CharacterController playerController;
    //public Animator goodDog;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public bool macGuffin;
    public bool beginningZone;
    public bool isRunning;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    //public Text boneText;
    public Text macGuffinText;
    public TMPro.TMP_Text betterBoneText;

    private int macGuffinValue;

    public int health { get { return currentHealth; } }
    int currentHealth;
    public int maxHealth = 3;

    public float timeInvincible = 1.0f;
    bool isInvincible;
    float invincibleTimer;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bark;
    [SerializeField] private AudioClip pickedUp;
    [SerializeField] private AudioClip slashed;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        //animator = GetComponent<Animator>();
        //goodDog = GetComponent<Animator>();

        currentHealth = maxHealth;
        macGuffin = false;
        beginningZone = false;
        isRunning = false;

        SetWinScreen();

        SetBoneText();
        betterBoneText.text = "";

        SetLoseScreen();

        macGuffinText.text = $"Magic Bone: {macGuffinValue.ToString()}";
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        playerController.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        playerController.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(bark);
        }
        /*
        if (Input.GetKeyDown(KeyCode.W))
        {
            isRunning = true;
            animator.SetBool("isRunning", true);
        }
        else
        {
            isRunning = false;
            animator.SetBool("isRunning", false);
        }
        */
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bone")
        {
            Debug.Log("hit");
            macGuffin = true;
            audioSource.PlayOneShot(pickedUp);
            macGuffinText.text = "Magic Bone: 1";
            SetWinScreen();
            SetBoneText();
            Destroy(collision.collider.gameObject);
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            Debug.Log("-1");
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            audioSource.PlayOneShot(slashed);
        }

        if (currentHealth == 1)
        {
            SetLoseScreen();
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthScript.instance.SetValue(currentHealth / (float)maxHealth);
    }

    private void SetWinScreen()
    {
        if (macGuffin == true && beginningZone == true)
        {
            SceneManager.LoadScene(3);
        }
    }

    private void SetBoneText()
    {
        if (macGuffin == true)
        {
            betterBoneText.text = "Go  back  to  your  dog  house  with  the  bone!";
        }
    }

    private void SetLoseScreen()
    {
        if (currentHealth == 1)
        {
            SceneManager.LoadScene(4);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        beginningZone = true;
        SetWinScreen();
    }

    private void OnTriggerExit(Collider other)
    {
        beginningZone = false;
    }
    /*
    public void OnRunning(bool isRunning)
    {
        animator.SetBool("isRunning", isRunning);
    }
    */
}
