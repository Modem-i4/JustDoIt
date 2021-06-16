using FluentValidation;
using JustDoIt.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace JustDoIt.Application
{
    public abstract class AbstractExtendedValidator<T> : AbstractValidator<T>
    {
        private readonly IMemoryCache _cache;
        public AbstractExtendedValidator(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool ValidateDeskId(int deskId)
        {
            var cacheDeskId = _cache.Get<int>("currentDesk");
            if (deskId != cacheDeskId)
            {
                throw new ApiException("Select this desk first");
            }
            return true;
        }
    }
}
