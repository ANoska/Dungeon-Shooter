using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTrigger : MonoBehaviour
{
    private DungeonManager dmRef;

    // Start is called before the first frame update
    void Start()
    {
        dmRef = GameObject.Find("DungeonManager").GetComponent<DungeonManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            dmRef.PortalTriggerPlayerCollision();
    }
}
