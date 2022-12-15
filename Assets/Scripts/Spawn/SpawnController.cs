using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    #region Spawn
    public GameObject Enemy;
    public Global_Parametres contr;
    public double timer;

    [Header("SpawnPoints")]
    public Transform spawnpoint;
    public Transform spawnpoint1;
    public Transform spawnpoint2;
    #endregion

    private void Update()
    { 
        if (contr.Controller == 3)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
            Instantiate(Enemy, spawnpoint.position, Quaternion.identity);
            Instantiate(Enemy, spawnpoint1.position, Quaternion.identity);
            Instantiate(Enemy, spawnpoint2.position, Quaternion.identity);
            contr.Controller += 1;
    }
}
