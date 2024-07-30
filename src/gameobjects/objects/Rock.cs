﻿using SerpentEngine;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleGame;
public class Rock : Object
{
    public Rock(string name) : base(name, Objects.GetPath(name))
    {
    }

    public override void Load()
    {
        base.Load();
    }


}
