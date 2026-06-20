namespace AddressBook.BLL.Exceptions
{
    public class NotFoundException(string message) : Exception(message)
    {
    }
}
