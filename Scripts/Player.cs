using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed; // Скорость
    private Rigidbody2D rb; // Тело игрока
    private Vector2 moveInput; // Сторона, в которую движемся
    private Vector2 moveVelocity; // Итоговая скорость в каком-то направлении
    private Animator anim; // Анимации
    public float health; // Здоровье игрока
    public Text healthDisplay; // Отображение здоровья
    public Text timeDisplay; // Отображение счета
    public Text experienceDisplay; // Отображение опыта
    private float time = 0f; // Счет
    public int experience; // Опыт
    private bool facingRight = true; // Повернут ли персонаж вправо

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
    }
    void Update()
    {
        // Передвижение персонажа
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); 
        moveVelocity = moveInput.normalized * speed;
        
        if (moveInput.x == 0 && moveInput.y == 0) anim.SetBool("isRunning", false); 
        else anim.SetBool("isRunning", true); 

        if (!facingRight && moveInput.x > 0) Flip(); 
        else if (facingRight && moveInput.x < 0) Flip(); 

        // Смерть персонажа
        if (health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 

        // Вывод информации о времени и опыте
        time += Time.deltaTime;
        timeDisplay.text = "Time: " + System.Convert.ToInt32(time); 
        experienceDisplay.text = "XP: " + experience; 

        // Выход из игры
        if (Input.GetKey("escape")) Application.Quit(); 
    }

    private void FixedUpdate()
    {
        // Передвижение персонажа
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }

    private void Flip() // Разворачивание персонажа
    {
        facingRight = !facingRight; 
        Vector3 Scaler = transform.localScale; 
        Scaler.x *= -1; 
        transform.localScale = Scaler; 
    }

    public void ChangeHealth(int healthValue) // Изменение здоровья персонажа
    {
        health += healthValue; 
        healthDisplay.text = "HP: " + health;
    }
}
