﻿using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tira;

public class Wall : Bit
{
    GameObjectState single = new GameObjectState("single");
    GameObjectState horizontal_middle = new GameObjectState("horizontal_middle");
    GameObjectState horizontal_right = new GameObjectState("horizontal_right");
    GameObjectState horizontal_left = new GameObjectState("horizontal_left");
    GameObjectState vertical_top = new GameObjectState("vertical_top");
    GameObjectState vertical_middle = new GameObjectState("vertical_middle");
    GameObjectState vertical_bottom = new GameObjectState("vertical_bottom");
    GameObjectState corner_top_right = new GameObjectState("corner_top_right");
    GameObjectState corner_top_left = new GameObjectState("corner_top_left");
    GameObjectState corner_bottom_right = new GameObjectState("corner_bottom_right");
    GameObjectState corner_bottom_left = new GameObjectState("corner_bottom_left");
    GameObjectState middle = new GameObjectState("middle");

    public Wall(string name, BitProperties bitProperties) : base(name, bitProperties)
    {
    }

    public override void Load()
    {
        StateMachine stateMachine = CreateAndAddComponent<StateMachine>();


        stateMachine.AddState(single);
        stateMachine.AddState(horizontal_middle);
        stateMachine.AddState(horizontal_left);
        stateMachine.AddState(horizontal_right);
        stateMachine.AddState(vertical_bottom);
        stateMachine.AddState(vertical_middle);
        stateMachine.AddState(vertical_top);
        stateMachine.AddState(corner_bottom_left);
        stateMachine.AddState(corner_bottom_right);
        stateMachine.AddState(corner_top_left);
        stateMachine.AddState(corner_top_right);
        stateMachine.AddState(middle);

        stateMachine.SetState(single.Name);

        AnimationTree animationTree = CreateAndAddComponent<AnimationTree>();
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation), _=> stateMachine.CurrentState.Name == single.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation)+ "_horizontal_middle", _ => stateMachine.CurrentState.Name == horizontal_middle.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_horizontal_left", _ => stateMachine.CurrentState.Name == horizontal_left.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_horizontal_right", _ => stateMachine.CurrentState.Name == horizontal_right.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_vertical_middle", _ => stateMachine.CurrentState.Name == vertical_middle.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_vertical_top", _ => stateMachine.CurrentState.Name == vertical_top.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_vertical_bottom", _ => stateMachine.CurrentState.Name == vertical_bottom.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_corner_top_right", _ => stateMachine.CurrentState.Name == corner_top_right.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_corner_top_left", _ => stateMachine.CurrentState.Name == corner_top_left.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_corner_bottom_right", _ => stateMachine.CurrentState.Name == corner_bottom_right.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_corner_bottom_left", _ => stateMachine.CurrentState.Name == corner_bottom_left.Name);
        animationTree.AddAnimation(Bits.GetPath(Name, AssetTypes.Animation) + "_middle", _ => stateMachine.CurrentState.Name == middle.Name);


        base.Load();
    }

    public override void Update()
    {

        CheckState();

        base.Update();
    }

    public void CheckState()
    {
        StateMachine stateMachine = GetComponent<StateMachine>();
        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        /*
        string north = BitGrid.North(this).Name;
        string west = BitGrid.West(this).Name;
        string south = BitGrid.South(this).Name;
        string east = BitGrid.East(this).Name;

        if(north == Name && west == Name && south == Name && east == Name)
        {
            stateMachine.SetState(middle.Name);

        }
        if (north != Name && west == Name && south != Name && east == Name)
        {
            stateMachine.SetState(horizontal_middle.Name);
        }
        if (north != Name && west == Name && south != Name && east != Name)
        {
            stateMachine.SetState(horizontal_right.Name);
        }
        if (north != Name && west != Name && south != Name && east == Name)
        {
            stateMachine.SetState(horizontal_left.Name);
        }
        if (north == Name && west != Name && south == Name && east != Name)
        {
            stateMachine.SetState(vertical_middle.Name);
        }
        if (north != Name && west != Name && south == Name && east != Name)
        {
            stateMachine.SetState(vertical_top.Name);
        }
        if (north == Name && west != Name && south != Name && east != Name)
        {
            stateMachine.SetState(vertical_bottom.Name);
        }
        if (north == Name && west == Name && south == Name && east == Name)
        {
            stateMachine.SetState(middle.Name);
        }
        if (north != Name && west != Name && south == Name && east == Name)
        {
            stateMachine.SetState(corner_top_left.Name);
        }
        if (north != Name && west == Name && south == Name && east != Name)
        {
            stateMachine.SetState(corner_top_right.Name);
        }
        if (north == Name && west != Name && south != Name && east == Name)
        {
            stateMachine.SetState(corner_bottom_left.Name);
        }
        if (north == Name && west == Name && south != Name && east != Name)
        {
            stateMachine.SetState(corner_bottom_right.Name);
        }
        if (north != Name && west != Name && south != Name && east != Name)
        {
            stateMachine.SetState(single.Name);
        }

        */


    }
}
