﻿using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CastleGame;

public class Blueprint : Object
{
    public string Type { get; set; }

    public bool Placed { get; set; }



    public Blueprint(string name) : base(name)
    {

    }

    public override void Load()
    {
        Color c = Color.CornflowerBlue;
        c.A = 150;
        Sprite sprite = new Sprite(Objects.GetPath(Name));
        Inventory inventory = CreateAndAddComponent<Inventory>();
        sprite.Color = c;
        AddComponent(sprite);
    }

    public override void Update()
    {
        base.Update();
    }

    public void AddItem(Item item)
    {
        GetInventory().Add(item);
    }

    public void RemoveLastItem()
    {
        GetInventory().RemoveLast();
    }

    public void RemoveItem(Item item)
    {
        GetInventory().Remove(item);
    }

    public Inventory GetInventory()
    {
        return GetComponent<Inventory>();
    }

}