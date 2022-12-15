using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidanceMulti : MonoBehaviour
{
    public GameObject bullet;
    public Transform shotPoint;
    public Transform shotPoint1;
    public Transform shotPoint2;

    private float timeBtwShots;
    public float startTimeBtwShots;
    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(bullet, shotPoint.position, transform.rotation);
                Instantiate(bullet, shotPoint1.position, transform.rotation);
                Instantiate(bullet, shotPoint2.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        } else {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
