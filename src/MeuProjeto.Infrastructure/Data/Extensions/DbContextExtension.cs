using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Infrastructure.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MeuProjeto.Infrastructure.Data.Extensions
{
    public static class DbContextExtension
    {
        private const string SeedPath = "..{0}MeuProjeto.Infrastructure{0}Data{0}Seed{0}";

        public static async Task<bool> AllMigrationsApplied(this DbContext context)
        {
            var applied = (await context.GetService<IHistoryRepository>()
                .GetAppliedMigrationsAsync()).Select(m => m.MigrationId);

            var total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static async Task EnsureSeeded(this MeuDbContext context, IServiceScope scope)
        {
            // InsertPermissionsRoleClaims trigger's require's a default UserProfile
            await SeedDefaultUserProfile(context, scope.ServiceProvider.GetRequiredService<IUserProfileService>());
            await SeedTriggers(context);
            await SeedPermissions(context);
            // Users and Users Profiles require's Permission's and Area's to be created in database 
            await context.SaveChangesAsync();

            await SeedUserProfiles(context, scope.ServiceProvider.GetRequiredService<IUserProfileService>());
            await SeedUsers(context, scope.ServiceProvider.GetRequiredService<IUserService>());
            await SeedMany<UserManual>(context);
            await SeedMany<Settings>(context);
            await context.SaveChangesAsync();
        }
        private static async Task SeedTriggers(MeuDbContext context)
        {
            var directory = String.Format(SeedPath + "Triggers", Path.DirectorySeparatorChar);
            foreach (var path in Directory.GetFiles(directory, "*.sql"))
            {
                var sql = await File.ReadAllTextAsync(path);
                var emptyparams = new SqlParameter[] { };
                await context.Database.ExecuteSqlRawAsync(sql, emptyparams);
            }
        }

        private static async Task SeedMany<TEntity>(MeuDbContext context) where TEntity : Entity
        {
            var db = context.Set<TEntity>();
            var path = SeedPath + typeof(TEntity).Name + ".json";
            path = String.Format(path, Path.DirectorySeparatorChar);

            var items = await ReadFromJson<TEntity>(path);
            items = items.Where(u => !db.AsNoTracking().Any(m => m.Id == u.Id)).ToList();

            if (items.Count > 0)
            {
                await db.AddRangeAsync(items);
            }
        }

        private static async Task SeedUsers(MeuDbContext context, IUserService userService)
        {
            var path = String.Format(SeedPath + "User.json", Path.DirectorySeparatorChar);

            var items = await ReadFromJson<DefaultUser>(path);
            items = items.Where(u => !context.Users.AsNoTracking().Any(m => m.Id == u.Id)).ToList();

            if (items != null && items.Count > 0)
            {
                await userService.CreateMany(items);
            }
        }
        private static async Task SeedUserProfiles(MeuDbContext context, IUserProfileService service)
        {
            var path = String.Format(SeedPath + "UserProfiles.json", Path.DirectorySeparatorChar);

            var items = await ReadFromJson<UserProfile>(path);
            items = items.Where(u => !context.Users.AsNoTracking().Any(m => m.Id == u.Id)).ToList();

            foreach (var item in items)
            {
                await service.Create(item);
            }
        }
        private static async Task SeedPermissions(MeuDbContext context)
        {
            var directory = String.Format(SeedPath + "Permissions", Path.DirectorySeparatorChar);
            foreach (var path in Directory.GetFiles(directory, "*.json"))
            {
                var items = await ReadFromJson<Permission>(path);
                items = items.Where(u => !context.Permissions.AsNoTracking().Any(m => m.Id == u.Id || (m.Type == u.Type && m.Value == u.Value))).ToList();

                if (items.Count > 0)
                {
                    await context.AddRangeAsync(items);
                }
            }
        }
        private static async Task SeedDefaultUserProfile(MeuDbContext context, IUserProfileService service)
        {
            var defaultUserProfile = new UserProfile() { Id = new Guid("95eac1f2-a193-4c42-8de3-7a1c9b18abea"), Name = "Desenvolvedores", Active = "True", Admin = true };
            if (await service.GetByIdAD(defaultUserProfile.Id) == null)
            {
                await service.Create(defaultUserProfile);
            }
        }
        private static async Task<List<TEntity>> ReadFromJson<TEntity>(string path)
        {
            return JsonConvert.DeserializeObject<List<TEntity>>(await File.ReadAllTextAsync(path));
        }

        public static string RemoveAllMediaNodes(this string html)
        {
            try
            {
                var document = new HtmlAgilityPack.HtmlDocument();
                document.LoadHtml(html);

                if (document.DocumentNode.InnerHtml.Contains("<img"))
                {
                    foreach (var eachNode in document.DocumentNode.SelectNodes("//img"))
                    {
                        eachNode.Remove();
                    }
                }

                if (document.DocumentNode.InnerHtml.Contains("<iframe"))
                {
                    foreach (var eachNode in document.DocumentNode.SelectNodes("//iframe"))
                    {
                        eachNode.Remove();
                    }
                }

                if (document.DocumentNode.InnerHtml.Contains("<video"))
                {
                    foreach (var eachNode in document.DocumentNode.SelectNodes("//video"))
                    {
                        eachNode.Remove();
                    }
                }

                html = document.DocumentNode.OuterHtml;
                return html;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
