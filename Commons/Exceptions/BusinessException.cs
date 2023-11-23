namespace Commons.Exceptions;

public class BusinessException(string errorMessage) : Exception(errorMessage);