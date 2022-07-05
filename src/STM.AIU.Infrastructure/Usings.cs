#region Packages
global using Dapper;
global using MimeKit;
global using System.Text;
global using System.Data;
global using MimeKit.Text;
global using Mailjet.Client;
global using Microsoft.Data.SqlClient;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Options;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.AspNetCore.WebUtilities;
global using Mailjet.Client.TransactionalEmails;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authentication;
global using Mailjet.Client.TransactionalEmails.Response;
#endregion

#region Application Layer
global using STM.AIU.Application.Enums;
global using STM.AIU.Application.Models;
global using STM.AIU.Application.Constants;
global using STM.AIU.Application.DTOs.AccountDTOs;
global using STM.AIU.Application.Contracts.Identity;
global using STM.AIU.Application.Contracts.Persistence;
global using STM.AIU.Application.Contracts.Infrastructure;
#endregion

#region Infrastructure Layer
global using STM.AIU.Infrastructure.Files;
global using STM.AIU.Infrastructure.Persistence;
global using STM.AIU.Infrastructure.Identity.Helpers;
global using STM.AIU.Infrastructure.Identity.Services;
global using STM.AIU.Infrastructure.Services.Configurations;
global using STM.AIU.Infrastructure.Services.Communication.SMS;
global using STM.AIU.Infrastructure.Services.Communication.Email;
#endregion