using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkPlayerController : MonoBehaviour
{
    // 1. ������ ����
    [Header("Movement Setting")]
    [SerializeField] private float moveSpeed = 8.0f;
    [SerializeField] private float turnRate = 360.0f;
    [SerializeField] private float jumpPower = 5.0f;

    private Rigidbody rigid;

    // 2. �Ѿ� �߻�
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

        // 1. ���콺 Ŀ�� ����
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
        // Project Setting - Input Manager Ȯ��
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float r = Input.GetAxis("Mouse X");


        if (photonView.IsMine)
        //���� ���ӿ��� �� ��ü���� �ƴ����� ����(�ٸ� �÷��̾�� ��ǲ�� �������� �ʰ� �ϱ� ����)
        //phtonView.Ismine�� ���� �� ��ü���� �ƴ����� �����Ѵ�.
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
