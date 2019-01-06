using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Htw.Cave.Controller
{
	[AddComponentMenu("Htw.Cave/Controller/Generic Controller")]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]
	public class GenericController : MonoBehaviour
    {
		[SerializeField]
		private Transform head;
		public Transform Head
		{
			get { return head; }
			set { this.head = value; }
		}

		[SerializeField]
		private float speed;
		public float Speed
		{
			get { return speed; }
			set { this.speed = value; }
		}

		[SerializeField]
		private float sensitivity;
		public float Sensitivity
		{
			get { return sensitivity; }
			set { this.sensitivity = value; }
		}

		private CapsuleCollider capsule;
		private Rigidbody rigid;

		public void Awake()
		{
			this.capsule = base.GetComponent<CapsuleCollider>();
			this.rigid = base.GetComponent<Rigidbody>();
		}

		public void Start()
		{
			this.rigid.freezeRotation = true;
			CalculateCollider();
		}

		public void Update()
		{
			CalculateCollider();
		}

#if UNITY_EDITOR
		public void Reset()
		{
			this.capsule = base.GetComponent<CapsuleCollider>();
			this.capsule.center = new Vector3(0f, 1f, 0f);
			this.capsule.height = 2f;
		}
#endif

		public void Move(float h, float v)
		{
			Vector3 direction = new Vector3(h, 0f, v);
			direction = direction.normalized * speed * Time.deltaTime;

			transform.Translate(direction);
		}

		public void Rotate(float y)
		{
			Vector3 rotation = new Vector3(0f, y, 0f);
			rotation = rotation * sensitivity;

			transform.Rotate(rotation);
		}

		private void CalculateCollider()
		{
			Vector3 position = this.head.localPosition;

			this.capsule.center = new Vector3(position.x, position.y * 0.5f, position.z);
			this.capsule.height = position.y;
		}
	}
}
