using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Transform gunBarrel;
    [SerializeField] GameObject prefabBullet;
    //private int shotTimer = 0;
    private int shotPower = 40;
    public void Shoot()
    {
        // Instantiate bullet at gunbarrel position, in player rotation
        GameObject bullet = GameObject.Instantiate(prefabBullet, gunBarrel.position, gunBarrel.transform.rotation);
        Vector3 shootDirection = gameObject.transform.forward;

        bullet.GetComponent<Rigidbody>().linearVelocity = shootDirection * shotPower;
    }

}
