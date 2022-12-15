using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Guidance : MonoBehaviour
{
    #region Shot
    [Header("Shot")]
    public GameObject bullet;
    public Transform shotPoint;
    private float timeBtwShots;
    public float startTimeBtwShots;
    #endregion

    private Player player;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        //наведение
        Vector3 difference = player.transform.position - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 180f;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
    }

    //анимация + атака
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (timeBtwShots <= 0)
            {
                anim.SetTrigger("attack");       
            }
            else
            {
                timeBtwShots -= Time.deltaTime;
            }
        }
    }

    //спавн пули
    public void OnEnemyAttack()
    {
        Instantiate(bullet, shotPoint.position, transform.rotation);
        timeBtwShots = startTimeBtwShots;
    }
}
