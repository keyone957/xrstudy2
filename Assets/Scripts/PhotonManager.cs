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
    //1. ui세팅
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
        startBtn.onClick.AddListener(() => JoinRoom());//방있으면 랜덤 방 생성, 없으면 생성
        PhotonNetwork.AutomaticallySyncScene=true;//같은 로비에 잇는 유저들에게 자동으로 씬을 로딩해주기
        PhotonNetwork.GameVersion = "1.0.0";//게임 버전 만들어주기

        nickNameField.text = "Player [001]";//바꾸지 않으면 그냥 원래 이전에 썼던 닉네임을 그대로 사용(?)
        PhotonNetwork.NickName = nickNameField.text;
        PhotonNetwork.ConnectUsingSettings();//서버에 접속하도록 요청함
        state.text = "server connecting.....";
    }
    //서버 접속이 완료 됐을때 처리
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        state.text = "server connect complete!";
        PhotonNetwork.JoinLobby();
    }
    //로비에 접속 완료 됐을때
    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        state.text = "lobby connect complete. please press the start button";
    }
    //룸 접속이 완료 됐을때
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        roomSetting.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //커서 룸세팅 ui삭제
    }
    //룸 접속이 실패 됐을때 처리
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
    //룸 생성 후 처리 
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        roomSetting.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    //룸 접속 종료 후 처리
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
