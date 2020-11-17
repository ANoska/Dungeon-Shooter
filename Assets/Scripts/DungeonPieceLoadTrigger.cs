using System;
using System.Collections;
using UnityEngine;

public class DungeonPieceLoadTrigger : MonoBehaviour
{
    public event EventHandler<string> OnDungeonPieceLoadTriggerPlayerCollision;

    private const string TAG_PLAYER = "Player";

    private bool isColliding;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == TAG_PLAYER)
        {
            OnDungeonPieceLoadTriggerPlayerCollision.Invoke(this, transform.name);
        }
    }
}
