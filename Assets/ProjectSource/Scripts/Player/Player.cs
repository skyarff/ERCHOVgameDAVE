using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : PlayerInteraction
{
    public int money = 0;
    public int health = 100;
    public TextMeshProUGUI textMoney;
    public TextMeshProUGUI textHealth;
    public UnityEngine.UI.Image image;

    public float moveSpeed;
    public float lowSpeed = 8f;
    public float highSpeed = 12f;
    public float jumpForce = 5f;
    public float jumpK = 5f;
    public float lookSensivity = 1.5f;

    private CharacterController characterController;
    private Camera playerCamera;
    private Vector3 moveDirection;
    private float verticalVelocity = 0f;

    private void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        GetMoney(0);
        GetDamage(0);
        image.enabled = false;

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();

        if (characterController.isGrounded)
        {
            
            if (Input.GetKey(KeyCode.LeftShift))
                moveSpeed = highSpeed;
            else 
                moveSpeed = lowSpeed;

            verticalVelocity = -0.5f;
            if (Input.GetButton("Jump"))
                verticalVelocity = jumpForce;
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }


        float horizontalRotation = Input.GetAxis("Mouse X") * lookSensivity;
        float verticalRotation1 = Input.GetAxis("Mouse Y") * lookSensivity;
        transform.Rotate(0f, horizontalRotation, 0f);
        playerCamera.transform.Rotate(-verticalRotation1 * lookSensivity, 0f, 0f);


        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        if ((moveX != 0 || moveZ != 0) && characterController.isGrounded)
        {
            if (!AudioIsActive())
                PlaySound(sounds[2], p3: 0.575f);
        }
        else
        {
            AudioStop();
        }

        
        moveDirection = new Vector3(moveX, 0f, moveZ) * moveSpeed * Time.deltaTime;
        moveDirection.y = verticalVelocity * jumpK * Time.deltaTime;

        characterController.Move(transform.TransformDirection(moveDirection));
    }


    

    public void GetMoney(int cost)
    {
        
        if (cost > 0)
        {
            textMoney.enabled = true;
            PlaySound(sounds[1], destroyed: true);
        }
        money += cost;
        textMoney.text = $"Money: {money}";

        if (money >= 30)
        {
            Debug.Log("Ничесе богатый!");
        }
    }
    public void GetHealth(int value)
    {

        health = health + value > 100 ? 100 : health + value;

        textHealth.text = $"Health: {health}";

        PlaySound(sounds[3], destroyed: true);
    }
    public void GetDamage(int damage)
    {
        
        
        health -= damage;

        if (damage > 0)
        {
            PlaySound(sounds[0], destroyed: true);
            StartCoroutine("RedScreen");
        }

        

        if (health < 1)
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            textHealth.text = $"Health: {health}";
        }


        
    }
    public void GetSpeed(int value, int seconds)
    {
        lowSpeed += value;
        highSpeed += value;
        jumpForce += value / 6;

        PlaySound(sounds[4], destroyed: true);

        StopCoroutine(SpeedUpIsOver(value, 8));
        StartCoroutine(SpeedUpIsOver(value, 8));

        
    }

    public IEnumerator RedScreen()
    {
        image.enabled = true;

        yield return new WaitForSeconds(1);

        image.enabled = false;
    }
    public IEnumerator SpeedUpIsOver(int value, int seconds)
    {


        yield return new WaitForSeconds(seconds);
        lowSpeed -= value;
        highSpeed -= value;
        jumpForce -= value / 6;

    }
}
