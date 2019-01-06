using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Kinect
{
	[AddComponentMenu("Htw.Cave/Kinect/Kinect Head")]
    public class KinectHead : MonoBehaviour
    {
		public KinectActor Actor { get; private set; }

		private KinectManager manager;
		private KinectHDManager hdManager;
		private bool hdFace;
		private float framesUntilSearch;
		private Vector3 center;

		public void Awake()
		{
			this.manager = base.GetComponentInParent<KinectManager>();
			this.hdManager = base.GetComponentInParent<KinectHDManager>();

			if(this.manager == null)
			{
				base.enabled = false;
				throw new InvalidOperationException("Kinect Head needs a Kinect Manager as parent.");
			}

#if UNITY_EDITOR
			if(!manager.EnableInEditor)
				base.enabled = false;
#endif

			this.framesUntilSearch = 60;
			this.center = this.manager.CenterOfTrackingArea();
		}

		public void Start()
		{
			this.hdFace = this.hdManager.enabled;
		}

		public void Update()
		{
			if(this.framesUntilSearch == 0)
			{
				this.Actor = this.manager.Actors().ClosestToPoint(this.center);

				if(this.hdFace)
					this.hdManager.ConnectActorToHDFace(this.Actor);

				this.framesUntilSearch = 60;
			}

			if(this.Actor != null)
			{
				UpdatePosition();
				UpdateRotation();
			}

			--this.framesUntilSearch;
		}

		public void UpdatePosition()
		{
			transform.localPosition = this.Actor.GetHeadPosition();
		}

		public void UpdateRotation()
		{
			if(this.hdFace)
				transform.localRotation = Quaternion.Slerp(transform.localRotation, this.hdManager.FaceRotation(), Time.deltaTime * 0.8f);
			else
				transform.localRotation = Quaternion.Slerp(transform.localRotation, this.Actor.GetFaceRotation(), Time.deltaTime * 0.8f);
		}
	}
}
