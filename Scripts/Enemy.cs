using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject floatingDamage; // ���������� ����
    private float timerAttackRate; // ������ �� ������ �����
    public float attackRate; // ����� ����� �������
    public int health; // �������� �����
    private float currentSpeed; // �������� ������������ �����
    public int damage; // ����, ������� ������� ����
    private float timerStun; // ������ �������, �� ������� ���������� ����
    public float stun; // �����, �� ������� ��������������� ���� ��� ��������� � ����
    public float normalSpeed; // ������� �������� �����
    private Player player; // �����
    private Animator anim; // ��������
    private bool facingRight = false; // �������� �� ������

    private void Start()
    {
        anim = GetComponent<Animator>(); 
        player = FindObjectOfType<Player>(); 
    }
    private void Update()
    {
        // ���� �����
        if (timerStun <= 0) currentSpeed = normalSpeed; 
        else 
        {
            currentSpeed = 0;
            timerStun -= Time.deltaTime; 
        }

        // ������ �����
        if (health <= 0)
        {
            player.experience += 1;
            Destroy(gameObject);
        }

        // �������� ����� � ������� ������
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

        // ������� �����
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

    public void TakeDamage(int damage) // ��������� �����
    {
        timerStun = stun; 
        health -= damage; 
        Vector2 damagePos = new Vector2(transform.position.x + 0.25f, transform.position.y + 1f); 
        Instantiate(floatingDamage, damagePos, Quaternion.identity); 
        floatingDamage.GetComponentInChildren<FloatingDamage>().damage = damage;
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        // ������ ����� �����
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

    public void OnEnemyAttack() // ����� �����
    {
        if ((player.transform.position.x - transform.position.x <= 3 && player.transform.position.x - transform.position.x >= -3) && (player.transform.position.y - transform.position.y <= 3 && player.transform.position.y - transform.position.y >= -3)) // ���� ���� ������ � ������
        {
            player.ChangeHealth(-damage);
        }
        timerAttackRate = attackRate;
    }

    private void Flip() // �������������� �����
    {
        Vector3 Scaler = transform.localScale; 
        Scaler.x *= -1; 
        transform.localScale = Scaler; 
    }
}
