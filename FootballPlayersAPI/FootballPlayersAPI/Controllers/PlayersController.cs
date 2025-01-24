using FootballPlayersAPI.Data;
using FootballPlayersAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FootballPlayersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly DBData _dbData;
        public PlayersController(DBData dbData)
        {
            _dbData = dbData;
        }


        [HttpGet]

        public IActionResult getAllPlayers()
        {
            List<FootballPlayer> playersList = _dbData.GetAllPlayers();
            if(playersList.Count < 1)
            {
                ExecuteETL();
            }
           
            return StatusCode(StatusCodes.Status200OK, playersList);
        }

        [HttpGet("page/{pageNumber}")]

        public IActionResult getPlayersByPage(int pageNumber)
        {
            List<FootballPlayer> playersList = _dbData.GetPlayersByPage(pageNumber);
            if (playersList.Count < 1)
            {
                ExecuteETL();
            }

            return StatusCode(StatusCodes.Status200OK, playersList);
        }

        [HttpGet("{playerID}")]

        public IActionResult GetPlayer(int playerID)
        {
            FootballPlayer player = _dbData.GetPlayer(playerID);
            if (player == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, null);
            }

            return StatusCode(StatusCodes.Status200OK, player);
        }




        [HttpGet("name/{name}")]

        public IActionResult filterByName(string name)
        {
            List<FootballPlayer> list = _dbData.FilterByName(name);
            return StatusCode(StatusCodes.Status200OK, list);
        }


        [HttpGet("nation/{nation}")]

        public IActionResult FilterByNationality(string nation)
        {
            List<FootballPlayer> list = _dbData.FilterByNationality(nation);
            return StatusCode(StatusCodes.Status200OK, list);
        }



        [HttpGet("overall/{overall}")]

        public IActionResult FilterByOverall(int overall)
        {
            List<FootballPlayer> list = _dbData.FilterByOverall(overall);
            return StatusCode(StatusCodes.Status200OK, list);
        }

        [HttpGet("club/{club}")]

        public IActionResult FilterByClub(string club)
        {
            List<FootballPlayer> list = _dbData.FilterByClub(club);
            return StatusCode(StatusCodes.Status200OK, list);
        }




        private void ExecuteETL()
        {
            var fileName = Path.Combine("Data\\players_22.csv");
            List<FootballPlayer> playersList = new List<FootballPlayer>();
            StreamReader reader = new StreamReader(fileName);

            //Lee los encabezados
            string line = reader.ReadLine();

            while (line != null)
            {
                line = reader.ReadLine();
                FootballPlayer player = new FootballPlayer();
                
                //hay que hacer un metodo para el split ya que el campo PlayerPositions tiene comas "RW, ST, CF"
                string[] data = SplitCSV(line);
               

                player.ShortName = data[2];
                player.LongName = data[3];
                player.PlayerPositions = data[4];
                player.Overall = Convert.ToInt32(data[5]);
                player.Age = Convert.ToInt32(data[9]);
                player.BirthDate = DateTime.Parse(data[10]);
                player.HeightCm = Convert.ToInt32(data[11]);
                player.WeightKg = Convert.ToInt32(data[12]);
                player.ClubName = data[14];
                player.LeagueName = data[15];
                player.NationalityName = data[23];
                player.PreferredFoot = data[27];
                player.WeakFoot = Convert.ToInt32(data[28]);
                player.SkillMoves = Convert.ToInt32(data[29]);
                player.Pace = Convert.ToInt32(data[54]);
                player.Shooting = Convert.ToInt32(data[55]);
                player.Passing = Convert.ToInt32(data[56]);
                player.Dribbling = Convert.ToInt32(data[57]);
                player.Defending = Convert.ToInt32(data[58]);
                player.Physic = Convert.ToInt32(data[59]);
                player.PlayerFaceUrl = data[105];
                player.ClubLogoUrl = data[106];
                player.NationFlagUrl = data[109];

                playersList.Add(player);

                if (playersList.Count >= 17000)
                    break;
            }

            reader.Close();

            _dbData.ResetIdentity();
            _dbData.InsertPlayers(playersList);
        }



        private string[] SplitCSV(string line)
        {
            List<string> campos = new List<string>();
            string campoActual = "";
            bool dentroDeComillas = false;

            if (line != null)
            {
                foreach (char c in line)
                {
                    if (c == ',' && !dentroDeComillas) //Si encontramos una coma (cambia al sig campo) y no estamos dentro de comillas
                    {
                        campos.Add(campoActual); //añadimos el campo
                        campoActual = "";
                    }
                    else if (c == '"') //Si encontramos comillas
                    {
                        dentroDeComillas = !dentroDeComillas;
                        //esto es asi porque hay comilla de apertura (pone en true) y comilla de cierre (pone en false)
                    }
                    else
                    {
                        campoActual += c; //Añadimos el caracter al campo actual
                    }
                }

                //añadir el ultimo campo de la linea
                campos.Add(campoActual);
            }
            return campos.ToArray();
        }
    }
}
