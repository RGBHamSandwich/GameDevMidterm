using UnityEngine;

public class Leaf : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Destroy the leaf object upon collision
        Destroy(gameObject);
    }
}