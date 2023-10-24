using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerController : MonoBehaviour
{
    // 1. 움직임 관련
    [Header("Movement Setting")]
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private float turnRate = 360.0f;
    [SerializeField] private float jumpPower = 5.0f;

    private Rigidbody rigid;

    // 2. 총알 발사
    [Header("Equip")]
    [SerializeField] WeaponController equipWeapon;

    private PhotonView photonView; 
    private void Awake()
    {
        // 1.
        rigid = GetComponent<Rigidbody>();
        photonView=GetComponent<PhotonView>();

    }

    // Start is called before the first frame update
    void Start()
    {

        // 1. 마우스 커서 관련
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // 1.
        CharacterMove();
    }

    // 1. 
    private void CharacterMove()
    {
        // Project Setting - Input Manager 확인
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float r = Input.GetAxis("Mouse X");


        if (photonView.IsMine)
        //현재 게임에서 내 객체인지 아닌지를 구분(다른 플레이어에게 인풋을 전달하지 않게 하기 위해)
        //phtonView.Ismine은 현재 내 객체인지 아닌지를 구분한다.
        {
            Vector3 dir = (Vector3.forward * v) + (Vector3.right * h);
            transform.Translate(dir.normalized * Time.deltaTime * moveSpeed, Space.Self);
            transform.Rotate(Vector3.up * Time.deltaTime * turnRate * r);   // smoothDeltaTime

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            }

            if (Input.GetMouseButtonDown(0))
            {
                //2. 
                equipWeapon.OnFire();
            }
        }
       
    }
}
