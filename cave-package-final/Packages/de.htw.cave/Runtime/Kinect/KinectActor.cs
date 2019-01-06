using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;
using Microsoft.Kinect.Face;

namespace Htw.Cave.Kinect
{
    public sealed class KinectActor
    {
		public static UnityEngine.Vector3 Origin { get; set; }
		public static UnityEngine.Vector4 Floor { get; set; }

		public Body Body { get; private set; }
		public FaceFrameResult Face { get; private set; }

		public static void RefreshAll(KinectActor[] actors, Body[] bodies, FaceFrameResult[] faces)
		{
			for(int i = actors.Length - 1; i >= 0; --i)
				actors[i].Refresh(bodies[i], faces[i]);
		}

		public KinectActor()
		{
			this.Body = null;
			this.Face = null;
		}

		public void Refresh(Body body, FaceFrameResult face)
		{
			this.Body = body;
			this.Face = face;
		}

		public bool IsTracked()
		{
			return this.Body.IsTracked;
		}

		public long FaceFrameTick()
		{
			return this.Face.RelativeTime.Ticks;
		}

		public UnityEngine.Vector3 GetJointPosition(JointType jointType)
		{
			return this.Body.Joints[jointType].Position.CameraSpacePointToRealSpace(Origin, Floor);
		}

		public UnityEngine.Quaternion GetJointRotation(JointType jointType)
		{
			return this.Body.JointOrientations[jointType].Orientation.ToUnityQuaternion().LeftHanded();
		}

		public UnityEngine.Vector3 GetHeadPosition()
		{
			return GetJointPosition(JointType.Head);
		}

		public UnityEngine.Quaternion GetFaceRotation()
		{
			return this.Face.FaceRotationQuaternion.ToUnityQuaternion().LeftHanded();
		}
	}
}
