using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    #region movement
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    #endregion

    [Header("Health")]
    #region Health
    [SerializeField] public int health;
    private int MaxHealth;
    [SerializeField] private Image[] healthImage;
    [SerializeField] private Sprite[] healthSprite;
    public GameObject deathEffect;
    #endregion

    [Header("Weapons")]
    #region Weapon
    public List<GameObject> unlockedWeapons;
    public GameObject[] allWeapons;
    public Image weaponIcon;
    #endregion

    #region Coins
    private int coins = 0;
    public TMP_Text coinsText;
    #endregion

    #region Vectors
    Vector2 movement;
    Vector3 dash;
    #endregion

    #region Private
    private bool facingRight = true;
    private Animator anim;
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>();
        MaxHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        #region Health
        for (int i = 0; i < MaxHealth; i++)
        {
            if (i < health)
            {
                healthImage[i].sprite = healthSprite[0];
            }
            else if (i == health)
                healthImage[i].sprite = healthSprite[1];
            else if (i > health)
                healthImage[i].sprite = healthSprite[2];
        }
        #endregion

        if ((movement.x != 0) | (movement.y != 0))
        {
            anim.SetBool("IsRunning", true);
        }
        else
        {
            anim.SetBool("IsRunning", false);
        }

        if (health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        #region Dash
        if (Input.GetKeyDown(KeyCode.LeftShift)) 
        {
            anim.SetBool("IsDashing", true);
            Dash();
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("IsDashing", false);
        }
        #endregion

        //����� ������
        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            SwitchWeapon();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        #region Flip
        if (facingRight == false && movement.x > 0)
        {
            facingRight = !facingRight;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (facingRight == true && movement.x < 0)
        {
            facingRight = !facingRight;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        #endregion
    }

    public void ChangeHealth(int healthValue)
    {
        health += healthValue;
    }

    public void SwitchWeapon()
    {
        for (int i = 0; i < unlockedWeapons.Count; i++)
        {
            if (unlockedWeapons[i].activeInHierarchy)
            {
                unlockedWeapons[i].SetActive(false);
                if (i != 0)
                {
                    unlockedWeapons[i - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[i - 1].GetComponent<SpriteRenderer>().sprite;
                }
                else
                {
                    unlockedWeapons[unlockedWeapons.Count - 1].SetActive(true);
                    weaponIcon.sprite = unlockedWeapons[unlockedWeapons.Count - 1].GetComponent<SpriteRenderer>().sprite;
                }
                weaponIcon.SetNativeSize();
                break;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        health -= damage;
    }

    private void Dash()
    {
        #region Dash
        if ((movement.x > 0) && (movement.y > 0))
        {
            dash = Vector3.up + Vector3.right;
            transform.position += dash;
        }
        else if ((movement.x > 0) && (movement.y == 0))
        {
            dash = Vector3.right;
            transform.position += dash;
        }
        else if ((movement.x == 0) && (movement.y > 0))
        {
            dash = Vector3.up;
            transform.position += dash;
        }
        else if ((movement.x > 0) && (movement.y < 0))
        {
            dash = Vector3.down + Vector3.right;
            transform.position += dash;
        }
        else if ((movement.x < 0) && (movement.y < 0))
        {
            dash = Vector3.down + Vector3.left;
            transform.position += dash;
        }
        else if ((movement.x < 0) && (movement.y == 0))
        {
            dash = Vector3.left;
            transform.position += dash;
        }
        else if ((movement.x == 0) && (movement.y < 0))
        {
            dash = Vector3.down;
            transform.position += dash;
        }
        else if ((movement.x < 0) && (movement.y > 0))
        {
            dash = Vector3.up + Vector3.left;
            transform.position += dash;
        }
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            coins += 5;
            coinsText.text = coins.ToString();
            Destroy(collision.gameObject);
        }

        else if (collision.gameObject.tag == "Potion")
        {
            health += 1;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "ShopWeapon")
        {
            if (Input.GetMouseButton(1))
            {
                if (coins >= 10)
                {
                    coins = coins - 10;
                    coinsText.text = coins.ToString();
                    for (int i = 0; i < allWeapons.Length; i++)
                    {
                        if (collision.name == allWeapons[i].name)
                        {
                            unlockedWeapons.Add(allWeapons[i]);
                        }
                    }
                    SwitchWeapon();
                    Destroy(collision.gameObject);
                }
            }
        }

        if (collision.gameObject.tag == "Weapon")
        {
            for (int i = 0; i < allWeapons.Length; i++)
            {
                if (collision.name == allWeapons[i].name)
                {
                    unlockedWeapons.Add(allWeapons[i]);
                }
            }
            SwitchWeapon();
            Destroy(collision.gameObject);
        }
    }
}
