using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Wintertale.Client.Services.BaseApi {
    internal interface IApiService {
        HttpClient client { get; set; }

        /// <summary>
        /// Выполняет HTTP запрос по указанному типу
        /// Принимает uri в ввиде: api/v...
        /// Отравляет <typeparamref name="TRequest"/> в теле запроса
        /// </summary>
        /// <typeparam name="TRequest">Отправляемый DTO Request файл</typeparam>
        /// <typeparam name="TResponse">Возвращемый DTO Response файл</typeparam>
        /// <param name="method">HttpMethod параметр</param>
        /// <param name="uri">Путь без хоста</param>
        /// <param name="request">DTO Request файл</param>
        /// <returns>Сериализованный объект <typeparamref name="TResponse"/></returns>
        Task<TResponse> HttpAsync<TRequest, TResponse>(HttpMethod method, string uri, TRequest? request);
        Task HttpAsync<TRequest>(HttpMethod method, string uri, TRequest? request);
        Task<TResponse> HttpAsync<TResponse>(HttpMethod method, string uri);

        /// <summary>
        /// Выполняет HTTP GET длинный запрос
        /// Принимает uri в ввиде: api/v...
        /// Отравляет <typeparamref name="TRequest"/> в теле запроса
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="uri">Путь без хоста</param>
        /// <param name="request">DTO Request файл</param>
        /// <returns>Поток строк полученных от сервера</returns>
        IAsyncEnumerable<string> SseAsync<TRequest>(string uri, TRequest? request, CancellationToken cancellationToken = default);
    }
}
