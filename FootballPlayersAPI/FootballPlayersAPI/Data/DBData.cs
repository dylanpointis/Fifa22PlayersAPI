using FootballPlayersAPI.Models;
using Microsoft.Data.SqlClient;

namespace FootballPlayersAPI.Data
{
    public class DBData
    {

        SqlConnection con = new SqlConnection(Connection.connectionString);

        public void InsertPlayers(List<FootballPlayer> playersList)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("InsertPlayer", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            foreach(FootballPlayer player in playersList)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.Add(new SqlParameter("@ShortName", player.ShortName));
                cmd.Parameters.Add(new SqlParameter("@LongName", player.LongName));
                cmd.Parameters.Add(new SqlParameter("@PlayerPositions", player.PlayerPositions));
                cmd.Parameters.Add(new SqlParameter("@Overall", player.Overall));
                cmd.Parameters.Add(new SqlParameter("@Age", player.Age));
                cmd.Parameters.Add(new SqlParameter("@BirthDate", player.BirthDate));
                cmd.Parameters.Add(new SqlParameter("@HeightCm", player.HeightCm));
                cmd.Parameters.Add(new SqlParameter("@WeightKg", player.WeightKg));
                cmd.Parameters.Add(new SqlParameter("@ClubName", player.ClubName));
                cmd.Parameters.Add(new SqlParameter("@LeagueName", player.LeagueName));
                cmd.Parameters.Add(new SqlParameter("@NationalityName", player.NationalityName));
                cmd.Parameters.Add(new SqlParameter("@PreferredFoot", player.PreferredFoot));
                cmd.Parameters.Add(new SqlParameter("@WeakFoot", player.WeakFoot));
                cmd.Parameters.Add(new SqlParameter("@SkillMoves", player.SkillMoves));
                cmd.Parameters.Add(new SqlParameter("@Pace", player.Pace));
                cmd.Parameters.Add(new SqlParameter("@Shooting", player.Shooting));
                cmd.Parameters.Add(new SqlParameter("@Passing", player.Passing));
                cmd.Parameters.Add(new SqlParameter("@Dribbling", player.Dribbling));
                cmd.Parameters.Add(new SqlParameter("@Defending", player.Defending));
                cmd.Parameters.Add(new SqlParameter("@Physic", player.Physic));
                cmd.Parameters.Add(new SqlParameter("@PlayerFaceUrl", player.PlayerFaceUrl));
                cmd.Parameters.Add(new SqlParameter("@ClubLogoUrl", player.ClubLogoUrl));
                cmd.Parameters.Add(new SqlParameter("@NationFlagUrl", player.NationFlagUrl));


                cmd.ExecuteNonQuery();
            }


            con.Close();
        }

        public List<FootballPlayer> GetAllPlayers()
        {

            List<FootballPlayer> list = new List<FootballPlayer>();

            con.Open();
            SqlCommand command = new SqlCommand("GetAllPlayers", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(SetValues(reader));
                }
            }
            con.Close();
            return list;
        }

        public List<FootballPlayer> GetPlayersByPage(int pageNumber)
        {

            List<FootballPlayer> list = new List<FootballPlayer>();

            con.Open();
            SqlCommand command = new SqlCommand("GetPlayersByPage", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@PageNumber", pageNumber));

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    list.Add(SetValues(reader));
                }
            }
            con.Close();
            return list;
        }

        public FootballPlayer GetPlayer(int playerID)
        {
            FootballPlayer player = new FootballPlayer();

            con.Open();
            SqlCommand command = new SqlCommand("GetPlayer", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@PlayerID", playerID));

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    player = SetValues(reader);
                }
            }
            con.Close();
            return player;
        }

        public List<FootballPlayer> FilterByName(string name)
        {

            List<FootballPlayer> list = new List<FootballPlayer>();

            con.Open();
            SqlCommand command = new SqlCommand("FilterByName", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Name", name));

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(SetValues(reader));
                }
            }
            con.Close();
            return list;
        }

        public List<FootballPlayer> FilterByNationality(string name)
        {
            List<FootballPlayer> list = new List<FootballPlayer>();

            con.Open();
            SqlCommand command = new SqlCommand("FilterByNationality", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Nationality", name));

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(SetValues(reader));
                }
            }
            con.Close();
            return list;
        }


        private FootballPlayer SetValues(SqlDataReader reader)
        {
            return new FootballPlayer()
            {
                PlayerID = Convert.ToInt32(reader["PlayerID"]),
                ShortName = reader["ShortName"].ToString(),
                LongName = reader["LongName"].ToString(),
                PlayerPositions = reader["PlayerPositions"].ToString(),
                Overall = Convert.ToInt32(reader["Overall"]),
                Age = Convert.ToInt32(reader["Age"]),
                BirthDate = Convert.ToDateTime(reader["BirthDate"]),
                HeightCm = Convert.ToInt32(reader["HeightCm"]),
                WeightKg = Convert.ToInt32(reader["WeightKg"]),
                ClubName = reader["ClubName"].ToString(),
                LeagueName = reader["LeagueName"].ToString(),
                NationalityName = reader["NationalityName"].ToString(),
                PreferredFoot = reader["PreferredFoot"].ToString(),
                WeakFoot = Convert.ToInt32(reader["WeakFoot"]),
                SkillMoves = Convert.ToInt32(reader["SkillMoves"]),
                Pace = Convert.ToInt32(reader["Pace"]),
                Shooting = Convert.ToInt32(reader["Shooting"]),
                Passing = Convert.ToInt32(reader["Passing"]),
                Dribbling = Convert.ToInt32(reader["Dribbling"]),
                Defending = Convert.ToInt32(reader["Defending"]),
                Physic = Convert.ToInt32(reader["Physic"]),
                PlayerFaceUrl = reader["PlayerFaceUrl"].ToString(),
                ClubLogoUrl = reader["ClubLogoUrl"].ToString(),
                NationFlagUrl = reader["NationFlagUrl"].ToString()
            };
        }

        public List<FootballPlayer> FilterByOverall(int overall)
        {
            List<FootballPlayer> list = new List<FootballPlayer>();

            con.Open();
            SqlCommand command = new SqlCommand("FilterByOverall", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Overall", overall));

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(SetValues(reader));
                }
            }
            con.Close();
            return list;
        }

        public List<FootballPlayer> FilterByClub(string club)
        {
            List<FootballPlayer> list = new List<FootballPlayer>();

            con.Open();
            SqlCommand command = new SqlCommand("FilterByClub", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.Add(new SqlParameter("@Club", club));

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(SetValues(reader));
                }
            }
            con.Close();
            return list;
        }



        public void ResetIdentity()
        {
            con.Open();
            SqlCommand command = new SqlCommand("ResetIdentity", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.ExecuteNonQuery();
            con.Close();
        }

        
    }
}
