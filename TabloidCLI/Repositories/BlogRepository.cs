﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        // Constructor to accept SQL connection string and pass to base class
        public BlogRepository(string connectionString) : base(connectionString) { }

        // Method to get all blogs from database
        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT
	                                          Id,
	                                          Title,
	                                          URL
                                          FROM
	                                          Blog";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Blog> blogs = new List<Blog>();

                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("URL"))
                        };

                        blogs.Add(blog);
                    }

                    reader.Close();

                    return blogs;
                }
            }
        }

        public Blog Get(int id)
        {
            return null;
        } 

        // Method to insert new blog to database
        public void Insert(Blog blog)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, URL)
                                             VALUES (@Title, @URL)";
                    cmd.Parameters.AddWithValue("@Title", blog.Title);
                    cmd.Parameters.AddWithValue("@URL", blog.Url);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Blog blog)
        {

        }

        public void Delete(int id)
        {

        }
    }
}
