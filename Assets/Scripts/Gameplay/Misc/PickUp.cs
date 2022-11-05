using System.Collections;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Misc
{
    public class PickUp : MonoBehaviour
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
                if(PlayerController.instance.canCollectCollectables)
                {
                    PlayerManager.instance.PlusCoin(+1, "+");
                    AudioManager.instance.PlaySound("SFX_CoinCollected");
                }
            }
            else if(coinOrDiamond == 1)
            {
                if(PlayerController.instance.canCollectCollectables)
                {
                    PlayerManager.instance.PlusDiamond(+1, "+");
                    AudioManager.instance.PlaySound("SFX_DiamondCollected");
                }
            }

            StartCoroutine(PickupEffectAndRespawn());

            IEnumerator PickupEffectAndRespawn()
            {
                var tempScale = transform.localScale;
                transform.localScale = new Vector3(.01f, .01f, .01f);
                var pickedUpEffect = Instantiate(pickupEffect, transform.position, transform.rotation);
                Destroy(pickedUpEffect, 2f);
                yield return new WaitForSeconds(.5f);
                transform.GetComponent<BoxCollider>().enabled = true;
                transform.localScale = tempScale;
            }
        }
    }
}
