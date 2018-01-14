using AssignmentWebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace AssignmentWebApp.Repository
{
    public class ProductRepository
    {
        private SqlConnection con;
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["AdventureConnection"].ConnectionString;
            con = new SqlConnection(constr);
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>Product list</returns>
        public List<Product> GetAllProducts()
        {
            connection();
            List<Product> productList = new List<Product>();
            SqlCommand cmd = new SqlCommand("GetAllproducts", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            productList = (from DataRow dr in dt.Rows
                           select new Product()
                           {
                               ProductID = Convert.ToInt32(dr["ProductID"]),
                               Name = Convert.ToString(dr["Name"]),
                               ListPrice = Convert.ToDouble(dr["ListPrice"]),
                               Description = Convert.ToString(dr["Description"])
                           }).ToList();
            return productList;
        }

        /// <summary>
        /// Get Product using ProductID
        /// </summary>
        /// <param name="id">ProductID</param>
        /// <returns>Product</returns>
        public Product GetProduct(int id)
        {
            connection();
            Product product = new Product();
            SqlCommand cmd = new SqlCommand("GetProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int);
            cmd.Parameters["@ProductID"].Value = id;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();

            if (dt.Rows.Count != 0)
            {
                product.ProductID = Convert.ToInt32(dt.Rows[0]["ProductID"]);
                product.Name = dt.Rows[0]["Name"].ToString();
                product.ListPrice = Convert.ToDouble(dt.Rows[0]["ListPrice"]);
                product.Description = dt.Rows[0]["Description"].ToString();
            }

            return product;
        }

        /// <summary>
        /// Update Title of the product
        /// </summary>
        /// <param name="id">ProductID</param>
        /// <param name="name">Title</param>
        /// <returns>0 : Update success, 1 : Update failed - the [Name] is duplicated</returns>
        public int UpdateProduct(int id, string name)
        {
            connection();
            Product product = new Product();
            SqlCommand cmd = new SqlCommand("UpdateProduct", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProductID", SqlDbType.Int);
            cmd.Parameters["@ProductID"].Value = id;
            cmd.Parameters.Add("@Name", SqlDbType.VarChar);
            cmd.Parameters["@Name"].Value = name;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            con.Close();
            return Convert.ToInt16(dt.Rows[0]["Result"]);
        }
    }
}