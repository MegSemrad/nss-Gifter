using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Gifter.Models;
using Gifter.Utils;

namespace Gifter.Repositories
{
    public class UserProfileRepository : BaseRepository
    {
        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }





        //public List<Post> GetAll()
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                  SELECT Id, Title, Caption, DateCreated, ImageUrl, UserProfileId
        //                    FROM Post
        //                ORDER BY DateCreated";

        //            var reader = cmd.ExecuteReader();

        //            var posts = new List<Post>();
        //            while (reader.Read())
        //            {
        //                posts.Add(new Post()
        //                {
        //                    Id = DbUtils.GetInt(reader, "Id"),
        //                    Title = DbUtils.GetString(reader, "Title"),
        //                    Caption = DbUtils.GetString(reader, "Caption"),
        //                    DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
        //                    ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
        //                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
        //                });
        //            }

        //            reader.Close();

        //            return posts;
        //        }
        //    }
        //}





        //public Post GetById(int id)
        //{
        //    using (var conn = Connection)
        //    {
        //        conn.Open();
        //        using (var cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //                  SELECT Title, Caption, DateCreated, ImageUrl, UserProfileId
        //                    FROM Post
        //                   WHERE Id = @Id";

        //            DbUtils.AddParameter(cmd, "@Id", id);

        //            var reader = cmd.ExecuteReader();

        //            Post post = null;
        //            if (reader.Read())
        //            {
        //                post = new Post()
        //                {
        //                    Id = id,
        //                    Title = DbUtils.GetString(reader, "Title"),
        //                    Caption = DbUtils.GetString(reader, "Caption"),
        //                    DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
        //                    ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
        //                    UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
        //                };
        //            }

        //            reader.Close();

        //            return post;
        //        }
        //    }
        //}





        public void Add(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO UserProfile (Name, Email, ImageUrl, DateCreated)
                        OUTPUT INSERTED.ID
                        VALUES (@Title, @Caption, @DateCreated, @ImageUrl, @UserProfileId)";

                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userProfile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@DateCreated", userProfile.DateCreated);

                    userProfile.Id = (int)cmd.ExecuteScalar();
                }
            }
        }






        public void Update(UserProfile userProfile)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE UserProfile
                           SET Name = @Name,
                               Email = @Email,
                               ImageUrl = @ImageUrl,
                               DateCreated = @DateCreated
                         WHERE Id = @Id";

                    DbUtils.AddParameter(cmd, "@Name", userProfile.Name);
                    DbUtils.AddParameter(cmd, "@Email", userProfile.Email);
                    DbUtils.AddParameter(cmd, "@ImageUrl", userProfile.ImageUrl);
                    DbUtils.AddParameter(cmd, "@DateCreated", userProfile.DateCreated);
                    DbUtils.AddParameter(cmd, "@Id", userProfile.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }





        public void Delete(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}