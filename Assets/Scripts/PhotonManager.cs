using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    //1. ui����
    [Header("UI Setting")]
    [SerializeField]
    private GameObject roomSetting;
    [SerializeField]
    private TMP_InputField nickNameField;
    [SerializeField]
    private TMP_Text state;
    [SerializeField]
    private Button startBtn;
    private void Awake()
    {
        startBtn.onClick.AddListener(() => JoinRoom());//�������� ���� �� ����, ������ ����
        PhotonNetwork.AutomaticallySyncScene=true;//���� �κ� �մ� �����鿡�� �ڵ����� ���� �ε����ֱ�
        PhotonNetwork.GameVersion = "1.0.0";//���� ���� ������ֱ�

        nickNameField.text = "Player [001]";//�ٲ��� ������ �׳� ���� ������ ��� �г����� �״�� ���(?)
        PhotonNetwork.NickName = nickNameField.text;
        PhotonNetwork.ConnectUsingSettings();//������ �����ϵ��� ��û��
        state.text = "server connecting.....";
    }
    //���� ������ �Ϸ� ������ ó��
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        state.text = "server connect complete!";
        PhotonNetwork.JoinLobby();
    }
    //�κ� ���� �Ϸ� ������
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        state.text = "lobby connect complete. please press the start button";
    }
    //�� ������ �Ϸ� ������
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        roomSetting.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Ŀ�� �뼼�� ui����
    }
    //�� ������ ���� ������ ó��
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        //Debug.Log(message);
        RoomOptions roomOpt = new RoomOptions();
        roomOpt.IsOpen = true;
        roomOpt.IsVisible = true;
        roomOpt.MaxPlayers = 3;
        PhotonNetwork.CreateRoom("Room [1]", roomOpt);
    }
    //�� ���� �� ó�� 
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        roomSetting.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //�� ���� ���� �� ó��
    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Debug.Log("disconnect server complete");
    }
    private void JoinRoom()
    {
        PhotonNetwork.JoinRandomOrCreateRoom();
    }
}
