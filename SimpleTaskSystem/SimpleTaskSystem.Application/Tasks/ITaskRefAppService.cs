﻿using Abp.Application.Services;
using SimpleTaskSystem.Tasks.Dtos;
using System.Threading.Tasks;

namespace SimpleTaskSystem.Tasks
{
    /// <summary>
    /// Defines an application service for <see cref="Task"/> operations.
    /// 
    /// It extends <see cref="IApplicationService"/>.
    /// Thus, ABP enables automatic dependency injection, validation and other benefits for it.
    /// 
    /// Application services works with DTOs (Data Transfer Objects).
    /// Service methods gets and returns DTOs.
    /// </summary>
    public interface ITaskReferenceAppService : IApplicationService
    {
        Task<GetTasksCriticalitiesOutput> GetTaskCriticalities();
    }
}