﻿using System;
using ParquetClassLibrary.Sandbox;

namespace ParquetCLTester
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var myMap = new RegionMap();
            myMap.FillTestPattern();
            Console.WriteLine(myMap);
        }
    }
}
