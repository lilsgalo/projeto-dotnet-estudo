using System.Collections.Generic;
using System.Threading.Tasks;
using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;

namespace MeuProjeto.Business.Services
{
    public class PermissionService : BaseService<Permission>, IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUser _user;

        public PermissionService(IPermissionRepository permissionRepository,
                              INotifier notificador,
                              IUser user) : base(notificador, permissionRepository)
        {
            _permissionRepository = permissionRepository;
            _user = user;
        }

        public async Task<List<Permission>> GetAll()
        {
            return await _permissionRepository.GetAll();
        }

        public override void Dispose()
        {
            _permissionRepository?.Dispose();
        }
    }
}