using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Finisar.SQLite;
using System.IO;
using PdfFileWriter;

namespace Sales
{
    class DataBase
    {
        public bool IsFirst { get; set; }
        public bool IsLoggedIn { get; set; }
        public string LoginError { get; set; }
        public int Loginid { get; set; }
        public int CurrentBillno { get; set; }
        public int ItemNo { get; set; }
        public int BillTotal { get; set; }
        public int ViewPageNo { get; set; }


        public string DatabaseName = "database.sqlite";

        SQLiteConnection sqlite_conn;
        SQLiteCommand sqlite_cmd;
        SQLiteDataReader sqlite_datareader;
        SQLiteDataReader sqlite_datareader2;
        SQLiteDataReader sqlite_datareader3;

        Random rndm = new Random();


        public DataBase()
        {
            IsFirst = true;
            IsLoggedIn = false;
            LoginError = "Please Sign in to Continue";
        }

        public bool Login(string name, string pass)
        {
            IsLoggedIn = false;

            string cmd = "SELECT * FROM credential  WHERE name='" + name + "' AND pass='" + pass + "'";

            sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = cmd;
            sqlite_datareader = sqlite_cmd.ExecuteReader();

            if (sqlite_datareader.Read())
            {
                IsLoggedIn = true;
                LoginError = "Welcome " + sqlite_datareader["name"];
                Loginid = Convert.ToInt32(sqlite_datareader["id"]);
            }
            else
            {
                LoginError = "Username or password is wrong.!";
            }
            sqlite_datareader.Close();
            return IsLoggedIn;
        }
        public bool IfIsFirst()
        {

            IsFirst = false;
            try
            {
                sqlite_conn = new SQLiteConnection("Data Source=" + DatabaseName + ";Version=3;New=False;Compress=True;");

                sqlite_conn.Open();

            }
            catch
            {
                IsFirst = true;
            }

            return IsFirst;
        }

