﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Villager : Character
{
    public Tool Tool { get; set; } = Tool.Empty(); 
    public Item Item { get; set; } = Item.Empty();

    public Home Home;
    public Villager(string name, CharacterProperties characterProperties) : base(name, characterProperties)
    {
    }

    public override void Load()
    {
        base.Load();

        StateMachine stateMachine = GetComponent<StateMachine>();

        stateMachine.AddState(new VillagerIdleState());
        stateMachine.AddState(new VillagerWorkingState());

        stateMachine.SetState("idle");
    }

    public override void Update()
    {

        base.Update();
        Item.Position = new Vector2(Position.X, Position.Y - 14);

        UpdateTool();
    }

    public void AddTaskFromWorld()
    {
        GetComponent<Highlight>().Drawable = false;

        if (SceneManager.CurrentScene.GetGameObject<Player>().GetComponent<StateMachine>().CurrentState is InteractState interact)
        {
            interact.Character = null;
        }

        AddTask(new GoTask(VectorHelper.Snap(Game.cursor.Position, 16)));

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();
        Vector2 position = Game.cursor.Position;

        List<GameObject> targets = SceneManager.CurrentScene.GetGameObjectsAt(VectorHelper.Snap(position, map.objectGrid.TileSize.X));

        GameObject target = GameObject.Empty();

        foreach (GameObject gameObject in targets)
        {
            if (gameObject is Tile && !(gameObject is Object)) continue;

            target = gameObject;
            break;
        }

        if (target is Interactable interactable)
        {
            AddTask(interactable.GetTaskType(this));
        }
    }

    public void SetHome(Home home)
    {
        Home = home;
    }

    public void UpdateTool()
    {
        if (Tool.Name == Tool.Empty().Name) return;

        Direction direction = GetComponent<Direction>();
        Sprite sprite = GetComponent<Sprite>();

        Tool.Position = new Vector2(Position.X + CurrentDirection.X * 6, Position.Y - 7);
        Tool.Layer = Layer + 1;

        Sprite toolSprite = Tool.GetComponent<Sprite>();

        toolSprite.Effect = SpriteEffects.None;

        if (direction.Name == Direction.East().Name) toolSprite.Effect = SpriteEffects.FlipHorizontally;

    }

    public int CaculateObjectDamage(Object target)
    {
        Tool tool = target.Properties.Mineable;
        if (tool == Tool.Empty()) return 1;

        if(tool.Name == Tool.Name)
        {
            return Tool.Damage;
        }

        return 1;
    }



    public virtual void SetTool(Item item)
    {
        if (!(item is Tool)) return;

        Tool tool = (Tool)item;

        if (Tool.Name != Tool.Empty().Name)
        {
            SceneManager.CurrentScene.Remove(Tool);
        }

        Tool = tool;
        SceneManager.CurrentScene.AddGameObject(Tool);
    }

    public virtual bool IsHolding(Tool tool)
    {
        if (Tool.Name == tool.Name) return true;

        return false;
    }
}