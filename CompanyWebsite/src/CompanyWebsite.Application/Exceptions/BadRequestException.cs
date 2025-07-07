using System.Text.Json;
using Shared;

namespace CompanyWebsite.Application.Exceptions;

public class BadRequestException : Exception
{
    protected BadRequestException(Error[] errors) 
        : base(JsonSerializer.Serialize(errors))
    {
    }
    
    // protected BadRequestException(IEnumerable<string> errors)
    //     : base(string.Join(", ", errors))
    // {
    // }
}