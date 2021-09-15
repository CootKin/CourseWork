using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject floatingDamage; // Вылетающий урон
    private float timerAttackRate; // Отсчет до начала атаки
    public float attackRate; // Время между атаками
    public int health; // Здоровье врага
    private float currentSpeed; // Скорость передвижения врага
    public int damage; // Урон, который наносит враг
    private float timerStun; // Отсчет времени, на которое остановлен враг
    public float stun; // Время, на которое останавливается враг при попадании в него
    public float normalSpeed; // Обычная скорость врага
    private Player player; // Игрок
    private Animator anim; // Анимации
    private bool facingRight = false; // Повернут ли вправо

    private void Start()
    {
        anim = GetComponent<Animator>(); 
        player = FindObjectOfType<Player>(); 
    }
    private void Update()
    {
        // Стан врага
        if (timerStun <= 0) currentSpeed = normalSpeed; 
        else 
        {
            currentSpeed = 0;
            timerStun -= Time.deltaTime; 
        }

        // Смерть врага
        if (health <= 0)
        {
            player.experience += 1;
            Destroy(gameObject);
        }

        // Движение врага в сторону игрока
        if (transform.position.x - player.transform.position.x < 0)
        {
            if (transform.position.y - player.transform.position.y > 0)
            {
                transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
                transform.Translate(Vector2.down * currentSpeed * Time.deltaTime);
            }
            else if (transform.position.y - player.transform.position.y < 0)
            {
                transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
                transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);
            }
            else if (transform.position.y - player.transform.position.y == 0)
            {
                transform.Translate(Vector2.right * currentSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (transform.position.y - player.transform.position.y > 0)
            {
                transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
                transform.Translate(Vector2.down * currentSpeed * Time.deltaTime);
            }
            else if (transform.position.y - player.transform.position.y < 0)
            {
                transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
                transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);
            }
            else if (transform.position.y - player.transform.position.y == 0)
            {
                transform.Translate(Vector2.left * currentSpeed * Time.deltaTime);
            }
        }

        // Поворот врага
        if (player.transform.position.x - 0.5 > transform.position.x && !facingRight) 
        {
            Flip(); 
            facingRight = true;

        }
        else if (player.transform.position.x + 0.5 < transform.position.x && facingRight)
        {
            Flip(); 
            facingRight = false;
        }
    }

    public void TakeDamage(int damage) // Получение урона
    {
        timerStun = stun; 
        health -= damage; 
        Vector2 damagePos = new Vector2(transform.position.x + 0.25f, transform.position.y + 1f); 
        Instantiate(floatingDamage, damagePos, Quaternion.identity); 
        floatingDamage.GetComponentInChildren<FloatingDamage>().damage = damage;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        // Начало атаки врага
        if (other.CompareTag("Player")) 
        {
            if (timerAttackRate <= 0) 
            {
                anim.SetTrigger("enemyAttack");
            }
            else 
            {
                timerAttackRate -= Time.deltaTime; 
            }
        }
    }

    public void OnEnemyAttack() // Атака врага
    {
        if ((player.transform.position.x - transform.position.x <= 3 && player.transform.position.x - transform.position.x >= -3) && (player.transform.position.y - transform.position.y <= 3 && player.transform.position.y - transform.position.y >= -3)) // Если враг близко к игроку
        {
            player.ChangeHealth(-damage);
        }
        timerAttackRate = attackRate;
    }

    private void Flip() // Разворачивание врага
    {
        Vector3 Scaler = transform.localScale; 
        Scaler.x *= -1; 
        transform.localScale = Scaler; 
    }
}
