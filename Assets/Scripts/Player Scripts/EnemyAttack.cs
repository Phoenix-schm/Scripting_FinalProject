using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //private float hurtTimer;

    //[Tooltip("The maximum amount of time before the player can be hurt again")]
    //public float maxHurtTimer = 3f;

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider == null) return;

    //    // if is hit by an enemy, get hit and start a timer before being allowed to be hit again
    //    if (collision.gameObject.TryGetComponent<PlayerInteraction>(out PlayerInteraction player) && hurtTimer == 0)
    //    {
    //        PlayerVariables playerVariables = player.playerVariables;
    //        playerVariables.health -= player.enemyData.damage;
    //        hurtTimer += Time.deltaTime;

    //        if (hurtTimer > maxHurtTimer)
    //        {
    //            hurtTimer = 0;
    //        }
    //    }
    //}
}
