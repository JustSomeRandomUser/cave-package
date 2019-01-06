using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Display;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Initializer")]
    public class ProjectorInitializer : MonoBehaviour
    {
		[SerializeField]
		private ProjectorSettings settings;
		public ProjectorSettings Settings
		{
			get { return this.settings; }
			set { this.settings = value; }
		}

		[SerializeField]
		private ProjectorHead head;
		public ProjectorHead Head
		{
			get { return this.head; }
			set { this.head = value; }
		}

		public void Awake()
		{
			InitializeManagers();
			InitializeQuality();
		}

		public void InitializeCameras()
		{
			Camera[] cameras = base.GetComponentsInChildren<Camera>(false);

			for(int i = cameras.Length - 1; i >= 0; --i)
			{
				cameras[i].stereoConvergence = this.settings.StereoConvergence;
				cameras[i].stereoSeparation = this.settings.StereoSeparation;
			}
		}

		public ProjectorData[] Collect()
		{
			ProjectorEmitter[] emitters = base.GetComponentsInChildren<ProjectorEmitter>(false);

			int length = emitters.Length;
			ProjectorData[] data = new ProjectorData[length];

			for(int i = length - 1; i >= 0; --i)
			{
				data[i] = new ProjectorData(emitters[i], emitters[i].Plane, emitters[i].Configuration);

				if(!this.settings.UseBimber)
					data[i].bimber = Matrix4x4.identity;
				else
					emitters[i].Configuration.RecomputeBimber();

				if(this.settings.UseMask)
					emitters[i].ApplyMask();
			}

			this.head.SetEyeSeparation(this.settings.StereoSeparation);

			return data;
		}

		private void InitializeManagers()
		{
			ProjectorManager[] managers = base.GetComponents<ProjectorManager>();
			for(int i = managers.Length - 1; i >= 0; --i)
				if(managers[i].DisplayOutput == this.settings.Output)
					managers[i].enabled = true;
				else
					managers[i].enabled = false;
		}

		private void InitializeQuality()
		{
			if(this.settings.VSync == VSyncMode.DontSync)
				QualitySettings.vSyncCount = 0;
		}
    }
}
