﻿using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira
{
    public class Drops : Registry
    {
        public static new Dictionary<string, Func<Drop>> List = new Dictionary<string, Func<Drop>>();

        public static readonly Func<Drop> Tree = Register(() => new Drop(new Drop.Settings().AddDrop(Items.Wood()).SetSource(Bits.Tree())));
        public static readonly Func<Drop> Rock = Register(() => new Drop(new Drop.Settings().AddDrop(Items.Stone()).AddDrop(Items.IronOre(), 1, 0.2f).SetSource(Bits.Rock())));
        public static readonly Func<Drop> IronOre = Register(() => new Drop(new Drop.Settings().AddDrop(Items.Stone()).SetSource(Bits.IronOre())));

        public static Func<Drop> Register(Func<Drop> drop)
        {
            List.Add(drop().DropSettings.Source.Name, drop);

            return drop;
        }


        public static void RegisterRecipes()
        {
            Debug.WriteLine("Registering drops for CastleGame!");
        }

        public static Func<Drop> Get(string name)
        {
            if (!List.ContainsKey(name)) return null;

            return List[name];
        }
    }
}
