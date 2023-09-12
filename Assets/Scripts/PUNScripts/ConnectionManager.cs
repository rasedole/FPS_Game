using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


//목적1 : Photon 게임 서버에 연결한다.

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public TMP_Text logText;

    public void Connect()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;
        
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnected()
    {
        base.OnConnected();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        logText.text += ("callback - 포톤 서버에 연결 되었습니다.\n");
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        print(System.Reflection.MethodBase.GetCurrentMethod().Name);
        logText.text += ("callback - 마스터 서버에 연결 되었습니다.\n");

        JoinLobby();
    }

    public void JoinLobby() => PhotonNetwork.JoinLobby();

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        logText.text += ("callback - 로비에 입장 하였습니다.\n");

        SceneManager.LoadScene("LobbyScene");
    }

    public void SoloPlay()
    {
        SceneManager.LoadScene("LoadingScene");
    }
}
