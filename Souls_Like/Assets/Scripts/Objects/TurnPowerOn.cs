using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SG;

public class TurnPowerOn : InteractableObject
{
    [SerializeField] GameObject[] toTurnOn = new GameObject[29];
    [SerializeField] GameObject[] toTurnOff = new GameObject[29];

    [SerializeField] GameObject[] toChangeColorBigLamps = new GameObject[5];
    [SerializeField] GameObject[] toChangeColorLongLamps = new GameObject[9];

    [SerializeField] Material lightOnMaterial;

    [SerializeField] AudioSource lightsSound;

    [SerializeField] protected GameObject guitarIntaractableObject;
    protected override void Interact(PlayerManager player)
    {
        for(int i = 0; i < toTurnOn.Length; i++)
        {
            toTurnOn[i].SetActive(true);
        }
        for (int i = 0; i < toTurnOff.Length; i++)
        {
            toTurnOff[i].SetActive(false);
        }
        for (int i = 0; i < toChangeColorLongLamps.Length; i++)
        {
            MeshRenderer meshRenderer = toChangeColorLongLamps[i].GetComponent<MeshRenderer>();
            Material[] materials = meshRenderer.materials;
            materials[0] = lightOnMaterial;

            meshRenderer.materials = materials;
        }
        for (int i = 0; i < toChangeColorBigLamps.Length; i++)
        {
            MeshRenderer meshRenderer = toChangeColorBigLamps[i].GetComponent<MeshRenderer>();
            Material[] materials = meshRenderer.materials;
            materials[1] = lightOnMaterial;

            meshRenderer.materials = materials;
        }

        lightsSound.Play();

        guitarIntaractableObject.GetComponent<PlayGuitar>().isElectricBoxOn = true;
    }
}
