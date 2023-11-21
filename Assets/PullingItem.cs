using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingItem : MonoBehaviour
{
    private Player _player;
    // Start is called before the first frame update
    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartPullingToPlayer()
    {
        Vector3 direction = _player.playerAnchor.position - transform.position;
        this.GetComponent<Rigidbody>().AddForce(direction.normalized * _player.pullForce ,ForceMode.Impulse);
        
    }

    public void HightlightSelf()
    {
        
    }

    public void UnHightlightSelf()
    {
        
    }
}
