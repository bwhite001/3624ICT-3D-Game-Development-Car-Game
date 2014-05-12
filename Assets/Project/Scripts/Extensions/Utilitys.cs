using System;
using System.Collections;
using UnityEngine;

namespace UtilityExtension
{
		public class Utilitys
		{
				public Utilitys ()
				{
				}

				public static bool HaveTheSameSign(float first, float second)
				{
					if (Mathf.Sign(first) == Mathf.Sign(second))
						return true;
					else
						return false;
				}
				
				public static float Convert_Miles_Per_Hour_To_Meters_Per_Second(float value)
				{
					return value * 0.44704f;
				}
				
				public static float Convert_Meters_Per_Second_To_Miles_Per_Hour(float value)
				{
					return value * 2.23693629f;	
				}
				
				
				
				public static float EvaluateSpeedToTurn(float speed, float maxSpeed, float minimumTurn, float maximumTurn)
				{
					if(speed > maxSpeed / 2)
						return minimumTurn;
					
					float speedIndex = 1 - (speed / (maxSpeed / 2));
					return minimumTurn + speedIndex * (maximumTurn - minimumTurn);
				}
				
				public static float EvaluateNormPower(float normPower)
				{
					if(normPower < 1)
						return 10f - normPower * 9f;
					else
						return 1.9f - normPower * 0.9f;
				}

		}
}

