using UnityEngine;

namespace Asteroids
{
	public class WeaponCrossBehaviour : IWeaponBehaviour
	{
		private const float MinThrustForce = 25;
		private const float MaxThrustForce = 75;

		#region IWeaponBehaviour implementation

		public WeaponType Type { get { return WeaponType.Cross; } }

		public void DoMovement (MovementController movementController)
		{
			movementController.ThrustForce = Random.Range(MinThrustForce, MaxThrustForce);

			movementController.DoForce (Random.insideUnitCircle);
		}

		#endregion
	}
}