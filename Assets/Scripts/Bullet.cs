using UnityEngine;
using UnityEngine.Events;


public class Bullet : MonoBehaviour
{
    public int damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, .01f);
    }
}
