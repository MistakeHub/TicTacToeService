using System;
using System.Collections.Generic;

namespace TTTService.Domain.Entities;

public partial class Move:BaseEntity
{
  

    public int Iduser { get; set; }

    public int Row { get; set; }

    public int Column { get; set; }

    public int Idgame { get; set; }

    public virtual Game IdgameNavigation { get; set; } = null!;

    public virtual User IduserNavigation { get; set; } = null!;
}
