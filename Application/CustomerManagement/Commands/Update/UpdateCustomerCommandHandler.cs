using Domain.CustomerManagement.Repository;
using Domain.Shared;
using MediatR;

namespace Application.CustomerManagement.Commands.Update
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCustomerCommandHandler(
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork)
        {
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.OfIdAsync(request.Id);

            if (customer == null)
            {
                throw new KeyNotFoundException($"{nameof(customer)} was not found for Id: {request.Id}");
            }

            customer.ChangeDetails(request.FirstName, request.LastName, request.Email, request.PersonalNumber, request.DateOfBirth.ToUniversalTime(), request.Gender);

            _customerRepository.Update(customer);
            await _unitOfWork.SaveAsync();

            return Unit.Value;
        }
    }
}
