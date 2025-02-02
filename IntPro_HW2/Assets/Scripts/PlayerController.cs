using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] 
    private int maxHP = 5;
    [SerializeField] 
    private TextMeshProUGUI hpText;
    private int currentHP;


    public float speed = 5;
    public float gravity = -5;

    float velocityY = 0;

    CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        currentHP = maxHP;
        hpText.text = "HP: 5";
    }

    void Update()
    {
        velocityY += gravity * Time.deltaTime;

        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        input = input.normalized;

        Vector3 temp = Vector3.zero;
        if (input.z == 1)
        {
            temp += transform.forward;
        }
        else if (input.z == -1)
        {
            temp += transform.forward * -1;
        }

        if (input.x == 1)
        {
            temp += transform.right;
        }
        else if (input.x == -1)
        {
            temp += transform.right * -1;
        }

        Vector3 velocity = temp * speed;
        velocity.y = velocityY;

        controller.Move(velocity * Time.deltaTime);

        if (controller.isGrounded)
        {
            velocityY = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1); // Player loses 1 HP
            Debug.Log("HP: " + currentHP);
            Destroy(other.gameObject); // Destroy enemy
        }
    }

    void TakeDamage(int damage)
    {
        currentHP = currentHP - damage;
        Debug.Log("Player HP: " + currentHP);
        UpdateHPUI();

        if (currentHP <= 0)
        {
            GameOver();
        }
    }

    void UpdateHPUI()
    {
        hpText.text = "HP: " + currentHP; // Update UI text
    }

    void GameOver()
    {
        Debug.Log("Game Over! Player has lost all HP.");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Restart level
    }
}
