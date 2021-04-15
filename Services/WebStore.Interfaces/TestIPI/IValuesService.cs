using System;
using System.Collections.Generic;
using System.Net;

namespace WebStore.Interfaces.TestAPI
{
    public interface IValuesService
    {
        IEnumerable<string> Get(); //Получать строки

        string Get(int id); //Получать конкретную строку по ее id

        Uri Create(string value); //Создовать строку

        HttpStatusCode Edit(int id, string value); //Редактировать

        HttpStatusCode Remove(int id); //Удалять
    }
}