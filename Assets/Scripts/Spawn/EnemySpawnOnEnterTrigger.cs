using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnOnEnterTrigger : MonoBehaviour
{
    #region Spawn
    public Global_Parametres contr;

    [Header("Spawn")]
    public GameObject Enemy;
    public Transform spawnpoint;
    public Transform spawnpoint1;
    public Transform spawnpoint2;
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(Enemy, spawnpoint.position, Quaternion.identity);
        Instantiate(Enemy, spawnpoint1.position, Quaternion.identity);
        Instantiate(Enemy, spawnpoint2.position, Quaternion.identity);
        contr.Controller = 0;
        Destroy(gameObject);
    }
}
