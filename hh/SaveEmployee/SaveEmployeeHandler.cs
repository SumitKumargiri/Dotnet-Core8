using App.EnglishBuddy.Application.Common.Utility;
using App.EnglishBuddy.Application.Features.UserFeatures.CreateUser;
using App.EnglishBuddy.Application.Features.UserFeatures.FcmToken;
using App.EnglishBuddy.Application.Features.UserFeatures.SaveEmployee;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.UserFeatures.SaveEmployee;

public sealed class SaveEmployeeHandler : IRequestHandler<SaveEmployeeRequest, SaveEmployeeResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRatingsRepository _iIRatingsRepository;
    private readonly ITotalRatingsRepository _iTotalRatingsRepository;
    private readonly IEmployeesRepository _iEmployeesRepository;
    private readonly ILogger<SaveEmployeeHandler> _logger;
    public SaveEmployeeHandler(IUnitOfWork unitOfWork,IMapper mapper, IRatingsRepository iIRatingsRepository,ITotalRatingsRepository iTotalRatingsRepository,ILogger<SaveEmployeeHandler> logger,IEmployeesRepository iEmployeesRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _iIRatingsRepository = iIRatingsRepository;
        _iTotalRatingsRepository = iTotalRatingsRepository;
        _iEmployeesRepository = iEmployeesRepository;
        _logger = logger;
    }

    public async Task<SaveEmployeeResponse> Handle(SaveEmployeeRequest request, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"Statring method {nameof(Handle)}");
        SaveEmployeeResponse saveempdata = new SaveEmployeeResponse();
        try
        {
            Employees createdata = new Employees();

            createdata.FirstName = request.FirstName;
            createdata.LastName = request.LastName;
            createdata.Mobile =request.Mobile;
            createdata.Address = request.Address;
            createdata.CreatedDate = DateTime.Now;
            createdata.Createdby = request.Createdby;
            createdata.UpdateDate = DateTime.Now;
            createdata.Updatedby = request.Updatedby;
            createdata.IsActive = true;

            _iEmployeesRepository.Create(createdata);
            await _unitOfWork.Save(cancellationToken);
            saveempdata.IsSuccess = true;
            _logger.LogDebug($"Ending method {nameof(Handle)}");


        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            saveempdata.IsSuccess = false;
            throw;
        }
        return saveempdata;
    }
}