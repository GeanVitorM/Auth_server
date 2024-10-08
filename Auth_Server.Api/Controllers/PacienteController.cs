﻿using Authetication.Server.Api.DTOs;
using Authetication.Server.Api.Models;
using Authetication.Server.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Authetication.Server.Api.Middlewares;
using System.Security.Claims;

namespace Authetication.Server.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class PacienteController : ControllerBase
{
    private readonly ILogger<PacienteController> _logger;
    private readonly IPacienteService _service;
    private readonly IUsuarioService _usuarioService;
    private readonly RandomPassword _randomPassword;

    public PacienteController(ILogger<PacienteController> logger, IPacienteService service, IAuthService authService, IUsuarioService usuarioService, RandomPassword randomPassword)
    {
        _logger = logger;
        _service = service;
        _usuarioService = usuarioService;
        _randomPassword = randomPassword;
    }

    [HttpGet]
    [Authorize(Policy = "FisioterapeutaPolicy")]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> Get()
    {
        try
        {
            var pacienteDto = await _service.GetAllPacientes();
            if (pacienteDto == null)
            {
                return NotFound("Pacientes not found");
            }
            return Ok(pacienteDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching all users.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpGet("{id}", Name = "GetPaciente")]
    [Authorize(Policy = "FisioterapeutaPolicy")]
    public async Task<ActionResult<PacienteDto>> Get(int id)
    {
        try
        {
            var pacienteDto = await _service.GetPacienteById(id);
            if (pacienteDto == null)
            {
                return NotFound("Pacientes not found");
            }
            return Ok(pacienteDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while fetching the user with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<ActionResult> Post([FromBody] PacienteDto pacienteDto)
    {
        if (pacienteDto == null)
            return BadRequest("Dados inválidos");

        try
        {
            var novoUsuarioDto = new UsuarioDto
            {
                Username = pacienteDto.EmailPaciente,
                Password = pacienteDto.Password,
                TipoUsuario = TipoUsuario.Paciente
            };

            await _usuarioService.CreateUsuario(novoUsuarioDto);
            pacienteDto.IdPaciente = novoUsuarioDto.IdUser;
            await _service.CreatePaciente(pacienteDto);
            return Ok(new { Paciente = pacienteDto, Usuario = novoUsuarioDto });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ocorreu um erro ao criar o Paciente");
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno do servidor");
        }
    }

    [HttpPatch("{id}/PrimeiraConsulta")]
    [Authorize(Policy = "FisioterapeutaPolicy")]
    public async Task<ActionResult> UpdatePrimeiraConsulta(int id)
    {
        try
        {
            var pacienteDto = await _service.GetPacienteById(id);
            if (pacienteDto == null)
            {
                return NotFound("Paciente not found");
            }

            pacienteDto.PrimeiraConsulta = false;
            await _service.UpdatePrimeiraConsulta(pacienteDto);

            return Ok(pacienteDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while updating the 'PrimeiraConsulta' field for user with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpPut()]
    [Authorize(Policy = "PacientePolicy")]
    public async Task<ActionResult> Put([FromBody] PacienteDto pacienteDto)
    {
        if (pacienteDto == null)
            return BadRequest("Data invalid");

        try
        {
            await _service.UpdatePaciente(pacienteDto);
            return Ok(pacienteDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the user.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "AdminOrCoordenadorPolicy")]
    public async Task<ActionResult<PacienteDto>> Delete(int id)
    {
        try
        {
            var pacienteDto = await _service.GetPacienteById(id);
            var usuarioDto = await _usuarioService.GetUsuarioById(id);
            if (pacienteDto == null)
            {
                return NotFound("Paciente not found");
            }

            await _service.DeletePaciente(id);
            await _usuarioService.DeleteUsuario(id);
            return Ok(pacienteDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while deleting the user with ID {id}.");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
        }
    }
}

