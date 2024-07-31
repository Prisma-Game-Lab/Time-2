using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public delegate void OnRoomCleared();
    public static OnRoomCleared onRoomCleared;
    [SerializeField] DoorController[] doors;

    private void OnEnable()
    {
        SoulScript.onSoulAssimilation += LockDoors;
    }

    private void OnDisable()
    {
        SoulScript.onSoulAssimilation -= LockDoors;
    }

    public void LockDoors() 
    {
        foreach(DoorController door in doors) 
        {
            door.toggleLock();
        }
    }

    public void OnEnemyDeath() 
    {
        if (gameObject.transform.childCount == 1)
        {
            onRoomCleared();
        }
    }
}
