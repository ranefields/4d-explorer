﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Helpers;

namespace Scripts.Shapes
{
    public class TriangularPrism : Polyhedron
    {
        public float Length = 1;

        protected override void GenerateVerticesAndEdges()
        {
            float yOffset = Length / 2;
            var vertexPositions = new Vector3[]
            {
                new Vector3(-MathHelpers.Sqrt3Inv, -yOffset, -1f/3f),
                new Vector3(MathHelpers.Sqrt3Inv, -yOffset, -1f/3f),
                new Vector3(0, -yOffset, 2f/3f),

                new Vector3(-MathHelpers.Sqrt3Inv, yOffset, -1f/3f),
                new Vector3(MathHelpers.Sqrt3Inv, yOffset, -1f/3f),
                new Vector3(0, yOffset, 2f/3f),
            };

            SetVerticiesFromVectors(vertexPositions);

            Edges = new Edge[]
            {
                new Edge(Vertices[0], Vertices[1]),
                new Edge(Vertices[1], Vertices[2]),
                new Edge(Vertices[2], Vertices[0]),

                new Edge(Vertices[0], Vertices[3]),
                new Edge(Vertices[1], Vertices[4]),
                new Edge(Vertices[2], Vertices[5]),

                new Edge(Vertices[3], Vertices[4]),
                new Edge(Vertices[4], Vertices[5]),
                new Edge(Vertices[5], Vertices[3]),
            };
        }
    }
}
