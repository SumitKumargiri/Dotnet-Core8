using System;
using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.EnglishBuddy.Application.Features.DeleteEmployee
{
    public sealed class DeleteEmployeeHandler : IRequestHandler<DeleteEmployeeRequest, DeleteEmployeeResponse>
    {
        private readonly IEmployeesRepository _iEmployeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteEmployeeHandler> _logger;

        public DeleteEmployeeHandler(IEmployeesRepository iEmployeeRepository, IUnitOfWork unitOfWork, ILogger<DeleteEmployeeHandler> logger)
        {
            _iEmployeeRepository = iEmployeeRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<DeleteEmployeeResponse> Handle(DeleteEmployeeRequest request, CancellationToken cancellationToken)
        {
            _logger.LogDebug($"Starting method {nameof(Handle)}");

            DeleteEmployeeResponse response = new DeleteEmployeeResponse();
            try
            {

                Employees employeeToDelete = await _iEmployeeRepository.GetById(request.Id, cancellationToken);

                if (employeeToDelete != null)
                {
                    _iEmployeeRepository.Delete(employeeToDelete);
                    await _unitOfWork.Save(cancellationToken);

                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
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
}
