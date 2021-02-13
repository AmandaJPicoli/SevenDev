using Microsoft.AspNetCore.Http;
using SevenDev.Domain.Entities.ValueObject;
using System.IO;
using System.Threading.Tasks;

namespace SevenDev.Domain.Core.Interfaces
{
    public interface IStorageHelper
    {
        Task<ImageBlob> Upload(IFormFile formFile, string nameFile);
        bool IsImage(string nameFile);

    }
}
