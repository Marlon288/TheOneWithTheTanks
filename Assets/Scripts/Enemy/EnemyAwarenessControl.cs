using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAwarenessControl : MonoBehaviour
{

    // Start is called before the first frame update
    public bool AwareOfPlayer{get; private set;}

    public Vector3 DirectionToPlayer{get; private set;}

    public float _playerAwarenessDistance;

    private Transform _player;
    private void Awake()
    {
        _player = FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;
        
        if(enemyToPlayerVector.magnitude <= _playerAwarenessDistance){
            AwareOfPlayer = true;
        }else{
            AwareOfPlayer = false;
        } 
    }
}
