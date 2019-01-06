using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Windows.Kinect;
using Microsoft.Kinect.Face;

namespace Htw.Cave.Kinect
{
    public sealed class KinectFaceTracker
    {
		public FaceFrameResult[] FaceFrameResults { get; private set; }

		private KinectSensor sensor;
		private FaceFrameSource[] faceFrameSources;
		private FaceFrameReader[] faceFrameReaders;

		public KinectFaceTracker(KinectSensor sensor)
		{
			this.sensor = sensor;
		}

		public void Initialize()
		{
			FaceFrameFeatures faceFrameFeatures =
				FaceFrameFeatures.BoundingBoxInColorSpace
                | FaceFrameFeatures.PointsInColorSpace
                | FaceFrameFeatures.BoundingBoxInInfraredSpace
                | FaceFrameFeatures.PointsInInfraredSpace
                | FaceFrameFeatures.RotationOrientation
                | FaceFrameFeatures.FaceEngagement
                | FaceFrameFeatures.Glasses
                | FaceFrameFeatures.Happy
                | FaceFrameFeatures.LeftEyeClosed
                | FaceFrameFeatures.RightEyeClosed
                | FaceFrameFeatures.LookingAway
                | FaceFrameFeatures.MouthMoved
				| FaceFrameFeatures.MouthOpen;

			this.FaceFrameResults = new FaceFrameResult[this.sensor.BodyFrameSource.BodyCount];
			this.faceFrameSources = new FaceFrameSource[this.sensor.BodyFrameSource.BodyCount];
			this.faceFrameReaders = new FaceFrameReader[this.sensor.BodyFrameSource.BodyCount];

			for(int i = this.faceFrameSources.Length - 1; i >= 0; --i)
			{
				this.faceFrameSources[i] = FaceFrameSource.Create(this.sensor, 0, faceFrameFeatures);
				this.faceFrameReaders[i] = this.faceFrameSources[i].OpenReader();
			}

			if(!this.sensor.IsOpen)
				this.sensor.Open();
		}

		public void Track(Body[] bodies)
		{
			if(bodies == null)
				return;

			for (int i = this.sensor.BodyFrameSource.BodyCount - 1; i >= 0; --i)
			{
				if(!this.faceFrameSources[i].IsTrackingIdValid)
				{
					if (bodies[i] != null && bodies[i].IsTracked)
		 				this.faceFrameSources[i].TrackingId = bodies[i].TrackingId;
					continue;
				}

				using(FaceFrame faceFrame = this.faceFrameReaders[i].AcquireLatestFrame())
				{
					if(faceFrame == null)
						continue;

					if(faceFrame.FaceFrameResult != null)
						this.FaceFrameResults[i] = faceFrame.FaceFrameResult;
				}
			}
		}

		public void Stop()
		{
			for(int i = this.faceFrameSources.Length - 1; i >= 0; --i)
			{
				if (this.faceFrameReaders[i] != null)
				{
					this.faceFrameReaders[i].Dispose();
					this.faceFrameReaders[i] = null;
				}

				if (this.faceFrameSources[i] != null)
					this.faceFrameSources[i] = null;
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
