using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Features.UserFeatures.GetAllUser;
using App.EnglishBuddy.Application.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.GetAllEmployee;

public sealed class GetAllEmployeeHandler : IRequestHandler<GetAllEmployeeRequest, List<GetAllEmployeeResponse>>
{
    private readonly IEmployeesRepository _employeeRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AccessTokenHandler> _logger;
    public GetAllEmployeeHandler(IEmployeesRepository employeeRepository, IMapper mapper, ILogger<AccessTokenHandler> logger)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<GetAllEmployeeResponse>> Handle(GetAllEmployeeRequest request, CancellationToken cancellationToken)
    {
        List<GetAllEmployeeResponse> responses = new List<GetAllEmployeeResponse>();


        _logger.LogDebug($"Statring method {nameof(Handle)}");
        try
        {
            var employees = await _employeeRepository.GetAll(request.PageNumber, request.PageSize, cancellationToken);
            foreach (var item in employees)
            {
                GetAllEmployeeResponse getAllEmployeeResponse = new GetAllEmployeeResponse();
                getAllEmployeeResponse.Email = item.Address;
                getAllEmployeeResponse.Name = item.FirstName;
                getAllEmployeeResponse.UserId = item.Id;
                responses.Add(getAllEmployeeResponse);
            }
            _logger.LogDebug($"Ending method {nameof(Handle)}");
            return responses;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}