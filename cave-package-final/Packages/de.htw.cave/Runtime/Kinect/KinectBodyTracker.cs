using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;

namespace Htw.Cave.Kinect
{
    public sealed class KinectBodyTracker
    {
		public Body[] Bodies { get; private set; }
		public Windows.Kinect.Vector4 Floor { get; private set; }

		private KinectSensor sensor;
		private MultiSourceFrameReader multiSourceFrameReader;

		public KinectBodyTracker(KinectSensor sensor)
		{
			this.sensor = sensor;
		}

		public void Initialize()
		{
			this.multiSourceFrameReader = sensor.OpenMultiSourceFrameReader(FrameSourceTypes.Body | FrameSourceTypes.BodyIndex | FrameSourceTypes.Depth);

			if(!this.sensor.IsOpen)
				this.sensor.Open();
		}

		public void Track()
		{
			if(this.multiSourceFrameReader == null)
				return;

			MultiSourceFrame multiFrame = this.multiSourceFrameReader.AcquireLatestFrame();

			if(multiFrame == null)
				return;

			if(this.Bodies == null)
				this.Bodies = new Body[this.sensor.BodyFrameSource.BodyCount];

			using(BodyFrame bodyFrame = multiFrame.BodyFrameReference.AcquireFrame())
			{
				if(bodyFrame == null)
					return;

				bodyFrame.GetAndRefreshBodyData(this.Bodies);
				this.Floor = bodyFrame.FloorClipPlane;
			}

			multiFrame = null;
		}

		public void Stop()
		{
			if(this.multiSourceFrameReader != null)
			{
				this.multiSourceFrameReader.Dispose();
				this.multiSourceFrameReader = null;
			}

			if(this.sensor != null)
			{
				if(this.sensor.IsOpen)
					this.sensor.Close();
				this.sensor = null;
			}
		}
    }
}
