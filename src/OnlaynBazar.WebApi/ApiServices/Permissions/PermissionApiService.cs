﻿using AutoMapper;
using OnlaynBazar.Domain.Entities.Users;
using OnlaynBazar.Service.Configurations;
using OnlaynBazar.Service.Services.Permissions;
using OnlaynBazar.WebApi.Extensions;
using OnlaynBazar.WebApi.Models.Permissions;
using OnlaynBazar.WebApi.Validators.Permissions;

namespace OnlaynBazar.WebApi.ApiServices.Permissions;

public class PermissionApiService(
    IMapper mapper,
    IPermissionService permissionService,
    PermissionCreateModelValidator createValidator,
    PermissionUpdateModelValidator updateValidator) : IPermissionApiService
{
    public async ValueTask<PermissionViewModel> PostAsync(PermissionCreateModel createModel)
    {
        await createValidator.EnsureValidatedAsync(createModel);
        var mappedPermission = mapper.Map<Permission>(createModel);
        var createdPermission = await permissionService.CreateAsync(mappedPermission);
        return mapper.Map<PermissionViewModel>(createdPermission);
    }

    public async ValueTask<PermissionViewModel> PutAsync(long id, PermissionUpdateModel updateModel)
    {
        await updateValidator.EnsureValidatedAsync(updateModel);
        var mappedPermission = mapper.Map<Permission>(updateModel);
        var updatedPermission = await permissionService.UpdateAsync(id, mappedPermission);
        return mapper.Map<PermissionViewModel>(updatedPermission);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await permissionService.DeleteAsync(id);
    }

    public async ValueTask<PermissionViewModel> GetAsync(long id)
    {
        var permission = await permissionService.GetByIdAsync(id);
        return mapper.Map<PermissionViewModel>(permission);
    }

    public async ValueTask<IEnumerable<PermissionViewModel>> GetAsync(PaginationParams @params, Filter filter, string search = null)
    {
        var permissions = await permissionService.GetAllAsync(@params, filter, search);
        return mapper.Map<IEnumerable<PermissionViewModel>>(permissions);
    }
}