﻿using CharlieBackend.Core.DTO.Account;
using System.Threading.Tasks;

namespace CharlieBackend.AdminPanel.Utils.Interfaces
{
    public interface IApiUtil
    {
        public Task<string> SignInAsync(string url, AuthenticationDto authModel);

        public Task<TResponse> GetAsync<TResponse>(string url);

        public Task<TResponse> CreateAsync<TResponse>(string url, TResponse data);

        public Task<TResponse> CreateAsync<TResponse, TRequest>(string url, TRequest data);

        public Task<TResponse> PostAsync<TResponse, TRequest>(string url, TRequest data);

        public Task<TResponse> PutAsync<TResponse>(string url, TResponse data);

        public Task<TResponse> PutAsync<TResponse, TRequest>(string url, TRequest data);

        public Task<TResponse> DeleteAsync<TResponse>(string url);

        public Task<TResponse> EnableAsync<TResponse>(string url);
    }
}
