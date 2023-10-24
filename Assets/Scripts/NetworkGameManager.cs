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

        yield return new WaitUntil(()=>isConnected);//�� ������ false�� ���� �κ��� ������� ���� true�϶� ���� �ڵ� ����
        Vector3 pos = spawnPoint[PhotonNetwork.CurrentRoom.PlayerCount-1].position;
        //���� �濡 ������ ������ ������ Ȯ���԰� ���ÿ� 
        Quaternion rot = spawnPoint[PhotonNetwork.CurrentRoom.PlayerCount-1].rotation;
        //���� ��ġ�� �޾ƿ;���

        PhotonNetwork.Instantiate("NetworkPlayer",pos,rot);//�������� �������� �����������.
    }
}
