using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;
using Microsoft.Kinect.Face;

namespace Htw.Cave.Kinect
{
	[AddComponentMenu("Htw.Cave/Kinect/Kinect High Definition Manager")]
	[RequireComponent(typeof(KinectManager))]
    public class KinectHDManager : MonoBehaviour
    {
		private KinectHDFaceTracker hdFaceTracker;
		private KinectManager manager;
		private KinectSensor sensor;

		public void Awake()
		{
			this.manager = base.GetComponent<KinectManager>();

			if(this.manager.Settings.TrackingMode != KinectTrackingMode.BodyAndHDFace)
				base.enabled = false;
		}

		public void Start()
		{
			this.sensor = this.manager.Sensor();
			InitializeTracker();
		}

		public void Update()
		{
			this.hdFaceTracker.Track();
		}

		public void ConnectActorToHDFace(KinectActor actor)
		{
			if(actor == null)
				return;

			this.hdFaceTracker.ConnectToBody(actor.Body);
		}

		public FaceAlignment FaceAlignment()
		{
			return this.hdFaceTracker.FaceAlignment;
		}

		public FaceModel FaceModel()
		{
			return this.hdFaceTracker.FaceModel;
		}

		public UnityEngine.Quaternion FaceRotation()
		{
			return this.hdFaceTracker.FaceAlignment.FaceOrientation.ToUnityQuaternion().LeftHanded();
		}

		private void InitializeTracker()
		{
			this.hdFaceTracker = new KinectHDFaceTracker(this.sensor);
			this.hdFaceTracker.Initialize();
		}
	}
}
