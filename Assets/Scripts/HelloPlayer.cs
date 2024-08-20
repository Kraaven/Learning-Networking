using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class HelloPlayer : NetworkBehaviour
{
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    

    public override void OnNetworkSpawn()
    {
        print($"Player {GetComponent<NetworkObject>().NetworkObjectId} has joined");
        if (IsOwner)
        {
            SubmitPositionRequestRpc();
        }
    }
    
    void Update()
    {

        transform.position = Position.Value;
    }
    

    [Rpc(SendTo.Server)]
    void SubmitPositionRequestRpc(RpcParams rpcParams = default)
    {
        var randomPosition = GetRandomPositionOnPlane();
        transform.position = randomPosition;
        Position.Value = randomPosition;
    }

    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), 0 , Random.Range(-3f, 3f));
    }
}
