using System.Collections.Generic;
using UnityEngine;

namespace BreadAndButter.Mobile
{
    public class GridManager : MonoBehaviour
    {
        #region Grid Stuff
        public List<Sprite> candies = new List<Sprite>(); //Images of tiles
        public GameObject tilePrefab; //display of tile sprites
        public int gridDimension = 8; //Stores the size of the Grid
        public float distance = 1.0f; //Distance of the cells in the game
        private GameObject[,] grid; //2D array IRL Grid
        public static GridManager Instance { get; private set; } //Singleton of GridManager
        #endregion

        private void Awake()
        {
            Instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            grid = new GameObject[gridDimension, gridDimension];
            InitialiseGrid();

        }

        /// <summary>
        /// This function obtains the coordinates of the tile and ensures validity,
        /// if less than or greater than the size of the table then return null.
        /// </summary>
        /// <param name="column">Tile input index</param>
        /// <param name="row">Tile input index</param>
        /// <returns></returns>
        private Sprite GetSpriteAt(int column, int row)
        {
            if (column < 0 || column >= gridDimension
                || row < 0 || row >= gridDimension)
                return null;
            GameObject tile = grid[column, row];
            SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
            return spriteRenderer.sprite;
        }

        public void InitialiseGrid()
        {
            //Calculate offset so the grid is centered in the position of the gameobject (this value will be added to the position of every tile/cell)
            Vector3 posOffset = transform.position - new Vector3(gridDimension * distance / 2.0f, gridDimension * distance / 2.0f, 0);
            for (int row = 0; row < gridDimension; row++) //loop through all candy sprites
            {
                for (int column = 0; column < gridDimension; column++)
                {
                    #region Spawn Grid with no more than 3 matches
                    //Copy of candies list to generate random grid with matches with no more than 3 
                    List<Sprite> possibleCandies = new List<Sprite>(candies); 
                    Sprite left1 = GetSpriteAt(column - 1, row); //GetSpriteAt 2 cells to the left
                    Sprite left2 = GetSpriteAt(column - 2, row); //GetSpriteAt 2 cells to the left

                    //To check if a sprite is valid or not in order for the cells to be different
                    if (left2 != null && left1 == left2)
                    {
                        possibleCandies.Remove(left1);
                    }

                    Sprite down1 = GetSpriteAt(column, row - 1); //GetSpriteAt 2 cells downwards
                    Sprite down2 = GetSpriteAt(column, row - 2); //GetSpriteAt 2 cells downwards
                    //To check if a sprite is valid or not in order for the cells to be different
                    if (down2 != null && down1 == down2)
                    {
                        possibleCandies.Remove(down1);
                    }
                    #endregion

                    #region Spawning Random Grid
                    GameObject newTile = Instantiate(tilePrefab);
                    SpriteRenderer spriteRenderer = newTile.GetComponent<SpriteRenderer>();
                    spriteRenderer.sprite = candies[Random.Range(0, candies.Count)];
                    //Add possibleCandies copy of candies list to spawn in grid
                    spriteRenderer.sprite = possibleCandies[Random.Range(0, possibleCandies.Count)];
                    Tile tile = newTile.AddComponent<Tile>(); //Add Tile instance to the newTile
                    tile.position = new Vector2Int(column, row); //Tell tile its position on the grid
                    newTile.transform.parent = transform;
                    newTile.transform.position = new Vector3(column * distance, row * distance, 0) + posOffset;
                    grid[column, row] = newTile; //Save reference to newTile
                    #endregion
                }
            }
        }

        /// <summary>
        /// Function to swap tiles and takes position of two cells of the grid.
        /// </summary>
        /// <param name="tile1Pos">Represent position of tile1</param>
        /// <param name="tile2Pos">Represent position of tile2</param>
        public void SwapTiles(Vector2Int tile1Pos, Vector2Int tile2Pos)
        {
            //Get SpriteRends of each sprite
            GameObject tile1 = grid[tile1Pos.x, tile1Pos.y];
            SpriteRenderer renderer1 = tile1.GetComponent<SpriteRenderer>();

            //Get SpriteRends of each sprite
            GameObject tile2 = grid[tile2Pos.x, tile2Pos.y];
            SpriteRenderer renderer2 = tile2.GetComponent<SpriteRenderer>();

            //Exchange sprites
            Sprite tempSprite = renderer1.sprite;
            renderer1.sprite = renderer2.sprite;
            renderer2.sprite = tempSprite;
        }
    }
}
