using Application.Services.V2.Student;
using Domain.Abtractions.UnitOfWork;
using Domain.Exceptions;
using Domain.Shared;
using MediatR;

namespace Application.UseCases.V2.Student.Commands
{
    public class CreateStudentCommandHandler : IRequestHandler<Command.CreateStudentCommand, Result>
    {
        private IUnitOfWorkMongboDb _unitOfWork;
        public CreateStudentCommandHandler(IUnitOfWorkMongboDb unitOfWork) { _unitOfWork = unitOfWork; }
        public async Task<Result> Handle(Command.CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var studentRepositoty =  _unitOfWork.GetRepository<Domain.Entities.Student, Guid>();
            var student = new Domain.Entities.Student(request.FirstName, request.LastName, request.Gender, request.ClassId);
            studentRepositoty.Add(student);
            return Result.Success();
        }
    }
}
