using UnityEngine;

namespace Asteroids
{
	[AddComponentMenu("ASTEROIDS / Player Motor")]
	[RequireComponent(typeof(MovementController))]
	public class PlayerMotor : MonoBehaviour
	{
		private IInputController m_inputController;
		private MovementController m_movement;
		private PlayerWeaponController m_weaponController;
		private ParticleSystem m_thrustEffect;
		private float m_rotationValue;
		private bool m_readyToPlay = true;

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
			if (!m_readyToPlay) return;

			m_rotationValue = m_inputController.GetRotationValue ();

			// Shoot.
			if (m_inputController.IsShot ())
			{
				m_weaponController.DoShoot();
			}
		}

		void FixedUpdate()
		{
			if (!m_readyToPlay) return;

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
			if (!m_readyToPlay) return;

			if (collider.IsEnemyShot ()) 
			{
				collider.gameObject.SetActive(false);

				OnHit();
			}
			else if (collider.IsObstacle())
			{
				collider.GetComponent<Obstacle>().DoImpact();

				OnHit();
			}
			else if (collider.IsPowerUp())
			{
				collider.gameObject.SetActive(false);
				
				m_weaponController.Upgrade();
			}
		}

		private void OnHit()
		{
			GlobalVariables.ExplosionPooling.DoExplosion(transform.position, transform.rotation, "PlayerDeath");

			m_movement.Stop(CheckLife);
			m_readyToPlay = false;
			
			m_weaponController.ForceDowngrade();
		}

		private void CheckLife()
		{
			if (UIGame.TryUpdateLife ())
			{
				m_movement.RendererBehaviour.Show();
				m_readyToPlay = true;
			}
			else
			{
				GameManager.OnGameOver();
			}
		}
	}
}