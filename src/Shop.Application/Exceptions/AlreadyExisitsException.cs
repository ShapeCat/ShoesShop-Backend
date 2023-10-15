namespace ShoesShop.Application.Exceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string objectName, Type objectType) : base($@"Entity '{objectName}' ({objectType}) already exists. Edit it or delete first.""") { }
    }
}
