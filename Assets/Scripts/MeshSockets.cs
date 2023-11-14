using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshSockets : MonoBehaviour
{
    public enum SocketID
    {
        Spine,
        RightHand
    }
    Dictionary<SocketID, MeshSocket> socketMap = new Dictionary<SocketID, MeshSocket>();
    void Start()
    {
        MeshSocket[] sockets = GetComponentsInChildren<MeshSocket>();
        foreach (MeshSocket socket in sockets)
        {
            socketMap[socket.socketID] = socket;
        }
    }

    public void Attach(Transform objectTransform, SocketID socketId)
    {
        socketMap[socketId].Attach(objectTransform);
    }
}
