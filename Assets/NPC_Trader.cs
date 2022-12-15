using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Trader : MonoBehaviour
{
    private Player player;

    private bool facingLeft = true;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (facingLeft == true && player.transform.position.x > transform.position.x)
        {
            facingLeft = !facingLeft;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (facingLeft == false && player.transform.position.x < transform.position.x)
        {
            facingLeft = !facingLeft;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
