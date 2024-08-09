﻿using SerpentEngine;
using System.Collections.Generic;
using System.Diagnostics;

namespace CastleGame;
public class Game : Scene
{
    private Map map;
    private Cursor cursor;
    private Bluprint bluprint;
    public List<Character> characters = new List<Character>();
    public Player player = new Player();

    public Game() : base("Game")
    {

    }

    public override void LoadContent()
    {
        Camera.Zoom = 5f;
        Camera.UIScale = 5f;

        map = new Map();
        cursor = new Cursor();
        characters.Add(Characters.Villager());

        bluprint = new Bluprint("furnace_off");

        AddGameObject(player);

        AddGameObject(map);
        AddGameObject(cursor);

        foreach (Character character in characters)
        {
            AddGameObject(character);
        }

        AddGameObject(bluprint);

        TestUiElement testUiElement = new TestUiElement();

        AddUIElement(testUiElement);

    }

    public override void Begin()
    {
        
    }

    public override void End()
    {
        
    }


    public override void Update()
    {
        base.Update();
        TryChangeMode();
    }

    public void TryChangeMode()
    {
        if(Input.Keyboard.GetKeyPress("B"))
        {
            Player.BuildingMode = true;
        }
        if (Input.Keyboard.GetKeyPress("V"))
        {
            Player.BuildingMode = false;
        }
    }
}
