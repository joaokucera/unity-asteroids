using UnityEngine;

namespace Asteroids
{
	public static class GlobalVariables
	{
		private static PlayerMotor m_player;
		public static PlayerMotor Player
		{
			get 
			{
				if (m_player == null)
				{
					m_player = GameObject.FindObjectOfType<PlayerMotor>();
				}	
				
				return m_player;
			}
		}

		private static ExplosionPooling m_explosionPooling;
		public static ExplosionPooling ExplosionPooling
		{
			get 
			{
				if (m_explosionPooling == null)
				{
					m_explosionPooling = GameObject.FindObjectOfType<ExplosionPooling>();
				}	
				
				return m_explosionPooling;
			}
		}

		private static PowerUpPooling m_powerUpPooling;
		public static PowerUpPooling PowerUpPooling
		{
			get 
			{
				if (m_powerUpPooling == null)
				{
					m_powerUpPooling = GameObject.FindObjectOfType<PowerUpPooling>();
				}	
				
				return m_powerUpPooling;
			}
		}

		private static EnemyWeaponPooling m_enemyWeaponPooling;
		public static EnemyWeaponPooling EnemyWeaponPooling
		{
			get 
			{
				if (m_enemyWeaponPooling == null)
				{
					m_enemyWeaponPooling = GameObject.FindObjectOfType<EnemyWeaponPooling>();
				}	
				
				return m_enemyWeaponPooling;
			}	
		}
	}
}