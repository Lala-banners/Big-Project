using System.Collections;
using System.Collections.Generic;

using Serializable = System.SerializableAttribute;
using UnityEngine;

namespace BreadAndButter.Loot
{
    [CreateAssetMenu(menuName = "Bread and Butter/Loot/LootTable", fileName = "NewLootable")]
    public class LootTable : ScriptableObject
    {
        [Serializable]
        public class WeightedLoot
        {
            [SerializeField, Range(1, 100)] protected int weighting = 50;
            [SerializeField] protected Lootable loot;

            /// <summary>
            /// Ratio of items inside table.
            /// Weighting is how many items in the table
            /// </summary>
            /// <param name="_table"></param>
            public void AddLootToTable(ref List<Lootable> _table) //Actual Loot Table itself
            {
                //Add as many copies of the loot as weighting into the table
                for (int i = 0; i < weighting; i++)
                {
                    _table.Add(loot);
                }
            }
        }
        [SerializeField] private WeightedLoot[] possibleLoot;

        private List<Lootable> table = new List<Lootable>();

        /// <summary>
        /// Fills a content list with loot based on the amount count passed.
        /// </summary>
        /// <param name="_count">How many items that are being added to the table.</param>
        public void FillContents(ref List<Lootable> _contents, int _count)
        {
            //Generate as many loot items as passed and add them to the contents
            for (int i = 0; i < _count; i++)
            {
                _contents.Add(GenerateLoot());
            }
        }

        public void GenerateTable()
        {
            //Clear the table to ensure new loot is put in
            table.Clear();

            //Fill the table with weighted loot from possible loot
            foreach (WeightedLoot loot in possibleLoot)
            {
                loot.AddLootToTable(ref table);
            }
        }

        /// <summary>
        /// Grabs a random item from the loot table and returns it.
        /// If the table hasn't been filled, it will automatically be filled.
        /// </summary>
        public Lootable GenerateLoot()
        {
            //If the table is empty, fill it with loot
            if(table.Count == 0)
            {
                //Fill the table
                foreach (WeightedLoot loot in possibleLoot)
                {
                    loot.AddLootToTable(ref table);
                }
            }

            //Return a random lootable from the loot table
            return table[Random.Range(0, table.Count - 1)];
        }
    }
}
