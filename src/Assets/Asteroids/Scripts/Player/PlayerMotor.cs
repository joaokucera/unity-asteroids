using System;
using System.Collections;
using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS/Player Motor")]
	[RequireComponent(typeof(MovementController))]
	public class PlayerMotor : MonoBehaviour
	{
		private IInputController m_inputController;
		private MovementController m_movement;
		private PlayerWeaponController m_weaponController;
		private ParticleSystem m_thrustEffect;
		private float m_rotationValue;
		private bool m_isBlinking;

		public PlayerData Data;
			
		void Awake()
		{
			Data = new PlayerData ();
		}

		void Start()
		{
			// Get input controller.
			m_inputController = InputFactory.GetCurrentInputController ();
			// Get movement controller.
			m_movement = GetComponent<MovementController>();
			// Get weapon controller (in children).
			m_weaponController = GetComponentInChildren<PlayerWeaponController> ();
			// Get thrust particles (in children).
			m_thrustEffect = GetComponentInChildren<ParticleSystem> ();
		}

		void Update()
		{
			m_rotationValue = m_inputController.GetRotationValue ();

			// Shoot.
			if (m_inputController.IsShot ())
			{
				m_weaponController.DoShoot();
			}
		}

		void FixedUpdate()
		{
			// Rotation.
			m_movement.DoRotation (-m_rotationValue);

			// Translation.
			if (m_inputController.IsForward ()) 
			{
				m_movement.DoForce (transform.up);

				// Play thrust particles.
				m_thrustEffect.Play();
			}
			else
			{
				// Stop thrust particles.
				m_thrustEffect.Stop();
			}
		}

		void OnTriggerEnter2D(Collider2D collider)
		{
			if (m_isBlinking) return;

			if (collider.IsEnemyShot ()) 
			{
				GlobalVariables.EnemyWeaponPooling.SetObjectToPool(collider.GetComponent<MovementController>());

				OnHit();
			}
			else if (collider.IsObstacle())
			{
				collider.GetComponent<Obstacle>().DoImpact();

				OnHit();
			}
			else if (collider.IsPowerUp())
			{
				GlobalVariables.PowerUpPooling.SetObjectToPool(collider.transform);
				
				m_weaponController.Upgrade();
			}
		}

		public void SetShotToPool (MovementController shot)
		{
			m_weaponController.SetObjectToPool (shot);
		}

		private void OnHit()
		{
			m_isBlinking = true;

			GlobalVariables.ExplosionPooling.DoExplosion(transform.position, transform.rotation, "PlayerDeath");

			m_movement.Rigidbody.velocity = Vector2.zero;
			m_movement.Rigidbody.angularVelocity = 0f;
			
			StartCoroutine(Blink());
		}

		private IEnumerator Blink()
		{
			for (int i = 0; i < 10; i++)
			{
				m_movement.RendererBehaviour.SpriteRenderer.enabled = !m_movement.RendererBehaviour.SpriteRenderer.enabled;
				
				yield return new WaitForSeconds (.25f);
			}
			
			m_movement.RendererBehaviour.SpriteRenderer.enabled = false;
			
			CheckLife ();
		}

		private void CheckLife()
		{
			if (UIGame.TryUpdateLife ())
			{
				m_movement.RendererBehaviour.SpriteRenderer.enabled = true;

				m_isBlinking = false;
			}
			else
			{
				GameManager.OnGameOver();
			}
		}
	}
}