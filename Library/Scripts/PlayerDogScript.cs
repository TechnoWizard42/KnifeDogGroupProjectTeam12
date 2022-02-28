using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDogScript : MonoBehaviour
{
    public CharacterController playerController;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public bool macGuffin;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    public Text winText;
    public Text loseText;
    public Text macGuffinText;

    private int macGuffinValue;

    // Start is called before the first frame update
    void Start()
    {
        macGuffin = false;
        SetWinText();
        winText.text = "";
        SetLoseText();
        loseText.text = "";
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Bone")
        {
            Debug.Log("hit");
            Destroy(collision.collider.gameObject);
            macGuffin = true;
            macGuffinText.text = $"Magic Bone: {macGuffinValue.ToString()}";
        }
    }

    private void SetWinText()
    {

    }

    private void SetLoseText()
    {

    }
}
