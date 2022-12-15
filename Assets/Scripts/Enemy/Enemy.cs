using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    #region GameObj
    [Header("Drop")]
    public GameObject potion;
    public GameObject coins;
    #endregion

    public int health;
    public float speed;
    public GameObject deathEffect;

    private Player player;
    private Animator anim;

    private bool facingLeft = true;

    public Global_Parametres contr;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        #region Flip
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
        #endregion

        //destroying
        if (health <= 0)
        {
            OnDeath();
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    //taking damage
    public void TakeDamage(int damage)
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        health -= damage;
    }

    public void OnDeath() {
        Random.InitState((int)Time.time);
        Vector3 randDrop = new Vector3(Random.Range(-1, 2), Random.Range(-1, 2), 0);

        Instantiate(potion, transform.position + randDrop, Quaternion.identity);
        int d = Random.Range(1, 6);
        while (d >= 1)
        {
            Instantiate(coins, transform.position + randDrop, Quaternion.identity);
            d--;
        }
        Destroy(gameObject);
        Destroy(mask);
    }
}
