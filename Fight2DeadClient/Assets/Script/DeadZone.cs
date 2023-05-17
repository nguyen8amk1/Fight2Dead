using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    public Collider2D Left;
    public Collider2D Right;
    public Collider2D Bottom;
    public Collider2D Top;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu nhân vật va chạm với vùng chết
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Player1"))
            {
                Player1 player1 = collision.gameObject.GetComponent<Player1>();

                if (player1 != null)
                {
                   player1.Die();
                     
                }
                else{
                    Debug.Log("Error");
                }
            }

        }
    }
}
