using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.HttpClients
{
    public interface IHttpClientService
    {
        Task<ResponseResult> ResponseWithModel<Model>(object body, string path);
        Task<ResponseResult> ResponseWithoutModel(object body, string path);
        Task<ResponseResult> ResponseTokenModel(object body, string path);
    }
}
