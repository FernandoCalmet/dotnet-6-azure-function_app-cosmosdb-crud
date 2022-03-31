using System;

namespace Tasks.FunctionApp.Exceptions;

public class NoteException : Exception
{
    public NoteException() { }

    public NoteException(string message) : base(message) { }

    public NoteException(string message, Exception innerException) : base(message, innerException) { }
}
