using System;
using System.Collections;
using UnityEngine;

public class DungeonPieceLoadTrigger : MonoBehaviour
{
    private const string TAG_PLAYER = "Player";

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
            GameObject.Find("DungeonManager").GetComponent<DungeonManager>().LoadTriggerPlayerCollision(transform.name);
        }
    }
}
