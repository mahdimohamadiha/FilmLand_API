﻿using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FilmLand.Database
{
    public class DapperEntities
    {
        public static void ExecuteWitoutReturn(string procName, string connectionString, DynamicParameters param = null)
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionString);
                if (db.State == ConnectionState.Closed)
                    db.Open();
                db.Execute(procName, param);
            }
            catch (Exception e)
            {
            }
        }

        public static async void ExecuteWitoutReturnAsync(string procName, string connectionString, DynamicParameters param = null)
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionString);
                if (db.State == ConnectionState.Closed)
                    db.Open();
                await db.ExecuteAsync(procName, param);
            }
            catch (Exception e)
            {
            }
        }
        public static void ExecuteWitoutReturnStore(string procName, string connectionString, DynamicParameters param = null)
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionString);
                if (db.State == ConnectionState.Closed)
                    db.Open();
                db.Execute(procName, param, commandType: CommandType.StoredProcedure);
            }
            catch (Exception e)
            {
            }
        }
        //DapperORM.ExecuteReturnScalar<int>
        public static T ExecuteReturnScalar<T>(string procName, string connectionString, DynamicParameters param = null)
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionString);
                if (db.State == ConnectionState.Closed)
                    db.Open();
                return (T)Convert.ChangeType(db.ExecuteScalar(procName, param), typeof(T));
            }
            catch (Exception e)
            {
                return (T)Convert.ChangeType("Application Error : " + e.Message, typeof(T));
            }
        }


        public static async Task<T> ExecuteReturnScalarQuery<T>(string query, string connectionString, DynamicParameters param = null)
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionString);
                if (db.State == ConnectionState.Closed)
                    db.Open();
                return (T)Convert.ChangeType(await db.ExecuteScalarAsync(query, param), typeof(T));
            }
            catch (Exception e)
            {
                return (T)Convert.ChangeType("Application Error : " + e.Message, typeof(T));
            }
        }

        //DapperORM.ReturnList<PersonelModel>
        public static IEnumerable<T> ReturnList<T>(string procName, string connectionString, DynamicParameters param = null)
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionString);
                if (db.State == ConnectionState.Closed)
                    db.Open();
                return db.Query<T>(procName, param);
            }
            catch (Exception e)
            {
                return new List<T>(1);
            }
        }

        public static async Task<IEnumerable<T>> ReturnListQuery<T>(string query, string connectionString, DynamicParameters param = null)
        {
            try
            {
                using IDbConnection db = new SqlConnection(connectionString);
                if (db.State == ConnectionState.Closed)
                    db.Open();
                return await db.QueryAsync<T>(query, param);
            }
            catch (Exception e)
            {
                return new List<T>(1);
            }
        }
    }
}
