namespace MovieApp.BL.Exceptions.Base;
public interface IBaseException
{
    public int StatusCode { get; }
    public string ErrorMessage { get; }
}

