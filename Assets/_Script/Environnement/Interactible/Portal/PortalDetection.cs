using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Supermarket.Gameplay
{
    public class PortalDetection : PortalManager
    {
        [SerializeField] public int numeroPortal;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("je me tp");
                Teleportation(collision.gameObject, numeroPortal);
            }
        }
    }
}

