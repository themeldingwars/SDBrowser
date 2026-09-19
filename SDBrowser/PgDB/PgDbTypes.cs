using System.Numerics;

namespace DBTypes
{
    public struct Box3
    {
        public Vector3 min;
        public Vector3 max;
        
        public static string GetDbTypeScript() => "CREATE TYPE Box3 as (" +
                                                  "min Vector3,"          +
                                                  "max Vector3);";
    }

    public struct Half3
    {
        public float x;
        public float y;
        public float z;

        public        Vector3 AsVector3()               => new Vector3(x, y, z);
        public static Half3   FromVector3(Vector3 vec3) => new Half3 {x = vec3.X, y = vec3.Y, z = vec3.Z};
        public static string GetDbTypeScript() => "CREATE TYPE Half3 as (" +
                                                  "x real,"                +
                                                  "y real,"                +
                                                  "z real);";
    }

    // Explicitly defined 4x4 matrix to avoid issues with System.Numerics.Matrix4x4 serialization
    public struct Matrix4x4
    {
        public float m11, m12, m13, m14;
        public float m21, m22, m23, m24;
        public float m31, m32, m33, m34;
        public float m41, m42, m43, m44;
    }

    public struct HalfMatrix4x3
    {
        public Half3 x;
        public Half3 y;
        public Half3 z;
        public Half3 w;
        
        public static string GetDbTypeScript() => "CREATE TYPE HalfMatrix4x3 as (" +
                                                  "x half3,"                       +
                                                  "y half3,"                       +
                                                  "z half3,"                       +
                                                  "w half3);";
    }
}