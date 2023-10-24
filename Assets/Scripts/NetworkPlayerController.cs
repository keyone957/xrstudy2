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
    [SerializeField]
    private Transform camPivot;
    [SerializeField]
    private MeshRenderer meshRenderer;
    private void Awake()
    {
        // 1.
        rigid = GetComponent<Rigidbody>();
        photonView = GetComponent<PhotonView>();

    }

    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)//=> 각 플레이어 카메라 위치를 조정해주는거(멀티플레이 일때 각자 자기 캐릭터에 따라
                              //카메라를 이동해야 하므로)
        {
            Camera.main.transform.parent = camPivot;
            Camera.main.transform.localPosition = camPivot.localPosition;
            Camera.main.transform.localRotation = camPivot.localRotation;   
        }
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
                //원격 프로시져 호출(Remote Procedure Call)
                //같은 룸에 있는 다른 유저의 함수를 호출하는 기능
                photonView.RPC("ShoEffectProcess", RpcTarget.Others);//나를 제외하고 shote~ 저 함수를 호출
            }
        }

    }
    [PunRPC]//원격 프로시져 호출이 가능해지게 하는거
    private void ShoEffectProcess()
    {
        equipWeapon.OnFire();
    }
    public void OnHitBullet(GameObject obj)
    {
        StartCoroutine(HitChecking());
    }
    private IEnumerator HitChecking()
    {
        meshRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        meshRenderer.material.color = Color.yellow;
    }
}
