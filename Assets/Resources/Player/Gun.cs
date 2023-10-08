using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab;
    //Vector3 gunDir;
    public void Shot(Vector3 direction)
    {
        //gunDir = direction;

        direction = direction.normalized;

        if (direction.x == 0.0f && direction.y == 0.0f && direction.z == 0.0f)
        {
            direction.y = 1.0f;
            float rad = Mathf.Atan2(direction.y, direction.x);
            float deg = Mathf.Rad2Deg * rad;
            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, deg));
            bullet.speed = 9.0f;
            bullet.direction = direction;
        }
        else
        {
            float rad = Mathf.Atan2(direction.y, direction.x);
            float deg = Mathf.Rad2Deg * rad;
            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, deg));
            bullet.speed = 9.0f;
            bullet.direction = direction;
        }

        //Debug.Log(direction);

        
    }

    //private void Update()
    //{
    //    float rad = Mathf.Atan2(gunDir.y, gunDir.x);
    //    float deg = Mathf.Rad2Deg * rad;
    //    if(deg < 0)
    //    {
    //        transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
    //    }
    //    else
    //    {
    //        transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
    //    }
    //    transform.localRotation = Quaternion.Euler(0, 0, deg);
    //}


}
