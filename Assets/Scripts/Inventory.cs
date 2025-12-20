using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<IItem> _items;

    public Inventory()
    {
        _items = new();
    }

    public event Action<IItem> itemAdded;
    public event Action<IItem> itemRemoved;

    public void Add(IItem item)
    {
        if (item == null)
            return;

        _items.Add(item);
        itemAdded?.Invoke(item);
    }

    public IItem Take(IItem item)
    {
        if (item == null)
            return null;

        if (Contains(item) == false)
            return null;

        _items.Remove(item);
        itemRemoved?.Invoke(item);
        return item;
    }

    public bool Contains(IItem item)
    {
        return _items.Contains(item);
    }
}
