
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.UpdateEmployee;

public sealed class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeRequest, UpdateEmployeeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICallsRepository _iCallsRepository;
    private readonly IEmployeesRepository _iEmployeeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateEmployeeHandler> _logger;
    public UpdateEmployeeHandler(IUnitOfWork unitOfWork,
        ICallsRepository iCallsRepository,
        IEmployeesRepository iEmployeeRepository,
        IMapper mapper,
        ILogger<UpdateEmployeeHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _iCallsRepository = iCallsRepository;
        _iEmployeeRepository = iEmployeeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<UpdateEmployeeResponse> Handle(UpdateEmployeeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Starting method {nameof(Handle)}");

        UpdateEmployeeResponse response = new UpdateEmployeeResponse();
        try
        {
            Employees updateEmployee = await _iEmployeeRepository.GetById(request.Id, cancellationToken);

            if (updateEmployee != null)
            {
                updateEmployee.FirstName = request.FirstName;
                updateEmployee.LastName = request.LastName;
                updateEmployee.Mobile = request.Mobile;
                updateEmployee.Address = request.Address;
                updateEmployee.Createdby = request.Createdby;
                updateEmployee.UpdateDate = request.UpdateDate;
                updateEmployee.Updatedby = request.Updatedby;
                updateEmployee.IsActive = request.IsActive;

                _iEmployeeRepository.Update(updateEmployee);
                await _unitOfWork.Save(cancellationToken);

                response.IsUserFound = true;
                response.IsSuccess = true;
                response.NotUpdated = true;
            }
            else
            {
                response.IsUserFound = false;
                response.NotUpdated = false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }

        return response;
    }
}