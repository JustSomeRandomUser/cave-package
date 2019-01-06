using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Htw.Cave.Util;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Live Debug/Live Debugger")]
	[RequireComponent(typeof(ProjectorInitializer))]
	[RequireComponent(typeof(ProjectorManager))]
    public class LiveDebugger : MonoBehaviour
    {
		private ProjectorInitializer initializer;
		private ProjectorManager manager;

		private float deltaTime;

		public void Awake()
		{
			this.initializer = base.GetComponent<ProjectorInitializer>();
			this.manager = base.GetComponent<ProjectorManager>();
			this.deltaTime = 0f;
		}

		public void Update()
		{
			this.deltaTime += (Time.unscaledDeltaTime - this.deltaTime) * 0.1f;
		}

		public float FramesPerSecond()
		{
			return 1f / this.deltaTime;
		}

		public float FrameTime()
		{
			return this.deltaTime * 1000f;
		}

    }
}
