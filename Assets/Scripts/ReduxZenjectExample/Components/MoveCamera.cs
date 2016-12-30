using UnityEngine;
using UniRx;
using Zenject;

namespace Reduxity.Example.Zenject {

    public class MoveCamera : IInitializable {

        readonly App app_;
        readonly Camera camera_; // bound through ZenjectBinding script on GameObject

        public MoveCamera(
            App app,
            Camera camera // via ZenjectBinding in editor
        ) {
            app_ = app;
            camera_ = camera;
        }

        public void Initialize() {
            // Debug.Log($"In MoveCamera.Initialize, App.Store: {app_.Store}");
            RenderLook();
        }

        /// <summary>
        /// Subscribe to store state changes and then render.
        /// </summary>
        void RenderLook() {
            app_.Store
                .Subscribe(state => {
                    // Debug.Log($"looking by rotation: {state.Camera.localRotation}");
                    camera_.transform.localRotation = state.Camera.localRotation;
                })
                .AddTo(camera_);
        }
    }
}
