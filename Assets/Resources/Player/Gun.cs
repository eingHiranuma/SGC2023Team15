using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField]
    Bullet bulletPrefab;
    [SerializeField]
    GameObject handgun;
    Vector3 fireLocalPos;
    Vector3 fireWorldPos;
    private GameObject gunResource = null;

    //Vector3 gunDir;
    private void Start()
    {
        gunResource = Resources.Load("Effect/GunFire") as GameObject;
    }

    public void Shot(Vector3 direction)
    {
        var effect = Instantiate(gunResource,handgun.transform);
        fireLocalPos = handgun.transform.localPosition + new Vector3(0.25f, 0.5f, 0);
        effect.transform.localPosition = fireLocalPos;
        fireWorldPos = transform.TransformPoint(fireLocalPos +  new Vector3(0.5f, -0.5f, 0));

        //gunDir = direction;

        direction = direction.normalized;

        if (direction.x == 0.0f && direction.y == 0.0f && direction.z == 0.0f)
        {
            direction.y = 1.0f;
            float rad = Mathf.Atan2(direction.y, direction.x);
            float deg = Mathf.Rad2Deg * rad;
            Bullet bullet = Instantiate(bulletPrefab, fireWorldPos , Quaternion.Euler(0, 0, deg));
            bullet.transform.SetParent(PlayerManager.Instance.PlayerRoot, false);
            bullet.speed = 9.0f;
            bullet.direction = direction;
        }
        else
        {
            float rad = Mathf.Atan2(direction.y, direction.x);
            float deg = Mathf.Rad2Deg * rad;
            Bullet bullet = Instantiate(bulletPrefab, fireWorldPos, Quaternion.Euler(0, 0, deg));
            bullet.transform.SetParent(PlayerManager.Instance.PlayerRoot, false);
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
