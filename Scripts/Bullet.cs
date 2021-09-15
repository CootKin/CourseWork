using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed; // Скорость пули
    public float lifetime; // Время, которое пуля не исчезает
    public float distance; // Расстояние, на которое пуля летит
    public int damage; // Урон, наносимый пулей
    public int critChance; // Шанс крита
    public LayerMask enemyLayer; // Слой, который пуля может поражать

    private void Start()
    {
        // Уничтожение пули
        Invoke("DestroyBullet", lifetime); 
    }
    private void Update()
    {
        // Передвижение пули и отслеживание попаданий
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, enemyLayer); 
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.CompareTag("Enemy")) 
            {
                // Нанесение урона и крит
                int rand = Random.Range(1, 101); 
                if (rand <= critChance) hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage * 3); 
                else hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage); 
            }
            DestroyBullet();
        }
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    public void DestroyBullet() // Уничтожение пули
    {
        Destroy(gameObject);
    }

}
