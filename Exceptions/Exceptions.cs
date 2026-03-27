namespace WebTemplate.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message) : base(message) { }

        public EntityNotFoundException(string entityName, object id) 
            : base($"{entityName} sa ID-jem {id} ne postoji u bazi.") { }
    }
}