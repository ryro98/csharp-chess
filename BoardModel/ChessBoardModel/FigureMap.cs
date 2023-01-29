﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessBoardModel
{
    public class FigureMap
    {
        public int[,] KnightMap =
        {
            { 1, 2, 3, 3, 3, 3, 2, 1, },
            { 2, 3, 4, 4, 4, 4, 3, 2, },
            { 3, 4, 5, 5, 5, 5, 4, 3, },
            { 3, 4, 5, 5, 5, 5, 4, 3, },
            { 3, 4, 5, 5, 5, 5, 4, 3, },
            { 3, 4, 5, 5, 5, 5, 4, 3, },
            { 2, 3, 4, 4, 4, 4, 3, 2, },
            { 1, 2, 3, 3, 3, 3, 2, 1, },
        };
        public int[,] KingMap =
        {
            { 4, 5, 4, 3, 3, 3, 5, 4, },
            { 3, 3, 2, 2, 2, 2, 3, 3, },
            { 1, 1, 1, 1, 1, 1, 1, 1, },
            { 1, 1, 1, 1, 1, 1, 1, 1, },
            { 1, 1, 1, 1, 1, 1, 1, 1, },
            { 1, 1, 1, 1, 1, 1, 1, 1, },
            { 1, 1, 1, 1, 1, 1, 1, 1, },
            { 1, 1, 1, 1, 1, 1, 1, 1, },
        };
        public int[,] QueenMap =
        {
            { 1, 2, 2, 3, 3, 2, 2, 1, },
            { 2, 4, 4, 2, 2, 4, 4, 2, },
            { 2, 4, 2, 5, 5, 2, 4, 2, },
            { 3, 4, 5, 5, 5, 5, 4, 3, },
            { 2, 3, 4, 4, 4, 4, 4, 2, },
            { 2, 3, 3, 3, 3, 3, 3, 2, },
            { 2, 3, 3, 3, 3, 3, 3, 2, },
            { 1, 2, 2, 2, 2, 2, 2, 1, },
        };
        public int[,] RookMap =
        {
            { 2, 2, 2, 3, 3, 2, 2, 2, },
            { 1, 2, 2, 2, 2, 2, 2, 1, },
            { 1, 2, 2, 2, 2, 2, 2, 1, },
            { 1, 2, 2, 2, 2, 2, 2, 1, },
            { 1, 2, 2, 2, 2, 2, 2, 1, },
            { 1, 2, 2, 2, 2, 2, 2, 1, },
            { 4, 5, 5, 5, 5, 5, 5, 4, },
            { 3, 3, 3, 3, 3, 3, 3, 3, },
        };
        public int[,] BishopMap =
        {
            { 2, 2, 2, 2, 2, 2, 2, 2, },
            { 2, 3, 2, 2, 2, 2, 3, 2, },
            { 2, 3, 3, 3, 3, 3, 3, 2, },
            { 2, 3, 4, 3, 3, 4, 3, 2, },
            { 2, 3, 3, 3, 3, 3, 3, 2, },
            { 2, 3, 3, 3, 3, 3, 3, 2, },
            { 2, 3, 3, 3, 3, 3, 3, 2, },
            { 1, 2, 2, 2, 2, 2, 2, 1, },
        };
        public int[,] PawnMap =
        {
            { 2, 2, 2, 2, 2, 2, 2, 2, },
            { 2, 3, 3, 1, 1, 3, 3, 2, },
            { 2, 2, 2, 3, 3, 2, 2, 2, },
            { 2, 2, 3, 4, 4, 3, 2, 2, },
            { 2, 3, 3, 4, 4, 3, 2, 2, },
            { 2, 3, 4, 4, 4, 4, 3, 3, },
            { 5, 5, 5, 5, 5, 5, 5, 5, },
            { 5, 5, 5, 5, 5, 5, 5, 5, },
        };

        public FigureMap() {

        }
    }
}