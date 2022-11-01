using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Misc
{
	public class ProjectileMoveScript : MonoBehaviour {

		public float speed;
		public GameObject muzzlePrefab;
		public GameObject hitPrefab;
		public AudioClip shotSfx;
		public AudioClip hitSfx;
		public List<GameObject> trails;

		private float _speedRandomness;
		private Vector3 _offset;
		private bool _collided;
		private Rigidbody _rb;

		private void Start () {	
			_rb = GetComponent <Rigidbody> ();
		
			Destroy(gameObject, 5f);

			if (muzzlePrefab != null) {
				var muzzleVFX = Instantiate (muzzlePrefab, transform.position, Quaternion.identity);
				muzzleVFX.transform.forward = gameObject.transform.forward + _offset;
				var ps = muzzleVFX.GetComponent<ParticleSystem>();
				if (ps != null)
					Destroy (muzzleVFX, ps.main.duration);
				else {
					var psChild = muzzleVFX.transform.GetChild(0).GetComponent<ParticleSystem>();
					Destroy (muzzleVFX, psChild.main.duration);
				}
			}

			if (shotSfx != null && GetComponent<AudioSource>()) {
				GetComponent<AudioSource> ().PlayOneShot (shotSfx);
			}
		}

		private void FixedUpdate () {	
			if (speed != 0 && _rb != null)
				_rb.position += (transform.forward + _offset)  * (speed * Time.deltaTime);
		}

		private DestroyableObstacleHandler _destroyableObstacleHandler;
	
		private void OnTriggerEnter(Collider other)
		{
			if (!other.gameObject.CompareTag("Bullet") && other.gameObject.CompareTag("ColliderSnowBoulder") && !_collided) {
				_collided = true;

				other.gameObject.GetComponent<DestroyableObstacleHandler>().Destroy();
			
				if (shotSfx != null && GetComponent<AudioSource>()) {
					GetComponent<AudioSource> ().PlayOneShot (hitSfx);
				}

				if (trails.Count > 0) {
					for (int i = 0; i < trails.Count; i++) {
						trails [i].transform.parent = null;
						var ps = trails [i].GetComponent<ParticleSystem> ();
						if (ps != null) {
							ps.Stop ();
							Destroy (ps.gameObject, ps.main.duration + ps.main.startLifetime.constantMax);
						}
					}
				}
		
				speed = 0;
				GetComponent<Rigidbody> ().isKinematic = true;

				var contact = other.ClosestPoint(transform.position);
				Quaternion rot = Quaternion.FromToRotation (Vector3.up, contact);
				Vector3 pos = contact;

				if (hitPrefab != null) {
					var hitVFX = Instantiate (hitPrefab, pos, rot);

					var ps = hitVFX.GetComponent<ParticleSystem> ();
					if (ps == null) {
						var psChild = hitVFX.transform.GetChild (0).GetComponent<ParticleSystem> ();
						Destroy (hitVFX, psChild.main.duration);
					} else
						Destroy (hitVFX, ps.main.duration);
				}

				StartCoroutine (DestroyParticle (0f));
			}
		}

		public IEnumerator DestroyParticle (float waitTime) {

			if (transform.childCount > 0 && waitTime != 0) {
				List<Transform> tList = new List<Transform> ();

				foreach (Transform t in transform.GetChild(0).transform) {
					tList.Add (t);
				}		

				while (transform.GetChild(0).localScale.x > 0) {
					yield return new WaitForSeconds (0.01f);
					transform.GetChild(0).localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
					for (int i = 0; i < tList.Count; i++) {
						tList[i].localScale -= new Vector3 (0.1f, 0.1f, 0.1f);
					}
				}
			}
		
			yield return new WaitForSeconds (waitTime);
			Destroy (gameObject);
		}
	}
}
