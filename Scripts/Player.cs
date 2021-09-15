using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed; // ��������
    private Rigidbody2D rb; // ���� ������
    private Vector2 moveInput; // �������, � ������� ��������
    private Vector2 moveVelocity; // �������� �������� � �����-�� �����������
    private Animator anim; // ��������
    public float health; // �������� ������
    public Text healthDisplay; // ����������� ��������
    public Text timeDisplay; // ����������� �����
    public Text experienceDisplay; // ����������� �����
    private float time = 0f; // ����
    public int experience; // ����
    private bool facingRight = true; // �������� �� �������� ������

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
    }
    void Update()
    {
        // ������������ ���������
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
        moveVelocity = moveInput.normalized * speed;
        
        if (moveInput.x == 0 && moveInput.y == 0) anim.SetBool("isRunning", false); 
        else anim.SetBool("isRunning", true); 

        if (!facingRight && moveInput.x > 0) Flip(); 
        else if (facingRight && moveInput.x < 0) Flip(); 

        // ������ ���������
        if (health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 

        // ����� ���������� � ������� � �����
        time += Time.deltaTime;
        timeDisplay.text = "Time: " + System.Convert.ToInt32(time); 
        experienceDisplay.text = "XP: " + experience; 

        // ����� �� ����
        if (Input.GetKey("escape")) Application.Quit(); 
    }

    private void FixedUpdate()
    {
        // ������������ ���������
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Flip() // �������������� ���������
    {
        facingRight = !facingRight; 
        Vector3 Scaler = transform.localScale; 
        Scaler.x *= -1; 
        transform.localScale = Scaler; 
    }

    public void ChangeHealth(int healthValue) // ��������� �������� ���������
    {
        health += healthValue; 
        healthDisplay.text = "HP: " + health;
    }
}
