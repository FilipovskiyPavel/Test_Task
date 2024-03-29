using UnityEngine;

namespace Spine.Unity.Examples {
	public class SpineboyTargetController : MonoBehaviour {

		public SkeletonAnimation skeletonAnimation;
		[SpineBone(dataField: "skeletonAnimation")]
		public string boneName;
		[SpineBone(dataField: "skeletonAnimation")]
		public string boneName2;
		public Camera cam;
		public float maxDistance;
		Bone bone;
		Bone bone2;
		private float distance;
		void OnValidate() 
		{
			if (skeletonAnimation == null) skeletonAnimation = GetComponent<SkeletonAnimation>();
		}

		void Start() 
		{
			bone = skeletonAnimation.Skeleton.FindBone(boneName);
		}

		void Update() 
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 worldMousePosition = cam.ScreenToWorldPoint(mousePosition);
			Vector3 skeletonSpacePoint = skeletonAnimation.transform.InverseTransformPoint(worldMousePosition);
			skeletonSpacePoint.x *= skeletonAnimation.Skeleton.ScaleX;
			skeletonSpacePoint.y *= skeletonAnimation.Skeleton.ScaleY;
            float distance = Vector2.Distance(worldMousePosition, skeletonSpacePoint);
			bone.SetLocalPosition(skeletonSpacePoint);
			
		}
	}

}
