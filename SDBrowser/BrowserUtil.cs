using FauFau.CommmonDataTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace FauFau.SDBrowser
{
    public static class BrowserUtil
    {
        private const int byteMaxPreviewCount = 20;
        private const int ushortMaxPreviewCount = 15;
        private const int uintMaxPreviewCount = 15;
        private const int vector2MaxPreviewCount = 10;
        private const int vector3MaxPreviewCount = 8;
        private const int vector4MaxPreviewCount = 5;

        private static void CreatePreview(Vector2 data, StringBuilder sb)
        {
            sb.Append("[");
            sb.Append(data.x);
            sb.Append(", ");
            sb.Append(data.y);
            sb.Append("]");
        }
        private static void CreatePreview(Vector3 data, StringBuilder sb)
        {
            sb.Append("[");
            sb.Append(data.x);
            sb.Append(", ");
            sb.Append(data.y);
            sb.Append(", ");
            sb.Append(data.z);
            sb.Append("]");
        }
        private static void CreatePreview(Vector4 data, StringBuilder sb)
        {
            sb.Append("[");
            sb.Append(data.x);
            sb.Append(", ");
            sb.Append(data.y);
            sb.Append(", ");
            sb.Append(data.z);
            sb.Append(", ");
            sb.Append(data.w);
            sb.Append("]");
        }
        private static void CreatePreview(Half3 data, StringBuilder sb)
        {
            sb.Append("[");
            sb.Append(data.x);
            sb.Append(", ");
            sb.Append(data.y);
            sb.Append(", ");
            sb.Append(data.z);
            sb.Append("]");
        }

        public static string CreatePreview(Vector2 data)
        {
            StringBuilder sb = new StringBuilder();
            CreatePreview(data, sb);
            return sb.ToString();
        }
        public static string CreatePreview(Vector3 data)
        {
            StringBuilder sb = new StringBuilder();
            CreatePreview(data, sb);
            return sb.ToString();
        }
        public static string CreatePreview(Vector4 data)
        {
            StringBuilder sb = new StringBuilder();
            CreatePreview(data, sb);
            return sb.ToString();
        }
        public static string CreatePreview(Matrix4x4 data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            CreatePreview(data.x, sb);
            sb.Append(", ");
            CreatePreview(data.y, sb);
            sb.Append(", ");
            CreatePreview(data.z, sb);
            sb.Append(", ");
            CreatePreview(data.w, sb);
            sb.Append(" ]");
            return sb.ToString();
        }
        public static string CreatePreview(Box3 data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ min: ");
            CreatePreview(data.min, sb);
            sb.Append(", max: ");
            CreatePreview(data.max, sb);
            sb.Append(" ]");
            return sb.ToString();
        }
        public static string CreatePreview(HalfMatrix4x3 data)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ ");
            CreatePreview(data.x, sb);
            sb.Append(", ");
            CreatePreview(data.y, sb);
            sb.Append(", ");
            CreatePreview(data.z, sb);
            sb.Append(", ");
            CreatePreview(data.w, sb);
            sb.Append(" ]");
            return sb.ToString();
        }

        public static string CreatePreview(List<byte> data)
        {
            StringBuilder sb = new StringBuilder();
            int count = data.Count > byteMaxPreviewCount ? byteMaxPreviewCount : data.Count;
            byte b;
            for (int bx = 0, cx = 0; bx < count; ++bx, ++cx)
            {
                b = ((byte)(data[bx] >> 4));
                sb.Append((char)(b > 9 ? b - 10 + 'A' : b + '0'));            
                b = ((byte)(data[bx] & 0x0F));
                sb.Append((char)(b > 9 ? b - 10 + 'A' : b + '0'));
                sb.Append(" ");
            }


            if(sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
                sb.Append("...");
            }
            return sb.ToString();
        }
        public static string CreatePreview(List<ushort> data)
        {
            StringBuilder sb = new StringBuilder();
            int count = data.Count > ushortMaxPreviewCount ? ushortMaxPreviewCount : data.Count;
            for (int i = 0; i < count; i++)
            {
                sb.Append(data[i]);
                sb.Append(", ");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
                sb.Append("...");
            }
            return sb.ToString();
        }
        public static string CreatePreview(List<uint> data)
        {
            StringBuilder sb = new StringBuilder();
            int count = data.Count > uintMaxPreviewCount ? uintMaxPreviewCount : data.Count;
            for (int i = 0; i < count; i++)
            {
                sb.Append(data[i]);
                sb.Append(", ");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 2, 2);
                sb.Append("...");
            }
            return sb.ToString();
        }
        public static string CreatePreview(List<Vector2> data)
        {
            StringBuilder sb = new StringBuilder();
            int count = data.Count > vector2MaxPreviewCount ? vector2MaxPreviewCount : data.Count;
            for (int i = 0; i < count; i++)
            {
                CreatePreview(data[i], sb);
                sb.Append(" ");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append("...");
            }
            return sb.ToString();
        }
        public static string CreatePreview(List<Vector3> data)
        {
            StringBuilder sb = new StringBuilder();
            int count = data.Count > vector3MaxPreviewCount ? vector3MaxPreviewCount : data.Count;
            for (int i = 0; i < count; i++)
            {
                CreatePreview(data[i], sb);
                sb.Append(" ");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append("...");
            }
            return sb.ToString();
        }
        public static string CreatePreview(List<Vector4> data)
        {
            StringBuilder sb = new StringBuilder();
            int count = data.Count > vector4MaxPreviewCount ? vector4MaxPreviewCount : data.Count;
            for (int i = 0; i < count; i++)
            {
                CreatePreview(data[i], sb);
                sb.Append(" ");
            }
            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
                sb.Append("...");
            }
            return sb.ToString();
        }

    }
}
