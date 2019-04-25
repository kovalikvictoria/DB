using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Configuration;

namespace ForDataBase
{
    //public class Discount
    //{
    //    public int discount_id { get; set; }
    //    public string name { get; set; }
    //    public double coef { get; set; }

    //    public Discount(int discount_id = 0, string name = "", double coef = 0.0)
    //    {
    //        this.discount_id = discount_id;
    //        this.name = name;
    //        this.coef = coef;
    //    }
    //}

    //public class Customer
    //{
    //    public int cus_id { get; set; }
    //    public string fname { get; set; }
    //    public string lname { get; set; }
    //    public int discount_id { get; set; }

    //    public Customer(int cus_id = 0, string fname = "", string lname = "", int discount_id = 0)
    //    {
    //        this.cus_id = cus_id;
    //        this.fname = fname;
    //        this.lname = lname;
    //        this.discount_id = discount_id;
    //    }
    //}

    //interface IRepository
    //{
    //    void Add( DbConnection c, DbProviderFactory f);
    //    void Select(DbConnection c, DbProviderFactory f);
    //    void Delete(DbConnection c, DbProviderFactory f);
    //    void Update(DbConnection c, DbProviderFactory f);
    //}

    public class DiscountRepository
    {
        public static void Add(DbConnection c, DbProviderFactory f)
        {
            var cmd = f.CreateCommand();
            cmd.Connection = c;
            c.Open();

            try
            {
                Console.WriteLine("ID: ");
                int discount_id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("NAME: ");
                string name = Console.ReadLine();
                Console.WriteLine("COEF: ");
                double coef = Convert.ToDouble(Console.ReadLine());
                

                string sql = string.Format("Insert into Discount (discount_id, name, coef) values ( {0}, '{1}', {2} )", discount_id, name, Program.ToMyString(coef.ToString()));

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.ToString());
            }
            finally
            {
                c.Close();
            }
        }
        //дописати функцію, що перетворює ',' на '.'
        public static void Select(DbConnection c, DbProviderFactory f)
        {
            var cmd = f.CreateCommand();
            cmd.Connection = c;
            c.Open();

            try
            {
                Console.WriteLine("Select columns (please, white with ',') : ");
                string str = Console.ReadLine().ToLower();
               
                string sql = string.Format("Select {0} from Discount", str);

                cmd.CommandText = sql;
                using (c)
                {
                    var reader = cmd.ExecuteReader();

                    if (str == "*" ||
                        str == "coef, discount_id, name" || str == "coef, name, discount_id" ||
                        str == "name, coef, discount_id" || str == "name, discount_id, coef" || 
                        str == "discount_id, coef, name" || str == "discount_id, name, coef")
                    {
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("|                    DISCOUNT                   |");
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("|\tID\t|\tNAME\t|\tCOEF\t|");
                        Console.WriteLine("-------------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(string.Format("|\t{0}\t|\t{1}\t|\t{2}\t|", reader["discount_id"], reader["name"], reader["coef"]));
                            Console.WriteLine("-------------------------------------------------");
                        }
                        Console.ReadLine();
                    }

                    else if (str == "discount_id, name" || str == "name, discount_id")
                    {
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("|                    DISCOUNT                   |");
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("|\tID\t|\tNAME\t|");
                        Console.WriteLine("-------------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(string.Format("|\t{0}\t|\t{1}\t|", reader["discount_id"], reader["name"]));
                            Console.WriteLine("-------------------------------------------------");
                        }
                        Console.ReadLine();
                    }

                    else if (str == "name, coef" || str == "coef, name")
                    {
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("|                    DISCOUNT                   |");
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("|\tNAME\t|\tCOEF\t|");
                        Console.WriteLine("-------------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(string.Format("|\t{0}\t|\t{1}\t|", reader["name"], reader["coef"]));
                            Console.WriteLine("-------------------------------------------------");
                        }
                        Console.ReadLine();
                    }

                    else if (str == "discount_id, coef" || str == "coef, discount_id")
                    {
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("|                    DISCOUNT                   |");
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("|\tID\t|\tCOEF\t|");
                        Console.WriteLine("-------------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(string.Format("|\t{0}\t|\t{1}\t|", reader["discount_id"], reader["coef"]));
                            Console.WriteLine("-------------------------------------------------");
                        }
                        Console.ReadLine();
                    }

                    else if (str == "discount_id" || str == "name" || str == "coef")
                    {
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("|                    DISCOUNT                   |");
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine(string.Format("|\t{0}\t|", str.ToUpper()));
                        Console.WriteLine("-------------------------------------------------");
                        while (reader.Read())
                        {
                            Console.WriteLine(string.Format("|\t{0}\t|", reader[str]));
                            Console.WriteLine("-------------------------------------------------");
                        }
                        Console.ReadLine();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error " + e.ToString());
            }
            finally
            {
                c.Close();
            }
        
    } //ready
        public static void Delete(DbConnection c, DbProviderFactory f)
        {
            var cmd = f.CreateCommand();
            cmd.Connection = c;
            c.Open();

            Console.WriteLine("Delete from Discount where coef = ");
            //string str = Console.ReadLine();
            double coef = Convert.ToDouble(Console.ReadLine());
            cmd.CommandText = string.Format("Delete from discount where coef={0}", Program.ToMyString(coef.ToString()));
            //cmd.CommandText = string.Format("Delete from Discount where {0}", Program.ToMyString(str.ToLower()));
            cmd.ExecuteNonQuery();
            c.Close();
            //try
            //{
            //    Console.WriteLine("Delete from Discount where ");
            //    //string str = Console.ReadLine();

            //    cmd.CommandText = "Delete from Discount where coef = 0.3";
            //    //cmd.CommandText = string.Format("Delete from Discount where {0}", Program.ToMyString(str.ToLower()));
            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Error" + e.ToString());
            //}
            //finally
            //{
            //    c.Close();
            //}
        }
        public static void Update(DbConnection c, DbProviderFactory f)
        {

        }
    }

    public class CustomerRepository
    {
        public static void Add(DbConnection c, DbProviderFactory f)
        {
            var cmd = f.CreateCommand();
            cmd.Connection = c;
            c.Open();

            try
            {
                Console.WriteLine("ID: ");
                int cus_id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("FIRSTNAME: ");
                string fname = Console.ReadLine();
                Console.WriteLine("LASTNAME: ");
                string lname = Console.ReadLine();
                Console.WriteLine("DISCOUNT:");
                int discount_id = Convert.ToInt32(Console.ReadLine());

                string sql = string.Format("Insert into Discount (cus_id, fname, lname, discount_id) values ( {0}, '{1}', '{2}', {3} )",
                    cus_id, fname, lname, discount_id);

                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.ToString());
            }
            finally
            {
                c.Close();
            }
        }
        public static void Select(DbConnection c, DbProviderFactory f)
        {
            var cmd = f.CreateCommand();
            cmd.Connection = c;
            c.Open();

            try
            {
                Console.WriteLine("Select columns (please, write with ','): ");
                string str = Console.ReadLine();
                string sql = string.Format("Select {0} from Customer", str);

                cmd.CommandText = sql;
                using (c)
                {
                    var reader = cmd.ExecuteReader();

                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|                    DISCOUNT                   |");
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine("|\tID\t|\tNAME\t|\tCOEF\t|");
                    Console.WriteLine("-------------------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine(string.Format("|\t{0}\t|\t{1}\t|\t{2}\t|", reader["discount_id"], reader["name"], reader["coef"]));
                        Console.WriteLine("-------------------------------------------------");
                    }
                    Console.ReadLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.ToString());
            }
            finally
            {
                c.Close();
            }

        }
        public static void Delete(DbConnection c, DbProviderFactory f)
        {
            var cmd = f.CreateCommand();
            cmd.Connection = c;
            c.Open();

            try
            {
                string str = Console.ReadLine();
                cmd.CommandText = str;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e.ToString());
            }
            finally
            {
                c.Close();
            }
        }
        public static void Update(DbConnection c, DbProviderFactory f)
        {

        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            string invariant = "Npgsql";
            DbProviderFactory factory = DbProviderFactories.GetFactory(invariant);

            //connection
            var conn = factory.CreateConnection();
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["Npgsql"].ConnectionString;

            Menu.ChooseTable(conn, factory);
        }

