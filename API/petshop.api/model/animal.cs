using System;

namespace petshop.api.model
{
    public class Animal
    {

        public string Id{get;set;} = Guid.NewGuid().ToString();
        public string nome{get;set;}="toto";
        public string idade{get;set;}
        public string cor{get;set;}


    }
}