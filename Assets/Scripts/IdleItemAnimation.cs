using System.Collections;
using UnityEngine;

public class IdleItemAnimation : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] float highestPoint;
    [SerializeField] float lowestPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine("IdleUpAndDown", speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerable IdleUpAndDown(int movementSpeed)
    {
        for (float i = lowestPoint; i <= highestPoint; i++)
        {
            transform.position = transform.position + new Vector3(0, i, 0) * movementSpeed;
            yield return null;
        }
    }
}