        public static string ToMyString(string s)
        {
            return s.Replace(',', '.');
        }

    }

    class Menu
    { 
        public static void ChooseTable(DbConnection c ,DbProviderFactory f)
        {
            Console.WriteLine("What table you choose?\n1. Discounts\n2. Customers\n");
            Console.Write("Your selection: ");
            string ch = Console.ReadLine();

            switch (ch)
            {
                case "1":
                    {
                        ChooseActionForDiscount(c, f);
                        break;
                    }

                case "2":
                    {
                        ChooseActionForCustomer(c, f);
                        break;
                    }

                default:
                    Console.WriteLine("Invalid selection. Please select 1 or 2.");
                    break;
            }
        }

        public static void ChooseActionForDiscount(DbConnection c, DbProviderFactory f)
        {
            Console.WriteLine("\nWhat action you choose?\n" +
                          "1. View table\n" +
                          "2. Add row\n" +
                          "3. Update row\n" +
                          "4. Delete row");
            Console.Write("Your selection: ");
            string ch = Console.ReadLine();

            switch (ch)
            {
                case "1":
                    {
                        DiscountRepository.Select(c, f);
                        break;   
                    }

                case "2":
                    {
                        DiscountRepository.Add(c, f);
                        break;
                    }

                case "3":
                    {
                        DiscountRepository.Update(c, f);
                        break;
                    }

                case "4":
                    {
                        DiscountRepository.Delete(c, f);
                        break;
                    }

                default:
                    c.Close();
                    Console.WriteLine("Invalid selection. Please select 1, 2, 3 or 4.");
                    break;
            }
        }

        public static void ChooseActionForCustomer(DbConnection c, DbProviderFactory f)
        {
            Console.WriteLine("What action you choose?\n" +
                          "1. View table\n" +
                          "2. Add row\n" +
                          "3. Update row\n" +
                          "4. Delete row");
            Console.Write("Your selection: ");
            string ch = Console.ReadLine();

            switch (ch)
            {
                case "1":
                    {
                        CustomerRepository.Select(c, f);
                        break;
                    }

                case "2":
                    {
                        CustomerRepository.Add(c, f);
                        break;
                    }

                case "3":
                    {
                        CustomerRepository.Update(c, f);
                        break;
                    }

                case "4":
                    {
                        CustomerRepository.Delete(c, f);
                        break;
                    }

                default:
                    c.Close();
                    Console.WriteLine("Invalid selection. Please select 1 or 2.");
                    break;
            }
        }
    }
}
