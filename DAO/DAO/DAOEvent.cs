using DAO.DBConnection;
using DAO.IDAO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TruckFlowDomain;

namespace DAO.DAO
{
    public class DAOEvent : IDAOEvent
    {
        MySqlConnection connection;
        MySqlCommand command;
        MySqlDataReader cursor;
        public DAOEvent(DBConnect db)
        {
            connection = db.Connection;
            command = new MySqlCommand();
        }
        public List<Event> GetLastEvents()
        {
            List<Event> eventlist = new();

            using (connection)
            {
                String req = "SELECT * FROM event ORDER BY id DESC LIMIT 20";

                command.Parameters.Clear();
                command.Connection = connection;
                command.CommandText = req;
                cursor = command.ExecuteReader();
                while (cursor.Read())
                {
                    Event e = new();
                    e.idevent = cursor.GetInt32(0);
                    e.mat = cursor.GetString(1);

                    e.dateevent = cursor.GetDateTime(2).Date;
                    e.heureevent = cursor.GetDateTime(3).ToLocalTime();
                    e.flux= cursor.GetString(4);
                    e.autorise = cursor.GetBoolean(5);
                    cursor.GetBytes(6, 0, e.photo, 0, e.photo.Length);
                    e.autorise = cursor.GetBoolean(5);

                    eventlist.Add(e);

                }
                cursor.Close();
            }
            return eventlist;
        }

        public void Insert(Event e)
        {
            using (connection)
            {
                command.Parameters.Clear();
                command.Connection = connection;
                String req = "insert into Event(mat,dateevent,heureevent,flux,autorise,photo,sync) " +
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
        }

        public void Update(Event e)
        {
            using (connection)
            {
                command.Parameters.Clear();
                command.Connection = connection;
                String req = "update Event set mat=@mat,dateevent=@dateevent,heureevent=@heureevent,flux=@flux,autorise=@autorise,photo=@photo,sync=@sync)" +
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
