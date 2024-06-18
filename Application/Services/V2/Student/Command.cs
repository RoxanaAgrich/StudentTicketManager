using Domain.Shared;
using MediatR;

namespace Application.Services.V2.Student
{
    public class Command
    {
        public record CreateStudentCommand(string FirstName,string LastName, string Gender,Guid? ClassId) : IRequest<Result>;
        public record DeleteStudentCommand(Guid Id) : IRequest<Result>;
    }
}
