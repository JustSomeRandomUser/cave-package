using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;
using Microsoft.Kinect.Face;

namespace Htw.Cave.Kinect
{
    public sealed class KinectHDFaceTracker
    {
		public FaceModel FaceModel { get; private set; }
		public FaceAlignment FaceAlignment { get; private set; }

		private KinectSensor sensor;
		private HighDefinitionFaceFrameSource hdFaceFrameSource;
		private HighDefinitionFaceFrameReader hdFaceFrameReader;

		public KinectHDFaceTracker(KinectSensor sensor)
		{
			this.sensor = sensor;
		}

		public void Initialize()
		{
			this.hdFaceFrameSource = HighDefinitionFaceFrameSource.Create(this.sensor);
			this.hdFaceFrameReader = this.hdFaceFrameSource.OpenReader();

			if(!this.sensor.IsOpen)
				this.sensor.Open();
		}

		public void ConnectToBody(Body body)
		{
			if(body == null)
				return;

			hdFaceFrameSource.TrackingId = body.TrackingId;
		}

		public void Track()
		{
			if(!this.hdFaceFrameSource.IsTrackingIdValid)
				return;

			using(HighDefinitionFaceFrame hdFaceFrame = this.hdFaceFrameReader.AcquireLatestFrame())
			{
				if(hdFaceFrame == null)
					return;

				if(hdFaceFrame.IsFaceTracked)
				{
					hdFaceFrame.GetAndRefreshFaceAlignmentResult(this.FaceAlignment);
					this.FaceModel = hdFaceFrame.FaceModel;
				}
			}
		}

		public void Stop()
		{
			if(this.FaceModel != null)
			{
				this.FaceModel.Dispose();
				this.FaceModel = null;
			}

			if (this.hdFaceFrameReader != null)
			{
				this.hdFaceFrameReader.Dispose();
				this.hdFaceFrameReader = null;
			}

			if (this.hdFaceFrameSource != null)
				this.hdFaceFrameSource = null;

			if(this.sensor != null)
			{
				if(this.sensor.IsOpen)
					this.sensor.Close();
				this.sensor = null;
			}
		}
    }
}
