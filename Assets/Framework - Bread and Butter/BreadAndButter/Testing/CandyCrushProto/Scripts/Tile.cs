using UnityEngine;

using NullReferenceException = System.NullReferenceException; //Same for null reference exceptions - gives feedback to users and us about specific errors that occur for specific reasons
using InvalidOperationException = System.InvalidOperationException; //If there is one component of UnityEngine that we want to access

namespace BreadAndButter.Mobile
{
    //THIS SCRIPT WILL START MAKING THE TILES MOVE

    public class Tile : MonoBehaviour
    {
        #region Tile Stuff
        private static Tile selectedTile; //Static variable to store currently selected tile
        private SpriteRenderer sRend;
        public Vector2Int position; //Stores location info to signal grid manager to swap tiles
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            sRend = GetComponent<SpriteRenderer>(); //To change the colors of the tiles on touch
            //MobileInput.Initialise();
        }

        public void SelectTile()
        {
            sRend.color = Color.grey; //Change selected color to grey
        }

        public void UnselectTile()
        {
            sRend.color = Color.white;
        }

        #region TEMP TOUCH INPUT
        private void OnMouseDown()
        {
            //If we click on a valid tile then call unselected since we haven't moved it or matched it 
            if(selectedTile != null)
            {
                selectedTile.UnselectTile();

                #region If distance between tiles is 1, then swap positions of 2 tiles
                if (Vector2Int.Distance(selectedTile.position, position) == 1)
                {
                    GridManager.Instance.SwapTiles(position, selectedTile.position);
                    selectedTile = null;
                }
                else //Else do not swap
                {
                    selectedTile = this; //Tile script is an instance
                    SelectTile(); //Call selected tile to make tile sprite grey
                }
                #endregion
            }
            else 
            {
                selectedTile = this; //Tile script is an instance
                SelectTile(); //Call selected tile to make tile sprite grey
            }
        }
        #endregion

        #region Framework Touch Input - Swiping
        //USE FRAMEWORK TOUCH INPUT HERE FOR SWIPING!!!
        #endregion

    }
}
