using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MultiPlayer.MainGameLogic
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 20.0f;

        Vector3 direction;
        Rigidbody rigid;

        private void Awake()
        {
            rigid = GetComponent<Rigidbody>();
        }

        public void OnFire(Vector3 dir)
        {
            direction = dir;
        }

        private void Update()
        {
            rigid.velocity = direction * moveSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                other.GetComponentInParent<NetworkPlayerController>().OnHitBullet(gameObject);
                gameObject.SetActive(false);
            }
            if (other.tag == "Environment")
            {
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "BattleArea")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
