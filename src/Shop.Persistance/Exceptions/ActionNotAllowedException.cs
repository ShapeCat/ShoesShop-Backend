namespace ShoesShop.Persistence.Exceptions
{
    public class ActionNotAllowedException : Exception
    {
        public ActionNotAllowedException(string ActionName, Type type) : base($"Action {ActionName} not allowed for entity {type}") { }
    }
}
