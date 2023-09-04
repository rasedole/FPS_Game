﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

//목적1 : 방의 정보를 보여주고, Leave room 버튼을 눌러서 방을 나갈 수 있다.
//속성1 : 방의 정보 Text

//목적2 : Photon view를 가진 플레이어를 생성한다.
//속성2 : Photon view 플레이어

public class RoomManager : MonoBehaviourPunCallbacks
{
    //속성1 : 방의 정보 Text
    public TMP_Text infoText;

    //속성2 : 플레이어 게임오브젝트
    public PhotonView playerPrefab;

    private void Start()
    {

    }

    public void showRoomInfo()
    {
        if (PhotonNetwork.InRoom)
        {
            string roomName = PhotonNetwork.CurrentRoom.Name;
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            int maxPlayer = PhotonNetwork.CurrentRoom.MaxPlayers;

            string playerNames = "<Player List>\n";
            for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            {
                playerNames += i+1 + ". " + PhotonNetwork.PlayerList[i].NickName + "\n";
            }

            infoText.text = string.Format("Room: {0} \n Players - {1}/{2} \n {3}", roomName, playerCount, maxPlayer, playerNames);

            PhotonNetwork.Instantiate(playerPrefab.name, Vector3.zero, Quaternion.identity);
        }
        else
        {
            infoText.text = null;
        }
    }

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        SceneManager.LoadScene("LobbyScene");
    }
}
