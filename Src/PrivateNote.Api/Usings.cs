﻿global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics;
global using System.Text;
global using System.Text.Json;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.OpenApi.Models;
global using PrivateNote.Service;
global using PrivateNote.Model;
global using PrivateNote.Service.Contract;
global using PrivateNote.Api.Dto;
global using PrivateNote.Api.Dto.Requests;
global using PrivateNote.Api.Dto.Responses;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
