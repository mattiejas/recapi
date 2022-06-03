using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Recapi.Application.Common.Models;

namespace Recapi.Application.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
        
        public BadRequestException(Result result) : base(JsonSerializer.Serialize(result))
        {
        }
    }
}
