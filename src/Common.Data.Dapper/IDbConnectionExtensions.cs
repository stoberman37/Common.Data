//using System;
//using System.Data;

//// ReSharper disable once CheckNamespace
//namespace Common.Data
//{
//    // ReSharper disable once InconsistentNaming
//    public static class IDbConnectionExtensions
//    {
//        public static void EnsureOpen(this IDbConnection connection)
//        {
//            if (connection == null) throw new ArgumentNullException(nameof(connection));

//            if (connection.State != ConnectionState.Open) connection.Open();
//        }

//        public static void EnsureClosed(this IDbConnection connection)
//        {
//            if (connection == null) throw new ArgumentNullException(nameof(connection));

//            if (connection.State != ConnectionState.Closed) connection.Close();
//        }
//    }
//}