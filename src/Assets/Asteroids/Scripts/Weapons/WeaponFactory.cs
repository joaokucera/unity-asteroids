using UnityEngine;

namespace Asteroids
{
	public static class WeaponFactory
	{
		public static IWeaponBehaviour GetCurrentWeapon(WeaponType type)
		{
			IWeaponBehaviour controller = null;

			switch (type)
			{
				case WeaponType.Laser:
					controller = new WeaponLaserBehaviour();
					break;
				case WeaponType.Bomb:
					controller = new WeaponBombBehaviour();
					break;
				case WeaponType.Cross:
					controller = new WeaponCrossBehaviour();
					break;
			}

			return controller;
		}
	}
}