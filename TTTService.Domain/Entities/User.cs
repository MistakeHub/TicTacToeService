using System;
using System.Collections.Generic;

namespace TTTService.Domain.Entities;

public partial class User:BaseEntity
{


    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? Totalgames { get; set; } = 0;

    public int? Totalwins { get; set; } = 0;

    public int? Totallosses { get; set; } = 0;

 

    public DateTime? Registerdate { get; set; } =DateTime.Now;

    public virtual ICollection<Game> GameIduser1Navigations { get; } = new List<Game>();

    public virtual ICollection<Game> GameIduser2Navigations { get; } = new List<Game>();

    public virtual ICollection<Move> Moves { get; } = new List<Move>();
}
