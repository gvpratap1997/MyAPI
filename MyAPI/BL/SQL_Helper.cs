using MyAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;

namespace MyAPI.BL
{
    public class SQL_Helper
    {
            internal List<Resources> LoadDb()
            {
                var sql_con = new Npgsql.NpgsqlConnection("Host=localhost;Username=postgres;Password=SmartWork@123;Database=postgres");
                sql_con.Open();
                var commandText = "select * from resources";
                var cmd = new Npgsql.NpgsqlCommand(commandText, sql_con);
                Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader();
                List<Resources> personList = new List<Resources>();
                personList = DataReaderMapToList<Resources>(rdr);
                return personList;
            }

        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (System.Reflection.PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }
        internal List<Resources> LoadDbGetId(int id)
            {
            var sql_con = new Npgsql.NpgsqlConnection("Host=localhost;Username=postgres;Password=SmartWork@123;Database=postgres");
            sql_con.Open();
            var commandText = "select * from resources where id = " + id + "";
            var cmd = new Npgsql.NpgsqlCommand(commandText, sql_con);
            Npgsql.NpgsqlDataReader rdr = cmd.ExecuteReader();

            List<Resources> personList = new List<Resources>();
            personList = DataReaderMapToList<Resources>(rdr);
            return personList;
            }
            internal void insert(Resources EmpData)
            {
            var sql_con = new Npgsql.NpgsqlConnection("Host=localhost;Username=postgres;Password=SmartWork@123;Database=postgres");
            sql_con.Open();
            Npgsql.NpgsqlCommand dbcmd = sql_con.CreateCommand();
            var commandText = "insert into resources (id,name,salery,age,place) values(@id,@name,@age,@salery,@place)";
            var cmd = new Npgsql.NpgsqlCommand(commandText, sql_con);
            dbcmd.CommandText = commandText;
            dbcmd.Parameters.AddWithValue("@id", EmpData.id);
            dbcmd.Parameters.AddWithValue("@name", EmpData.name);
            dbcmd.Parameters.AddWithValue("@age", EmpData.age);
            dbcmd.Parameters.AddWithValue("@salery", EmpData.salery);
            dbcmd.Parameters.AddWithValue("@place", EmpData.place);
            dbcmd.ExecuteNonQuery();
            }
            internal void update(int id, Resources value)
            {
            var sql_con = new Npgsql.NpgsqlConnection("Host=localhost;Username=postgres;Password=SmartWork@123;Database=postgres");
            sql_con.Open();
            Npgsql.NpgsqlCommand dbcmd = sql_con.CreateCommand();
            var commandText = "update resources set \"name\"=:name,\"salery\"=:salery,\"age\"=:age,\"place\"=:place where id = " + id + "";
            var cmd = new Npgsql.NpgsqlCommand(commandText, sql_con);
            dbcmd.CommandText = commandText;
            dbcmd.Parameters.AddWithValue("name", value.name);
            dbcmd.Parameters.AddWithValue("age", value.age);
            dbcmd.Parameters.AddWithValue("salery", value.salery);
            dbcmd.Parameters.AddWithValue("place", value.place);
            dbcmd.ExecuteNonQuery();
            }
            internal void delete(int id)
            {
            var sql_con = new Npgsql.NpgsqlConnection("Host=localhost;Username=postgres;Password=SmartWork@123;Database=postgres");
            sql_con.Open();
            Npgsql.NpgsqlCommand dbcmd = sql_con.CreateCommand();
            var commandText = "delete from resources where id =@id";
            var cmd = new Npgsql.NpgsqlCommand(commandText, sql_con);
            dbcmd.CommandText = commandText;
            dbcmd.Parameters.AddWithValue("@id", id);
            dbcmd.ExecuteNonQuery();
            }
    }
}

