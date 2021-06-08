using JustDoIt.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JustDoIt.Application.Interfaces.Repositories
{
    public interface ICommentRepositoryAsync : IGenericRepositoryAsync<Comment>
    {
        //Task<bool> IsUniqueBarcodeAsync(string barcode);
    }
}
