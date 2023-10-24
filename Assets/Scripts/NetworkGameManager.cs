using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NetworkGameManager : MonoBehaviour
{
    public static NetworkGameManager instance;
    public bool isConnected = false;

    [SerializeField]
    private GameObject prefplayer;
    [SerializeField]
    private Transform[] spawnPoint;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        StartCoroutine(CreatePlayer());
    }
    IEnumerator CreatePlayer()
    {

        yield return new WaitUntil(()=>isConnected);//이 조건이 false면 다음 부분은 실행되지 않음 true일때 다음 코드 실행
        Vector3 pos = spawnPoint[PhotonNetwork.CurrentRoom.PlayerCount-1].position;
        //현재 방에 입장한 유저의 순서를 확인함과 동시에 
        Quaternion rot = spawnPoint[PhotonNetwork.CurrentRoom.PlayerCount-1].rotation;
        //유저 위치도 받아와야함

        PhotonNetwork.Instantiate("NetworkPlayer",pos,rot);//서버에서 프리펩을 생성해줘야함.
    }
}
