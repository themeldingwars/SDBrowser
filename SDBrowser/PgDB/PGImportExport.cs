using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FauFau.Formats;
using FauFau.SDBrowser;
using Npgsql;
using Matrix4x4 = System.Numerics.Matrix4x4;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;
using Vector4 = System.Numerics.Vector4;

namespace SDBrowser
{
    public class PgImportExport
    {
        public string               ConnStr;
        public string               Schema;
        public StaticDB             DB;
        public Action<string, bool> LogMessage;

        private NpgsqlConnection DbConn;

        public PgImportExport(string connStr, string schema, StaticDB db, Action<string, bool> logMessage)
        {
            ConnStr    = connStr;
            Schema     = schema;
            DB         = db;
            LogMessage = logMessage;

            DbConn = OpenDbConnection();
        }

        private NpgsqlConnection OpenDbConnection()
        {
            try {
                var conn = new NpgsqlConnection(ConnStr);
                conn.Open();

                LogMsg($"Connected to DB at: {ConnStr}");

                return conn;
            }
            catch (Exception e) {
                LogError($"Error Connecting to DB at: {ConnStr}");
                LogError($"Error: {e}");
            }

            return null;
        }

        public void DropExisting()
        {
            LogMsg("==== Dropping existing tables and types. ====");
            var sqls = new List<string>();

            // Tables
            sqls.AddRange(DB.Tables.Select(x => $"DROP TABLE IF EXISTS {Schema}.\"{Form1.GetTableOrFieldName(x.Id)}\"; "));
            sqls.Add($"DROP TABLE IF EXISTS {Schema}.\"Meta\";");

            // Types
            sqls.AddRange(new[] {"Box3", "Matrix4x4", "HalfMatrix4x3", "Vector2", "Vector3", "Half3", "Vector4"}.Select(x => $@"DROP TYPE IF EXISTS {Schema}.{x};"));

            ExecuteSqls(sqls);
            LogMsg("==== Tables dropped ====");
        }

