﻿using UnityEngine;
using System.Collections;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/Explosion Pooling")]
	public class ExplosionPooling : Pooling<ParticleSystem> 
	{
		public void DoExplosion (Vector2 position, Quaternion rotation, string explosionClipName)
		{
			// Explosion sound.
			SoundManager.PlaySoundEffect (explosionClipName);
			// Shake screen.
			ScreenShake.Shake ();

			var explosion = GetObjectFromPool ();
			
			explosion.transform.position = position;
			explosion.transform.rotation = rotation;
			explosion.Play ();

			StartCoroutine (Hide (explosion));
		}

		private IEnumerator Hide(ParticleSystem explosion)
		{
			yield return new WaitForSeconds(explosion.startLifetime);

			GlobalVariables.ExplosionPooling.SetObjectToPool (explosion);
		}
	}
}