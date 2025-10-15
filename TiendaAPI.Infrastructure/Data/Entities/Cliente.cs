﻿using System;
using System.Collections.Generic;

namespace TiendaAPI.Infrastructure.Data.Entities;

public partial class Cliente
{
    public int Clienteid { get; set; }

    public string Nombre { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public DateTime? Fechacreacion { get; set; }

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
