using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using WebTemplate.Exceptions;

namespace WebTemplate.Filters
{
    public class ErrorHandlingFilter : IExceptionFilter
    {
        private readonly IHostEnvironment env;

        public ErrorHandlingFilter(IHostEnvironment env)
        {
            this.env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            // 1. Logika za određivanje HTTP Status Koda na osnovu tipa izuzetka
            var (statusCode, message) = exception switch
            {
                EntityNotFoundException => (HttpStatusCode.NotFound, exception.Message),
                
                LogistikaException => (HttpStatusCode.Conflict, exception.Message), // 409
                
                BusinessRuleException => (HttpStatusCode.UnprocessableEntity, exception.Message), // 422
                
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Nemate dozvolu za pristup ovom resursu."),
                
                KeyNotFoundException => (HttpStatusCode.NotFound, "Traženi podatak nije pronađen u bazi."),
                
                // Podrazumevana greška za sve ostalo (500 Internal Server Error)
                _ => (HttpStatusCode.InternalServerError, "Došlo je do neočekivane greške na serveru.")
            };

            // 2. Kreiranje uniformnog odgovora za frontend
            var errorResponse = new
            {
                success = false,
                error = message,
                type = exception.GetType().Name,
                timestamp = DateTime.Now,
                // StackTrace šaljemo samo ako smo u Development modu (zbog tvoje sigurnosti u produkciji)
                stackTrace = env.IsDevelopment() ? exception.StackTrace : null
            };

            // 3. Postavljanje rezultata koji se šalje klijentu
            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = (int)statusCode
            };

            // 4. Označavamo da je izuzetak obrađen kako ASP.NET ne bi pokušavao da baci svoju podrazumevanu grešku
            context.ExceptionHandled = true;
        }
    }
}