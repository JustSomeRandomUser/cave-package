using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;

namespace Htw.Cave.Kinect
{
	[AddComponentMenu("Htw.Cave/Kinect/Kinect Manager")]
    public class KinectManager : MonoBehaviour
    {
		[SerializeField]
		private KinectSettings settings;
		public KinectSettings Settings
		{
			get { return this.settings; }
			set { this.settings = value; }
		}

#if UNITY_EDITOR
		[SerializeField]
		private bool enableInEditor;
		public bool EnableInEditor
		{
			get { return this.enableInEditor; }
			set { this.enableInEditor = value; }
		}
#endif

		private KinectSensor sensor;
		private KinectBodyTracker bodyTracker;
		private KinectFaceTracker faceTracker;
		private KinectActor[] actors;

		public void Awake()
		{
#if UNITY_EDITOR
			if(!this.enableInEditor)
			{
				base.enabled = false;
				return;
			}
#endif
			if(this.Settings.TrackingMode == KinectTrackingMode.BodyAndHDFace)
				if(base.GetComponent<KinectHDManager>() == null)
					gameObject.AddComponent<KinectHDManager>();
		}

		public void Start()
		{
			this.sensor = KinectSensor.GetDefault();

			if(this.sensor == null)
			{
				base.enabled = false;
				throw new InvalidOperationException("Kinect Manager cannot find a Kinect Sensor.");
			}

			InitializeTracker();
			InitializeActors();
		}

		public void Update()
		{
			this.bodyTracker.Track();
			this.faceTracker.Track(this.bodyTracker.Bodies);

			KinectActor.Floor = this.bodyTracker.Floor.ToUnityVector4();
			KinectActor.RefreshAll(this.actors, this.bodyTracker.Bodies, this.faceTracker.FaceFrameResults);
		}

		public KinectSensor Sensor()
		{
			return this.sensor;
		}

		public KinectActor[] Actors()
		{
			return this.actors;
		}

		public Vector3 CenterOfTrackingArea()
		{
			Vector3[] area = this.settings.TrackingArea;

			return new Vector3(
				(area[0].x + area[1].x + area[2].x + area[3].x) / 4,
				(area[0].y + area[1].y + area[2].y + area[3].y) / 4,
				(area[0].z + area[1].z + area[2].z + area[3].z) / 4
			);
		}

		private void InitializeTracker()
		{
			this.bodyTracker = new KinectBodyTracker(this.sensor);
			this.faceTracker = new KinectFaceTracker(this.sensor);

			this.bodyTracker.Initialize();
			this.faceTracker.Initialize();
		}

		private void InitializeActors()
		{
			KinectActor.Origin = this.settings.SensorLocation;
			this.actors = new KinectActor[this.sensor.BodyFrameSource.BodyCount];

			for(int i = this.actors.Length - 1; i >= 0; --i)
				this.actors[i] = new KinectActor();
		}
	}
}
