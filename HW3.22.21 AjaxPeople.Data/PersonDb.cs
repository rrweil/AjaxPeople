using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace HW3._22._21_AjaxPeople.Data
{
    public class PersonDb
    {
        private readonly string _connectionString;

        public PersonDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Person person)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) VALUES " +
                              "(@firstName, @lastName, @age) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            conn.Open();
            person.Id = (int)(decimal)cmd.ExecuteScalar();
        }

        public List<Person> GetAll()
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            List<Person> ppl = new List<Person>();
            conn.Open();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ppl.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"]
                });
            }

            return ppl;
        }

        public Person GetPersonById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM People WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            var reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                return null;
            }
            else
            {
                return new Person
                {
                    Id = (int)reader["id"],
                    FirstName = (string)reader["firstName"],
                    LastName = (string)reader["lastName"],
                    Age = (int)reader["age"]
                };
            }
        }

        public void UpdatePerson(Person p)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"UPDATE People 
SET firstName = @firstName, LastName = @lastName, Age = @age
WHERE ID = @id";
            cmd.Parameters.AddWithValue("@id", p.Id);
            cmd.Parameters.AddWithValue("@firstName", p.FirstName);
            cmd.Parameters.AddWithValue("@lastName", p.LastName);
            cmd.Parameters.AddWithValue("@age", p.Age);
            conn.Open();
            cmd.ExecuteNonQuery();

        }

        public void DeletePerson(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"DELETE FROM People WHERE ID = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
    }
}