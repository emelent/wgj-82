using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteReceiver : MonoBehaviour
{

    public void ChannelUp(){
        print("Receiver: Channel Up");
        GM.instance.audioManager.PlaySound("ChannelChange");
    }

    public void ChannelDown(){
        print("Receiver: Channel Down");
        GM.instance.audioManager.PlaySound("ChannelChange");
    }
}
