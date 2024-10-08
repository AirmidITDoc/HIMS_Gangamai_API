﻿using HIMS.Common.Extensions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.IO;
using System.Reflection;

namespace HIMS.Data
{
    public class GenericRepository : IGenericRepository
    {
        public readonly IUnitofWork _unitofWork;
        public readonly SqlCommand _sqlCommand;
        //public readonly SqlCommand _sqlcommandSelect

        public GenericRepository(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
            _sqlCommand = unitofWork.CreateCommand();
            //_sqlcommandSelect = unitofWork.Select_CreateCommand();
        }

        public SqlCommand CreateCommand(
            string query,
            CommandType type,
            Dictionary<string, object> entity,
            SqlParameter outputParam = null
            )
        {
            _sqlCommand.CommandText = query;
            _sqlCommand.CommandType = type;
            _sqlCommand.Parameters.Clear();
            if (outputParam != null)
            {
                entity.Remove(outputParam.ParameterName);
                _sqlCommand.Parameters.Add(outputParam);
            }
            if ((entity?.Count ?? 0) > 0)
            {
                foreach (var property in entity)
                {
                    var param = new SqlParameter
                    {
                        ParameterName = property.Key,
                        Value = (object)property.Value
                    };

                    _sqlCommand.Parameters.Add(param);
                }
            }
            return _sqlCommand;
        }

        public SqlCommand Select_CreateCommand(
            string query,
            CommandType type,
            Dictionary<string, object> entity,
            SqlParameter outputParam = null
            )
        {
            _sqlCommand.CommandText = query;
            _sqlCommand.CommandType = type;
            _sqlCommand.Parameters.Clear();
            if (outputParam != null)
            {
                entity.Remove(outputParam.ParameterName);
                _sqlCommand.Parameters.Add(outputParam);
            }

            foreach (var property in entity)
            {
                var param = new SqlParameter
                {
                    ParameterName = property.Key,
                    Value = property.Value.ToString()
                };

                _sqlCommand.Parameters.Add(param);
            }

            return _sqlCommand;
        }

        public SqlCommand Select_CreateCommand(
            string query,
            CommandType type,
            SqlParameter[] entity,
            SqlParameter outputParam = null
            )
        {
            _sqlCommand.CommandText = query;
            _sqlCommand.CommandType = type;
            _sqlCommand.Parameters.Clear();
            if (outputParam != null)
            {
                _sqlCommand.Parameters.Add(outputParam);
            }
            _sqlCommand.Parameters.AddRange(entity);
            return _sqlCommand;
        }

        //Exec Non Query
        public int ExecNonQuery(string query, Dictionary<string, object> entity)
        {
            var cmd = CreateCommand(query, CommandType.Text, entity);
            var result = cmd.ExecuteNonQuery();
            _unitofWork.SaveChanges();
            return result;
        }
        public int ExecNonQueryWithoutPrams(string query)
        {
            _sqlCommand.CommandText = query;
            _sqlCommand.CommandType = CommandType.Text;
            var result = _sqlCommand.ExecuteNonQuery();
            _unitofWork.SaveChanges();
            return result;
        }
        public int ExecNonQueryProc(string proc, Dictionary<string, object> entity)
        {
            var cmd = CreateCommand(proc, CommandType.StoredProcedure, entity);
            var result = cmd.ExecuteNonQuery();
            _unitofWork.SaveChanges();
            return result;
        }
        public int ExecNonQueryProcBulk(string proc, JArray entities)
        {
            _sqlCommand.CommandText = proc;
            _sqlCommand.CommandType = CommandType.StoredProcedure;

            var dataTable = ToListExtensions.ToDataTable(entities.ToString());
            var parm = new SqlParameter
            {
                ParameterName = "@QueryParameters",
                Value = dataTable
            };
            _sqlCommand.Parameters.Add(parm);

            var result = _sqlCommand.ExecuteNonQuery();
            _unitofWork.SaveChanges();
            return result;
        }
        public string ExecNonQueryProcWithOutSaveChanges(
            string proc,
            Dictionary<string, object> entity,
            SqlParameter outputParam = null
            )
        {
            var id = string.Empty;
            CreateCommand(proc, CommandType.StoredProcedure, entity, outputParam);
            _sqlCommand.ExecuteNonQuery();
            if (outputParam != null)
            {
                id = Convert.ToInt64(_sqlCommand.Parameters[outputParam.ParameterName].Value).ToString();
            }
            _sqlCommand.Parameters.Clear();
            return id;
        }


        public object ExecScalar(string query, Dictionary<string, object> entity)
        {
            var cmd = CreateCommand(query, CommandType.Text, entity);
            var result = cmd.ExecuteScalar();
            _unitofWork.SaveChanges();
            return result;
        }
        public object ExecScalarProc(string proc, Dictionary<string, object> entity, bool isSave = true)
        {
            var cmd = CreateCommand(proc, CommandType.StoredProcedure, entity);
            var result = cmd.ExecuteScalar();
            if (isSave)
                _unitofWork.SaveChanges();
            return result;
        }
        public SqlDataReader ExecDataReader(string query, Dictionary<string, object> entity)
        {
            var cmd = CreateCommand(query, CommandType.Text, entity);
            var result = cmd.ExecuteReader();
            _unitofWork.SaveChanges();
            return result;
        }
        public SqlDataReader ExecDataReaderProc(string proc, Dictionary<string, object> entity)
        {
            var cmd = CreateCommand(proc, CommandType.StoredProcedure, entity);
            var result = cmd.ExecuteReader();
            _unitofWork.SaveChanges();
            return result;
        }

