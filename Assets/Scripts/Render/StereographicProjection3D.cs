﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Scripts.Geometry3D;
using Scripts.Geometry3D.Shapes;
using Scripts.Debugging;

namespace Scripts.Render
{
    [RequireComponent(typeof(Polyhedron))]
    public class StereographicProjection3D : MonoBehaviour
    {
        // Set in Unity
        public bool LogVertices = false;
        public bool LogEdges = false;

        public Polyhedron Polyhedron { get; private set; }

        private GeneratedPolyhedron projectionPolyhedron;
        private GameObject projectionObj;

        // Run on script load
        public void Awake()
        {
            Polyhedron = GetComponent<Polyhedron>();
        }

        // Run on object load
        public void Start()
        {
            GenerateProjectionPolyhedron();
        }

        // Run each frame
        public void Update()
        {
            UpdateProjectionPolyhedron();
        }

        private void GenerateProjectionPolyhedron()
        {
            DestroyImmediate(projectionObj);
            projectionObj = new GameObject("Stereographic Projection");
            projectionObj.transform.SetParent(gameObject.transform);
            
            projectionPolyhedron = Polyhedron.GenerateFor(
                projectionObj,
                (self) =>
                {
                    var vertexPositions = new Vector3[Polyhedron.Vertices.Length];
                    for (int i = 0; i < vertexPositions.Length; i++)
                    {
                        vertexPositions[i] = Project(Polyhedron.Vertices[i].GlobalPosition);
                    }
                    return vertexPositions;
                },
                (self) =>
                {
                    var edges = new Edge3D[Polyhedron.Edges.Length];
                    for (int i = 0; i < edges.Length; i++)
                    {
                        int indexA = Polyhedron.Edges[i].Endpoints[0].Index;
                        int indexB = Polyhedron.Edges[i].Endpoints[1].Index;
                        edges[i] = new Edge3D(self.Vertices[indexA], self.Vertices[indexB]);
                    }
                    return edges;
                }
            );

            projectionObj.AddComponent<RenderWireframe>();

            if (LogVertices) { projectionObj.AddComponent<LogVertices3D>(); }
            if (LogEdges) { projectionObj.AddComponent<LogEdges3D>(); }
        }

        private void UpdateProjectionPolyhedron()
        {
            for (int i = 0; i < projectionPolyhedron.Vertices.Length; i++)
            {
                Vector3 result = Project(Polyhedron.Vertices[i].GlobalPosition);
                projectionPolyhedron.Vertices[i].LocalPosition = result;
            }
        }

        public static Vector3 Project(Vector3 source)
        {
            return new Vector3(source.x, 0, source.z);
        }
    }
}
