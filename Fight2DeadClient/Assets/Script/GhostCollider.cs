using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCollider : MonoBehaviour
{
    public GameObject enemy; // tham chiếu đến game object của Enemy

    private void Start()
    {
        // Ngăn chặn va chạm giữa Player và tất cả các Collider 2D của Enemy
        Collider2D[] enemyColliders = enemy.GetComponentsInChildren<Collider2D>();
        foreach (Collider2D collider in enemyColliders)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collider);
        }
    }
}

