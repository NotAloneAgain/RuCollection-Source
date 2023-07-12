using System.Collections.Generic;
using UnityEngine;

namespace RuCollection.API.Subclasses
{
    public class Inventory
    {
        private List<ItemType> _items;

        public Inventory()
        {
            _items = new (8);
        }

        public Inventory(Dictionary<int, Dictionary<ItemType, int>> chances) : this() => Chances = chances;

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

            if (Chances == null)
            {
                return null;
            }

            _items.Clear();

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
