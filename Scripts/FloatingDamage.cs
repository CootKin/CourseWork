using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingDamage : MonoBehaviour
{
    [HideInInspector] public float damage; // Урон, также прячем его в инспекторе
    private TextMesh textMesh; // Текст, который вылетает из врагов

    private void Start()
    {
        textMesh = GetComponent<TextMesh>(); 
        textMesh.text = "-" + damage; 
    }

    public void OnAnimationOver() // Уничтожение текста после окончания анимации
    {
        Destroy(gameObject); 
    }
}
