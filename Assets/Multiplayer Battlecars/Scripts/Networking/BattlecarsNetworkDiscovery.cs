using System;
using System.Net;
using Mirror;
using Mirror.Discovery;

using UnityEngine;
using UnityEngine.Events;

using Battlecars.UI;

/*
	Discovery Guide: https://mirror-networking.com/docs/Guides/NetworkDiscovery.html
    Documentation: https://mirror-networking.com/docs/Components/NetworkDiscovery.html
    API Reference: https://mirror-networking.com/docs/api/Mirror.Discovery.NetworkDiscovery.html
*/

namespace Battlecars.Networking
{

    // Data to send to the client
    public class DiscoveryRequest : NetworkMessage
    {
        // The name of the game being sent
        public string gameName;
    }

    // Recieved by the client and converted
    public class DiscoveryResponse : NetworkMessage
    {
        // The server that sent this message
        // this is a property so that it is not serialized but the client
        // fills this up after we recieve it
        public IPEndPoint EndPoint { get; set; }

        public Uri uri;

        public long serverId;

        // The name of the game being sent
        public string gameName;
    }

    [Serializable] public class ServerFoundEvent : UnityEvent<DiscoveryResponse> {}

    public class BattlecarsNetworkDiscovery : NetworkDiscoveryBase<DiscoveryRequest, DiscoveryResponse>
    {
        #region Server

        public long ServerId { get; private set; }

        [Tooltip("Transport to be advertised during discovery")]
        public Transport transport;

        [Tooltip("Invoked when a server is found")]
        public ServerFoundEvent onServerFound = new ServerFoundEvent();

        private Lobby lobby;

        public override void Start()
        {
            ServerId = RandomLong();

            // If the transport wasn't set in the inspector,
            // find the active one. activeTransport is set in awake
            if(transport == null)
                transport = Transport.activeTransport;

            base.Start();
        }

        private void Update()
        {
            if(lobby == null)
                lobby = FindObjectOfType<Lobby>();
        }

        /// <summary>
        /// Process the request from a client
        /// </summary>
        /// <remarks>
        /// Override if you wish to provide more information to the clients
        /// such as the name of the host player
        /// </remarks>
        /// <param name="_request">Request coming from client</param>
        /// <param name="_endpoint">Address of the client that sent the request</param>
        /// <returns>A message containing information about this server</returns>
        protected override DiscoveryResponse ProcessRequest(DiscoveryRequest _request, IPEndPoint _endpoint) 
        {
            try
            {
                // This is just an example reply message,
                // you could add the game name here or game mode
                // if the player wants a specific game mode.
                return new DiscoveryResponse()
                {
                    serverId = ServerId,
                    uri = transport.ServerUri(),
                    gameName = lobby.LobbyName
                };
            }
            catch(NotImplementedException)
            {
                // Someone dun goofed, so let us know what happened.
                Debug.LogError($"Transport {transport} does not support network discovery");
                throw;
            }
        }

        #endregion

        #region Client

        /// <summary>
        /// Create a message that will be broadcasted on the network to discover servers
        /// </summary>
        /// <remarks>
        /// Override if you wish to include additional data in the discovery message
        /// such as desired game mode, language, difficulty, etc... </remarks>
        /// <returns>An instance of ServerRequest with data to be broadcasted</returns>
        protected override DiscoveryRequest GetRequest() => new DiscoveryRequest();

        /// <summary>
        /// Process the answer from a server
        /// </summary>
        /// <remarks>
        /// A client receives a reply from a server, this method processes the
        /// reply and raises an event
        /// </remarks>
        /// <param name="_response">Response that came from the server</param>
        /// <param name="_endpoint">Address of the server that replied</param>
        protected override void ProcessResponse(DiscoveryResponse _response, IPEndPoint _endpoint) 
        {
            // We don't fully understand this code, we just know it's something we need to do.
            #region WTF
            // we recieved a message from the remote endpoint
            _response.EndPoint = _endpoint;

            // although we got a supposedly valid url we may not be able to resolve
            // the provided host
            // However we know the real ip address ip address of the server because we just
            // recieve a packet from it, so use that as host.
            UriBuilder realUri = new UriBuilder(_response.uri)
            {
                Host = _response.EndPoint.Address.ToString()
            };
            _response.uri = realUri.Uri;
            #endregion

            onServerFound.Invoke(_response);
        }

        #endregion
    }
}