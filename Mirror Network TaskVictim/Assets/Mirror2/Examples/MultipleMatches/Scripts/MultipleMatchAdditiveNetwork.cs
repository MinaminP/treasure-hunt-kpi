using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mirror.Examples.MultipleMatch
{
    [AddComponentMenu("")]
    public class MultipleMatchAdditiveNetwork : NetworkManager
    {
        [Header("Match GUI")]
        public GameObject canvas;
        public CanvasController canvasController;

        [Header("Additive Scenes - First is start scene")]

        [Scene, Tooltip("Add additive scenes here.\nFirst entry will be players' start scene")]
        public string[] additiveScenes;

        // This is set true after server loads all subscene instances
        bool subscenesLoaded;

        // This is managed in LoadAdditive, UnloadAdditive, and checked in OnClientSceneChanged
        bool isInTransition;

        public static new MultipleMatchAdditiveNetwork singleton { get; private set; }

        /// <summary>
        /// Runs on both Server and Client
        /// Networking is NOT initialized when this fires
        /// </summary>
        public override void Awake()
        {
            base.Awake();
            singleton = this;
            canvasController.InitializeData();
        }

        /// <summary>
        /// Called on the server when a scene is completed loaded, when the scene load was initiated by the server with ServerChangeScene().
        /// </summary>
        /// <param name="sceneName">The name of the new scene.</param>
        public override void OnServerSceneChanged(string sceneName)
        {
            // This fires after server fully changes scenes, e.g. offline to online
            // If server has just loaded the Container (online) scene, load the subscenes on server
            if (sceneName == onlineScene)
                StartCoroutine(ServerLoadSubScenes());
        }

        IEnumerator ServerLoadSubScenes()
        {
            foreach (string additiveScene in additiveScenes)
                yield return SceneManager.LoadSceneAsync(additiveScene, new LoadSceneParameters
                {
                    loadSceneMode = LoadSceneMode.Additive,
                    localPhysicsMode = LocalPhysicsMode.Physics3D // change this to .Physics2D for a 2D game
                });

            subscenesLoaded = true;
        }

        public override void OnClientChangeScene(string sceneName, SceneOperation sceneOperation, bool customHandling)
        {
            //Debug.Log($"{System.DateTime.Now:HH:mm:ss:fff} OnClientChangeScene {sceneName} {sceneOperation}");

            if (sceneOperation == SceneOperation.UnloadAdditive)
                StartCoroutine(UnloadAdditive(sceneName));

            if (sceneOperation == SceneOperation.LoadAdditive)
                StartCoroutine(LoadAdditive(sceneName));
        }

        IEnumerator LoadAdditive(string sceneName)
        {
            isInTransition = true;

            // host client is on server...don't load the additive scene again
            if (mode == NetworkManagerMode.ClientOnly)
            {
                // Start loading the additive subscene
                loadingSceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

                while (loadingSceneAsync != null && !loadingSceneAsync.isDone)
                    yield return null;
            }

            // Reset these to false when ready to proceed
            NetworkClient.isLoadingScene = false;
            isInTransition = false;

            OnClientSceneChanged();
        }

        IEnumerator UnloadAdditive(string sceneName)
        {
            isInTransition = true;

            // host client is on server...don't unload the additive scene here.
            if (mode == NetworkManagerMode.ClientOnly)
            {
                yield return SceneManager.UnloadSceneAsync(sceneName);
                yield return Resources.UnloadUnusedAssets();
            }

            // Reset these to false when ready to proceed
            NetworkClient.isLoadingScene = false;
            isInTransition = false;

            OnClientSceneChanged();

            // There is no call to FadeOut here on purpose.
            // Expectation is that a LoadAdditive or full scene change
            // will follow that will call FadeOut after that scene loads.
        }

        /// <summary>
        /// Called on clients when a scene has completed loaded, when the scene load was initiated by the server.
        /// <para>Scene changes can cause player objects to be destroyed. The default implementation of OnClientSceneChanged in the NetworkManager is to add a player object for the connection if no player object exists.</para>
        /// </summary>
        /// <param name="conn">The network connection that the scene change message arrived on.</param>
        public override void OnClientSceneChanged()
        {
            // Only call the base method if not in a transition.
            // This will be called from DoTransition after setting doingTransition to false
            // but will also be called first by Mirror when the scene loading finishes.
            if (!isInTransition)
                base.OnClientSceneChanged();
        }

        #region Server System Callbacks

        /// <summary>
        /// Called on the server when a client is ready.
        /// <para>The default implementation of this function calls NetworkServer.SetClientReady() to continue the network setup process.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerReady(NetworkConnectionToClient conn)
        {
            base.OnServerReady(conn);
            canvasController.OnServerReady(conn);
        }

        /// <summary>
        /// Called on the server when a client disconnects.
        /// <para>This is called on the Server when a Client disconnects from the Server. Use an override to decide what should happen when a disconnection is detected.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            StartCoroutine(DoServerDisconnect(conn));
        }

        IEnumerator DoServerDisconnect(NetworkConnectionToClient conn)
        {
            yield return canvasController.OnServerDisconnect(conn);
            base.OnServerDisconnect(conn);
        }

        #endregion

        #region Client System Callbacks

        /// <summary>
        /// Called on the client when connected to a server.
        /// <para>The default implementation of this function sets the client as ready and adds a player. Override the function to dictate what happens when the client connects.</para>
        /// </summary>
        public override void OnClientConnect()
        {
            base.OnClientConnect();
            canvasController.OnClientConnect();
        }

        /// <summary>
        /// Called on clients when disconnected from a server.
        /// <para>This is called on the client when it disconnects from the server. Override this function to decide what happens when the client disconnects.</para>
        /// </summary>
        public override void OnClientDisconnect()
        {
            canvasController.OnClientDisconnect();
            base.OnClientDisconnect();
        }

        #endregion

        #region Start & Stop Callbacks

        /// <summary>
        /// This is invoked when a server is started - including when a host is started.
        /// <para>StartServer has multiple signatures, but they all cause this hook to be called.</para>
        /// </summary>
        public override void OnStartServer()
        {
            if (mode == NetworkManagerMode.ServerOnly)
                canvas.SetActive(true);

            canvasController.OnStartServer();
        }

        /// <summary>
        /// This is invoked when the client is started.
        /// </summary>
        public override void OnStartClient()
        {
            canvas.SetActive(true);
            canvasController.OnStartClient();
        }

        /// <summary>
        /// This is called when a server is stopped - including when a host is stopped.
        /// </summary>
        public override void OnStopServer()
        {
            canvasController.OnStopServer();
            canvas.SetActive(false);
        }

        /// <summary>
        /// This is called when a client is stopped.
        /// </summary>
        public override void OnStopClient()
        {
            canvasController.OnStopClient();
        }

        #endregion
    }
}
