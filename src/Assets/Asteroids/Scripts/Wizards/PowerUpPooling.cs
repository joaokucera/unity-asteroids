using UnityEngine;
using System.Collections;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / PowerUp Pooling")]
	public class PowerUpPooling : Pooling<Transform> 
	{
		[Header("Power Up Settings")]
		[Range(0, 30)] [SerializeField] private float m_waitTime;

		public void DoPowerUp (Vector2 position, Quaternion rotation)
		{
			var powerUp = GetObjectFromPool ();
			
			powerUp.transform.position = position;
			powerUp.transform.rotation = rotation;

			StartCoroutine (Hide (powerUp));
		}

		private IEnumerator Hide(Transform powerUp)
		{
			yield return new WaitForSeconds(m_waitTime);

			powerUp.gameObject.SetActive (false);
		}
	}
}