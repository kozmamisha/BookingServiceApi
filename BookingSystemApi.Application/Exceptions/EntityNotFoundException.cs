namespace BookingSystemApi.Application.Exceptions;

public class EntityNotFoundException(string message) : Exception(message);