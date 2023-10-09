using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace adddemo.Models
{
    public class RepoModel
    {
        public List<MyAdvertiseModel> Advertises()
        {
            List<MyAdvertiseModel> myAdvertiseList = new List<MyAdvertiseModel>();

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
            {
                try
                {
                    // string query = "GetAdvertiseData2";
                    SqlCommand cmd = new SqlCommand("GetAdvertiseData", con);

                    cmd.CommandType = CommandType.StoredProcedure;

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        MyAdvertiseModel myAdvertise = new MyAdvertiseModel
                        {
                            advertiseId = Convert.ToInt32(reader["advertiseId"]),
                            //  pro=Convert.ToInt32(reader["productCategoryName"]),
                            advertiseTitle = Convert.ToString(reader["advertiseTitle"]),
                            advertiseDescription = Convert.ToString(reader["advertiseDescription"]),
                            advertisePrice = Convert.ToInt32(reader["advertisePrice"]),
                            //areaName=Convert.ToInt32(reader["areaName"]),
                            advertiseStatus = Convert.ToBoolean(reader["advertiseStatus"]),
                            //firstName = Convert.ToString(reader["firstName"]),
                            //advertiseApproved = Convert.ToBoolean(reader["advertiseApproved"])

                        };

                        myAdvertiseList.Add(myAdvertise);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return myAdvertiseList;
        }

        public bool ApproveAdvertisement(int advertiseId)
        {

                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("ApproveAdvertisement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@advertiseId", advertiseId);
                    //cmd.Parameters.Add(new SqlParameter("@advertiseId", SqlDbType.Int)).Value = obj.advertiseId;

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    return rowsAffected >= 1;
                }
 
        }

        public bool RejectAdvertisement(MyAdvertiseModel obj)
        {
            
           using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
                {
                    SqlCommand cmd = new SqlCommand("RejectAdvertisement", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@advertiseId", obj.advertiseId);

                    con.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    return rowsAffected >= 1;
                }
            
           
        }
        //public MyAdvertiseModel GetAdvertisementDetails(int advertiseId)
        //{
        //    using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
        //    {
        //        SqlCommand cmd = new SqlCommand(
        //            "SELECT advertiseId, advertiseTitle, advertiseDescription, advertisePrice, advertiseStatus " +
        //            "FROM YourAdvertisementTable " + // Replace with your actual table name
        //            "WHERE advertiseId = @advertiseId", con);

        //        cmd.Parameters.AddWithValue("@advertiseId", advertiseId);

        //        con.Open();
        //        SqlDataReader reader = cmd.ExecuteReader();

        //        MyAdvertiseModel advertisement = new MyAdvertiseModel(); // Create a default instance

        //        if (reader.Read())
        //        {
        //            advertisement.advertiseId = Convert.ToInt32(reader["advertiseId"]);
        //            advertisement.advertiseTitle = Convert.ToString(reader["advertiseTitle"]);
        //            advertisement.advertiseDescription = Convert.ToString(reader["advertiseDescription"]);
        //            advertisement.advertisePrice = Convert.ToDecimal(reader["advertisePrice"]);
        //            advertisement.advertiseStatus = Convert.ToBoolean(reader["advertiseStatus"]);
        //            // Add other properties as needed
        //        }

        //        reader.Close();

        //        return advertisement; // Return the MyAdvertiseModel object with data or default values
        //    }
        //}
        public MyAdvertiseModel GetAdvertiseById(int? advertiseId)
        {
            MyAdvertiseModel product = null;

            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("GetProductData", con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Add the parameter to the SqlCommand
                cmd.Parameters.AddWithValue("@advertiseId", advertiseId);

                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    product = new MyAdvertiseModel();
                    product.advertiseId = Convert.ToInt32(rdr["advertiseId"]);
                    product.advertiseTitle = rdr["advertiseTitle"].ToString();
                    product.advertiseDescription = rdr["advertiseDescription"].ToString();
                    product.advertisePrice = Convert.ToDecimal(rdr["advertisePrice"]);
                    product.advertiseStatus = Convert.ToBoolean(rdr["advertiseStatus"]);
                    // Add other fields as needed
                }

                rdr.Close();
            }

            return product;
        }
        public bool UpdateAdvertisement(MyAdvertiseModel advertisement)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
            {
                SqlCommand com = new SqlCommand("UpdateAdvertisement", con);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@advertiseId", advertisement.advertiseId);
                com.Parameters.AddWithValue("@advertiseTitle", advertisement.advertiseTitle);
                com.Parameters.AddWithValue("@advertiseDescription", advertisement.advertiseDescription);
                com.Parameters.AddWithValue("@advertisePrice", advertisement.advertisePrice);
                com.Parameters.AddWithValue("@advertiseStatus", advertisement.advertiseStatus);
                // Add other parameters as needed

                con.Open();
                int rowsAffected = com.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

        public bool DeleteAdvertise(int? advertiseId)
        {
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["connection"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("DeleteAdvertise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@advertiseId", advertiseId);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected > 0;
            }
        }

    }
}