        public bool CreateNewDatabase()
        {
            try
            {
                sqlite_conn = new SQLiteConnection("Data Source=" + DatabaseName + ";Version=3;New=True;Compress=True;");
                sqlite_conn.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void AddTables()
        {

            string[] cmds ={

                                "CREATE TABLE credential    (id integer primary key, name   varchar(50) , pass varchar(50) , access int, date varchar(50));",
                                "CREATE TABLE measurement   (id integer primary key, name   varchar(50));",
                                "CREATE TABLE items         (id integer primary key, billno integer, itemid integer, qty integer, price integer, bo varchar(5) );",
                                "CREATE TABLE itemlist      (id integer primary key, sp     varchar(5), measureid integer, name varchar(50));",
                                "CREATE TABLE billinfo      (billno integer, sp varchar(5), date   varchar(50), partyid integer, items integer, itotal integer, vat integer, postage integer, himali integer, total integer ,pm varchar(10), gr varchar(10), through varchar(50), rno integer );",   
                                "CREATE TABLE party         (id integer primary key, name   varchar(50), date varchar(50), license varchar(50), address varchar(150), sp varchar(5));",
                                "CREATE TABLE orderbook     (id integer primary key, date   varchar(50), deldate varchar(50), reminddate varchar(50), itemsno integer, completed integer, partyname varchar(50) );",
                                "CREATE TABLE orderitems    (id integer primary key, name varchar(50),qty integer, orderno integer)",
                                "CREATE TABLE pay           (id integer primary key, date   varchar(50), sp varchar(5), bill integer, partyid integer, amount integer);"
                                
                          };

            foreach (string cmd in cmds)
            {
                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    LoginError = ex.ToString();
                }


            }

            AddDefaults();



        }   
        public void AddDefaults()
        {
            SetDefaultCredentials();
            AddMeasurementScales();
        }

        public void SetDefaultCredentials()
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string dfltname = "user1";
                string dfltpass = "user1";

                string cmd = "INSERT INTO credential (name, pass, access, date) VALUES('" + dfltname.ToString() + "','" + dfltpass.ToString() + "',1," + DateTime.Today.Date.ToShortDateString() + ");";

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_cmd.ExecuteNonQuery();
            }
        }
        public void AddMeasurementScales()
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd1 = "INSERT INTO measurement (name) VALUES('";
                string cmd2 = "');";
                string[] values ={
                                 "Dozen",
                                 "Kg",
                                 "Box"
                            };
                foreach (string s in values)
                {

                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd1 + s + cmd2;
                    sqlite_cmd.ExecuteNonQuery();
                }

            }
        }

        public bool AddItem(string name, int Measurementid, char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "INSERT INTO itemlist (sp, measureid, name) VALUES('" + sp + "', " + Measurementid + ", '" + name + "')";

                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public int GetItemNo(char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM itemlist WHERE sp='" + sp + "'";
                int count = 0;

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {

                    count++;
                }

                sqlite_datareader.Close();

                return count;
            }
            else
                return 0;
        }
        public string[] ItemInfo(int k, char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM itemlist WHERE sp='" + sp + "'";

                string[] info = new string[4];

                int count = 0;

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    if (count == k)
                    {
                        info[0] = sqlite_datareader["id"].ToString();
                        info[1] = sqlite_datareader["name"].ToString();
                        info[2] = sqlite_datareader["measureid"].ToString();
                        info[3] = sqlite_datareader["sp"].ToString();


                    }
                    count++;
                }

                return info;
            }
            else
                return null;
        }
        public string[] ItemInfo(int id)
        {
            //sqlite_conn.Close();
            //if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM itemlist WHERE id=" + id;

                string[] info = new string[5];

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                sqlite_datareader.Read();

                info[0] = sqlite_datareader["id"].ToString();
                info[1] = sqlite_datareader["name"].ToString();
                info[2] = sqlite_datareader["measureid"].ToString();
                info[3] = sqlite_datareader["sp"].ToString();

                sqlite_datareader.Close();

                return info;

            }
           // else
             //   return null;
        }


        public string[] GetItems(char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM itemlist WHERE sp='" + sp + "'";
                string[] items = new string[GetItemNo(sp)];

                int count = 0;


                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    items[count] = new string(sqlite_datareader["name"].ToString().ToCharArray());

                    count++;
                }


                sqlite_datareader.Close();


                return items;

            }
            else
                return null;
        }
        public bool DeleteItem(int k, char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string[] info = ItemInfo(k, sp);

                string cmd = "DELETE FROM itemlist WHERE id=" + Convert.ToInt32(info[0]);
                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    //LoginError = ex.ToString();
                    return false;
                }
            }
            else
                return false;
        }
        public bool UpdateItem(int id, string name, int measurementid, char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "UPDATE itemlist SET name='" + name + "', measureid=" + measurementid + ", sp='" + sp + "' WHERE id=" + id;

                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
        public int GetItemId(string name, char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM itemlist WHERE name='" + name + "' AND sp='" + sp + "'";

                int id;

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                sqlite_datareader.Read();

                id = Convert.ToInt32(sqlite_datareader["id"]);

                sqlite_datareader.Close();

                return id;
            }
            else
                return 0;
        }

        private int GetMeasurementScalesno()
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM measurement";

                int count = 0;

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {

                    count++;
                }

                sqlite_datareader.Close();

                return count;
            }
            else
                return 0;

        }
        public string[] GetMeasurementScales()
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM measurement";

                string[] mea = new string[GetMeasurementScalesno()];

                int count = 0;

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();


                while (sqlite_datareader.Read())
                {
                    mea[count] = new string(sqlite_datareader["name"].ToString().ToCharArray());
                    count++;
                }

                sqlite_datareader.Close();
                return mea;
            }
            else
                return null;
        }
        public int GetMeasurementScaleid(string name)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM measurement WHERE name ='" + name + "'";
                int id;

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                sqlite_datareader.Read();

                id = Convert.ToInt32(sqlite_datareader["id"]);

                sqlite_datareader.Close();

                return id;
            }
            else
                return 0;
        }
        public string GetMeasurementScaleName(int k)
        {
            //sqlite_conn.Close();
            //if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM measurement WHERE id =" + k;
                string scale = "";

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                sqlite_datareader.Read();

                scale = sqlite_datareader["name"].ToString();

                sqlite_datareader.Close();

                return scale;
            }
            //else
              //  return null;
        }


        public bool AddParty(string name, string license, string address, char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "INSERT INTO party (name,date,license, address,sp) values('" + name + "','" + DateTime.Today.Date.ToShortDateString().ToString() + "','" + license + "','" + address + "','" + sp + "') ";

                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();

                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
        public int GetPartyNo(char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM party where sp='" + sp + "'";

                int count = 0;

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {

                    count++;
                }

                sqlite_datareader.Close();

                return count;
            }
            else
                return 0;
        }
        public string[] GetSPartys()
        {
            return GetPartys('s');
        }
        public string[] GetPartys(char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                //insertparty();
                string cmd = "SELECT * FROM party where sp='" + sp + "'";



                string[] partys = new string[GetPartyNo(sp)];

                int count = 0;

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    partys[count] = new string(sqlite_datareader["name"].ToString().ToCharArray());

                    count++;
                }


                sqlite_datareader.Close();

                return partys;
            }
            else
                return null;

        }
        public string[] PartyInfo(int k, char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM party where sp='" + sp + "'";

                string[] info = new string[5];


                int count = 0;

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                while (sqlite_datareader.Read())
                {
                    if (count == k)
                    {
                        info[0] = sqlite_datareader["id"].ToString();
                        info[1] = sqlite_datareader["name"].ToString();
                        info[2] = sqlite_datareader["date"].ToString();
                        info[3] = sqlite_datareader["license"].ToString();
                        info[4] = sqlite_datareader["address"].ToString();


                    }
                    count++;
                }

                sqlite_datareader.Close();

                return info;
            }
            else
                return null;
        }
        public string[] PartyInfo(int id)
        {
            //sqlite_conn.Close();
            //if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM party WHERE id=" + id;

                string[] info = new string[5];

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                sqlite_datareader.Read();

                info[0] = sqlite_datareader["id"].ToString();
                info[1] = sqlite_datareader["name"].ToString();
                info[2] = sqlite_datareader["date"].ToString();
                info[3] = sqlite_datareader["license"].ToString();
                info[4] = sqlite_datareader["address"].ToString();

                sqlite_datareader.Close();

                return info;
            }
            //else
              //  return null;
        }

        public bool UpdateParty(int id, string name, string license, string address, char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "UPDATE party SET name='" + name + "', license='" + license + "', address='" + address + "', sp='" + sp + "' WHERE id=" + id + "";

                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;

        }
        public bool DeleteParty(int k, char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string[] info = PartyInfo(k, sp);

                string cmd = "DELETE FROM party WHERE id=" + Convert.ToInt32(info[0]);

                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
        public int GetMaxBillno(char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                int billno;
                string cmd = "SELECT MAX(billno) FROM billinfo WHERE sp='" + sp + "'";


                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                sqlite_datareader.Read();
                try
                {


                    billno = Convert.ToInt32(sqlite_datareader[0]);


                }
                catch
                {
                    billno = 0;
                }
                sqlite_datareader.Close();
                return billno;
            }
            else
                return 0;
        }

        public string[,] GetPayDetails(char BillType, int Pid)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM pay WHERE sp='" + BillType + "'AND partyid=" + Pid;
                int count = 0;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    count++;
                }
                sqlite_datareader.Close();
                string[,] Details = new string[4, count + 1];
                Details[0, 0] = count.ToString();
                int i = 1, Bal = 0;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    Details[0, i] = sqlite_datareader["date"].ToString();
                    Details[1, i] = sqlite_datareader["bill"].ToString();
                    Details[2, i] = sqlite_datareader["amount"].ToString();
                    if (Convert.ToInt32(Details[1, i]) == 1)
                    {
                        Bal += Convert.ToInt32(Details[2, i]);
                    }
                    else
                    {
                        Bal -= Convert.ToInt32(Details[2, i]);
                    }
                    i++;
                }

                sqlite_datareader.Close();
                Details[1, 0] = Bal.ToString();


                return Details;
            }
            else
                return null;

        }
        public bool AddPay(char BillType, int Pid, int amount)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "INSERT INTO pay (date,sp,bill,partyid,amount) VALUES('" + DateTime.Today.Date.ToShortDateString() + "','" + BillType + "',0," + Pid.ToString() + "," + amount.ToString() + ");";
                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;

        }

        public int NewBill(int PartyId, char sp)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                int billno;

                billno = GetMaxBillno(sp);
                billno++;
                string cmd = "INSERT INTO billinfo (billno, sp, date, partyid,items,itotal,total) VALUES(" + billno + ",'" + sp + "','" + DateTime.Today.Date.ToShortDateString() + "', " + PartyId + ",0,0,0)";

                //int pid = 0;

                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();
                    return billno;

                }
                catch
                {
                    return 0;
                }
            }
            else
                return 0;
        }
        public bool AddItemToBill(int BillNo, int ItemId, int Qty, int Price, char BillType)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "INSERT INTO items (billno, itemid, qty, price, bo ) values(" + BillNo + "," + ItemId + "," + Qty + "," + Price + ",'" + BillType + "')";

                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();



                    return true;
                }
                catch
                {

                    return false;
                }
            }
            else
                return false;
        }
        public bool CancelBill(int BillNo, char BillType)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "DELETE FROM billinfo WHERE billno=" + BillNo + " AND sp='" + BillType + "'";
                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();

                    cmd = "DELETE FROM items WHERE billno=" + BillNo + " AND bo='" + BillType + "'";

                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();

                    return true;
                }
                catch
                {
                    // LoginError = ex.ToString();
                    return false;
                }
            }
            else
                return false;
        }
        public bool MakeBill(char BillType, string date, int Vat, int Postage, int Himali, int Total, string PM, string GR, string Through, int rno)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT partyid FROM billinfo WHERE billno=" + CurrentBillno;
                int pid = 0;
                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_datareader = sqlite_cmd.ExecuteReader();
                    sqlite_datareader.Read();
                    pid = Convert.ToInt32(sqlite_datareader[0]);
                    sqlite_datareader.Close();

                    cmd = "DELETE FROM billinfo WHERE billno=" + CurrentBillno;

                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();

                    cmd = "INSERT INTO billinfo VALUES(" + CurrentBillno + ",'" + BillType + "','" + date + "'," + pid + "," + ItemNo + "," + BillTotal + "," + Vat + "," + Postage + "," + Himali + "," + Total + ",'" + PM + "','" + GR + "','" + Through + "'," + rno + ")";
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();

                    cmd = "INSERT INTO pay (date,sp,bill,partyid,amount) VALUES('" + DateTime.Today.Date.ToShortDateString() + "','" + BillType + "',1," + pid + "," + Total + ")";
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();

                    return true;

                }
                catch (Exception ex)
                {
                    LoginError = ex.ToString();
                    return false;
                }
            }
            else
                return false;
        }


        public int AddOrder()
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "INSERT INTO orderbook (date,completed) VALUES('" + DateTime.Today.Date.ToShortDateString() + "',0)";
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_cmd.ExecuteNonQuery();

                cmd = "SELECT MAX(id) FROM orderbook WHERE date='" + DateTime.Today.Date.ToShortDateString() + "'";
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                sqlite_datareader.Read();
                int order = Convert.ToInt32(sqlite_datareader[0]);
                sqlite_datareader.Close();
                return order;
            }
            return 0;
        }
        public void DeleteOrder(int orderno)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "DELETE FROM orderbook WHERE id=" + orderno;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_cmd.ExecuteNonQuery();
                cmd = "DELETE FROM orderitems WHERE orderno=" + orderno;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_cmd.ExecuteNonQuery();
            }
            

            
        }
        public bool SaveOrder(int Orderno, int Items, string rdate, string ddate, string partyname)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "UPDATE orderbook SET itemsno=" + Items + ",deldate='" + ddate + "', reminddate='" + rdate + "', partyname='" + partyname + "' WHERE id=" + Orderno;
                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return false;
        }
        public bool AddOrderItem(int Orderno, string name, int qty)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "INSERT INTO orderitems (name, qty, orderno) VALUES('" + name + "'," + qty + "," + Orderno + ")";
                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_cmd.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public string Orderstatus()
        {

            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM orderbook WHERE reminddate='" + DateTime.Today.Date.ToShortDateString() + "'";
                int rcount = 0, dcount = 0;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    rcount++;
                }
                sqlite_datareader.Close();

                cmd = "SELECT * FROM orderbook WHERE deldate='" + DateTime.Today.Date.ToShortDateString() + "'";
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    dcount++;
                }
                sqlite_datareader.Close();

                string status = "R :" + rcount.ToString() + " D :" + dcount.ToString();

                cmd = "UPDATE orderbook SET completed=1 WHERE deldate='" + DateTime.Today.Date.AddDays(-1).ToShortDateString() + "'";
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_cmd.ExecuteNonQuery();

                return status;
            }
            else
                return null;
        }
        public string[,] GetOrders()
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                int count = 0, cc = 1;
                string cmd = "SELECT * FROM orderbook WHERE completed=0 ORDER BY ID DESC";
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    count++;
                }
                sqlite_datareader.Close();
                count++;
                string[,] Orders = new string[6, count];
                Orders[0, 0] = count.ToString();
                cmd = "SELECT * FROM orderbook WHERE completed=0";
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    Orders[0, cc] = sqlite_datareader["date"].ToString();
                    Orders[1, cc] = sqlite_datareader["deldate"].ToString();
                    Orders[2, cc] = sqlite_datareader["reminddate"].ToString();
                    Orders[3, cc] = sqlite_datareader["itemsno"].ToString();
                    Orders[4, cc] = sqlite_datareader["partyname"].ToString();
                    Orders[5, cc] = sqlite_datareader["id"].ToString();
                    cc++;
                }
                sqlite_datareader.Close();

                return Orders;
            }
            else
                return null;
        }
        public string[,] GetOrderItems(int Orderid, int Items)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM orderitems WHERE orderno=" + Orderid;
                string[,] Detail = new string[2, Items];
                int cc = 0;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    Detail[0, cc] = sqlite_datareader["name"].ToString();
                    Detail[1, cc] = sqlite_datareader["qty"].ToString();
                    cc++;
                }
                sqlite_datareader.Close();

                return Detail;
            }
            else
                return null;
        }

        public int ChangePassword(string old, string pass)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM credential";
                int ret = 0;
                string oldpass = "";
                try
                {
                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_datareader = sqlite_cmd.ExecuteReader();
                    sqlite_datareader.Read();
                    oldpass = sqlite_datareader["pass"].ToString();
                    sqlite_datareader.Close();


                    if (old == oldpass)
                    {
                        cmd = "UPDATE credential SET pass='" + pass + "'";
                        try
                        {
                            sqlite_cmd = sqlite_conn.CreateCommand();
                            sqlite_cmd.CommandText = cmd;
                            sqlite_cmd.ExecuteNonQuery();
                            return 1;
                        }
                        catch
                        {
                            return -2;
                        }
                    }
                    else
                    {
                        return -1;
                    }


                }
                catch
                {
                    return 0;
                }
            }
            else
                return 0;

        }
        /*public int MakePdfBill(int billno, char BillType)
        {
            string cmd = "";
            if (BillType == 's' || BillType=='p')
            {

                cmd = "SELECT * FROM billinfo WHERE billno=" + billno + "AND sp='" + BillType + "'";

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader3 = sqlite_cmd.ExecuteReader();
                sqlite_datareader3.Read();

                if (Convert.ToInt32(sqlite_datareader3["billno"]) == 0)
                {
                    return 0;
                }

                System.IO.Directory.CreateDirectory("Bills/Sales/");
                System.IO.Directory.CreateDirectory("Bills/Purch/");

                PdfDocument bill;
                try
                {

                    bill = (BillType == 's') ? new PdfDocument("Bills/Sales/Billno" + billno + ".pdf") : new PdfDocument("Bills/Purch/Billno" + billno + ".pdf");
                }
                catch
                {
                    return -1;
                }
                PdfFont pdfont = new PdfFont(bill, "Microsoft Sans Serif", System.Drawing.FontStyle.Regular, true);

                PdfPage page = new PdfPage(bill);
                PdfContents cont = new PdfContents(page);

                PdfFileWriter.TextBox box = new PdfFileWriter.TextBox(325, 0.25);

                box.AddText(pdfont, 20, "Chauhan International");
                Double he = 750;
                cont.DrawText(240.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();

                box.AddText(pdfont, 12, "Mob :09463464033");
                he = 750;
                cont.DrawText(20.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();

                box.AddText(pdfont, 12, "Mob :09317754158");
                he = 750;
                cont.DrawText(480.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();

                box.AddText(pdfont, 12, "Mlk(Pb)");
                he = 730;
                cont.DrawText(300.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();



                box.AddText(pdfont, 12, "Date :" + sqlite_datareader3["date"]);
                he = 710;
                cont.DrawText(500.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();

                box.AddText(pdfont, 12, "Bill No. :" + billno.ToString());
                he = 690;
                cont.DrawText(500.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();


                string[] pinfo = PartyInfo(Convert.ToInt32(sqlite_datareader3["partyid"]));

                he = 710;
                box.AddText(pdfont, 15, "Name :  ");
                cont.DrawText(20.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();

                he = 710;
                box.AddText(pdfont, 12, pinfo[1]);
                cont.DrawText(120.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();

                he = 710;
                box.AddText(pdfont, 15, "TIN no. : ");
                cont.DrawText(300.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();

                he = 710;
                box.AddText(pdfont, 12, pinfo[3]);
                cont.DrawText(370.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();

                he = 690;
                box.AddText(pdfont, 15, "Address : ");
                cont.DrawText(20.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();

                he = 690;
                box.AddText(pdfont, 12, pinfo[4]);
                cont.DrawText(120.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();


                he = 600;
                box.AddText(pdfont, 12, "Sr No.");
                cont.DrawText(30.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                he = 600;
                box.AddText(pdfont, 12, "Item Name");
                cont.DrawText(100.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                he = 600;
                box.AddText(pdfont, 12, "Qty.");
                cont.DrawText(300.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                he = 600;
                box.AddText(pdfont, 12, "Price");
                cont.DrawText(380.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                he = 600;
                box.AddText(pdfont, 12, "Tot");
                cont.DrawText(460.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                cmd = "SELECT * FROM items WHERE billno=" + billno + "AND bo='"+BillType+"'";

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader2 = sqlite_cmd.ExecuteReader();
                int c = 0;
                Double h = he;
                string[] iinfo = new string[4];
                while (sqlite_datareader2.Read())
                {
                    c++;
                    h -= 20;
                    iinfo = ItemInfo(Convert.ToInt32(sqlite_datareader2["itemid"]));

                    he = h;
                    box.AddText(pdfont, 12, c.ToString());
                    cont.DrawText(30.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, iinfo[1]);
                    cont.DrawText(100.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, sqlite_datareader2["qty"] + " " + GetMeasurementScaleName(Convert.ToInt32(iinfo[2])));
                    cont.DrawText(300.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, "" + sqlite_datareader2["price"]);
                    cont.DrawText(380.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, "" + Convert.ToInt32(sqlite_datareader2["qty"]) * Convert.ToInt32(sqlite_datareader2["price"]));
                    cont.DrawText(460.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();


                }
                sqlite_datareader2.Close();

                
                h -= 35;


                he = h;
                box.AddText(pdfont, 12, "iTotal :");
                cont.DrawText(410.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                he = h - 10;

                box.AddText(pdfont, 12, "-------------------");
                cont.DrawText(440.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                he = h;

                box.AddText(pdfont, 12, sqlite_datareader3["itotal"].ToString());
                //cont.DrawText(465.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                cont.DrawText(pdfont, 12, 490, h - 15, TextJustify.Right, sqlite_datareader3["itotal"].ToString());
                box.Clear();

                h -= 20;
                he = h;
                box.AddText(pdfont, 12, "vat :");
                cont.DrawText(410.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                he = h;
                box.AddText(pdfont, 12, sqlite_datareader3["vat"].ToString());
                //cont.DrawText(465.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                cont.DrawText(pdfont, 12, 490, h-15, TextJustify.Right, sqlite_datareader3["vat"].ToString());
                box.Clear();
                h -= 20;
                he = h;
                box.AddText(pdfont, 12, "Postage :");
                cont.DrawText(410.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                he = h;
                box.AddText(pdfont, 12, sqlite_datareader3["postage"].ToString());
                //cont.DrawText(465.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                cont.DrawText(pdfont, 12, 490, h-15, TextJustify.Right, sqlite_datareader3["postage"].ToString());
                box.Clear();
                h -= 20;
                he = h;
                box.AddText(pdfont, 12, "Himali :");
                cont.DrawText(410.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                he = h;
                box.AddText(pdfont, 12, sqlite_datareader3["himali"].ToString());
                //cont.DrawText(465, ref he, 0.0, 0, 0.015, 0.05, TextJustify.Right, box);
                cont.DrawText(pdfont, 12, 490, h-15, TextJustify.Right, sqlite_datareader3["himali"].ToString());
		
                box.Clear();
                h -= 10;
                he = h;
                box.AddText(pdfont, 12, "-------------------");
                cont.DrawText(440.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                
                h -= 20;
                if (BillType == 's')
                {
                 

                    he = h;
                    box.AddText(pdfont, 12, "PM :");
                    cont.DrawText(50.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, sqlite_datareader3["pm"].ToString());
                    cont.DrawText(100.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, "GR :");
                    cont.DrawText(160.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, sqlite_datareader3["gr"].ToString());
                    cont.DrawText(210.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, "Through :");
                    cont.DrawText(260.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, sqlite_datareader3["through"].ToString());
                    cont.DrawText(310.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    
                }

                he = h;
                box.AddText(pdfont, 12, "Total :");
                cont.DrawText(410.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                he = h;
                box.AddText(pdfont, 12, sqlite_datareader3["total"].ToString());
                //cont.DrawText(465.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                cont.DrawText(pdfont, 12, 490, h - 15, TextJustify.Right, sqlite_datareader3["total"].ToString());
                box.Clear();
                
                
                he = 30;
                box.AddText(pdfont, 12, "Signature");
                cont.DrawText(510.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                box.Clear();
                
                


                bill.CreateFile();
                
                

                return 1;
            }
            else
                return 0;
        }*/
        public int MakePdfBill(int billno, char BillType)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "";
                if (BillType == 's' || BillType == 'p')
                {

                    cmd = "SELECT * FROM billinfo WHERE billno=" + billno + "AND sp='" + BillType + "'";

                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_datareader3 = sqlite_cmd.ExecuteReader();
                    sqlite_datareader3.Read();

                    if (Convert.ToInt32(sqlite_datareader3["billno"]) == 0)
                    {
                        return 0;
                    }

                    System.IO.Directory.CreateDirectory("Bills/Sales/");
                    System.IO.Directory.CreateDirectory("Bills/Purch/");

                    PdfDocument bill;
                    try
                    {

                        bill = (BillType == 's') ? new PdfDocument("Bills/Sales/Billno" + billno + ".pdf") : new PdfDocument("Bills/Purch/Billno" + billno + ".pdf");
                    }
                    catch
                    {
                        return -1;
                    }
                    PdfFont pdfont = new PdfFont(bill, "Microsoft Sans Serif", System.Drawing.FontStyle.Regular, true);

                    PdfPage page = new PdfPage(bill);
                    PdfContents cont = new PdfContents(page);

                    PdfFileWriter.TextBox box = new PdfFileWriter.TextBox(325, 0.25);

                    Double he = 750;
                    Double Y = 750;
                    int yint = 17;

                    cont.DrawText(pdfont, 20, 30, Y, TextJustify.Left, "Chauhan International");

                    Y -= 20;
                    cont.DrawText(pdfont, 15, 110, Y, TextJustify.Left, "Mlk(Pb)");

                    Y -= yint;
                    cont.DrawText(pdfont, 10, 245, Y, TextJustify.Right, "Mob :09463464033");
                    cont.DrawText(pdfont, 12, 10, Y, TextJustify.Left, "Date :" + sqlite_datareader3["date"]);
                    Y -= yint;
                    cont.DrawText(pdfont, 10, 245, Y, TextJustify.Right, "09317754158");




                    string[] pinfo = PartyInfo(Convert.ToInt32(sqlite_datareader3["partyid"]));
                    Y -= yint;
                    cont.DrawText(pdfont, 12, 10, Y, TextJustify.Left, "TIN No.");
                    cont.DrawText(pdfont, 10, 70, Y, TextJustify.Left, pinfo[3]);
                    cont.DrawText(pdfont, 12, 245, Y, TextJustify.Right, "Bill No. :" + billno.ToString());

                    Y -= yint;
                    cont.DrawText(pdfont, 12, 10, Y, TextJustify.Left, "Name :");
                    cont.DrawText(pdfont, 10, 70, Y, TextJustify.Left, pinfo[1]);
                    cont.DrawText(pdfont, 8, 245, Y, TextJustify.Right, "R No. :" + sqlite_datareader3["rno"].ToString());
                    Y -= yint;
                    cont.DrawText(pdfont, 12, 10, Y, TextJustify.Left, "Address :");
                    he = Y + 10;
                    box.AddText(pdfont, 10, pinfo[4]);
                    cont.DrawText(70.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();

                    Y -= (yint + 30);
                    cont.DrawText(pdfont, 10, 250, Y, TextJustify.Right, "---------------------------------------------------------------------------------");
                    Y -= 10;

                    cont.DrawText(pdfont, 10, 10, Y, TextJustify.Left, "Sr");
                    cont.DrawText(pdfont, 10, 25, Y, TextJustify.Left, "Item Name");
                    cont.DrawText(pdfont, 10, 120, Y, TextJustify.Left, "Qty");
                    cont.DrawText(pdfont, 10, 175, Y, TextJustify.Left, "Price");
                    cont.DrawText(pdfont, 10, 245, Y, TextJustify.Right, "Total");
                    Y -= 10;
                    cont.DrawText(pdfont, 10, 250, Y, TextJustify.Right, "---------------------------------------------------------------------------------");
                    Y -= 10;
                    Y += yint;


                    cmd = "SELECT * FROM items WHERE billno=" + billno + "AND bo='" + BillType + "'";

                    sqlite_cmd = sqlite_conn.CreateCommand();
                    sqlite_cmd.CommandText = cmd;
                    sqlite_datareader2 = sqlite_cmd.ExecuteReader();
                    int c = 0;
                    Double h = he;
                    string[] iinfo = new string[4];
                    while (sqlite_datareader2.Read())
                    {


                        c++;

                        Y -= yint;
                        iinfo = ItemInfo(Convert.ToInt32(sqlite_datareader2["itemid"]));



                        cont.DrawText(pdfont, 10, 10, Y, TextJustify.Left, c.ToString());
                        cont.DrawText(pdfont, 10, 25, Y, TextJustify.Left, iinfo[1]);
                        cont.DrawText(pdfont, 10, 120, Y, TextJustify.Left, sqlite_datareader2["qty"] + " " + GetMeasurementScaleName(Convert.ToInt32(iinfo[2])));
                        cont.DrawText(pdfont, 10, 175, Y, TextJustify.Left, sqlite_datareader2["price"].ToString());
                        cont.DrawText(pdfont, 10, 245, Y, TextJustify.Right, (Convert.ToInt32(sqlite_datareader2["qty"]) * Convert.ToInt32(sqlite_datareader2["price"])).ToString());


                    }
                    sqlite_datareader2.Close();
                    Y -= 10;

                    cont.DrawText(pdfont, 10, 250, Y, TextJustify.Right, "---------------------------------------------------------------------------------");
                    Y -= 10;
                    cont.DrawText(pdfont, 10, 150, Y, TextJustify.Left, "iTotal :");
                    cont.DrawText(pdfont, 10, 245, Y, TextJustify.Right, sqlite_datareader3["itotal"].ToString());
                    Y -= 10;
                    cont.DrawText(pdfont, 10, 250, Y, TextJustify.Right, "-------------");
                    Y -= 10;
                    cont.DrawText(pdfont, 10, 150, Y, TextJustify.Left, "Vat/CST :");
                    cont.DrawText(pdfont, 10, 245, Y, TextJustify.Right, sqlite_datareader3["vat"].ToString());

                    cont.DrawText(pdfont, 10, 10, Y, TextJustify.Left, "PM :");
                    cont.DrawText(pdfont, 10, 130, Y, TextJustify.Right, sqlite_datareader3["pm"].ToString() + "  |");
                    Y -= 10;
                    cont.DrawText(pdfont, 10, 130, Y, TextJustify.Right, "---------------------------------------------------------");


                    Y -= yint;
                    cont.DrawText(pdfont, 10, 150, Y, TextJustify.Left, "Himali :");
                    cont.DrawText(pdfont, 10, 245, Y, TextJustify.Right, sqlite_datareader3["himali"].ToString());

                    cont.DrawText(pdfont, 10, 10, Y, TextJustify.Left, "GR :");
                    cont.DrawText(pdfont, 10, 130, Y, TextJustify.Right, sqlite_datareader3["gr"].ToString() + "  |");

                    Y -= 10;
                    cont.DrawText(pdfont, 10, 130, Y, TextJustify.Right, "---------------------------------------------------------");

                    
                    Y -= yint;
                    cont.DrawText(pdfont, 10, 150, Y, TextJustify.Left, "Postage :");
                    cont.DrawText(pdfont, 10, 245, Y, TextJustify.Right, sqlite_datareader3["postage"].ToString());

                   

                    cont.DrawText(pdfont, 10, 10, Y, TextJustify.Left, "Through :");
                    cont.DrawText(pdfont, 10, 130, Y, TextJustify.Right, sqlite_datareader3["through"].ToString() + "  |");

                    Y -= 10;
                    cont.DrawText(pdfont, 10, 130, Y, TextJustify.Right, "---------------------------------------------------------");


                    Y -= 10;
                    cont.DrawText(pdfont, 10, 250, Y, TextJustify.Right, "-------------");
                    Y -= 10;
                    cont.DrawText(pdfont, 10, 150, Y, TextJustify.Left, "Total :");
                    cont.DrawText(pdfont, 10, 245, Y, TextJustify.Right, sqlite_datareader3["total"].ToString());
                    Y -= 10;
                    cont.DrawText(pdfont, 10, 250, Y, TextJustify.Right, "-------------");


                    Y -= yint;
                    cont.DrawText(pdfont, 10, 250, Y, TextJustify.Right, "---------------------------------------------------------------------------------");
                    //Y -= 10;
                    //cont.DrawText(pdfont, 10, 250, Y, TextJustify.Right, "------------------------------------------ END ----------------------------------");
                    Y -= 10;
                    cont.DrawText(pdfont, 10, 250, Y, TextJustify.Right, "---------------------------------------------------------------------------------");



                    /*
                     * h -= 35;


                    he = h;
                    box.AddText(pdfont, 12, "iTotal :");
                    cont.DrawText(410.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h - 10;

                    box.AddText(pdfont, 12, "-------------------");
                    cont.DrawText(440.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;

                    box.AddText(pdfont, 12, sqlite_datareader3["itotal"].ToString());
                    //cont.DrawText(465.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    cont.DrawText(pdfont, 12, 490, h - 15, TextJustify.Right, sqlite_datareader3["itotal"].ToString());
                    box.Clear();
                    */

                    /*
                    h -= 200;
                    he = h;
                    box.AddText(pdfont, 12, "vat :");
                    cont.DrawText(410.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, sqlite_datareader3["vat"].ToString());
                    //cont.DrawText(465.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    cont.DrawText(pdfont, 12, 490, h - 15, TextJustify.Right, sqlite_datareader3["vat"].ToString());
                    box.Clear();
                    h -= 20;
                    he = h;
                    box.AddText(pdfont, 12, "Postage :");
                    cont.DrawText(410.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, sqlite_datareader3["postage"].ToString());
                    //cont.DrawText(465.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    cont.DrawText(pdfont, 12, 490, h - 15, TextJustify.Right, sqlite_datareader3["postage"].ToString());
                    box.Clear();
                    h -= 20;
                    he = h;
                    box.AddText(pdfont, 12, "Himali :");
                    cont.DrawText(410.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, sqlite_datareader3["himali"].ToString());
                    //cont.DrawText(465, ref he, 0.0, 0, 0.015, 0.05, TextJustify.Right, box);
                    cont.DrawText(pdfont, 12, 490, h - 15, TextJustify.Right, sqlite_datareader3["himali"].ToString());

                    box.Clear();
                    h -= 10;
                    he = h;
                    box.AddText(pdfont, 12, "-------------------");
                    cont.DrawText(440.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    */
                    /*
                    h -= 20;
                    if (BillType == 's')
                    {


                        he = h;
                        box.AddText(pdfont, 12, "PM :");
                        cont.DrawText(50.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                        box.Clear();
                        he = h;
                        box.AddText(pdfont, 12, sqlite_datareader3["pm"].ToString());
                        cont.DrawText(100.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                        box.Clear();
                        he = h;
                        box.AddText(pdfont, 12, "GR :");
                        cont.DrawText(160.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                        box.Clear();
                        he = h;
                        box.AddText(pdfont, 12, sqlite_datareader3["gr"].ToString());
                        cont.DrawText(210.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                        box.Clear();
                        he = h;
                        box.AddText(pdfont, 12, "Through :");
                        cont.DrawText(260.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                        box.Clear();
                        he = h;
                        box.AddText(pdfont, 12, sqlite_datareader3["through"].ToString());
                        cont.DrawText(310.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                        box.Clear();

                    }

                    he = h;
                    box.AddText(pdfont, 12, "Total :");
                    cont.DrawText(410.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();
                    he = h;
                    box.AddText(pdfont, 12, sqlite_datareader3["total"].ToString());
                    //cont.DrawText(465.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    cont.DrawText(pdfont, 12, 490, h - 15, TextJustify.Right, sqlite_datareader3["total"].ToString());
                    box.Clear();


                    he = 30;
                    box.AddText(pdfont, 12, "Signature");
                    cont.DrawText(510.00, ref he, 0.0, 0, 0.015, 0.05, TextBoxJustify.FitToWidth, box);
                    box.Clear();

                    */


                    bill.CreateFile();



                    return 1;
                }
                else
                    return 0;
            }
            else
                return 0;
        }



        public int GetViewPages(char BillType)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM billinfo WHERE sp='" + BillType + "'";
                int Billcount = 0, Pages = 0;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                while (sqlite_datareader.Read())
                {
                    Billcount++;
                }
                sqlite_datareader.Close();
                Pages = Billcount / 10;
                Pages += (Billcount % 10 > 0) ? 1 : 0;

                return Pages;
            }
            else
                return 0;
        }
        public string[] GetBillInfo(int Billno, char BillType)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM billinfo WHERE billno=" + Billno + " AND sp='" + BillType + "'";
                string[] info = new string[5];

                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();
                sqlite_datareader.Read();
                if (Convert.ToInt32(sqlite_datareader["billno"]) == 0)
                {
                    //   billno does not exit
                    info[0] = "Billno does not exit";
                }
                else
                {
                    info[0] = sqlite_datareader["date"].ToString();
                    info[1] = sqlite_datareader["items"].ToString();
                    info[2] = sqlite_datareader["itotal"].ToString();
                    info[3] = sqlite_datareader["partyid"].ToString();
                    info[4] = sqlite_datareader["total"].ToString();
                }
                sqlite_datareader.Close();

                return info;
            }
            else
                return null;
        }
        public int[] GetBillnos(char BillType)
        {
            sqlite_conn.Close();
            if (!IfIsFirst())
            {

                string cmd = "SELECT * FROM billinfo WHERE sp='" + BillType + "' ORDER BY billno DESC";
                int[] billnos = new int[10];
                int a, b, c;
                sqlite_cmd = sqlite_conn.CreateCommand();
                sqlite_cmd.CommandText = cmd;
                sqlite_datareader = sqlite_cmd.ExecuteReader();

                a = 0;
                b = (ViewPageNo * 10) - 10;          // Bills to skip
                c = 0;

                while (sqlite_datareader.Read())
                {
                    a++;
                    if (b >= a)
                        continue;
                    if (c == 10)
                        break;
                    billnos[c] = Convert.ToInt32(sqlite_datareader["billno"]);

                    c++;
                }

                while (c < 10)
                {
                    billnos[c] = 0;
                    c++;
                }

                return billnos;

                sqlite_datareader.Close();
            }
            else
                return null;
            // //////////////////////////////////////////////////////////////
        }
    }
}
