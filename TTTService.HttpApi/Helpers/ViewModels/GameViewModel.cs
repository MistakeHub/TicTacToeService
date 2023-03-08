namespace TTTService.HttpApi.Helpers.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }
        public int Iduser1 { get; set; }

        public int Iduser2 { get; set; }

        public int? Idwinner { get; set; }

        public int Idcurrentturn { get; set; }

        public DateTime? Dategamestart { get; set; }

        public bool Isfinished { get; set; }
    }
}
