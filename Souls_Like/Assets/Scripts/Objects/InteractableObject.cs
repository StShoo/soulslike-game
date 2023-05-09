using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{
    public class InteractableObject : MonoBehaviour
    {
        protected PlayerManager player;
        protected Collider interactableCollider;
        [SerializeField] protected GameObject interactableImage;

        private void OnTriggerEnter(Collider other)
        {
            if(player == null)
            {
                player = other.GetComponent<PlayerManager>();
            }

            if(player != null)
            {
                interactableImage.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (player == null)
            {
                player = other.GetComponent<PlayerManager>();
            }

            if (player != null)
            {
                interactableImage.SetActive(false);
            }
        }
    }
}
