using UnityEngine;

namespace Asteroids
{
	public class WeaponCrossBehaviour : IWeaponBehaviour
	{
		#region IWeaponBehaviour implementation

		public WeaponType Type { get { return WeaponType.Cross; } }

		public void DoMovement (MovementController movementController)
		{
			movementController.ThrustForce = Random.Range(25, 75);

			movementController.DoForce (Random.insideUnitCircle);
		}

		#endregion
	}
}