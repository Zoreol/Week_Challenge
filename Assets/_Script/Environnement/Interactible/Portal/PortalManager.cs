using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supermarket.Gameplay
{
    public class PortalManager : MonoBehaviour
    {
        [SerializeField] Transform[] listTransformPortal;

        public void Teleportation(GameObject player,int numPortal)
        {
            if (numPortal == 0)
            {
                player.transform.position = listTransformPortal[1].position;
                player.transform.rotation = Quaternion.Euler(player.transform.rotation.x, player.transform.rotation.y, -180f);
                player.GetComponent<Rigidbody2D>().gravityScale = -1;
                player.GetComponent<PlayerManager>().inGravite = -1;
            }
            if (numPortal == 1)
            {
                player.transform.position = listTransformPortal[0].position;
                player.transform.rotation = Quaternion.Euler(player.transform.rotation.x, player.transform.rotation.y, 0f);
                player.GetComponent<Rigidbody2D>().gravityScale = 1;
                player.GetComponent<PlayerManager>().inGravite = 1;
            }

        }
    }
}

