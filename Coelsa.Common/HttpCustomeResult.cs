using Coelsa.Models;
using System;

namespace Coelsa.Common
{
    public static class HttpCustomeResult<T>
    {
        public static Response<T> Response(Constants.ResponseCode code, T data, string message)
        {
            var response = new Response<T>();
            response.Code = code.ToString();
            response.Data = data;
            response.Message = message;
            return response;
        }
        public static Response<T> ResponseOK(T data, string message)
        {
            return new Response<T>
            {
                Code = Constants.ResponseCode.SUCCESS.ToString(),
                Data = data,
                Message = message
            };
        }

        public static Response<T> ResponseBusinessError(string message)
        {
            return new Response<T>
            {
                Code = Constants.ResponseCode.BUSINESS_ERROR.ToString(),
                Message = message
            };
        }
        public static Response<T> ResponseApplicationError(string message)
        {
            return new Response<T>
            {
                Code = Constants.ResponseCode.APPLICATION_ERROR.ToString(),
                Message = message
            };
        }
    }
}
