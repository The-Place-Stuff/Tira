﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Objects : Registry
{
    public static new Dictionary<string, Func<Object>> List = new Dictionary<string, Func<Object>>();

    public static new string Path = "assets/img/objects/";





    public static readonly Func<Object> Bush = Register(() => new Bush("bush"));

    public static readonly Func<Object> Rock = Register( () => new Rock("rock"));

    public static readonly Func<Object> Campfire = Register(() => new Campfire("campfire"));

    public static readonly Func<Object> Furnace = Register(() => new Workstation("furnace"));

    public static readonly Func<Object> Tree = Register(() => new Tree("tree"));

    public static readonly Func<Object> Stockpile = Register( () => new Stockpile("stockpile"));

    public static readonly Func<Object> Tent = Register(() => new Tent("tent"));

    public static readonly Func<Object> Blueprint = Register(() => new Blueprint("blueprint"));

    public static readonly Func<Object> Workbench = Register(() => new Workbench("workbench"));

    public static Func<Object> Register(Func<Object> obj)
    {

        List.Add(obj().Name, obj);
        return obj;
    }

    public static void RegisterObjects()
    {
        Debug.WriteLine("Registering Objects for CastleGame!");
    }

    public static new string GetPath(string name)
    {
        return Path + name;
    }


}