        public void CreateSchema()
        {
            LogMsg("==== Creating schema ====");
            var sqls = new List<string>();
            sqls.Add($"CREATE SCHEMA IF NOT EXISTS {Schema}; SET search_path TO {Schema};");
            ExecuteSqls(sqls, false);
            sqls.Clear();
            LogMsg("Schema created");

            LogMsg("==== Creating types =====");
            CreateTypes();
            LogMsg("Types created");

            // Tables
            LogMsg("==== Creating tables ====");
            var tableMetas = new List<TableMeta>();
            for (var tblIdx = 0; tblIdx < DB.Tables.Count; tblIdx++) {
                var table     = DB.Tables[tblIdx];
                var tableName = Form1.GetTableOrFieldName(table.Id);
                var tableMeta = new TableMeta
                {
                    Idx  = tblIdx,
                    Name = tableName,
                    Cols = new List<ColMeta>()
                };

                var sb = new StringBuilder();
                sb.Append($@"CREATE TABLE IF NOT EXISTS {Schema}.""{tableName}"" ");
                sb.Append("(");
                for (var i = 0; i < table.Columns.Count; i++) {
                    var col  = table.Columns[i];
                    var typ  = GetSqlTypeForColum(col.Type);
                    var name = Form1.GetTableOrFieldName(col.Id);
                    sb.Append($@"""{name}"" {typ}");

                    //if (false) sb.Append(" PRIMARY KEY");
                    if (table.NullableColumn.All(x => x.Id != col.Id)) sb.Append(" NOT NULL");

                    if (i != table.Columns.Count - 1) sb.Append(", ");

                    tableMeta.Cols.Add(new ColMeta
                    {
                        Idx     = i,
                        Name    = name,
                        SdbType = col.Type,
                        Padding = col.Padding
                    });
                }

                sb.Append(");");
                sqls.Add(sb.ToString());
                tableMetas.Add(tableMeta);
            }

            ExecuteSqls(sqls);
            LogMsg("Tables Created");

            // The meta table
            SetMeta(tableMetas);
        }

        private void SetMeta(List<TableMeta> tableMeta)
        {
            var sqls = new List<string>();
            sqls.Add($@"CREATE TABLE IF NOT EXISTS {Schema}.""Meta"" (name text, type integer, data json);");
            var dbMeta = new SdbMeta
            {
                Patch        = DB.Patch,
                Flags        = DB.Flags,
                Timestamp    = DB.Timestamp,
                FileVersion  = (uint)GetInstanceField(DB.GetType(), DB, "fileVersion"),
                TableVersion = (uint)GetInstanceField(DB.GetType(), DB, "tableVersion")
            };

            void AddToMetaTable(string name, MetaType type, string data) => sqls.Add($@"INSERT INTO {Schema}.""Meta"" (name, type, data) VALUES ('{name}', {(int) type}, CAST('{data}' as json));");

            var dbMetaStr = JsonSerializer.Serialize(dbMeta);
            AddToMetaTable("DBMeta", MetaType.Db, dbMetaStr);

            foreach (var table in tableMeta) {
                var tableMetaStr = JsonSerializer.Serialize(table);
                AddToMetaTable($@"{table.Name}", MetaType.Table, tableMetaStr);
            }

            ExecuteSqls(sqls);
        }
        
        // Remove when we have .net 5 and can update to the new faufau
        internal static object GetInstanceField(Type type, object instance, string fieldName)
        {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                                   | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
        
        // Creates custom types for the sdb, bind to native numeric types where we can
        private void CreateTypes()
        {
            var sqls = new List<string>();

            // Vector2
            sqls.Add("CREATE TYPE Vector2 as (" +
                     "x real,"                  +
                     "y real);");

            // Vector3
            sqls.Add("CREATE TYPE Vector3 as (" +
                     "x real,"                  +
                     "y real,"                  +
                     "z real);");

            // Half3
            sqls.Add("CREATE TYPE Half3 as (" +
                     "x real,"                +
                     "y real,"                +
                     "z real);");

            // Vector4
            sqls.Add("CREATE TYPE Vector4 as (" +
                     "x real,"                  +
                     "y real,"                  +
                     "z real,"                  +
                     "w real);");

            // Matrix4x4
            sqls.Add("CREATE TYPE Matrix4x4 as (" +
                     "m11 real,"                  +
                     "m12 real,"                  +
                     "m13 real,"                  +
                     "m14 real,"                  +
                     "m21 real,"                  +
                     "m22 real,"                  +
                     "m23 real,"                  +
                     "m24 real,"                  +
                     "m31 real,"                  +
                     "m32 real,"                  +
                     "m33 real,"                  +
                     "m34 real,"                  +
                     "m41 real,"                  +
                     "m42 real,"                  +
                     "m43 real,"                  +
                     "m44 real);");

            // HalfMatrix4x3
            sqls.Add("CREATE TYPE HalfMatrix4x3 as (" +
                     "x half3,"                       +
                     "y half3,"                       +
                     "z half3,"                       +
                     "w half3);");

            // Box3
            sqls.Add("CREATE TYPE Box3 as (" +
                     "min Vector3,"          +
                     "max Vector3);");

            ExecuteSqls(sqls, false);

            BindTypes(DbConn);
        }

        private void BindTypes(NpgsqlConnection conn)
        {
            var schemaLc = Schema.ToLower();
            NpgsqlConnection.GlobalTypeMapper.MapComposite<Vector2>($"{schemaLc}.vector2");
            NpgsqlConnection.GlobalTypeMapper.MapComposite<Vector3>($"{schemaLc}.vector3");
            NpgsqlConnection.GlobalTypeMapper.MapComposite<Vector4>($"{schemaLc}.vector4");
            NpgsqlConnection.GlobalTypeMapper.MapComposite<Matrix4x4>($"{schemaLc}.matrix4x4");
            NpgsqlConnection.GlobalTypeMapper.MapComposite<DBTypes.Half3>($"{schemaLc}.half3");
            NpgsqlConnection.GlobalTypeMapper.MapComposite<DBTypes.HalfMatrix4x3>($"{schemaLc}.halfmatrix4x3");
            NpgsqlConnection.GlobalTypeMapper.MapComposite<DBTypes.Box3>($"{schemaLc}.box3");
            
            conn.ReloadTypes();
        }

        public void ImportData()
        {
            LogMsg("==== Importing data ====");
            Parallel.ForEach(DB.Tables, table =>
            {
                var tableName = Form1.GetTableOrFieldName(table.Id);
                var conn      = OpenDbConnection();
                BindTypes(conn);

                var tableCopySql = CreateCopySql(table);
                using (var writer = conn.BeginBinaryImport(tableCopySql)) {
                    for (var i = 0; i < table.Rows.Count; i++) {
                        var row = table.Rows[i];
                        writer.StartRow();
                        for (var index = 0; index < row.Fields.Count; index++) {
                            var field      = row.Fields[index];
                            var fieldType  = table.Columns[index].Type;
                            var columnName = Form1.GetTableOrFieldName(table.Columns[index].Id);

                            if (field == null) {
                                writer.WriteNull();
                            }
                            else if (IsBasicType(fieldType)) {
                                if (fieldType == StaticDB.DBType.UShort) {
                                    writer.Write(Convert.ToInt32(field));
                                }
                                else if (fieldType == StaticDB.DBType.UInt) {
                                    writer.Write(Convert.ToInt64(field));
                                }
                                else if (fieldType == StaticDB.DBType.ULong) {
                                    var val = (long) (ulong) field;
                                    writer.Write(val);
                                }
                                else if (fieldType == StaticDB.DBType.Half) {
                                    writer.Write(Convert.ToSingle(field));
                                }
                                else if (field is string fieldStr) { // clean nulls from the end of strings
                                    var val = fieldStr.Replace("\0", "");
                                    writer.Write(val);
                                }
                                else if (field is char fieldChar) {
                                    if (fieldChar == '\0') { // feels abit eh, TODO: check incase of char trouble
                                        Console.WriteLine($"Got an invalid char {fieldChar} in table {tableName} on column {columnName} row {i}");
                                        writer.Write(' ');
                                    }
                                    else {
                                        writer.Write(fieldChar);
                                    }
                                }
                                else {
                                    writer.Write(field);
                                }
                            }
                            else if (IsCustomType(fieldType)) {
                                if (field is FauFau.Util.CommmonDataTypes.Vector2 v2) {
                                    writer.Write(new Vector2(v2.x, v2.y));
                                }
                                else if (field is FauFau.Util.CommmonDataTypes.Vector3 v3) {
                                    writer.Write(new Vector3(v3.x, v3.y, v3.z));
                                }
                                else if (field is FauFau.Util.CommmonDataTypes.Vector4 v4) {
                                    writer.Write(new Vector4(v4.x, v4.y, v4.z, v4.w));
                                }
                                else if (field is FauFau.Util.CommmonDataTypes.Matrix4x4 m4) {
                                    writer.Write(new Matrix4x4(m4.x.x, m4.x.y, m4.x.z, m4.x.w,
                                        m4.y.x, m4.y.y, m4.y.z, m4.y.w,
                                        m4.z.x, m4.z.y, m4.z.z, m4.z.w,
                                        m4.w.x, m4.w.y, m4.w.z, m4.w.w));
                                }
                                else if (field is FauFau.Util.CommmonDataTypes.Half3 h3) {
                                    writer.Write(new DBTypes.Half3 {x = h3.x, y = h3.y, z = h3.z});
                                }
                                else if (field is FauFau.Util.CommmonDataTypes.Box3 b3) {
                                    writer.Write(new DBTypes.Box3 {min = new Vector3(b3.min.x, b3.min.y, b3.min.z), max = new Vector3(b3.max.x, b3.max.y, b3.max.z)});
                                }
                                else if (field is FauFau.Util.CommmonDataTypes.HalfMatrix4x3 hm4x3) {
                                    writer.Write(new DBTypes.HalfMatrix4x3
                                    {
                                        x = new DBTypes.Half3 {x = hm4x3.x.x, y = hm4x3.x.y, z = hm4x3.x.z},
                                        y = new DBTypes.Half3 {x = hm4x3.y.x, y = hm4x3.y.y, z = hm4x3.y.z},
                                        z = new DBTypes.Half3 {x = hm4x3.z.x, y = hm4x3.z.y, z = hm4x3.z.z},
                                        w = new DBTypes.Half3 {x = hm4x3.w.x, y = hm4x3.w.y, z = hm4x3.w.z}
                                    });
                                }
                                else {
                                    writer.Write(field);
                                }
                            }
                            else if (IsArrayType(fieldType)) {
                                if (field is List<ushort> shortsList) {
                                    var val = shortsList.Select(x => Convert.ToInt32(x)).ToList();
                                    writer.Write(val);
                                }
                                else if (field is List<uint> intsList) {
                                    var val = intsList.Select(x => Convert.ToInt64(x)).ToList();
                                    writer.Write(val);
                                }
                                else if (field is List<FauFau.Util.CommmonDataTypes.Vector2> v2List) {
                                    var val = v2List.Select(x => new Vector2(x.x, x.y)).ToList();
                                    writer.Write(val);
                                }
                                else if (field is List<FauFau.Util.CommmonDataTypes.Vector3> v3List) {
                                    var val = v3List.Select(x => new Vector3(x.x, x.y, x.z)).ToList();
                                    writer.Write(val);
                                }
                                else if (field is List<FauFau.Util.CommmonDataTypes.Vector4> v4List) {
                                    var val = v4List.Select(x => new Vector4(x.x, x.y, x.z, x.w)).ToList();
                                    writer.Write(val);
                                }
                                else {
                                    writer.Write(field);
                                }
                            }
                            else {
                                Console.WriteLine("Unhandled type");
                                writer.WriteNull();
                            }
                        }
                    }

                    writer.Complete();
                }
                
                LogMsg($"Imported data for {tableName}, ({table.Rows.Count} rows)");
                conn.Close();
            });
            LogMsg("==== Imported data ====");
        }

        private string CreateCopySql(StaticDB.Table table)
        {
            var sb        = new StringBuilder();
            var tableName = Form1.GetTableOrFieldName(table.Id);
            sb.Append($"COPY {Schema}.\"{tableName}\" (");

            for (var i = 0; i < table.Columns.Count; i++) {
                var col  = table.Columns[i];
                var name = Form1.GetTableOrFieldName(col.Id);
                sb.Append($"\"{name}\"");

                if (table.Columns.Count - 1 != i) sb.Append(", ");
            }

            sb.Append(") FROM STDIN (FORMAT BINARY)");
            return sb.ToString();
        }

        public string GetSqlTypeForColum(StaticDB.DBType sdbType)
        {
            var result = sdbType switch
            {
                StaticDB.DBType.Byte          => "smallint",
                StaticDB.DBType.UShort        => "integer",
                StaticDB.DBType.UInt          => "bigint",
                StaticDB.DBType.ULong         => "bigint",
                StaticDB.DBType.SByte         => "smallint",
                StaticDB.DBType.Short         => "smallint",
                StaticDB.DBType.Int           => "integer",
                StaticDB.DBType.Long          => "bigint",
                StaticDB.DBType.Float         => "real",
                StaticDB.DBType.Double        => "double precision",
                StaticDB.DBType.String        => "varchar",
                StaticDB.DBType.Vector2       => "vector2",
                StaticDB.DBType.Vector3       => "vector3",
                StaticDB.DBType.Vector4       => "vector4",
                StaticDB.DBType.Matrix4x4     => "Matrix4x4",
                StaticDB.DBType.Blob          => "bytea",
                StaticDB.DBType.Box3          => "box3",
                StaticDB.DBType.Vector2Array  => "vector2[]",
                StaticDB.DBType.Vector3Array  => "vector3[]",
                StaticDB.DBType.Vector4Array  => "vector4[]",
                StaticDB.DBType.AsciiChar     => "character",
                StaticDB.DBType.ByteArray     => "bytea",
                StaticDB.DBType.UShortArray   => "integer[]",
                StaticDB.DBType.UIntArray     => "bigint]",
                StaticDB.DBType.HalfMatrix4x3 => "halfmatrix4x3",
                StaticDB.DBType.Half          => "real",
                _                             => ""
            };

            return result;
        }

        public static bool IsBasicType(StaticDB.DBType dbType)
        {
            var result = dbType == StaticDB.DBType.Byte      ||
                         dbType == StaticDB.DBType.UShort    ||
                         dbType == StaticDB.DBType.UInt      ||
                         dbType == StaticDB.DBType.ULong     ||
                         dbType == StaticDB.DBType.SByte     ||
                         dbType == StaticDB.DBType.Short     ||
                         dbType == StaticDB.DBType.Int       ||
                         dbType == StaticDB.DBType.Long      ||
                         dbType == StaticDB.DBType.Float     ||
                         dbType == StaticDB.DBType.Double    ||
                         dbType == StaticDB.DBType.String    ||
                         dbType == StaticDB.DBType.AsciiChar ||
                         dbType == StaticDB.DBType.Half;

            return result;
        }

        public static bool IsArrayType(StaticDB.DBType dbType)
        {
            var result = dbType == StaticDB.DBType.Vector2Array ||
                         dbType == StaticDB.DBType.Vector3Array ||
                         dbType == StaticDB.DBType.Vector4Array ||
                         dbType == StaticDB.DBType.ByteArray    ||
                         dbType == StaticDB.DBType.UShortArray  ||
                         dbType == StaticDB.DBType.Blob         ||
                         dbType == StaticDB.DBType.UIntArray;

            return result;
        }

        public static bool IsCustomType(StaticDB.DBType dbType)
        {
            var result = dbType == StaticDB.DBType.Vector2       ||
                         dbType == StaticDB.DBType.Vector3       ||
                         dbType == StaticDB.DBType.Vector4       ||
                         dbType == StaticDB.DBType.Matrix4x4     ||
                         dbType == StaticDB.DBType.HalfMatrix4x3 ||
                         dbType == StaticDB.DBType.Box3;

            return result;
        }

        private void ExecuteSqls(List<string> sqls, bool logSql = true)
        {
            foreach (var sql in sqls) {
                try {
                    using var cmd = new NpgsqlCommand(sql, DbConn);
                    cmd.ExecuteNonQuery();
                    if (logSql) {
                        LogMsg($"Ran: {sql}");
                    }
                }
                catch (Exception e) {
                    LogError($"Error running: {sql}");
                    LogError(e.ToString());
                }
            }
        }

        private void LogMsg(string msg)
        {
            if (LogMessage != null) {
                LogMessage(msg, false);
            }
        }

        private void LogError(string msg)
        {
            if (LogMessage != null) {
                LogMessage(msg, true);
            }
        }

        public enum MetaType : byte
        {
            Db,
            Table,
            Misc
        }

        public class SdbMeta
        {
            public string   Patch        { get; set; }
            public DateTime Timestamp    { get; set; }
            public uint     Flags        { get; set; }
            public uint     FileVersion  { get; set; }
            public uint     TableVersion { get; set; }
        }

        public class TableMeta
        {
            public string        Name { get; set; }
            public int           Idx  { get; set; }
            public List<ColMeta> Cols { get; set; }
        }

        public struct ColMeta
        {
            public string          Name    { get; set; }
            public int             Idx     { get; set; }
            public StaticDB.DBType SdbType { get; set; }
            public int             Padding { get; set; }
        }
    }
}