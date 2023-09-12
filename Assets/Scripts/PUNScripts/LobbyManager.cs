using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//목적1 : 로비에 방을 만든다.
//속성1 : 방 이름을 넣을 InputField, 최대 인원수
//순서1-1. InputField에 내용이 있다면 방을 만든다.


public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_Text logText;

    //속성1 : 방 이름을 넣을 InputField, 최대 인원수
    [SerializeField] protected TMP_InputField roomNameInput;
    public int maxPlayerNumber = 3;

    public void CreatRoom()
    {
        //순서1-1. InputField에 내용이 있다면 방을 만든다.
        if (roomNameInput.text != string.Empty)
        {
            
            PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = maxPlayerNumber}, null);
            print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }

    public void JoinRoom()
    {
        if (roomNameInput.text != string.Empty)
        {

            PhotonNetwork.JoinRoom(roomNameInput.text);
            print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        }
    }

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        logText.text += ("방을 만들었습니다.\n");
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        logText.text += ("callback - 방에 입장하였습니다.\n");
                
        SceneManager.LoadScene("LoadingScene");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        logText.text += ("callback - 방 생성에 실패하였습니다.\n");
        print(message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        logText.text += ("callback - 방 입장에 실패하였습니다.\n");
        print(message);
    }
}
