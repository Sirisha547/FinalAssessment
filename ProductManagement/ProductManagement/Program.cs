using System;
using System.Data;
using System.Data.SqlClient;

namespace ProductManagement
{
    class Product
    {
        public SqlDataAdapter adp;
        public DataSet ds;
        public SqlCommandBuilder builder;


        public Product(SqlConnection con)
        {
            adp = new SqlDataAdapter("select * from Products", con);
            ds = new DataSet();
            builder = new SqlCommandBuilder(adp);
            adp.Fill(ds);
        }

        public void InsertProduct()
        {
            var row = ds.Tables [0].NewRow();

            Console.WriteLine("Enter ProductName: ");
            row["ProductName"] = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter Description: ");
            row["Descrp"] = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Enter Quantity: ");
            row["Quantity"] = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Price: ");
            row["Price"] = Convert.ToInt32(Console.ReadLine());

            ds.Tables[0].Rows.Add(row);

            adp.Update(ds);
            Console.WriteLine("Product inserted successfully");
        }
        public void ViewbyId()
        {
            Console.WriteLine("Enter the id to view:");
            int Id = Convert.ToInt32(Console.ReadLine());

            DataRow[] rows = ds.Tables[0].Select($"ProductId = {Id}");

            if (rows.Length > 0)
            {
                Console.WriteLine("ProductId | ProductName | Descrp | Quantity | Price");
                Console.Write($"{rows[0]["ProductId"]} | {rows[0]["ProductName"]} | {rows[0]["Descrp"]} | {rows[0]["Quantity"]} | {rows[0]["Price"]}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("Product not found");
            }

        }
        public void ViewAll()
        {
            if (ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
            {
                Console.WriteLine("ProductId | ProductName | Descrp | Quantity | Price");

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Console.WriteLine($"{row["ProductId"]} | {row["ProductName"]} | {row["Descrp"]} | {row["Quantity"]} | {row["Price"]}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Products not found");
            }

        }
        public void Update()
        {
            Console.WriteLine("Enter the id: ");
            int Id = Convert.ToInt32(Console.ReadLine());

            DataRow[] rows = ds.Tables[0].Select($"ProductId = {Id}");

            if (rows.Length > 0)
            {
                Console.WriteLine("Enter the column name u want to update: ");
                string colname = Console.ReadLine();

                Console.WriteLine("Enter the updated value:");
                string value = Console.ReadLine();

                rows[0][colname] = value;


                adp.Update(ds);
                Console.WriteLine("Product updated successfully");
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }
        public void Delete()
        {
            Console.WriteLine("Enter the id u want to delete:");
            int Id = Convert.ToInt32(Console.ReadLine());

            DataRow[] rows = ds.Tables[0].Select($"ProductId = {Id}");

            if (rows.Length > 0)
            {
                rows[0].Delete();

                adp.Update(ds);
                Console.WriteLine("Product deleted successfully");
            }
            else
            {
                Console.WriteLine("Product not found");
            }
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("Server=IN-7G3K9S3; database=ProductManagement; Integrated Security=true");
            Product obj = new Product(con);
            string result = "";
            do
            {
                Console.WriteLine("Welcome to Product Management App");
                Console.WriteLine("1. Insert Product");
                Console.WriteLine("2. View Product by id");
                Console.WriteLine("3. View All Products");
                Console.WriteLine("4. Update Product");
                Console.WriteLine("5. Delete Product");

                int ch = 0;
                try
                {
                    Console.WriteLine("Enter ur choice");
                    ch = Convert.ToInt16(Console.ReadLine());
                }
                catch (FormatException)
                {

                    Console.WriteLine("Enter only Numbers");
                }

                switch (ch)
                {
                    case 1:
                        {
                            obj.InsertProduct();
                            break;
                        }
                    case 2:
                        {
                            obj.ViewbyId();
                            break;
                        }
                    case 3:
                        {
                            obj.ViewAll();
                            break;
                        }
                    case 4:
                        {
                            obj.Update();
                            break;
                        }
                    case 5:
                        {
                            obj.Delete();
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("u entered wrong choice");
                            break;
                        }

                }
                Console.WriteLine("Do you want to continue[y/n]");
                result= Console.ReadLine();
            } while (result.ToLower() == "y");
            
        }
    }
}