﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tira;
public class Chunk
{
    public int X { get; set; }
    public int Y { get; set; }

    public Chunk(int x, int y)
    {
        X = x;
        Y = y;
    }
}