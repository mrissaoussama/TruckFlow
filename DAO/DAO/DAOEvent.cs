using DAO.IDAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckFlowDomain;

namespace DAOMySql.DAO
{
    public class DAOEvent : IDAOEvent
    {
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader cursor;
        public DAOEvent(DatabaseConnection db)
        {
            connection = db.getConnection();
            command = new MySqlCommand();
            command.Connection = connection;
        }
        public List<Event> GetLastEvents()
        {
            List<Event> eventlist = new();

            try
            {
                if (command.Connection.State == ConnectionState.Closed)
                    command.Connection.Open();



                String req = "SELECT * FROM event ORDER BY idevent DESC LIMIT 20";

                command.Parameters.Clear();
                command.CommandText = req;
                cursor = command.ExecuteReader();
                while (cursor.Read())
                {
                    Event e = new();
                    e.idevent = cursor.GetInt32(0);
                    e.mat = cursor.GetString(1);

                    e.dateevent = cursor.GetDateTime(2).Date;
                    e.heureevent = cursor.GetTimeSpan(3);
                    e.flux = cursor.GetString(4);
                    e.autorise = cursor.GetBoolean(5);
                    e.photo = (byte[])(cursor["photo"]);
                    e.autorise = cursor.GetBoolean(5);

                    eventlist.Add(e);

                }
                cursor.Close();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("error retrieving events: " + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error retrieving events: " + ex);
            }
            finally
            {
              connection.Close();

            }

            return SyncEvents(eventlist);

        }
        public List<Event> SyncEvents(List<Event> eventlist)
        {if (eventlist != null || eventlist.Count != 0)
            {
                try
                {
                    if (command.Connection.State == ConnectionState.Closed)
                        command.Connection.Open();


                    command.Parameters.Clear();
                    String req = "update event set sync=true where idevent in ("
                   + string.Join(",", eventlist)
                   + ")";
                    command.CommandText = req;
                    command.ExecuteNonQuery();
                    eventlist.ForEach(x => x.autorise = true);
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("error retrieving events: " + ex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("error retrieving events: " + ex);
                }
                finally
                {
                    // connection.Close();
                }
            }
            return eventlist;
        }
    
        public void Insert(Event e)
        {
            try
           { 
                if (command.Connection.State== ConnectionState.Closed)
                    command.Connection.Open();
                command.Parameters.Clear();
                String req = "insert into event(mat,dateevent,heureevent,flux,autorise,photo,sync) " +
                    "        values (@mat,@dateevent,@heureevent,@flux,@autorise,@photo,@sync)";
                command.Parameters.AddWithValue("@mat", e.mat);
                command.Parameters.AddWithValue("@dateevent", e.dateevent.Date);
                command.Parameters.AddWithValue("@heureevent", e.heureevent);
                command.Parameters.AddWithValue("@flux", e.flux);
                command.Parameters.AddWithValue("@autorise", e.autorise);
                command.Parameters.AddWithValue("@photo", e.photo);
                command.Parameters.AddWithValue("@sync", e.sync);
                command.CommandText = req;
                command.ExecuteNonQuery();
            }
            catch(MySqlException ex)
            {
                Console.WriteLine("insertion error: " + ex);
            }
            catch(InvalidOperationException exp)
            {
                Console.WriteLine("insertion error: " + exp);


            }
            finally {     
               connection.Close();
}
        }

        public void Update(Event e)
        {
            using (connection)
            {
                command.Parameters.Clear();
                String req = "update event set mat=@mat,dateevent=@dateevent,heureevent=@heureevent,flux=@flux,autorise=@autorise,photo=@photo,sync=@sync)" +
                    " where idevent=@idevent " ;
                command.Parameters.AddWithValue("@idevent", e.idevent);
                command.Parameters.AddWithValue("@mat", e.mat);
                command.Parameters.AddWithValue("@dateevent", e.dateevent);
                command.Parameters.AddWithValue("@heureevent", e.heureevent);
                command.Parameters.AddWithValue("@flux", e.flux);
                command.Parameters.AddWithValue("@autorise", e.autorise);
                command.Parameters.AddWithValue("@photo", e.photo);
                command.Parameters.AddWithValue("@sync", e.sync);
                command.CommandText = req;
                command.ExecuteNonQuery();
            }
        }
    }
    
}
