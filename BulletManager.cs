using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public List<BulletController> bullets = new List<BulletController>();
    public BulletController bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i <= 150; i++)
        {
            BulletController bullet = GameObject.Instantiate<BulletController>(bulletPrefab);
            bullets.Add(bullet);
            bullet.gameObject.SetActive(false);
        }

    }

    public void shootBullet(Vector3 shootPosition)
    {
        for (int i = 0; i <= 150; i++)
        {
            if (!bullets[i].gameObject.activeSelf)
            {
                bullets[i].ShootBullet(shootPosition);
                break;
            }

        }
    }
}
