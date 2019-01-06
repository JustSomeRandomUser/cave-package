using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Kinect
{
	[CreateAssetMenu(fileName = "New Kinect Settings", menuName = "Htw.Cave/Kinect Settings", order = 30)]
    public class KinectSettings : ScriptableObject
    {
		[SerializeField]
		private KinectTrackingMode trackingMode;
		public KinectTrackingMode TrackingMode
		{
			get { return trackingMode; }
			set { this.trackingMode = value; }
		}

		[SerializeField]
		private Vector3 sensorLocation;
		public Vector3 SensorLocation
		{
			get { return sensorLocation; }
			set { this.sensorLocation = value; }
		}

		[SerializeField]
		private Vector3[] trackingArea;
		public Vector3[] TrackingArea
		{
			get { return trackingArea; }
			set { this.trackingArea = value; }
		}
    }
}
