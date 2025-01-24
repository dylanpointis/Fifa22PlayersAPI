namespace FootballPlayersAPI.Models
{
    public class FootballPlayer
    {
        public int PlayerID { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public string PlayerPositions { get; set; }
        public int Overall { get; set; }
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public int HeightCm { get; set; }
        public int WeightKg { get; set; }
        public string ClubName { get; set; }
        public string LeagueName { get; set; }
        public string NationalityName { get; set; }
        public string PreferredFoot { get; set; }
        public int WeakFoot { get; set; }
        public int SkillMoves { get; set; }
        public int Pace { get; set; }
        public int Shooting { get; set; }
        public int Passing { get; set; }
        public int Dribbling { get; set; }
        public int Defending { get; set; }
        public int Physic { get; set; }
        public string PlayerFaceUrl { get; set; }
        public string ClubLogoUrl { get; set; }
        public string NationFlagUrl { get; set; }
    }
}
