using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityStandardAssets.ImageEffects;
using Htw.Cave.Util;

namespace Htw.Cave.Projector
{
	[AddComponentMenu("Htw.Cave/Projector/Projector Emitter")]
	[RequireComponent(typeof(Camera))]
	[RequireComponent(typeof(ScreenOverlay))]
    public sealed class ProjectorEmitter : MonoBehaviour
    {
		[SerializeField]
		private ProjectorConfiguration configuration;
		public ProjectorConfiguration Configuration
		{
			get { return this.configuration; }
			set { this.configuration = value; }
		}

		[SerializeField]
		private ProjectorPlane plane;
		public ProjectorPlane Plane
		{
			get { return this.plane; }
			set { this.plane = value; }
		}

		private Camera cam;
		private ScreenOverlay overlay;
		private PixelMask pixelMask;

		public void Awake()
		{
			this.cam = base.GetComponent<Camera>();
			this.overlay = base.GetComponent<ScreenOverlay>();

			InitializePixelMask();
			InitializeCamera();
			ResizePlane();
		}

#if UNITY_EDITOR
		public void Reset()
		{
			this.overlay = base.GetComponent<ScreenOverlay>();
			this.overlay.overlayShader = Shader.Find("Hidden/BlendModesOverlay");
			this.overlay.blendMode = ScreenOverlay.OverlayBlendMode.AlphaBlend;
		}
#endif

		public void ApplyMask()
		{
			Texture2D texture = new Texture2D(this.cam.pixelWidth, this.cam.pixelHeight);
			this.pixelMask.Apply(texture, Color.black);
			this.overlay.texture = texture;
		}

		public void EnableMask(bool enable)
		{
			if(enable)
			{
				this.overlay.enabled = true;
				ApplyMask();
			} else {
				this.overlay.enabled = false;
			}
		}

		public void ResizePlane()
		{
			this.plane.Resize(this.configuration.Width, this.configuration.Height);
		}

		private void InitializeCamera()
		{
			this.cam.fieldOfView = this.configuration.FieldOfView;
			this.cam.nearClipPlane = this.configuration.Near;
			this.cam.farClipPlane = this.configuration.Far;
		}

		private void InitializePixelMask()
		{
			this.pixelMask = this.configuration.GeneratePixelMask();
		}
    }
}
