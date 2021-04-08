using System;
using System.Collections.Generic;

namespace Library.Generic
{
    public static class MMath {
        public const float PI = (float)Math.PI;
        public const float E = (float)Math.E;

        public static double GetDistance(Point p1, Point p2) => (p1 - p2).Magnitude;

        public static double GetDistance(Plane plane, Point p) => plane.GetDistance(p);

        public static double GetDistance(Plane plane, Edge edge) => Math.Min(GetDistance(plane, edge.p1), GetDistance(plane, edge.p2));

        public static double GetDistance(Plane p1, Plane p2) => GetDistance(p1, p2.GetSomePoint());

        public static double GetDistance(Edge edge, Point point) => Math.Min((edge.p1 - point).Magnitude, (edge.p2 - point).Magnitude);

        // For disjoint edges only!
        public static double GetDistance(Edge edge1, Edge edge2) => Math.Min(GetDistance(edge1, edge2.p1), GetDistance(edge1, edge2.p2));

        public static double GetDistance(Triangle t, Point p) => Math.Min(Math.Min(GetDistance(t.p1, p), GetDistance(t.p2, p)), GetDistance(t.p3, p));

        public static float Dot(Point p1, Point p2) => p1.x * p2.x + p1.y * p2.y + p1.z * p2.z;

        public static float Cos(float d) => (float)Math.Cos(d);
        public static float Sin(float d) => (float)Math.Sin(d);
        public static float Acos(float d) => (float)Math.Acos(d);
        public static float Tan(float d) => (float)Math.Tan(d);
        public static float Round(float d) => (float)Math.Round(d);
        public static float Sqrt(float x) => (float)Math.Sqrt(x);
        public static float Pow(float x, float y) => (float)Math.Pow(x, y);
        public static float Log(float x) => (float)Math.Log(x);

        public static float GetRandomGaussian() {
            float v1, s;
            do {
                v1 = 2.0f * RandomGenerator.Float() - 1.0f;
                var v2 = 2.0f * RandomGenerator.Float() - 1.0f;
                s = v1 * v1 + v2 * v2;
            } while (s >= 1.0f || s == 0.0f);

            s = Sqrt(-2.0f * Log(s) / s);

            return v1 * s;
        }

        public static float GetRandomGaussian(float mean, float standardDeviation) {
            return mean + GetRandomGaussian() * standardDeviation;
        }

        public static float GetNormalDistributionProbabilityDensity(float x, float mean, float standardDeviation) {
            return Pow(E, -Pow(x - mean, 2) / (2 * Pow(standardDeviation, 2))) / (standardDeviation * Sqrt(2 * PI));
        }

        public static double GetDeterminant(List<List<double>> c) {
            if (c.Count > 2) {
                var result = 0.0;
                for (var i = 0; i < c.Count; ++i) {
                    var m = new List<List<double>>();
                    for (var j = 1; j < c.Count; ++j) {
                        var line = new List<double>();
                        for (var k = 0; k < c.Count; ++k) {
                            if (k != i) {
                                line.Add(c[j][k]);
                            }
                        }

                        m.Add(line);
                    }

                    result += c[0][i] * (float) Math.Pow(-1, i) * GetDeterminant(m);
                }

                return result;
            }

            if (c.Count == 2) {
                return c[0][0] * c[1][1] - c[0][1] * c[1][0];
            }

            if (c.Count == 1) {
                return c[0][0];
            }

            throw new Exception("Not implemented");
        }
    }
}
