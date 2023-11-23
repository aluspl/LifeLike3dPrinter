namespace PrinterService.Handlers;

public class NotFoundException(string error) : Exception(error);