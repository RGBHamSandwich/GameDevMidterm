using UnityEngine;

public class Leaf : MonoBehaviour
{
    void Update()
    {
        if (transform.position.y < -5)
        {
            Destroy(gameObject);
        }
    }
}
