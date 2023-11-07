Imports System
Imports System.Net.Http

Module Program
    Dim st As String = $"
----------------------------------------------------------------------------------------------------
Example that demonstrates Asynchronous Programming with Async and Await.
It uses HttpClient.GetStringAsync to download the contents of a website.
Link : https://learn.microsoft.com/en-us/dotnet/visual-basic/programming-guide/concepts/async/ 
----------------------------------------------------------------------------------------------------
"
    Sub Main(args As String())
        Console.WriteLine(st)
        MainAsync()
        Console.ReadKey()
    End Sub
    'private static async Task MainAsync()
    Async Sub MainAsync()
        ' Call and await immediately.
        ' suspends until AccessTheWebAsync is done.
        Dim contentLength As Integer = Await AccessTheWebAsync()
        Console.WriteLine($"{vbCrLf}Length of the downloaded string: {contentLength}.{vbCrLf}")
    End Sub
    ' Three things to note about writing an Async Function:
    '  - The function has an Async modifier.
    '  - Its return type is Task or Task(Of T). (See "Return Types" section.)
    '  - As a matter of convention, its name ends in "Async".
    Async Function AccessTheWebAsync() As Task(Of Integer)
        Dim stopwatch As Stopwatch = Stopwatch.StartNew()
        Using client As New HttpClient()
            ' Call and await separately.
            '  - AccessTheWebAsync can do other things while GetStringAsync is also running.
            '  - getStringTask stores the task we get from the call to GetStringAsync.
            '  - Task(Of String) means it is a task which returns a String when it is done.
            Dim getStringTask As Task(Of String) =
                client.GetStringAsync("https://learn.microsoft.com/dotnet")
            ' You can do other work here that doesn't rely on the string from GetStringAsync.
            DoIndependentWork()
            ' The Await operator suspends AccessTheWebAsync.
            '  - AccessTheWebAsync does not continue until getStringTask is complete.
            '  - Meanwhile, control returns to the caller of AccessTheWebAsync.
            '  - Control resumes here when getStringTask is complete.
            '  - The Await operator then retrieves the String result from getStringTask.
            Dim urlContents As String = Await getStringTask
            ' The Return statement specifies an Integer result.
            ' A method which awaits AccessTheWebAsync receives the Length value.
            stopwatch.[Stop]()
            Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}")
            'Console.WriteLine(urlContents)
            Return urlContents.Length

        End Using

    End Function
    Private Sub DoIndependentWork()
        'You can do other work here
        Console.WriteLine($"Doing other work. . . . . . . .{vbCrLf}")
        'await Task.Delay(1000) 'hold execution for 1 second
    End Sub
End Module
