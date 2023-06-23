using UnityEngine;
using System.Collections.Generic;

namespace RuCollection.API.Subclasses
{
    public class Inventory
    {
        private List<ItemType> _items;

        public Inventory()
        {
            _items = new (8);
        }

        public Inventory(Dictionary<int, Dictionary<ItemType, int>> chances) : base() => Chances = chances;

        public IReadOnlyCollection<ItemType> Items
        {
            get
            {
                return Randomize();
            }
        }

        public Dictionary<int, Dictionary<ItemType, int>> Chances { get; }

        public IReadOnlyCollection<ItemType> Randomize()
        {
            if (_items == null)
            {
                _items = new(8);
            }
            else
            {
                _items.Clear();
            }

            for (int i = 0; i < Chances.Count; i++)
            {
                ItemType selected = ItemType.None;

                if (Chances.ContainsKey(i))
                {
                    foreach (var item in Chances[i])
                    {
                        if (Random.Range(0, 101) <= item.Value)
                        {
                            selected = item.Key;

                            break;
                        }
                    }
                }

                _items.Add(selected);
            }

            return _items.AsReadOnly();
        }
    }
}
