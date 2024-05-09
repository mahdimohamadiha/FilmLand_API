using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmLand.Logs
{
    public interface ICustomLogger
    {
        void ErrorDatabase(string err);
        void SuccessDatabase(string message);
        void StartAPI(string apiName);
        void EndAPI(string apiName);
        void CustomApiError(string message);
    }
}
