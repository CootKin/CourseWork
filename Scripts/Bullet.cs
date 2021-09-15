using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed; // �������� ����
    public float lifetime; // �����, ������� ���� �� ��������
    public float distance; // ����������, �� ������� ���� �����
    public int damage; // ����, ��������� �����
    public int critChance; // ���� �����
    public LayerMask enemyLayer; // ����, ������� ���� ����� ��������

    private void Start()
    {
        // ����������� ����
        Invoke("DestroyBullet", lifetime); 
    }
    private void Update()
    {
        // ������������ ���� � ������������ ���������
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, enemyLayer); 
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy")) 
            {
                // ��������� ����� � ����
                int rand = Random.Range(1, 101); 
                if (rand <= critChance) hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage * 3); 
                else hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage); 
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void DestroyBullet() // ����������� ����
    {
        Destroy(gameObject);
    }

}
