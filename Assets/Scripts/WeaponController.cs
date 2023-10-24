using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MultiPlayer.MainGameLogic;

public class WeaponController : MonoBehaviour
{
    // 1.
    [SerializeField] private GameObject prefBullet;
    [SerializeField] private Transform firePoint;

    List<GameObject> bulletList = new List<GameObject>();

    public void OnFire()
    {
        bool isFire = false;
        foreach(var bullet in bulletList)
        {
            if (!bullet.activeSelf) // 발사되지 않은 총알이 있을 때
            {
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = firePoint.rotation;

                bullet.GetComponent<Bullet>().OnFire(GetBulletDirection());
                bullet.SetActive(true);

                isFire = true;
                break;
            }
        }

        if (!isFire)
        {
            GameObject bullet = Instantiate(prefBullet, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().OnFire(GetBulletDirection());
            bullet.SetActive(true);

            bulletList.Add(bullet);
        }
    }

    private Vector3 GetBulletDirection()
    {
        return firePoint.forward;
        //return (firePoint.transform.position - transform.position).normalized;
    }

    private void OnDestroy()
    {
        foreach(var bullet in bulletList)
        {
            Destroy(bullet);
        }
        bulletList.Clear();
    }
}
