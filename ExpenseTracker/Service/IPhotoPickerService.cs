using System.IO;
using System.Threading.Tasks;

namespace ExpenseTracker.Service
{
    public interface IPhotoPickerService
    {
        Task<Stream> GetImageStreamAsync();
    }
}
