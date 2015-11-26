using UnityEngine;

namespace Asteroids
{
	public enum WeaponType
	{
		Laser = 0,
		Bomb = 1,
		Cross = 2
	}

	[AddComponentMenu("ASTEROIDS / Player Weapon Controller")]
	public class PlayerWeaponController : Pooling<MovementController>
	{
		private const float DowngradeTimeLimit = 30f;

		private IWeaponBehaviour m_behaviour;
		private float m_downgradeTime;

		[Header("Weapon Types")]
		[SerializeField] private Sprite[] m_weaponSprites;

		void Start()
		{
			m_behaviour = WeaponFactory.GetCurrentWeapon(WeaponType.Laser);
			m_downgradeTime = Time.time;
		}

		void Update()
		{
			if (Time.time - m_downgradeTime > DowngradeTimeLimit) 
			{
				m_downgradeTime = Time.time;

				Downgrade();
			}
		}

		public void DoShoot ()
		{
			SoundManager.PlaySoundEffect ("PlayerShoot");

			var shot = GetObjectFromPool ();

			shot.transform.position = transform.position;
			shot.transform.rotation = transform.rotation;

			// Try to change sprite.
			var newSprite = m_weaponSprites[(int)m_behaviour.Type];
			shot.RendererBehaviour.TryChangeSprite(newSprite);

			// Execute specific movement.
			m_behaviour.DoMovement (shot);
		}

		public void Upgrade()
		{
			var previousType = m_behaviour.Type;
			
			switch (previousType)
			{
				case WeaponType.Laser:
					m_behaviour = WeaponFactory.GetCurrentWeapon(WeaponType.Bomb);
					break;
				case WeaponType.Bomb:
					m_behaviour = WeaponFactory.GetCurrentWeapon(WeaponType.Cross);
					break;
				default:
					break;
			}

			TryUpdateWeapon (previousType);
		}

		public void ForceDowngrade()
		{
			if (m_behaviour.Type == WeaponType.Laser) return;

			var previousType = m_behaviour.Type;
			m_behaviour = WeaponFactory.GetCurrentWeapon(WeaponType.Laser);

			TryUpdateWeapon (previousType);
		}

		private void Downgrade()
		{
			var previousType = m_behaviour.Type;

			switch (previousType)
			{
				case WeaponType.Bomb:
					m_behaviour = WeaponFactory.GetCurrentWeapon(WeaponType.Laser);
					break;
				case WeaponType.Cross:
					m_behaviour = WeaponFactory.GetCurrentWeapon(WeaponType.Bomb);
					break;
				default:
					break;
			}

			TryUpdateWeapon (previousType);
		}

		private void TryUpdateWeapon(WeaponType previousType)
		{
			if (previousType == m_behaviour.Type) return;

			UIGame.UpdatePowerUp ((int)m_behaviour.Type);
		}
	}
}