using UnityEngine;
using Zenject;
using System;
using Middleware = Reduxity.Middleware;

namespace Reduxity.Example.Zenject {
    [CreateAssetMenu(fileName = "ReduxZenjectSettingsInstaller", menuName = "Installers/ReduxZenjectSettingsInstaller")]
    public class ReduxZenjectSettingsInstaller : ScriptableObjectInstaller<ReduxZenjectSettingsInstaller> {

        /* These variables provide access nested classes below */
        public AppSettings App;
        public PlayerSettings Player;
        public HttpSettings Http;
        public NetworkSettings Network;

        [Serializable]
        /// <summary>
        /// App wide settings
        /// </summary>
        public class AppSettings {
            public Middleware.LoggerSettings LogLevel;
            public GameInstaller.Settings GameInstaller;
            public App.Settings App;
        }

        [Serializable]
        /// <summary>
        /// Settings related to the player including character and camera
        /// </summary>
        public class PlayerSettings {
            public CharacterMover.Settings Movement;
            public CameraLook.Settings Looking;
        }

        [Serializable]
        /// <summary>
        /// Settings related to Http requests
        /// </summary>
        public class HttpSettings {
            public ApiRequestor.Settings Api;
            public ApiRequest.Settings ApiRequest;
            public DisplayHttpButton.Settings SubmitButton;
            public DisplayHttpText.Settings Text;
        }

        [Serializable]
        /// <summary>
        /// Settings related to multiplayer network requests
        /// </summary>
        public class NetworkSettings {
            public CloudConnector.Settings Cloud;
            public ClientConnector.Settings Server;
            public LobbyConnector.Settings Lobby;
            public RoomConnector.Settings Room;
            public NetworkPlayer.Settings NetworkPlayer;
        }

        /// <summary>
        /// Binding settings as an instance
        /// </summary>
        public override void InstallBindings() {
            Container.BindInstance(Player.Movement);
            Container.BindInstance(Player.Looking);
            Container.BindInstance(Http.Api);
            Container.BindInstance(Http.ApiRequest);
            Container.BindInstance(Http.SubmitButton);
            Container.BindInstance(Http.Text);
            Container.BindInstance(Network.Cloud);
            Container.BindInstance(Network.Server);
            Container.BindInstance(Network.Lobby);
            Container.BindInstance(Network.Room);
            Container.BindInstance(Network.NetworkPlayer);
            Container.BindInstance(App.LogLevel);
            Container.BindInstance(App.GameInstaller);
            Container.BindInstance(App.App);
        }
    }
}
