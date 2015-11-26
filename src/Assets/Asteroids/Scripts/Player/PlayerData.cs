using System;
using UnityEngine;

namespace Asteroids
{
	public class PlayerData
	{
		public int HighScore;
		public int CurrentScore;
		public int Lifes;
		public int PowerUps;
		public int Level;

		public PlayerData ()
		{
			HighScore = PlayerPrefs.GetInt ("highscore");
			CurrentScore = 0;
			Lifes = 3;
			PowerUps = 1;
			Level = 1;
		}

		public void CheckHighScore ()
		{
			if (CurrentScore > HighScore)
			{
				HighScore = CurrentScore;

				PlayerPrefs.SetInt ("highscore", HighScore);
			}
		}
	}
}