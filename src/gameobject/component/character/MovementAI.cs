﻿using Microsoft.Xna.Framework;
using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Tira;
public class MovementAI : Component
{
    public Vector2 PreviousPath { get; private set; } = new Vector2();
    public bool Moving { get; private set; } = false;

    private Vector2 currentPath = new Vector2();

    private Stack<Node> PathStack = new Stack<Node>();
    private Node currentPathingNode;

    public MovementAI() : base(false)
    {

    }

    public void SetPath(Vector2 path)
    {
        currentPath = path;
        PreviousPath = path;
        Moving = true;
    }

    public override void Update()
    {
        if (PathStack.Count > 0 || currentPathingNode != null)
        {
            Move();
            return;
        }

        if (Moving) Moving = false;

        if (currentPath == Vector2.Zero || PathStack.Count > 0) return;

        Character character = GameObject as Character;

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        Vector2 start = BitGrid.ConvertWorldCoordinatesToGridCoordinates(GameObject.Position);
        Vector2 end = BitGrid.ConvertWorldCoordinatesToGridCoordinates(currentPath);

        Stack<Node> path = map.PathFinder.FindPath(start, end);

        if (path == null)
        {
            Moving = false;
            return;
        }

        PathStack = path;

        Moving = true;
    }

    private void Move()
    {
        //Debug.WriteLine("Moving");

        Character character = GameObject as Character;

        Map map = SceneManager.CurrentScene.GetGameObject<Map>();

        if (currentPathingNode == null) currentPathingNode = PathStack.Pop();

        Vector2 targetPosition = BitGrid.ConvertGridCoordinatesToWorldCoordinates(currentPathingNode.Position);

        if (Vector2.Distance(GameObject.Position, targetPosition) < 0.4f && PathStack.Count > 0)
        {
            currentPathingNode = PathStack.Pop();
        }

        targetPosition = BitGrid.ConvertGridCoordinatesToWorldCoordinates(currentPathingNode.Position);

        Vector2 direction = targetPosition - GameObject.Position;

        direction.Normalize();

        character.SetDirection(direction);
        GameObject.Position += direction * MathHelper.Min(character.Properties.Speed * SerpentGame.DeltaTime, Vector2.Distance(GameObject.Position, targetPosition));

        Vector2 snappedGameObjectPosition = VectorHelper.Snap(GameObject.Position, 16);
        Vector2 gameObjectGridPosition = BitGrid.ConvertWorldCoordinatesToGridCoordinates(snappedGameObjectPosition);

        if (PathStack.Count == 0 && Vector2.Distance(GameObject.Position, targetPosition) < 0.2f)
        {
            currentPath = new Vector2();
            currentPathingNode = null;

            Moving = false;

            //Debug.WriteLine("Finished moving");
        }
    }
}
