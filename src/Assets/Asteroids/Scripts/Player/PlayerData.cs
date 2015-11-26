using System;
using UnityEngine;

namespace Asteroids
{
	public class PlayerData
	{
		public int HighScore { get; private set; }
		public int CurrentScore { get; private set; }
		public int Lifes { get; private set; }
		public int Level { get; private set; }

		public PlayerData ()
		{
			HighScore = PlayerPrefs.GetInt ("highscore");
			CurrentScore = 0;
			Lifes = 3;
			Level = 1;
		}

		public void NextLevel()
		{
			SoundManager.PlaySoundEffect ("LevelComplete");

			Level++;

			UIGame.UpdateLevel ();
		}

		public void LoseLife()
		{
			Lifes--;
		}

		public void AddScore (int newScore)
		{
			CurrentScore += newScore;
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