using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class AimController : MonoBehaviour
{
		public SkeletonAnimation skeletonAnimation;
		[SpineBone(dataField: "skeletonAnimation")]
		public string boneName;
		[SpineBone(dataField: "skeletonAnimation")]
		public string boneName2;
		public Camera cam;
		public float distance;
		public float maxDistance;
		Bone bone;
		Bone bone2;
		void OnValidate() 
		{
			if (skeletonAnimation == null) skeletonAnimation = GetComponent<SkeletonAnimation>();
		}

		void Start() 
		{
			bone = skeletonAnimation.Skeleton.FindBone(boneName);
			bone2 = skeletonAnimation.Skeleton.FindBone(boneName2);
		}

		void Update() 
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 worldMousePosition = cam.ScreenToWorldPoint(mousePosition);
			Vector3 skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(worldMousePosition);
			skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
			skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;
            distance = Vector2.Distance(worldMousePosition, skeletonSpacePoint);
			bone.SetLocalPosition(skeletonSpacePoint);
			
		}
	}

