using Mirror;

using UnityEngine;
using UnityEngine.SceneManagement;

namespace NetworkGame.Networking
{
    [RequireComponent(typeof(PlayerController))]
    public class NetworkPlayer : NetworkBehaviour
    {
        [SerializeField] private GameObject enemyToSpawn;
        [SyncVar(hook = nameof(OnSetCubeColor)), SerializeField] private Color cubeColor;

        private readonly SyncList<float> syncedFloats = new SyncList<float>();

        // SyncVarHooks get called in the order the VARIABLES are defined not the functions
        // [SyncVar(hook = "SetX")] public float x;
        // [SyncVar(hook = "SetY")] public float y;
        // [SyncVar(hook = "SetZ")] public float z;
        //
        // [Command]
        // public void CmdSetPosition(float _x, float _y, float _z)
        // {
        //     z = _z;
        //     x = _x;
        //     y = _y;
        // }
        
        private Material cachedMaterial;

        // Typical naming convention for SyncVarHooks is OnSet<VariableName>
        private void OnSetCubeColor(Color _old, Color _new)
        {
            if(cachedMaterial == null)
                cachedMaterial = gameObject.GetComponent<MeshRenderer>().material;

            cachedMaterial.color = _new;
        }
        
        private void Awake()
        {
            // This will run REGARDLESS if we are the local or remote player
        }

        private void Update()
        {
            // MeshRenderer render = gameObject.GetComponent<MeshRenderer>();
            // render.material.color = cubeColor;

            string floatString = "";
            for(int i = 0; i < syncedFloats.Count; i++)
                floatString += syncedFloats[i] + ",";
            
            Debug.Log(floatString, gameObject);
            
            // First determine if this function is being run on the local player
            if(isLocalPlayer)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    // Run a function that tells every client to change the colour of this gameObject
                    CmdRandomColor();
                }

                if(Input.GetKeyDown(KeyCode.E))
                {
                    CmdSpawnEnemy();
                }
            }
        }

        [Command]
        public void CmdSpawnEnemy()
        {
            // NetworkServer.Spawn requires an instance of the object in the server's scene to be present
            // so if the object being spawned is a prefab, instantiate needs to be called first
            GameObject newEnemy = Instantiate(enemyToSpawn);
            NetworkServer.Spawn(newEnemy);
        }
        
        // RULES FOR COMMANDS:
        // 1. Cannot return anything
        // 2. Must follow the correct naming convention: The function name MUST start with 'Cmd' exactly like that
        // 3. The function must have the attribute [Command] found in Mirror namespace
        // 4. Can only be certain serializable types (see Command in the documentation)
        [Command]
        public void CmdRandomColor()
        {
            // SyncVar MUST be set on the server, otherwise it won't be synced between clients
            cubeColor = Random.ColorHSV(0, 1, 1, 1, 1, 1);

            // This is running on the server
            // RpcRandomColor(Random.Range(0f, 1f));
        }
        
        // RULES FOR CLIENT RPC:
        // 1. Cannot return anything
        // 2. Must follow the correct naming convention: The function name MUST start with 'Rpc' exactly like that
        // 3. The function must have the attribute [ClientRpc] found in Mirror namespace
        // 4. Can only be certain serializable types (see Command in the documentation)
        // [ClientRpc]
        // public void RpcRandomColor(float _hue)
        // {
        //     // This is running on every instance of the same object that the client was calling from.
        //     // i.e. Red GO on Red Client runs Cmd, Red GO on Red, Green and Blue client's run Rpc
        //     MeshRenderer rend = gameObject.GetComponent<MeshRenderer>();
        //     rend.material.color = Color.HSVToRGB(_hue, 1, 1);
        // }

        // This is run via the network starting and the player connecting...
        // NOT Unity
        // It is run when the object is spawned via the networking system NOT when Unity
        // instantiates the object
        public override void OnStartLocalPlayer()
        {
            // This is run if we are the local player and NOT a remote player
            SceneManager.LoadSceneAsync("Lobby", LoadSceneMode.Additive);
        }

        // This is run via the network starting and the player connecting...
        // NOT Unity
        // It is run when the object is spawned via the networking system NOT when Unity
        // instantiates the object
        public override void OnStartClient()
        {
            // This will run REGARDLESS if we are the local or remote player
            // isLocalPlayer is true if this object is the client's local player otherwise it's false
            PlayerController controller = gameObject.GetComponent<PlayerController>();
            controller.enabled = isLocalPlayer;
            
            CustomNetworkManager.AddPlayer(this);
        }

        public override void OnStopClient()
        {
            CustomNetworkManager.RemovePlayer(this);
        }

        // This runs when the server starts... ON the server on all clients
        public override void OnStartServer()
        {
            for(int i = 0; i < 10; i++)
                syncedFloats.Add(Random.Range(0, 11));
        }

        public void StartMatch()
        {
            if(isLocalPlayer)
                CmdStartMatch();
        }

        [Command]
        public void CmdStartMatch()
        {
            MatchManager.instance.StartMatch();
        }
    }
}