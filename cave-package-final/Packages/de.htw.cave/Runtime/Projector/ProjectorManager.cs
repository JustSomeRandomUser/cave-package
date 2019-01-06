using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Htw.Cave.Display;

namespace Htw.Cave.Projector
{
	[RequireComponent(typeof(ProjectorInitializer))]
    public abstract class ProjectorManager : MonoBehaviour
    {
		public abstract DisplayOutput DisplayOutput { get; }

		protected ProjectorData[] data;
		protected ProjectorHead head;

		private ProjectorInitializer initializer;

		public void Awake()
		{
			this.initializer = base.GetComponent<ProjectorInitializer>();
		}

		public void Start()
		{
			Setup();
		}

		public void LateUpdate()
		{
			Render();
		}

		public void Setup()
		{
			this.data = this.initializer.Collect();
			this.head = this.initializer.Head;

			this.initializer.InitializeCameras();
			RefreshData();
			ResizeViewport();
		}

		public void RefreshData()
		{
			for(int i = this.data.Length - 1; i >= 0; --i)
			{
				data[i].RefreshAll();

				if(!this.initializer.Settings.UseBimber)
					data[i].bimber = Matrix4x4.identity;
			}
		}

		public void ResizeViewport()
		{
			//  x axis          y axis
			//   +---+---+       +-------+
			//   |   |   |       |_______|
			//   |   |   |       |_______|
			//   +---+---+ ...   +-------+ ...
			float steps = 1f / this.data.Length;
			float offset = 0f;

			SortedDictionary<int, Camera> table = new SortedDictionary<int, Camera>();

			for(int i = this.data.Length - 1; i >= 0; --i)
				if(table.ContainsKey(this.data[i].order))
					throw new InvalidOperationException("All order values must be unique.");
				else
					table.Add(this.data[i].order, this.data[i].camera);

			switch(this.initializer.Settings.Axis)
			{
				case ViewportAxis.X:
					foreach(int order in table.Keys)
					{
						table[order].rect = new Rect(offset, 0f, steps, 1f);
						offset += steps;
					}
					break;
				case ViewportAxis.Y:
					foreach(int order in table.Keys)
					{
						table[order].rect = new Rect(0f, offset, 1f, steps);
						offset += steps;
					}
					break;
			}
		}

		public abstract void Render();
    }
}
