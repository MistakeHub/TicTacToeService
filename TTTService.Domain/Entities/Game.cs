using System;
using System.Collections.Generic;

namespace TTTService.Domain.Entities;

public partial class Game:BaseEntity
{
   

    public int Iduser1 { get; set; }

    public int Iduser2 { get; set; }

    public int? Idwinner { get; set; } = -1;



    public int Idcurrentturn { get; set; }

    public DateTime? Dategamestart { get; set; } = DateTime.UtcNow;

    public bool Isfinished { get; set; } = false;

    public virtual User Iduser1Navigation { get; set; } = null!;

    public virtual User Iduser2Navigation { get; set; } = null!;

    public virtual ICollection<Move> Moves { get; } = new List<Move>();
}
