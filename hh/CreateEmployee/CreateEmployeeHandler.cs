using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Common.Utility;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Features.UserFeatures.GetUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace App.EnglishBuddy.Application.Features.UserFeatures.CreateEmployeeHandler;

public sealed class CreateEmployeeHandler : IRequestHandler<CreateEmployeeRequest, CreateEmployeeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRatingsRepository _iIRatingsRepository;
    private readonly IEmployeesRepository _iEmployeesRepository;
    private readonly ILogger<CreateEmployeeHandler> _logger;
    public CreateEmployeeHandler(IUnitOfWork unitOfWork,IMapper mapper, IRatingsRepository iIRatingsRepository,ITotalRatingsRepository iTotalRatingsRepository,ILogger<CreateEmployeeHandler> logger,IEmployeesRepository iEmployeesRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iIRatingsRepository = iIRatingsRepository;
        _iEmployeesRepository = iEmployeesRepository;
        _logger = logger;
    }

    public async Task<CreateEmployeeResponse> Handle(CreateEmployeeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Statring method {nameof(Handle)}");
        CreateEmployeeResponse createemp = new CreateEmployeeResponse();
        try
        {
            Employees empdata = new Employees();
            empdata.FirstName = "Vinay";
            empdata.LastName = "GGG";
            empdata.Address = "Test";
            empdata.Mobile = "7783232";
            empdata.Updatedby = new Guid();
           
            _iEmployeesRepository.Create(empdata);
            await _unitOfWork.Save(cancellationToken);
            createemp.IsSuccess = true;
            _logger.LogDebug($"Ending method {nameof(Handle)}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            createemp.IsSuccess = false;
            throw;
        }
        return createemp;
    }


   
}