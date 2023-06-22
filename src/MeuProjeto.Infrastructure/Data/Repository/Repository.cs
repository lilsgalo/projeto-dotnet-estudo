using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using MeuProjeto.Infrastructure.Data.Extensions;
using MeuProjeto.Business.Enums;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace MeuProjeto.Infrastructure.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext Db;
        protected readonly DbSet<TEntity> DbSet;
        protected readonly ICustomLogger Logger;

        protected Repository(MeuDbContext db, ICustomLogger logger)
        {
            Db = db;
            Logger = logger;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> GetById(Guid id, List<string> stringIncludes = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var includedData = DbSet.AsNoTracking().AsQueryable();
            foreach (var include in stringIncludes ?? new List<string>())
            {
                includedData = includedData.Include(include);
            }
            foreach (var include in includes)
            {
                includedData = includedData.Include(include);
            }
            return await includedData.FirstOrDefaultAsync(p => p.Id == id);
        }

        public virtual async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IPagedList<TEntity>> GetPagedList(Expression<Func<TEntity, bool>> predicate, PagedListParameters parameters, List<string> stringIncludes = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var includedData = DbSet.AsQueryable();
            foreach (var include in stringIncludes ?? new List<string>())
            {
                includedData = includedData.Include(include);
            }
            foreach (var include in includes)
            {
                includedData = includedData.Include(include);
            }
            var dadosFiltrados = includedData.Where(predicate);

            dadosFiltrados = dadosFiltrados.OrderBy(parameters.Sort);

            return await PagedList<TEntity>.ToPagedList(dadosFiltrados, parameters.CurrentPage, parameters.PageSize);
        }

        public virtual async Task Create(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Update(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        public virtual async Task Delete(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            SaveTransactionLogs();

            return await Db.SaveChangesAsync();
        }

        private void SaveTransactionLogs()
        {
            var dataDB = new Dictionary<string, object>();
            var dataCurrent = new Dictionary<string, object>();

            var type = LogTypeEnum.Update;

            var trackedEntity = Db.ChangeTracker.Entries<TEntity>().FirstOrDefault();

            if (trackedEntity.Entity.GetType().Name == nameof(RefreshToken)) return;

            switch (trackedEntity.State)
            {
                case EntityState.Added:
                    dataCurrent = CurrentTables(trackedEntity);
                    type = LogTypeEnum.Add;
                    break;

                case EntityState.Modified:
                    bool teste;
                    if (trackedEntity.CurrentValues.TryGetValue("Deleted", out teste) == false || teste == false)
                    {
                        dataDB = DatabaseTables(trackedEntity);
                        dataCurrent = CurrentTables(trackedEntity);
                    }
                    else
                    {
                        dataDB = DatabaseTables(trackedEntity);
                        type = LogTypeEnum.Delete;
                    }
                    break;

                case EntityState.Deleted:
                    dataDB = DatabaseTables(trackedEntity);
                    type = LogTypeEnum.Delete;
                    break;
            }
            Logger.RegistrationRecord<TEntity>(type, dataDB, dataCurrent);
        }

        private Dictionary<string, object> DatabaseTables(EntityEntry trackedEntity)
        {
            var dataBaseSimpleProperty = trackedEntity.GetDatabaseValues();
            var dataDB = dataBaseSimpleProperty.Properties.ToDictionary(p => p.Name, p => dataBaseSimpleProperty[p]);

            foreach (var reference in trackedEntity.References)
            {
                if (reference.TargetEntry == null) continue;

                var dataBaseReferenceProperty = reference.TargetEntry.GetDatabaseValues();
                dataDB.Add(
                    reference.Metadata.Name,
                    dataBaseReferenceProperty.Properties.ToDictionary(
                        p => p.Name,
                        p => dataBaseReferenceProperty.EntityType.Name.Contains("Text")
                            ? dataBaseReferenceProperty.GetValue<string>("Value").RemoveAllMediaNodes()
                            : dataBaseReferenceProperty[p]
                    )
                );
            }

            var candidateTables = Db.ChangeTracker.Entries().Where(p => p.State == EntityState.Deleted).ToList();
            candidateTables.AddRange(Db.ChangeTracker.Entries().Where(p => p.State == EntityState.Modified));
            foreach (var collection in trackedEntity.Collections)
            {
                if (!collection.IsModified) continue;

                var filteredDeletedCollections = candidateTables.Where(p => p.Metadata.Name == collection.Metadata.DeclaringEntityType.Name);

                var dataBaseCollectionProperty = new List<Dictionary<string, object>>();
                foreach (var filteredDeletedCollection in filteredDeletedCollections)
                {
                    var properties = filteredDeletedCollection.GetDatabaseValues();
                    dataBaseCollectionProperty.Add(properties.Properties.ToDictionary(p => p.Name, p => properties[p]));
                }

                dataDB.Add(collection.Metadata.Name, dataBaseCollectionProperty);
            }

            return dataDB;
        }

        private Dictionary<string, object> CurrentTables(EntityEntry trackedEntity)
        {
            var SimpleProperty = trackedEntity.CurrentValues;
            var dataCurrent = SimpleProperty.Properties.ToDictionary(p => p.Name, p => SimpleProperty[p]);

            foreach (var reference in trackedEntity.References)
            {
                if (reference.TargetEntry == null) continue;

                var referenceProperty = reference.TargetEntry.CurrentValues;
                dataCurrent.Add(
                    reference.Metadata.Name,
                    referenceProperty.Properties.ToDictionary(
                        p => p.Name,
                        p => referenceProperty.EntityType.Name.Contains("Text")
                            ? referenceProperty.GetValue<string>("Value").RemoveAllMediaNodes()
                            : referenceProperty[p]
                    )
                );
            }

            var addedCollections = Db.ChangeTracker.Entries().Where(p => p.State == EntityState.Added).ToList();
            addedCollections.AddRange(Db.ChangeTracker.Entries().Where(p => p.State == EntityState.Modified));
            foreach (var collection in trackedEntity.Collections)
            {
                if (!collection.IsModified) continue;

                var filteredAddedCollections = addedCollections.Where(p => p.Metadata.Name == collection.Metadata.DeclaringEntityType.Name);

                var currentCollectionProperty = new List<Dictionary<string, object>>();
                foreach (var filteredAddedCollection in filteredAddedCollections)
                {
                    var properties = filteredAddedCollection.CurrentValues;
                    currentCollectionProperty.Add(properties.Properties.ToDictionary(p => p.Name, p => properties[p]));
                }

                dataCurrent.Add(collection.Metadata.Name, currentCollectionProperty);
            }

            return dataCurrent;
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}