using System.Collections;
using UnityEngine;

public class DestroyableObstacleHandler : MonoBehaviour
{
    public GameObject boulder;
    public GameObject shatteredBoulder;
    public GameObject diamondPickup;

    public void Destroy()
    {
        StartCoroutine(DestroyBoulder());

        IEnumerator DestroyBoulder()
        {
            transform.GetComponent<SphereCollider>().enabled = false;
            boulder.SetActive(false);
            var shattered = Instantiate(shatteredBoulder, transform.position, transform.rotation);
            var diamond = Instantiate(diamondPickup, transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
            Destroy(shattered);
            yield return new WaitForSeconds(2f);
            Destroy(diamond);
            transform.GetComponent<SphereCollider>().enabled = true;
            print(transform.GetComponent<SphereCollider>().enabled);
            print("enabled");
            boulder.SetActive(true);
        }
    }
}
