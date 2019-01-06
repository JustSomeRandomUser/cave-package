using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Windows.Kinect;

namespace Htw.Cave.Kinect
{
    public static class KinectActorExtensions
    {
		public static KinectActor FindShortest(this KinectActor[] actors)
		{
			float z = float.PositiveInfinity;
			int index = -1;

			for(int i = actors.Length - 1; i >= 0; --i)
			{
				if(!actors[i].IsTracked())
					continue;

				Vector3 position = actors[i].GetJointPosition(JointType.Head);

				if(position.z < z)
				{
					z = position.z;
					index = i;
				}
			}

			if(index < 0)
				return null;

			return actors[index];
		}

		public static KinectActor FindTallest(this KinectActor[] actors)
		{
			float z = 0f;
			int index = -1;

			for(int i = actors.Length - 1; i >= 0; --i)
			{
				if(!actors[i].IsTracked())
					continue;

				Vector3 position = actors[i].GetJointPosition(JointType.Head);

				if(position.z > z)
				{
					z = position.z;
					index = i;
				}
			}

			if(index < 0)
				return null;

			return actors[index];
		}

		public static KinectActor ClosestToPoint(this KinectActor[] actors, UnityEngine.Vector3 point)
		{
			return actors.ClosestToPoint(point, JointType.Head);
		}

		public static KinectActor ClosestToPoint(this KinectActor[] actors, UnityEngine.Vector3 point, JointType jointType)
		{
			float distance = float.PositiveInfinity;
			int index = -1;

			for(int i = actors.Length - 1; i >= 0; --i)
			{
				if(!actors[i].IsTracked())
					continue;

				Vector3 position = actors[i].GetJointPosition(jointType);
				float sqr = (position - point).sqrMagnitude;

				if(sqr < distance)
				{
					distance = sqr;
					index = i;
				}
			}

			if(index < 0)
				return null;

			return actors[index];
		}
	}
}
