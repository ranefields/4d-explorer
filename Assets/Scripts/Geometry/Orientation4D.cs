﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Explorer4D.Geometry
{
    // TODO: Do some fancy math because 6 planes of rotation is redundant... but easy to think about
    [Serializable]
    public class Orientation4D
    {
        public float XY = 0;
        public float XZ = 0;
        public float YZ = 0;
        public float XW = 0;
        public float YW = 0;
        public float ZW = 0;

        public Orientation4D() { }

        public Orientation4D(float xy, float xz, float yz, float xw, float yw, float zw)
        {
            XY = xy;
            XZ = xz;
            YZ = yz;
            XW = xw;
            YW = yw;
            ZW = zw;
        }
        
        // TODO: Uhhhhhh rotations aren't commutative so the order the components are applied here is non trivial...
        public Vector4 TransformVector(Vector4 source)
        {
            var x = source.x;
            var y = source.y;
            var z = source.z;
            var w = source.w;

            RotateFrom2DPlane(ref x, ref y, XY);
            RotateFrom2DPlane(ref x, ref z, XZ);
            RotateFrom2DPlane(ref z, ref y, YZ);
            RotateFrom2DPlane(ref x, ref w, XW);
            RotateFrom2DPlane(ref w, ref y, YW);
            RotateFrom2DPlane(ref z, ref w, ZW);

            return new Vector4(x, y, z, w);
        }

        private void RotateFrom2DPlane(ref float x, ref float y, float angle)
        {
            var sinA = Mathf.Sin(Mathf.Deg2Rad * angle);
            var cosA = Mathf.Cos(Mathf.Deg2Rad * angle);
            var newX = x * cosA - y * sinA;
            var newY = y * cosA + x * sinA;
            x = newX;
            y = newY;
        }
    }
}
