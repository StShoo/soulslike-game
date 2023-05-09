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

        protected virtual void OnTriggerEnter(Collider other)
        {
            if(player == null)
            {
                player = other.GetComponent<PlayerManager>();
            }

            if(player != null)
            {
                interactableImage.SetActive(true);
                player.canInteract = true;
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (player == null)
            {
                player = other.GetComponent<PlayerManager>();
            }

            if (player != null)
            {
                interactableImage.SetActive(false);
                player.canInteract = false;
            }
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (player != null)
            {
                if (player.inputManager.interactInput)
                {
                    Interact(player);
                    player.inputManager.interactInput = false;
                }
            }
        }

        protected virtual void Interact(PlayerManager player)
        {
            Debug.Log("you have interacted"); 
        } 
    }
}
