using System.Collections;
using UnityEngine;
public class PowerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    public int coinOrDiamond;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup();
        }
    }
    
    private void Pickup()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        if (coinOrDiamond == 0)
        {
            PlayerManager.coinCount += 1;
            //FindObjectOfType<AudioManager>().PlaySound("CoinPickUp");
        }
        else if(coinOrDiamond == 1)
        {
            PlayerManager.diamondCount += 1;
            //FindObjectOfType<AudioManager>().PlaySound("DiamondPickUp");
        }

        StartCoroutine(PickupEffectAndRespawn());

        IEnumerator PickupEffectAndRespawn()
        {
            Instantiate(pickupEffect, transform.position, transform.rotation);
            yield return new WaitForSeconds(.1f);
            transform.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
