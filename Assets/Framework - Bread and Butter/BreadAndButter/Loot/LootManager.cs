using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreadAndButter.Loot
{
    

    public class LootManager : MonoSingleton<LootManager>
    {
        [SerializeField] private List<LootTable> tables = new List<LootTable>();

        // Awake is called when the script instance is being loaded
        private void Awake()
        {
            CreateInstance();
            FlagAsPersistant();

            tables.ForEach((table) => table.GenerateTable()); //This is a function without a function like lambda for ifs etc
        }


    }
}