        //Get All Record
        public List<dynamic> ExecDataSetProc(string proc, Dictionary<string, object> entity)
        {
            var cmd = Select_CreateCommand(proc, CommandType.StoredProcedure, entity);
            var adapt = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            adapt.Fill(ds);
            var result = ds.Tables[0].ToDynamic();
            _unitofWork.SaveChanges();
            return result;
        }
        public DataTable GetDataTableProc(string proc, Dictionary<string, object> entity)
        {
            var cmd = Select_CreateCommand(proc, CommandType.StoredProcedure, entity);
            var adapt = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapt.Fill(dt);
            return dt;
        }
        public DataTable GetDataTableProc(string proc, SqlParameter[] entity)
        {
            var cmd = Select_CreateCommand(proc, CommandType.StoredProcedure, entity);
            var adapt = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapt.Fill(dt);
            return dt;
        }

        public dynamic GetDataSetByProc(string proc, Dictionary<string, object> entity)
        {
            var cmd = Select_CreateCommand(proc, CommandType.StoredProcedure, entity);
            var adapt = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            adapt.Fill(ds);
            dynamic dyn = new ExpandoObject();
            foreach (DataTable dt in ds.Tables)
            {
                var dict = (IDictionary<string, object>)dyn;
                if (dt.Rows.Count > 0)
                    dict[dt.TableName] = dt.ToDynamic();
            }
            _unitofWork.SaveChanges();
            return dyn;
        }
        public List<dynamic> ExecDataSetProcWithDataTable(string proc, JArray entity)
        {
            _sqlCommand.CommandText = proc;
            _sqlCommand.CommandType = CommandType.StoredProcedure;

            var dataTable = ToListExtensions.ToDataTable(entity.ToString());
            var parm = new SqlParameter
            {
                ParameterName = "@QueryParameters",
                Value = dataTable
            };
            _sqlCommand.Parameters.Add(parm);

            var adapt = new SqlDataAdapter(_sqlCommand);
            var ds = new DataSet();
            adapt.Fill(ds);
            var result = new List<dynamic>();

            if (ds.Tables.Count > 1)
            {
                foreach (DataTable table in ds.Tables)
                {
                    result.Add(table.ToDynamic());
                }
            }
            else
            {
                result = ds.Tables[0].ToDynamic();
            }
            _unitofWork.SaveChanges();
            return result;
        }
        public List<dynamic> ExecDataSetQuery(string query, Dictionary<string, object> entity)
        {
            var cmd = CreateCommand(query, CommandType.Text, entity);
            var adapt = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            adapt.Fill(ds);
            var result = ds.Tables[0].ToDynamic();
            return result;
        }
        public DataTable GetDataTableQuery(string query, Dictionary<string, object> entity)
        {
            var cmd = CreateCommand(query, CommandType.Text, entity);
            var adapt = new SqlDataAdapter(cmd);
            var dt = new DataTable();
            adapt.Fill(dt);
            return dt;
        }
        public T GetListItem<T>(SqlDataReader dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            for (int i = 0; i < dr.FieldCount; i++)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name.ToLower().Split('_')[0] == dr.GetName(i).ToLower().Split('_')[0])
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(Convert.ToString(dr[i])))
                            {
                                if (pro.PropertyType.Name == "String")
                                    pro.SetValue(obj, Convert.ToString(dr[i]));
                                else if (pro.PropertyType.Name == "Byte[]" && string.IsNullOrEmpty(Convert.ToString(dr[i])))
                                    pro.SetValue(obj, new byte[0]);
                                else
                                    pro.SetValue(obj, dr[i]);
                            }
                            else
                            {
                                if (pro.PropertyType.Name == "String")
                                    pro.SetValue(obj, "");
                            }
                        }
                        catch (Exception) { 
                        }
                        break;
                    }
                }
            }
            return obj;
        }

        public List<T> GetList<T>(string query, SqlParameter[] entity)
        {
            var cmd = Select_CreateCommand(query, CommandType.Text, entity);
            var dr = cmd.ExecuteReader();
            List<T> lst = new List<T>();
            while (dr.Read())
            {
                T item = GetListItem<T>(dr);
                lst.Add(item);
            }
            dr.Close();
            //_unitofWork.SaveChanges();
            return lst;
        }
        public List<T> GetListBySp<T>(string query, SqlParameter[] entity)
        {
            List<T> lst = new List<T>();
            try
            {
                var cmd = Select_CreateCommand(query, CommandType.StoredProcedure, entity);
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    T item = GetListItem<T>(dr);
                    lst.Add(item);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
            }
            //_unitofWork.SaveChanges();
            return lst;
        }
        public object ExecuteObjectBySP(string query, SqlParameter[] entity)
        {
            var cmd = Select_CreateCommand(query, CommandType.StoredProcedure, entity);
            object obj = cmd.ExecuteScalar();
            _unitofWork.SaveChanges();
            return obj;
        }

    }

    public static class CommonMethods
    {
        public static int ToInt(this object obj)
        {
            try
            {
                return Convert.ToInt32(obj);
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public static bool ToBool(this object obj)
        {
            try
            {
                return Convert.ToBoolean(obj);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
