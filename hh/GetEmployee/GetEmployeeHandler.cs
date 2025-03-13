using App.EnglishBuddy.Application.Common.Exceptions;
using App.EnglishBuddy.Application.Features.UserFeatures.GetUser;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetEmployee;

public sealed class GetEmployeeHandler : IRequestHandler<GetEmployeeRequest, GetEmployeeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmployeesRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetEmployeeHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IUsersImagesRepository _iUsersImagesRepository;
    
    public GetEmployeeHandler(IUnitOfWork unitOfWork, IEmployeesRepository employeeRepository,IMapper mapper, ILogger<GetEmployeeHandler> logger, IMediator mediator, IUsersImagesRepository iUsersImagesRepository)
    {
        _unitOfWork = unitOfWork;
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _logger = logger;
        _mediator = mediator;
        _iUsersImagesRepository= iUsersImagesRepository;
    }

    public async Task<GetEmployeeResponse> Handle(GetEmployeeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Statring method {nameof(Handle)}");
        GetEmployeeResponse response = new GetEmployeeResponse();
        try
        {
            Employees employee = await _employeeRepository.GetById(request.Id, cancellationToken);
            _logger.LogInformation("user" + JsonConvert.SerializeObject(employee));
            if (employee != null)
            {
                response.Id = employee.Id;
                response.FirstName = employee.FirstName;
                response.LastName = employee.LastName;
                response.Mobile = employee.Mobile;
                response.Address = employee.Address;
                response.CreatedDate = DateTime.Now;
                response.Createdby = employee.Createdby;
                response.UpdatedDate = DateTime.Now;
                response.Updatedby = employee.Updatedby;
                response.IsActive = employee.IsActive;

            } else
            {
                throw new NotFoundException("No Record Found");
            }
            _logger.LogDebug($"Ending method {nameof(Handle)}");
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}