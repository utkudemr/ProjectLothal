﻿

using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Core.CrossCuttingConcerns.Exceptions.Extensions;

public static class ProblemDetailExtensions
{
    public static string ToJson<TProblemDetail>(this TProblemDetail details) where TProblemDetail :ProblemDetails 
    {
        return JsonSerializer.Serialize(details);
    }
}
