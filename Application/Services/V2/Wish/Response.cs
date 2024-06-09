using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.V2.Wish;

public static class Response
{
    public record WishResponse(Guid Id, string Name, decimal Price, string Description);
}
