using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        public Transform firePoint;
        public GameObject bulletProjectile;
        
        public void ShootProjectile()
        {
            Instantiate(bulletProjectile, firePoint.transform.position, firePoint.transform.rotation);
            AudioManager.instance.PlaySound("SFX_WeaponShot");
        }
    }
}