﻿using Authetication.Server.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Authetication.Server.Api.DTOs;

public class UsuarioDto
{
    [Key]
    public int IdUser { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "The Email field is not a valid e-mail address.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Type is required")]
    public TipoUsuario TipoUsuario { get; set; }

    public AdminDto? Admin { get; set; }
    public PacienteDto? Paciente { get; set; }
    public FisioterapeutaDto? Fisioterapeuta { get; set; }
    public CoordenadorDto? Coordenador { get; set; }

    public string? TOTPSecret { get; set; }
}
