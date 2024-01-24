using Spine.Unity;
using UnityEngine;

namespace Spine.Unity.Test
{
    public class AnimationController : MonoBehaviour
    {
        public BoyAnimations model;
        public SkeletonAnimation skeletonAnimation;
        public AnimationReferenceAsset run, idle, aim, shoot, jump, turn, walk;
        SpineBeginnerBodyState previousViewState;
        Animation nextAnimation;
        void Start()
        {
			if (skeletonAnimation == null) return;
            model.StartAimEvent += StartPlayingAim;
            model.StopAimEvent += StopPlayingAim;

        }
        void Update()
        {
            if ((skeletonAnimation.skeleton.ScaleX < 0) != model.facingLeft) 
            {  
                Turn(model.facingLeft);

            }

            SpineBeginnerBodyState currentModelState = model.state;

            if (previousViewState != currentModelState) 
            {
                PlayNewStableAnimation();
            }

            previousViewState = currentModelState;
        }

        void PlayNewStableAnimation() 
        {
            SpineBeginnerBodyState newModelState = model.state;

            // Add conditionals to not interrupt transient animations.
                if (newModelState == SpineBeginnerBodyState.Running) 
                {
                    nextAnimation = run;
                } 
                else if(newModelState == SpineBeginnerBodyState.Idle)
                {
                    nextAnimation = idle;
                }
                else if(newModelState == SpineBeginnerBodyState.Walk)
                {
                    nextAnimation = walk;
                }
            skeletonAnimation.AnimationState.ClearTrack(1);
            skeletonAnimation.AnimationState.SetAnimation(0, nextAnimation, true);
        }

        public void StartPlayingAim() 
        {
            // Play the aim animation on track 2 to aim at the mouse target.
            TrackEntry aimTrack = skeletonAnimation.AnimationState.SetAnimation(2, aim, true);
            
            aimTrack.AttachmentThreshold = 1f;
            aimTrack.MixDuration = 0f;
        }

        public void StopPlayingAim() 
        {
            skeletonAnimation.state.AddEmptyAnimation(2, 0.5f, 0.1f);
        }

        public void Turn(bool facingLeft) 
        {
            skeletonAnimation.AnimationState.ClearTrack(1);
            skeletonAnimation.Skeleton.ScaleX = facingLeft ? -1f : 1f;
            TrackEntry turnTrack = skeletonAnimation.AnimationState.SetAnimation(1, turn, false);
            skeletonAnimation.state.AddEmptyAnimation(1, 0.1f, 0);
        }
    }
}