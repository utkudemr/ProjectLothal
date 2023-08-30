﻿
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public class InternetServerErrorProblemDetails : ProblemDetails
{
    public InternetServerErrorProblemDetails(string detail)
    {
        Title = "Internal Server Error";
        Detail = detail;
        Status = StatusCodes.Status500InternalServerError;
        Type = "internal";
    }
}

